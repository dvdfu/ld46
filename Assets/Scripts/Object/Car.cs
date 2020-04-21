using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;
    const int CAR_CRASH_DAMAGE = 3;
    const int CRASH_SPEED_THRESHOLD = 5000;
    const float SWERVE_CHANCE = 0.01f;
    const float NEAR_PLAYER_SWERVE_DISTANCE = 80;
    const float NEAR_PLAYER_DISTANCE = 100;
    const float FAR_PLAYER_DISTANCE = 130;
    const float NEAR_BUILDING_DISTANCE = 100;

    [SerializeField] PlayerData playerData;
    [SerializeField] SessionData sessionData;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CastsShadow castsShadow;
    [SerializeField] Flammable flammable;
    [SerializeField] Mortal mortal;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject propanePrefab;
    [SerializeField] Sprite ashSprite;
    [SerializeField] bool isBus = false;

    Vector3 destination;
    Transform target;

    int peopleInside;
    bool canChase;
    bool hasPropane;
    Transform swerveTarget;

    enum State {
        Normal,
        Chase,
        Swerve,
        Stop,
        Dead,
    }
    State state;

    public void Init(Transform target, Vector2 destination) {
        this.target = target;
        this.destination = new Vector3(destination.x, destination.y, 0f);
    }

    public void OnIgnite() {
        if (peopleInside > 0) {
            for (int i = 0; i < peopleInside; i++) {
                Vector3 offset = MathUtils.PolarToCartesian(360f * i / peopleInside, 10);
                Instantiate(personPrefab, transform.position + offset, Quaternion.identity, transform.parent);
            }
            peopleInside = 0;
            state = State.Stop;
            spriteRenderer.color = Color.grey;
        }
    }

    public void OnDie() {
        if (peopleInside > 0) {
            sessionData.peopleDied += peopleInside;
            peopleInside = 0;
            Instantiate(tombstonePrefab, transform.position, Quaternion.identity, transform.parent);
        }
        if (flammable.IsOnFire()) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        }
        spriteRenderer.sprite = ashSprite;
        spriteRenderer.sortingLayerName = "Floor";
        spriteRenderer.color = Color.white;
        castsShadow.RemoveShadow();
        state = State.Dead;
    }

    public bool IsMoving() {
        return state == State.Normal || state == State.Chase || state == State.Swerve;
    }

    void Start() {
        state = State.Normal;
        if (isBus) {
            peopleInside = 3;
            canChase = false;
            hasPropane = false;
        } else {
            peopleInside = 1;
            canChase = Random.value < GetChaseChance();
            hasPropane = Random.value < GetPropaneChance();
        }
        
        if (Random.value < GetChaseChance()) {
            StartCoroutine(CheckForSwerveTargets());
        }
    }

    void FixedUpdate() {
        switch (state) {
            case State.Dead:
            break;

            case State.Stop:
            if (!flammable.IsOnFire()) {
                mortal.Damage(gameObject.tag, 5);
            }
            break;

            default:
            body.AddForce(GetMoveDirection().normalized * MAX_SPEED);
            break;
        }
        Vector2 playerDelta = playerData.position - body.position;
        if (playerDelta.sqrMagnitude < NEAR_PLAYER_DISTANCE * NEAR_PLAYER_DISTANCE) {
            // Near player
            if (state == State.Normal && canChase) {
                state = State.Chase;
            }
            if (hasPropane) {
                Instantiate(propanePrefab, transform.position, Quaternion.identity, transform.parent);
                hasPropane = false;
            }
        } else if (playerDelta.sqrMagnitude > FAR_PLAYER_DISTANCE * FAR_PLAYER_DISTANCE) {
            // Far player
            if (state == State.Chase) {
                state = State.Normal;
            }
        }

        Vector2 dist = destination - transform.position;
        if (dist.sqrMagnitude < 100) {
            Destroy(gameObject);
        }
    }

    void LateUpdate() {
        if (state != State.Dead) {
            sprite8Directional.SetAngle(MathUtils.VectorToAngle(GetMoveDirection()));
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (IsMoving() && body.velocity.sqrMagnitude > CRASH_SPEED_THRESHOLD) {
            SpriteSquish spriteSquish = collision.gameObject.GetComponent<SpriteSquish>();
            if (spriteSquish) {
                spriteSquish.SquishThin();
            }
            Flammable flammable = collision.gameObject.GetComponentInChildren<Flammable>();
            if (flammable) {
                this.flammable.SetOnFire();
                flammable.SetOnFire();
            }
            Mortal mortal = collision.gameObject.GetComponent<Mortal>();
            if (mortal) {
                mortal.Damage(gameObject.tag, CAR_CRASH_DAMAGE);
            }
        }
    }

    float GetChaseChance() {
        float progress = sessionData.GetGameProgress();
        if (progress < 0.2f) {
            return 0.05f;
        }
        if (progress < 0.4f) {
            return 0.1f;
        }
        return 0.2f;
    }

    float GetPropaneChance() {
        float progress = sessionData.GetGameProgress();
        if (progress < 0.05f) {
            return 0;
        }
        if (progress < 0.2f) {
            return 0.05f;
        }
        return 0.1f;
    }

    Vector2 GetMoveDirection() {
        switch(state) {
            case State.Normal: return (destination - transform.position).normalized;
            case State.Chase: return (target.position - transform.position).normalized;
            case State.Swerve: return (swerveTarget.position - transform.position).normalized;
            case State.Stop: return body.velocity;
            default: return Vector2.zero;
        }
    }

    IEnumerator CheckForSwerveTargets() {
        while (true) {
            RaycastHit2D[] results = Physics2D.CircleCastAll(transform.position, NEAR_BUILDING_DISTANCE, Vector2.zero, 0);
            foreach (RaycastHit2D result in results) {
                Flammable flammable = result.collider.gameObject.GetComponent<Flammable>();
                Vector2 playerDelta = playerData.position - body.position;
                if (flammable
                    && !flammable.IsOnFire()
                    && result.collider.gameObject.transform.parent != null
                    && result.collider.gameObject.transform.parent.tag == "Building"
                    && state == State.Normal
                    && Random.value < SWERVE_CHANCE
                    && playerDelta.sqrMagnitude < NEAR_PLAYER_SWERVE_DISTANCE * NEAR_PLAYER_SWERVE_DISTANCE) {

                    state = State.Swerve;
                    swerveTarget = result.collider.gameObject.transform;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}

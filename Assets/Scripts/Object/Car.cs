using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;
    const int CAR_CRASH_DAMAGE = 3;
    const int CRASH_SPEED_THRESHOLD = 5000;
    const float CHASE_CHANCE = 0.15f;
    const float CHASE_DISTANCE = 150;
    const float PROPANE_CHANCE = 0.1f;

    [SerializeField] SessionData sessionData;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Flammable flammable;
    [SerializeField] Mortal mortal;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject propanePrefab;
    [SerializeField] Sprite ashSprite;

    Vector3 destination;
    Transform target;

    bool hasPerson = true;
    bool canChase = false;

    enum State {
        Normal,
        Chase,
        Stop,
        Dead,
    }
    State state;

    public void Init(Transform target, Vector2 destination) {
        this.target = target;
        this.destination = new Vector3(destination.x, destination.y, 0f);
    }

    public void OnIgnite() {
        if (hasPerson) {
            hasPerson = false;
            state = State.Stop;
            spriteRenderer.color = Color.grey;
            Instantiate(personPrefab, transform.position, Quaternion.identity, transform.parent);
        }
    }

    public void OnDie() {
        sessionData.carsDestroyed++;
        if (hasPerson) {
            sessionData.peopleDied++;
            Instantiate(tombstonePrefab, transform.position, Quaternion.identity, transform.parent);
        }
        if (flammable.IsOnFire()) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        }
        spriteRenderer.sprite = ashSprite;
        spriteRenderer.color = Color.white;
        state = State.Dead;
    }

    public bool IsMoving() {
        return state == State.Normal || state == State.Chase;
    }

    void Start() {
        state = State.Normal;
        canChase = Random.value < CHASE_CHANCE;
        if (canChase) {
            spriteRenderer.color = new Color(1, 0.8f, 0.8f);
        }
        StartCoroutine(PropaneRoutine());
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

        Player player = collision.gameObject.GetComponent<Player>();
        if (player && player.IsDashing()) {
            Mortal mortal = GetComponent<Mortal>();
            if (mortal) {
                mortal.Die();
            }
        }
    }

    Vector2 GetMoveDirection() {
        switch(state) {
            case State.Normal:
            Vector2 dist = destination - transform.position;
            if (dist.magnitude < 10f) {
                Destroy(gameObject);
            }
            if ((target.position - transform.position).magnitude < CHASE_DISTANCE && canChase) {
                state = State.Chase;
            }
            return dist.normalized;

            case State.Chase:
            return (target.position - transform.position).normalized;

            case State.Stop:
            return body.velocity;

            default:
            return Vector2.zero;
        }
    }

    IEnumerator PropaneRoutine() {
        bool hasPropane = Random.value < PROPANE_CHANCE;
        yield return new WaitForSeconds(3);
        if (hasPropane) {
            Instantiate(propanePrefab, transform.position, Quaternion.identity, transform.parent);
        }
    }
}

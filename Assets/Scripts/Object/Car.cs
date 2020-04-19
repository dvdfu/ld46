using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;
    const int CAR_CRASH_DAMAGE = 3;
    const int CRASH_SPEED_THRESHOLD = 5000;
    const float CHASE_CHANCE = 0.15f;
    const float CHASE_DISTANCE = 120;
    const float PROPANE_CHANCE = 0.1f;

    [SerializeField] SessionData sessionData;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CastsShadow castsShadow;
    [SerializeField] Flammable flammable;
    [SerializeField] Mortal mortal;
    [SerializeField] Collider2D collider;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject propanePrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Sprite ashSprite;

    Vector3 destination;
    Transform target;

    bool hasPerson = true;
    bool canChase = false;
    bool canShoot = true;

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
        spriteRenderer.sortingLayerName = "Floor";
        spriteRenderer.color = Color.white;
        castsShadow.RemoveShadow();
        state = State.Dead;
    }

    public bool IsMoving() {
        return state == State.Normal || state == State.Chase;
    }

    void Start() {
        state = State.Normal;
        canChase = Random.value < CHASE_CHANCE;
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

        if (canShoot && (target.position - transform.position).magnitude < CHASE_DISTANCE) {
            StartCoroutine(ShootRoutine(target));
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

    IEnumerator ShootRoutine(Transform shotTarget) {
        canShoot = false;
        int shots = 8;
        float shotDelay = 0.1f;
        float spread = 10;
        for (int i = 0; i < shots; i++) {
            Vector2 delta = shotTarget.position - transform.position;
            float angle = MathUtils.VectorToAngle(delta);
            angle += Random.Range(-spread / 2, spread / 2);
            Bullet bullet = Instantiate(bulletPrefab, transform.position + Vector3.up * 10, Quaternion.identity, transform.parent).GetComponent<Bullet>();
            bullet.Init(collider, angle);
            yield return new WaitForSeconds(shotDelay);
        }
    }
}

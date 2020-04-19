using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;
    const int CAR_CRASH_DAMAGE = 20;
    const int CRASH_SPEED_THRESHOLD = 7000;

    [SerializeField] SessionData sessionData;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] Flammable flammable;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tombstonePrefab;

    Vector3 destination;
    Vector2 moveDirection;
    Transform target;

    bool canChase = false;

    enum State {
        Normal,
        Chase
    }
    State state;

    public void Init(Transform target, Vector2 destination) {
        this.target = target;
        this.destination = new Vector3(destination.x, destination.y, 0f);
    }

    public void OnDie() {
        sessionData.carsDestroyed++;
        sessionData.peopleDied++;
        Instantiate(tombstonePrefab, transform.position, Quaternion.identity, transform.parent);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Start() {
        state = State.Normal;
        canChase = Random.Range(0, 50) == 0;
    }

    void FixedUpdate() {
        body.AddForce(GetMoveDirection().normalized * MAX_SPEED);
    }

    void LateUpdate() {
        sprite8Directional.SetAngle(MathUtils.VectorToAngle(GetMoveDirection()));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (body.velocity.sqrMagnitude > CRASH_SPEED_THRESHOLD) {
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

    Vector2 GetMoveDirection() {
        switch(state) {
            case State.Normal:
                Vector2 dist = destination - transform.position;
                if (dist.magnitude < 10f) {
                    Destroy(gameObject);
                }
                
                if ((target.position - transform.position).magnitude < 50f && canChase) {
                    state = State.Chase;
                }

                return dist.normalized;
            case State.Chase:
                return (target.position - transform.position).normalized;
        }

        // Switch should be exhaustive
        Debug.Assert(false);
        return Vector2.zero;
    }
}

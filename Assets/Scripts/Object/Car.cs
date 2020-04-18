using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;

    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] Flammable flammable;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject explosionPrefab;

    Vector2 moveDirection;
    bool shouldChase = true;

    public void Chase(Transform target) {
        this.target = target;
    }

    public void OnDie() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Start() {
        StartCoroutine(ChaseRoutine());
    }

    void FixedUpdate() {
        if (shouldChase) {
            moveDirection = (target.position - transform.position).normalized;
        }
        body.AddForce(moveDirection.normalized * MAX_SPEED);
    }

    void LateUpdate() {
        sprite8Directional.SetAngle(MathUtils.VectorToAngle(moveDirection));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        SpriteSquish spriteSquish = collision.gameObject.GetComponent<SpriteSquish>();
        if (spriteSquish) {
            spriteSquish.SquishThin();
        }
        Flammable flammable = collision.gameObject.GetComponent<Flammable>();
        if (flammable) {
            if (collision.gameObject.GetComponent<Car>() == null) {
                this.flammable.SetOnFire();
                flammable.SetOnFire();
            }
            
        }
    }

    IEnumerator ChaseRoutine() {
        while (true) {
            yield return new WaitForSeconds(4 + 2 * Random.value);
            shouldChase = false;
            yield return new WaitForSeconds(1 + Random.value);
            shouldChase = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // Consts
    const float MAX_SPEED = 800;
    const float WATER_SHOOT_INTERVAL = 0.05f;
    const int WATER_AMMO_MAX = 200;
    const int WATER_DEPLETION_IN_FIRE = 10;

    // Member vars
    Vector2 moveDirection = Vector2.zero;
    Vector2 speed = Vector2.zero;
    int waterAmmo = WATER_AMMO_MAX;

    // Unity vars
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] GameObject waterPelletPrefab;
    [SerializeField] GameObject collisionPrefab;
    [SerializeField] Rigidbody2D body;
    [SerializeField] RectTransform waterFill;

    public void RefillWater() {
        if (waterAmmo < WATER_AMMO_MAX) {
            waterAmmo++;
        }
    }

    void DepleteWater(int amount = 1) {
        if (waterAmmo > 0) {
            waterAmmo -= amount;
        }
    }

    void Start() {
        StartCoroutine(ShootWaterRoutine());
    }

    void FixedUpdate() {
        moveDirection = new Vector2(Input.GetAxisRaw("PlayerHorizontal"), Input.GetAxisRaw("PlayerVertical"));
        body.AddForce(moveDirection.normalized * MAX_SPEED);
    }

    void LateUpdate() {
        waterFill.sizeDelta = new Vector2(64f * waterAmmo / WATER_AMMO_MAX, 8);
        sprite8Directional.SetAngle(MathUtils.VectorToAngle(body.velocity));
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Fire")) {
            DepleteWater(WATER_DEPLETION_IN_FIRE);
        }
    }

    IEnumerator ShootWaterRoutine() {
        while (true) {
            if (waterAmmo > 0) {
                float x = Input.GetAxisRaw("WaterHorizontal");
                float y = Input.GetAxisRaw("WaterVertical");
                if (x != 0 || y != 0) {
                    float angle = Mathf.Atan2(y, x);
                    WaterPellet waterPellet = Instantiate(waterPelletPrefab, transform.position + Vector3.up * 20, Quaternion.identity, transform.parent).GetComponent<WaterPellet>();
                    waterPellet.Shoot(angle);
                    DepleteWater();
                }
            }
            yield return new WaitForSeconds(WATER_SHOOT_INTERVAL);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Moving fast enough
        if (body.velocity.sqrMagnitude > 10000) {
            Instantiate(collisionPrefab, collision.GetContact(0).point, Quaternion.identity, transform.parent);
            SpriteSquish spriteSquish = collision.gameObject.GetComponent<SpriteSquish>();
            if (spriteSquish) {
                spriteSquish.SquishThin();
            }
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (flammable) {
                flammable.SetOnFire();
            }
        }
    }
}

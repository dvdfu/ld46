using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    const float MAX_SPEED = 900;
    const float WATER_SHOOT_INTERVAL = 0.05f;
    const int WATER_AMMO_MAX = 200;

    // Member vars
    Vector2 speed = Vector2.zero;
    int waterAmmo = WATER_AMMO_MAX;

    // Unity vars
    [SerializeField] GameObject waterPelletPrefab;
    [SerializeField] Rigidbody2D body;

    void Start() {
        StartCoroutine(ShootWaterRoutine());
    }

    void Update() {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("PlayerHorizontal"), Input.GetAxisRaw("PlayerVertical"));
        body.AddForce(moveDirection * MAX_SPEED);
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
                    waterAmmo--;
                }
            }
            yield return new WaitForSeconds(WATER_SHOOT_INTERVAL);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    const float MAX_SPEED = 300;
    const float WATER_SHOOT_INTERVAL = 0.05f;

    // Member vars
    Vector2 speed = Vector2.zero;

    // Unity vars
    [SerializeField] GameObject waterPelletPrefab;
    [SerializeField]
    Rigidbody2D body;

    void Start() {
        StartCoroutine(ShootWaterRoutine());
    }

    void Update() {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("PlayerHorizontal"), Input.GetAxisRaw("PlayerVertical"));
        body.AddForce(moveDirection.normalized * MAX_SPEED);
    }

    IEnumerator ShootWaterRoutine() {
        while (true) {
            float x = Input.GetAxisRaw("WaterHorizontal");
            float y = Input.GetAxisRaw("WaterVertical");
            if (x != 0 || y != 0) {
                float angle = Mathf.Atan2(y, x);
                WaterPellet waterPellet = Instantiate(waterPelletPrefab, transform.position + Vector3.up * 20, Quaternion.identity, transform.parent).GetComponent<WaterPellet>();
                waterPellet.Shoot(angle);
            }
            yield return new WaitForSeconds(WATER_SHOOT_INTERVAL);
        }
    }
}

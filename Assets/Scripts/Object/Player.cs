using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    const float MAX_SPEED = 1000;
    const float WATER_SHOOT_INTERVAL = 0.05f;
    const int WATER_AMMO_MAX = 200;
    const int WATER_DEPLETION_IN_FIRE = 10;

    // Member vars
    Vector2 moveDirection = Vector2.zero;
    Vector2 speed = Vector2.zero;

    // Unity vars
    [SerializeField] SessionData sessionData;
    [SerializeField] PlayerData playerData;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] GameObject waterPelletPrefab;
    [SerializeField] GameObject collisionPrefab;
    [SerializeField] GameObject personPrefab;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Transform helicopter;

    public void RescuePeople() {
        for (int i = 0; i < playerData.people; i++) {
            float progress = 1f * i / playerData.people;
            Vector3 offset = MathUtils.PolarToCartesian(360 * progress, 16);
            GameObject go = Instantiate(personPrefab, helicopter.position + offset, Quaternion.identity, transform.parent);
            go.GetComponent<Person>().WaitForPickup();
            go.AddComponent<Expirable>().SetDuration(1 + progress);
        }

        sessionData.peopleSaved += playerData.people;
        playerData.UnloadPeople();
    }

    void Start() {
        StartCoroutine(ShootWaterRoutine());
    }

    void Update() {
        playerData.position = body.position;
    }

    void FixedUpdate() {
        moveDirection = new Vector2(Input.GetAxisRaw("PlayerHorizontal"), Input.GetAxisRaw("PlayerVertical"));
        body.AddForce(moveDirection.normalized * GetSpeed());
    }

    void LateUpdate() {
        sprite8Directional.SetAngle(MathUtils.VectorToAngle(body.velocity));
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Fire")) {
            playerData.DepleteWater(WATER_DEPLETION_IN_FIRE);
        }
    }

    float GetSpeed() {
        return (5f / (5 + playerData.people)) * MAX_SPEED;
    }

    IEnumerator ShootWaterRoutine() {
        while (true) {
            if (playerData.water > 0) {
                float x = Input.GetAxisRaw("WaterHorizontal");
                float y = Input.GetAxisRaw("WaterVertical");
                if (x != 0 || y != 0) {
                    float angle = Mathf.Atan2(y, x);
                    WaterPellet waterPellet = Instantiate(waterPelletPrefab, transform.position + Vector3.up * 20, Quaternion.identity, transform.parent).GetComponent<WaterPellet>();
                    waterPellet.Shoot(angle);
                    playerData.DepleteWater();
                }
            }
            yield return new WaitForSeconds(WATER_SHOOT_INTERVAL);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        Person person = other.GetComponent<Person>();
        if (person) {
            playerData.AddPerson();
            person.Remove();
            return;
        }
        // Moving fast enough
        if (body.velocity.sqrMagnitude > 10000) {
            Instantiate(collisionPrefab, collision.GetContact(0).point, Quaternion.identity, transform.parent);
            SpriteSquish spriteSquish = other.GetComponent<SpriteSquish>();
            if (spriteSquish) {
                spriteSquish.SquishThin();
            }
            if (other.GetComponent<Car>()) {
                other.GetComponent<Mortal>().Damage(gameObject.tag, 40);
            }
        }
    }
}

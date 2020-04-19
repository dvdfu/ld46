using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // Consts
    const float MAX_SPEED = 1000;
    const float WATER_SHOOT_INTERVAL = 0.05f;
    const int WATER_AMMO_MAX = 200;
    const int WATER_DEPLETION_IN_FIRE = 10;

    // Member vars
    Vector2 moveDirection = Vector2.zero;
    Vector2 speed = Vector2.zero;
    int waterAmmo = WATER_AMMO_MAX;
    int people = 0;

    // Unity vars
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] GameObject waterPelletPrefab;
    [SerializeField] GameObject collisionPrefab;
    [SerializeField] Rigidbody2D body;
    [SerializeField] RectTransform waterFill;
    [SerializeField] RectTransform peopleContainer;
    [SerializeField] Text personCount;

    public void RefillWater(int amount = 1) {
        if (waterAmmo + amount < WATER_AMMO_MAX) {
            waterAmmo += amount;
        } else {
            waterAmmo = WATER_AMMO_MAX;
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
        body.AddForce(moveDirection.normalized * GetSpeed());
    }

    void LateUpdate() {
        waterFill.sizeDelta = new Vector2(64f * waterAmmo / WATER_AMMO_MAX, 8);
        sprite8Directional.SetAngle(MathUtils.VectorToAngle(body.velocity));
        peopleContainer.anchoredPosition = transform.position - Camera.main.transform.position + new Vector3(24, 40);
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Fire")) {
            DepleteWater(WATER_DEPLETION_IN_FIRE);
        }
    }

    float GetSpeed() {
        return (8f / (8 + people)) * MAX_SPEED;
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
        Person person = collision.gameObject.GetComponent<Person>();
        if (person) {
            person.Remove();
            people++;
            peopleContainer.gameObject.SetActive(true);
            peopleContainer.GetComponent<SpriteSquish>().SquishThin();
            personCount.text = people.ToString();
            return;
        }
        // Moving fast enough
        if (body.velocity.sqrMagnitude > 10000) {
            Instantiate(collisionPrefab, collision.GetContact(0).point, Quaternion.identity, transform.parent);
            SpriteSquish spriteSquish = collision.gameObject.GetComponent<SpriteSquish>();
            if (spriteSquish) {
                spriteSquish.SquishThin();
            }
            if (collision.gameObject.GetComponent<Car>()) {
                collision.gameObject.GetComponent<Mortal>().Damage(gameObject.tag, 40);
            }
        }
    }
}

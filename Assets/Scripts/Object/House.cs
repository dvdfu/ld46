using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField] GameObject personPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalHouse;
    [SerializeField] Sprite burnedHouse;
    [SerializeField] Sprite ashes;
    [SerializeField] Mortal mortal;
    [SerializeField] Flammable flammable;

    int peopleInside;

    public void OnIgnite() {
        if (peopleInside > 0) {
            for (int i = 0; i < peopleInside; i++) {
                Vector2 offset = MathUtils.PolarToCartesian(360f * i / peopleInside, 10);
                Instantiate(personPrefab, transform.position + (Vector3) offset, Quaternion.identity, transform.parent);
            }
            peopleInside = 0;
        }
    }

    void Start() {
        peopleInside = Random.Range(1, 4);
    }

    void Update() {
        if (mortal.GetAlive()) {
            if (mortal.GetHealth() >= 50.0f) {
                spriteRenderer.sprite = normalHouse;
            } else {
                spriteRenderer.sprite = burnedHouse;
            }
        } else {
            spriteRenderer.sprite = ashes;
        }
    }

    public void Die() {}
}

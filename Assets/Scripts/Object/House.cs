using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite burnedHouse;
    [SerializeField] Sprite ashes;

    int peopleInside = 3;

    public void OnIgnite() {
        spriteRenderer.sprite = burnedHouse;
    }

    public void OnExtinguish() {
        if (peopleInside > 0) {
            for (int i = 0; i < peopleInside; i++) {
                Vector2 offset = MathUtils.PolarToCartesian(360f * i / peopleInside, 10);
                Instantiate(personPrefab, transform.position + (Vector3) offset, Quaternion.identity, transform.parent);
            }
            peopleInside = 0;
        }
    }

    public void Die() {
        spriteRenderer.sprite = ashes;
        if (peopleInside > 0) {
            for (int i = 0; i < peopleInside; i++) {
                Vector2 offset = MathUtils.PolarToCartesian(360f * i / peopleInside, 20);
                Instantiate(tombstonePrefab, transform.position + (Vector3) offset, Quaternion.identity, transform.parent);
            }
            peopleInside = 0;
        }
    }
}

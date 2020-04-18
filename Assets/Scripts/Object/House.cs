using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalHouse;
    [SerializeField] Sprite burnedHouse;
    [SerializeField] Sprite ashes;
    [SerializeField] Mortal mortal;
    [SerializeField] Flammable flammable;

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

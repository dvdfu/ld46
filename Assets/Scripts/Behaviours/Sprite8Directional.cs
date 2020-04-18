using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite8Directional : MonoBehaviour {
    const float MOVE_THRESHOLD = 30;

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Sprite spriteN;
    [SerializeField] Sprite spriteNE;
    [SerializeField] Sprite spriteE;
    [SerializeField] Sprite spriteSE;
    [SerializeField] Sprite spriteS;

    public void SetAngle(float angle) {
        angle += 45f / 2;
        angle = (angle + 360) % 360;
        if (angle < 45) {
            spriteRenderer.sprite = spriteE;
            spriteRenderer.flipX = false;
        } else if (angle < 90) {
            spriteRenderer.sprite = spriteNE;
            spriteRenderer.flipX = false;
        } else if (angle < 135) {
            spriteRenderer.sprite = spriteN;
        } else if (angle < 180) {
            spriteRenderer.sprite = spriteNE;
            spriteRenderer.flipX = true;
        } else if (angle < 225) {
            spriteRenderer.sprite = spriteE;
            spriteRenderer.flipX = true;
        } else if (angle < 270) {
            spriteRenderer.sprite = spriteSE;
            spriteRenderer.flipX = true;
        } else if (angle < 315) {
            spriteRenderer.sprite = spriteS;
        } else {
            spriteRenderer.sprite = spriteSE;
            spriteRenderer.flipX = false;
        }
    }
}

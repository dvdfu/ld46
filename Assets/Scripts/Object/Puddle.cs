using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    Expirable expirable;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        expirable = GetComponent<Expirable>();
    }

    void Update() {
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1f - expirable.GetElapsed());
        float scale = 1 - MathUtils.Map(expirable.GetElapsed(), 0, 1, 0, 0.3f);
        transform.localScale = new Vector2(1, 0.7f) * scale;
    }
}

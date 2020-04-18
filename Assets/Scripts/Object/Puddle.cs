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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f - expirable.GetElapsed());

        float scale = MathUtils.Map(expirable.GetElapsed(), 0f, 1f, 0f, 0.3f);
        transform.localScale = new Vector2(1f, 1f) * (1f - scale);
    }
}

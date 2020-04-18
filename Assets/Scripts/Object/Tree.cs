using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite burnedSprite;

    public void OnIgnite() {
        spriteRenderer.sprite = burnedSprite;
    }
}

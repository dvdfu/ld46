using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownSortable : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;

    void LateUpdate() {
        spriteRenderer.sortingOrder = -Mathf.FloorToInt(transform.position.y);
    }
}

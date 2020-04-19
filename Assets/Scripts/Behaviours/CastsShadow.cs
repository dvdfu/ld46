using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastsShadow : MonoBehaviour {
    [SerializeField] GameObject shadowPrefab;
    [SerializeField] Vector2 scale = new Vector2(1, 1);
    [SerializeField] Vector2 offset = new Vector2(0, 6);

    GameObject shadow;

    public void RemoveShadow() {
        Destroy(shadow);
    }
    
    void Start() {
        shadow = Instantiate(shadowPrefab, transform.position + (Vector3) offset, Quaternion.identity, transform);
        shadow.transform.localScale = new Vector3(scale.x, scale.y, 1);
    }
}

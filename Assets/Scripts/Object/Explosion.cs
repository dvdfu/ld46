using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    void Start() {
        RaycastHit2D[] results = Physics2D.CircleCastAll(transform.position, 32, Vector2.zero, 0);
        foreach (RaycastHit2D result in results) {
            Flammable flammable = result.collider.gameObject.GetComponent<Flammable>();
            if (flammable) {
                flammable.SetOnFire();
            }
        }
        Camera.main.gameObject.GetComponent<CameraHelper>().Shake();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour {
    [SerializeField] LineRenderer hose;

    Player player = null;

    void Start() {
        hose.SetPosition(0, transform.position + new Vector3(0, 6));
        hose.SetPosition(1, transform.position + new Vector3(0, 6));
    }

    void FixedUpdate() {
        if (player) {
            player.RefillWater(2);
        }
    }
    
    void LateUpdate() {
        Vector3 target = player ? player.transform.position : transform.position;
        target = target + Vector3.up * 6;
        target = Vector3.Lerp(hose.GetPosition(1), target, 0.3f);
        hose.SetPosition(1, target);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<Player>()) {
            player = collider.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.GetComponent<Player>()) {
            player = null;
        }
    }
}

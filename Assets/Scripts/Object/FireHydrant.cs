using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();
        if (player) {
            player.RefillWater();
        }
    }
}

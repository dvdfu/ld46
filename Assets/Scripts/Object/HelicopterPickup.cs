using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPickup : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player) {
            player.RescuePeople();
        }
    }
}

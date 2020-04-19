using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour {
    [SerializeField] PlayerData playerData;
    [SerializeField] LineRenderer hose;
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip endSound;

    Player player = null;

    void Start() {
        hose.SetPosition(0, transform.position + new Vector3(0, 6));
        hose.SetPosition(1, transform.position + new Vector3(0, 6));
    }

    void FixedUpdate() {
        if (player) {
            playerData.RefillWater(2);
        }
    }
    
    void LateUpdate() {
        Vector3 target = player ? player.transform.position : transform.position;
        target = target + Vector3.up * 6;
        target = Vector3.Lerp(hose.GetPosition(1), target, 0.3f);
        hose.SetPosition(1, target);
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (player == null && collider.GetComponent<Player>()) {
            if (playerData.water < PlayerData.WATER_MAX) {
                player = collider.GetComponent<Player>();
                SoundManager.Play(startSound);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.GetComponent<Player>()) {
            player = null;
        }
    }
}

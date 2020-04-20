using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour {
    [SerializeField] AudioClip deathSound;

    void Start() {
        SoundManager.Play(deathSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flammable : MonoBehaviour {
    public UnityEvent igniteEvent = new UnityEvent();
    public UnityEvent extinguishEvent = new UnityEvent();

    [SerializeField] GameObject steamPrefab;
    [SerializeField] GameObject debrisPrefab;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] Mortal mortal;
    [SerializeField] AudioClip flameSound;
    [SerializeField] AudioClip extinguishSound;

    int fireHealth = 0;

    public void SetOnFire() {
        if (!IsOnFire()) {
            fireHealth = 10;
            fire.Play();
            smoke.Play();
            igniteEvent.Invoke();
            SoundManager.Play(flameSound);
        }
    }

    public bool IsOnFire() {
        return fireHealth > 0;
    }

    public void Extinguish() {
        if (IsOnFire()) {
            fireHealth--;
            if (fireHealth == 0) {
                SoundManager.Play(extinguishSound);
                fire.Stop();
                smoke.Stop();
                extinguishEvent.Invoke();
                Instantiate(steamPrefab, transform.position + Vector3.up * 8, Quaternion.identity, transform.parent);
            }
        }
    }

    void Start() {
        StartCoroutine(SpawnDebrisRoutine());
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Fire")) {
            SetOnFire();
        } else if (collider.gameObject.CompareTag("Water")) {
            Extinguish();
        }
    }

    void FixedUpdate() {
        if (IsOnFire()) {
            mortal.Damage(gameObject.tag, Time.deltaTime);
        }
    }

    IEnumerator SpawnDebrisRoutine() {
        while (true) {
            while (!IsOnFire()) {
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(2, 5));
            if (IsOnFire()) {
                Instantiate(debrisPrefab, transform.position + Vector3.up * 20, Quaternion.identity, transform.parent);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Expirable : MonoBehaviour {
    public UnityEvent expireEvent = new UnityEvent();

    [SerializeField] float duration;
    float startTime;

    void Start() {
        startTime = Time.time;
        StartCoroutine(ExpireRoutine());
    }

    IEnumerator ExpireRoutine() {
        yield return new WaitForSeconds(duration);
        expireEvent.Invoke();
        Destroy(gameObject);
    }

    public float GetElapsed() {
        return Mathf.Clamp((Time.time - startTime) / duration, 0f, 1f);
    }
}

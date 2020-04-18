using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Expirable : MonoBehaviour {
    public UnityEvent expireEvent = new UnityEvent();

    [SerializeField] float duration;

    void Start() {
        StartCoroutine(ExpireRoutine());
    }

    IEnumerator ExpireRoutine() {
        yield return new WaitForSeconds(duration);
        expireEvent.Invoke();
        Destroy(gameObject);
    }
}

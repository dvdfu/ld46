using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSquish : MonoBehaviour {
    const float DURATION = 0.2f;

    [SerializeField] Transform target;

    Coroutine squishRoutine = null;

    public void SquishThin() {
        if (squishRoutine != null) {
            StopCoroutine(squishRoutine);
        }
        squishRoutine = StartCoroutine(SquishRoutine());
    }

    IEnumerator SquishRoutine() {
        float t = 0;
        float progress, scale;
        while (t < DURATION) {
            t += Time.deltaTime;
            progress = Easing.CubicIn(t / DURATION);
            scale = 1 + (1 - progress) / 2;
            transform.localScale = new Vector3(1 / scale, scale);
            yield return null;
        }
        squishRoutine = null;
    }
}

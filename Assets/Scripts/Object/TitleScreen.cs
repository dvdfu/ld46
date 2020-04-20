using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
    [SerializeField] RectTransform titleWhatThe;
    [SerializeField] RectTransform titleFiretruck;
    [SerializeField] Text subtitle;

    void Start() {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine() {
        float t = 0;
        float scale = 0;
        titleWhatThe.localScale = Vector2.zero;
        titleFiretruck.localScale = Vector2.zero;
        subtitle.color = Color.clear;

        yield return new WaitForSeconds(1);

        while (t < 0.5f) {
            scale = Easing.ElasticOut(t * 2);
            titleWhatThe.localScale = new Vector2(scale, scale);
            t += Time.deltaTime;
            yield return null;
        }
        titleWhatThe.localScale = new Vector2(1, 1);

        t = 0;
        while (t < 0.5f) {
            scale = Easing.ElasticOut(t * 2);
            titleFiretruck.localScale = new Vector2(scale, scale);
            t += Time.deltaTime;
            yield return null;
        }
        titleFiretruck.localScale = new Vector2(1, 1);

        t = 0;
        while (t < 0.3f) {
            float alpha = t / 0.3f;
            subtitle.color = new Color(0, 0, 0, alpha);
            t += Time.deltaTime;
            yield return null;
        }
        subtitle.color = Color.black;

        while (true) {
            if (Input.anyKey) {
                SceneManager.LoadScene("Game");
            }
            yield return null;
        }
    }
}

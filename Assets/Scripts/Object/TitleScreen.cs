using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
    [SerializeField] RectTransform titleWhatThe;
    [SerializeField] RectTransform titleFiretruck;
    [SerializeField] Text prompt;
    [SerializeField] Text subtitle;
    [SerializeField] AudioClip thudSound;
    [SerializeField] AudioClip startSound;

    void Start() {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine() {
        float t = 0;
        float scale = 0;
        titleWhatThe.localScale = Vector2.zero;
        titleFiretruck.localScale = Vector2.zero;
        prompt.color = Color.clear;
        subtitle.color = Color.clear;

        yield return new WaitForSeconds(1);

        SoundManager.Play(thudSound);
        while (t < 0.5f) {
            scale = Easing.ElasticOut(t * 2);
            titleWhatThe.localScale = new Vector2(scale, scale);
            t += Time.deltaTime;
            yield return null;
        }
        titleWhatThe.localScale = new Vector2(1, 1);

        SoundManager.Play(thudSound);
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
            prompt.color = new Color(1, 1, 1, alpha);
            subtitle.color = new Color(0, 0, 0, alpha);
            t += Time.deltaTime;
            yield return null;
        }
        prompt.color = Color.white;
        subtitle.color = Color.black;

        while (true) {
            if (Input.anyKey) {
                SoundManager.Play(startSound);
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("Game");
            }
            yield return null;
        }
    }
}

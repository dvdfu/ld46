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
        Color promptColor = prompt.color;
        Color subtitleColor = subtitle.color;
        prompt.color = Color.clear;
        subtitle.color = Color.clear;

        yield return new WaitForSeconds(0.3f);

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
            prompt.color = new Color(promptColor.r, promptColor.g, promptColor.b, alpha);
            subtitle.color = new Color(subtitleColor.r, subtitleColor.g, subtitleColor.b, alpha);
            t += Time.deltaTime;
            yield return null;
        }
        prompt.color = promptColor;
        subtitle.color = subtitleColor;

        while (true) {
            if (Input.anyKey) {
                SoundManager.Play(startSound);
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("Instructions");
            }
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour {
    [SerializeField] string nextScene;
    [SerializeField] Image seal;
    [SerializeField] Text text;
    [SerializeField] AudioClip pressSound;

    bool canPress;

    void Start() {
        StartCoroutine(PressRoutine());
    }

    IEnumerator PressRoutine() {
        seal.enabled = false;
        text.enabled = false;
        canPress = false;
        yield return new WaitForSeconds(2);

        text.enabled = true;
        canPress = true;
        StartCoroutine(Bulge(text.GetComponent<RectTransform>()));

        while (canPress) {
            if (Input.anyKey) {
                SoundManager.Play(pressSound);
                break;
            } else {
                yield return null;
            }
        }
        canPress = false;
        seal.enabled = true;
        text.enabled = false;
        yield return Bulge(seal.GetComponent<RectTransform>());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator Bulge(RectTransform rectTransform) {
        float t = 0;
        while (t < 0.3f) {
            float progress = Easing.ElasticOut(t / 0.3f);
            rectTransform.localScale = new Vector3(progress, progress);
            t += Time.deltaTime;
            yield return null;
        }
        rectTransform.localScale = new Vector3(1, 1);
    }
}

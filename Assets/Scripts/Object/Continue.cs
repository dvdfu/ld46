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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextScene);
    }
}

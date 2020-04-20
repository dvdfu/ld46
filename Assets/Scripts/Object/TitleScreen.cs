using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
    [SerializeField] PlayerData playerData;
    [SerializeField] RectTransform titleWhatThe;
    [SerializeField] RectTransform titleFiretruck;
    [SerializeField] GameObject worldUI;
    [SerializeField] Text prompt;
    [SerializeField] AudioClip thudSound;
    [SerializeField] AudioClip promptSound;
    [SerializeField] GameObject house;
    [SerializeField] GameObject fireHydrant;

    void Start() {
        playerData.Reset();
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine() {
        float t = 0;
        float scale = 0;
        titleWhatThe.localScale = Vector2.zero;
        titleFiretruck.localScale = Vector2.zero;
        worldUI.SetActive(false);

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

        RectTransform worldTransform = worldUI.GetComponent<RectTransform>();
        worldTransform.anchoredPosition = Vector2.down * 100;
        worldUI.SetActive(true);

        t = 0;
        while (t < 0.5f) {
            float y = (1 - Easing.CubicIn(t * 2)) * 100;
            worldTransform.anchoredPosition = Vector2.down * y;
            t += Time.deltaTime;
            yield return null;
        }
        worldTransform.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(0.3f);

        SoundManager.Play(promptSound);
        prompt.enabled = true;
        while (Input.GetAxisRaw("PlayerHorizontal") == 0 && Input.GetAxisRaw("PlayerVertical") == 0) {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SoundManager.Play(promptSound);

        fireHydrant.SetActive(true);
        house.SetActive(true);
        Flammable f = house.GetComponentInChildren<Flammable>();
        f.SetOnFire();

        prompt.text = "Water - <color=\"#1E8AD4\">Arrow Keys</color>";
        while (f.IsOnFire()) {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        prompt.text = "Starting mission!";
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Instructions");
    }
}

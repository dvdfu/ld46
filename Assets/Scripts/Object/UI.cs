using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    [SerializeField] SessionData sessionData;
    [SerializeField] PlayerData playerData;
    [SerializeField] SpriteSquish peopleContainer;
    [SerializeField] Text peopleCount;
    [SerializeField] Text timer;
    [SerializeField] Text deathCount;
    [SerializeField] RectTransform waterFill;
    [SerializeField] RectTransform timerFill;
    [SerializeField] Image overlay;
    [SerializeField] Image ready;
    [SerializeField] Image go;
    [SerializeField] Image finish;

    public void OnPeopleChange() {
        peopleCount.text = playerData.people.ToString();
        peopleContainer.gameObject.SetActive(playerData.people > 0);
        if (playerData.people > 0) {
            peopleContainer.SquishThin();
        }
    }

    public void OnGameOver() {
        StartCoroutine(GameOverRoutine());
    }

    void Awake() {
        StartCoroutine(InitRoutine());
    }

    void Start() {
        playerData.peopleChangeEvent.AddListener(OnPeopleChange);
        GameManager.gameOverEvent.AddListener(OnGameOver);
    }

    void OnDestroy() {
        playerData.peopleChangeEvent.RemoveListener(OnPeopleChange);
        GameManager.gameOverEvent.RemoveListener(OnGameOver);
    }

    void LateUpdate() {
        waterFill.sizeDelta = new Vector2(64f * playerData.water / PlayerData.WATER_MAX, 24);
        Vector2 camPosition = Camera.main.transform.position;
        peopleContainer.GetComponent<RectTransform>().anchoredPosition = playerData.position - camPosition + new Vector2(20, 40);

        timer.text = Formatter.TimeToString(sessionData.time);
        timerFill.sizeDelta = new Vector2(64 * (1 - sessionData.GetGameProgress()), 24);
        deathCount.text = sessionData.peopleDied.ToString();
    }

    IEnumerator InitRoutine() {
        Time.timeScale = 0;
        ready.enabled = true;
        yield return new WaitForSecondsRealtime(2);

        ready.enabled = false;
        go.enabled = true;
        overlay.enabled = false;
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(1);

        go.enabled = false;
    }

    IEnumerator GameOverRoutine() {
        Time.timeScale = 0;
        overlay.enabled = true;
        finish.enabled = true;
        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 1;
        SceneManager.LoadScene("Results");
    }
}

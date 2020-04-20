using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static UnityEvent gameOverEvent = new UnityEvent();

    [SerializeField] SessionData sessionData;
    [SerializeField] PlayerData playerData;

    void Start() {
        Application.targetFrameRate = 60;
        sessionData.Reset();
        playerData.Reset();
    }

    void Update() {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate() {
        if (sessionData.time > Time.deltaTime) {
            sessionData.time -= Time.deltaTime;
        } else {
            sessionData.time = 0;
            GameManager.gameOverEvent.Invoke();
            SceneManager.LoadScene("Results");
        }
    }
}

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

    #if UNITY_EDITOR
    void Update() {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene("Game");
        }
    }
    #endif

    void FixedUpdate() {
        if (sessionData.time > Time.deltaTime) {
            sessionData.time -= Time.deltaTime;
        } else {
            sessionData.time = 0;
            GameManager.gameOverEvent.Invoke();
        }
    }
}

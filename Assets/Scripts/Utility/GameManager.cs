using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] SessionData sessionData;

    void Start() {
        Application.targetFrameRate = 60;
        sessionData.Reset();
    }

    void Update() {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene("Game");
        }
    }
}

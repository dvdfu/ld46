using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    void Start() {
        Application.targetFrameRate = 60;
    }

    void Update() {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene("Game");
        }
    }
}

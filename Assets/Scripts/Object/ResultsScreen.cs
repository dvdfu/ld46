using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour {
    [SerializeField] SessionData sessionData;
    [SerializeField] Text peopleDied;
    [SerializeField] Text peopleSave;
    [SerializeField] Text thingsIgnited;
    [SerializeField] Text thingsExtinguished;
    [SerializeField] Text waterUsed;

    void Start() {
        peopleDied.text = sessionData.peopleDied.ToString();
        peopleSave.text = sessionData.peopleSaved.ToString();
        thingsIgnited.text = sessionData.unitsBurned.ToString();
        thingsExtinguished.text = sessionData.unitsExtinguished.ToString();
        waterUsed.text = (sessionData.waterUsed * 4).ToString() + " L";
    }
}

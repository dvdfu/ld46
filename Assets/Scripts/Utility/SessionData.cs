using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Session Data")]
public class SessionData : ScriptableObject {
    public const float GAME_DURATION = 120;

    public int peopleSaved;
    public int peopleDied;
    public int unitsBurned;
    public int unitsExtinguished;
    public int waterUsed;
    public float time;

    public void Reset() {
        peopleSaved = 0;
        peopleDied = 0;
        unitsBurned = 0;
        unitsExtinguished = 0;
        waterUsed = 0;
        time = GAME_DURATION;
    }
}

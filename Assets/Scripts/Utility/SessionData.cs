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

    public struct Ratings {
        public Rating peopleRating;
        public Rating unitRating;
        public Rating finalRating;
    }

    public enum Rating {
        Bad,
        Average,
        Good,
        Perfect,
    }

    public enum Title {
        None,
        GrimReaper, // > 50 kills
        Arsonist, // < 5 extinguished
        HydroHomie, // > 2000L
    }

    public void Reset() {
        peopleSaved = 0;
        peopleDied = 0;
        unitsBurned = 0;
        unitsExtinguished = 0;
        waterUsed = 0;
        time = GAME_DURATION;
    }

    public Ratings GetRating() {
        Ratings ratings = new Ratings();

        if (peopleDied < 5) {
            ratings.peopleRating = Rating.Perfect;
        } else if (peopleDied < 10) {
            ratings.peopleRating = Rating.Good;
        } else if (peopleDied < 20) {
            ratings.peopleRating = Rating.Average; 
        } else {
            ratings.peopleRating = Rating.Bad;
        }

        float burnRate = (unitsExtinguished + 1) / (unitsBurned + 1);
        if (burnRate > 0.85f) {
            ratings.unitRating = Rating.Perfect;
        } else if (burnRate > 0.7f) {
            ratings.unitRating = Rating.Good;
        } else if (burnRate > 0.5f) {
            ratings.unitRating = Rating.Average;
        } else {
            ratings.unitRating = Rating.Bad;
        }

        int totalRating = (int)ratings.peopleRating + (int)ratings.unitRating;
        if (totalRating == 6) {
            ratings.finalRating = Rating.Perfect;
        } else if (totalRating > 3) {
            ratings.finalRating = Rating.Good;
        } else if (totalRating > 0) {
            ratings.finalRating = Rating.Average;
        } else {
            ratings.finalRating = Rating.Bad;
        }

        return ratings;
    }

    public Title GetTitle() {
        if (peopleDied > 50) {
            return Title.GrimReaper;
        } else if (unitsExtinguished < 5) {
            return Title.Arsonist;
        } else if (waterUsed > 2000) {
            return Title.HydroHomie;
        } else {
            return Title.None;
        }
    }

    public float GetGameProgress() {
        return (GAME_DURATION - time) / GAME_DURATION;
    }
}

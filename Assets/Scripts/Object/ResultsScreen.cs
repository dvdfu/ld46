using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour {
    [SerializeField] SessionData sessionData;
    [SerializeField] Text peopleDied;
    [SerializeField] Text peopleSave;
    [SerializeField] Text thingsIgnited;
    [SerializeField] Text thingsExtinguished;
    [SerializeField] Text waterUsed;
    [SerializeField] Text summary;

    void Start() {
        peopleDied.text = sessionData.peopleDied.ToString();
        peopleSave.text = sessionData.peopleSaved.ToString();
        thingsIgnited.text = sessionData.unitsBurned.ToString();
        thingsExtinguished.text = sessionData.unitsExtinguished.ToString();
        waterUsed.text = sessionData.waterUsed.ToString() + " L";

        SessionData.Ratings r = sessionData.GetRating();
        SessionData.Title t = sessionData.GetTitle();

        summary.text = "Fire truck unit LDXLVI was immediately dispatched to the scene of the fire";

        switch (r.finalRating) {
            case SessionData.Rating.Bad:
                summary.text += ", only to further worsen the situation. ";
                break;
            case SessionData.Rating.Average:
                summary.text += ". ";
                break;
            case SessionData.Rating.Good:
                summary.text += ", and handled the situation to the best of their ability. ";
                break;
            case SessionData.Rating.Perfect:
                summary.text += ", and valiantly handled the situation. ";
                break;
        }

        switch (r.peopleRating) {
            case SessionData.Rating.Bad:
                summary.text += "There were mass casualties ";
                break;
            case SessionData.Rating.Average:
                summary.text += "There were a number of casualties";
                break;
            case SessionData.Rating.Good:
                summary.text += "There were a few casualties ";
                break;
            case SessionData.Rating.Perfect:
                summary.text += "There were very few casualties ";
                break;
        }

        switch (r.unitRating) {
            case SessionData.Rating.Bad:
                summary.text += " and charred buildings everywhere.";
                break;
            case SessionData.Rating.Average:
                summary.text += " and they did their best to extinguish buildings, although some did burn down.";
                break;
            case SessionData.Rating.Good:
                summary.text += " and most buildings were extinguished.";
                break;
            case SessionData.Rating.Perfect:
                summary.text += " and almost no building was left unextinguished.";
                break;
        }

        switch (t) {
            case SessionData.Title.None:
                break;
            case SessionData.Title.GrimReaper:
                summary.text += " You have been awarded the <color=#DF3E23>Grim Reaper</color> distinction.";
                break;
            case SessionData.Title.Arsonist:
                summary.text += " You have been awarded the <color=#DD6000>Arsonist</color> distinction.";
                break;
            case SessionData.Title.HydroHomie:
                summary.text += " You have been awarded the <color=#249FDE>HydroHomie</color> distinction.";
                break;
        }
    }
}

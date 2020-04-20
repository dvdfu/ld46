using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject {
    public const int WATER_MAX = 150;

    public UnityEvent peopleChangeEvent = new UnityEvent();

    [SerializeField] SessionData sessionData;

    public Vector2 position;
    public int water;
    public int people;

    public void Reset() {
        position = Vector2.zero;
        water = WATER_MAX;
        people = 0;
    }

    public void RefillWater(int amount = 1) {
        if (water + amount < WATER_MAX) {
            water += amount;
        } else {
            water = WATER_MAX;
        }
    }

    public void DepleteWater(int amount = 1) {
        if (water > amount) {
            water -= amount;
            sessionData.waterUsed += amount;
        } else {
            water = 0;
        }
    }

    public void AddPerson() {
        people++;
        peopleChangeEvent.Invoke();
    }

    public void UnloadPeople() {
        people = 0;
        peopleChangeEvent.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static GameState instance = null;
    public int dayNum { get; set; }
    public Resources resources { get; set; }

    public int numInfiltrators = 0;

    private GameState() {
        this.dayNum = 1;
        this.resources = new Resources(10, 10, 10, 4);
    }

    public static GameState GetGameState() {
        if (instance == null) {
            return new GameState();
        }
        return instance;
    }

    public void IncrementDayNum() {
        this.dayNum++;
    }

    public int getNumPeople() {
        return this.resources.people;
    }

    // Note: If resources hit zero, numPeople will decrement
    public void UpdateResources(Resources res) {
        this.resources.defense += res.defense;
        this.resources.morale += res.morale;
        this.resources.supplies += res.supplies;
        this.resources.people += res.people;
    }
}

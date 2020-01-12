using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static GameState instance = null;
    public int dayNum { get; set; }
    public Resources resources { get; set; } // TODO: ensure resources all >= 0

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

    // Note: If resources are hit zero, numPeople will decrement
    // public void UpdateResources()
}

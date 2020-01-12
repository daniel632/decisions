using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static GameState instance = null;
    public int dayNum { get; set; }
    public int numPeople { get; set; }
    private Resources resources; // TODO: ensure resources all >= 0

    public int numInfiltrators = 0;

    private GameState() {
        this.dayNum = 1;
        this.numPeople = 1;
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

    // Note: If resources are hit zero, numPeople will decrement
    // public void UpdateResources()
}

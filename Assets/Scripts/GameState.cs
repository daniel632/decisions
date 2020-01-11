using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static GameState instance = null;
    public int dayNum = 1;
    public int numPeople = 0;
    public int morale;
    public int defense;
    public int resources;

    private GameState() {

    }

    public static GameState getGameState() {
        if (instance == null) {
            return new GameState();
        }
        return instance;
    }

    public void incrementDayNum() {
        this.dayNum++;
    }
}

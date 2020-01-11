﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    GameState gs;

    private bool hasQuit = false;

    void Start() {
        gs = GameState.getGameState();

        run();
    }

    void Update() {
        // TODO: If player presses esc, ask them if they want to quit. Then set hasQuit if true.
    }

    private void run() {
        while (gs.numPeople > 0 && !hasQuit) {
            startDay();
            endDay();
        }
        
        // TODO: Display Game Over view (number of days survived, and maybe some interesting info like max number of survivors)
    }

    private void startDay() {
        // Randomly generate event (or none at all)
        // Event e = new Event();
        Event e = Event.createRandom();

        // TODO: Present the event to the user, and get their response
        

        // Now allow the player to make an aditional action

    }

    private void endDay() {
        gs.incrementDayNum();
    }
}

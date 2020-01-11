using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    GameState gs;

    void Start() {
        gs = GameState.getGameState();

        run();
    }

    void Update() {
        
    }

    private void run() {
        while (gs.numPeople > 0) {
            startDay();
            endDay();
        }
        
        // TODO: Present Game Over view
    }

    private void startDay() {
        // Randomly generate event (or none at all) and present it to the player
        // Event event = Event.create();


        // (After the event) Allow the player to make an action

    }

    private void endDay() {
        gs.incrementDayNum();
    }
}

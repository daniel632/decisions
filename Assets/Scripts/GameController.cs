using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("UI Elements")]

    [Header("Game Config")]
    public int MAX_DAYS;

    [Header("Top Left")]
    public Text dayNumTextUI;
    public Text numPeopleTextUI;
    public Text defenseTextUI;
    public Text moraleTextUI;

    [Header("Top Right")]
    public Text suppliesTextUI;
    public Text medicalTextUI;
    public Text energyTextUI;

    [Header("Panels")]
    public RectTransform eventPanel;
    // These optional panels are for when the player needs to respond to an event
    public RectTransform optionalEventFollowUpPanel; 
    public RectTransform optionalEventOutcomePanel;
    public RectTransform actionPanel;
    public RectTransform actionOutcomePanel;
    public RectTransform pauseMenuPanel;
    public RectTransform gameOverPanel;

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
        if (e != null) {
            EventOutcome eOutcome = populateEventPanel(e);
            if (eOutcome != null && e.interactive) {
                getPlayerEventResponse(e);
                // eOutcome = e.getEventOutcome(playerResponse);
            }
        }


        // TODO Now allow the player to make an aditional action

    }

    private EventOutcome populateEventPanel(Event e) {
        // TODO
        return new EventOutcome();
    }

    private void getPlayerEventResponse(Event e) {
        // TODO
    }

    private void endDay() {
        gs.incrementDayNum();
    }
}

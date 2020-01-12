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

        StartCoroutine(run());
    }

    void Update() {
        // TODO: If player presses esc, ask them if they want to quit. Then set hasQuit if true.
    }

    private IEnumerator run() {
        while (gs.numPeople > 0 && !hasQuit) {
            presentEventPanel();
            yield return new WaitUntil(() => !this.eventPanel.gameObject.activeSelf);
            yield return new WaitForSeconds(5);

            presentActionPanel();
            yield return new WaitUntil(() => !this.actionPanel.gameObject.activeSelf);
            yield return new WaitForSeconds(2);
            presentActionOutcomePanel();

            endDay();
        }
        
        // TODO: Display Game Over view (number of days survived, and maybe some interesting info like max number of survivors)
    }

    private void presentEventPanel() {
        Event e = Event.createRandom();
        if (e != null) {
            Debug.Log(e.name + " - " + e.description);
            populateEventPanel(e);
            


            // if (eOutcome != null && e.interactive) {
            //     getPlayerEventResponse(e);
            //     // eOutcome = e.getEventOutcome(playerResponse);
            // }
        }

    }

    private void presentActionPanel() {
        List<Action> actions = Actions.createRandomActions();
        populateActionPanel(actions);
    }

    private void presentActionOutcomePanel() {
        Debug.Log("Action Outcome Panel");
        this.actionOutcomePanel.gameObject.SetActive(true);
    }

    private void populateEventPanel(Event e) {
        Debug.Log("Event Panel");
        this.eventPanel.gameObject.SetActive(true);
        Text title = (Text)this.eventPanel.Find("Title").GetComponent<Text>();
        Text desc = (Text)this.eventPanel.Find("Description").GetComponent<Text>();
        if (title != null && desc != null) {
            title.text = e.name;
            desc.text = e.description;
        }
    }

    private void populateActionPanel(List<Action> actions) {
        Debug.Log("Action Panel");
        this.actionPanel.gameObject.SetActive(true);
        Button[] btns = {
            (Button)this.actionPanel.Find("Btn1").GetComponent<Button>(), 
            (Button)this.actionPanel.Find("Btn2").GetComponent<Button>(), 
            (Button)this.actionPanel.Find("Btn3").GetComponent<Button>()
        };

        if (!(btns[0] && btns[1] && btns[2])) {
            Debug.Log("Error: Action buttons null");
            return;
        }

        for (int i = 0; i < btns.Length; i++) {
            // TODO - null check btn
            btns[i].gameObject.GetComponentInChildren<Text>().text = actions[i].description;
        }
    }

    public void closeEventPanel() {
        this.eventPanel.gameObject.SetActive(false);
    }

    private void getPlayerEventResponse(Event e) {
        // TODO
    }

    private void endDay() {
        gs.incrementDayNum();
    }
}

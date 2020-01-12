using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("UI Elements")]

    [Header("Game Config")]
    public int MAX_DAYS = 100;

    [Header("Top Left")]
    public Text dayNumTextUI;
    public Text numPeopleTextUI;

    [Header("Top Right")]
    public Text defenseTextUI;
    public Text moraleTextUI;
    public Text suppliesTextUI;


    [Header("Panels")]
    public RectTransform interactiveEventPanel; 
    // An event outcome can either follow an interactive event, or appears on it's own
    public RectTransform eventOutcomePanel;
    public RectTransform actionPanel;
    public RectTransform actionOutcomePanel;
    public RectTransform pauseMenuPanel;
    public RectTransform gameOverPanel;

    GameState gs;

    private bool hasQuit = false;

    void Start() {
        this.gs = GameState.GetGameState();
        UpdateResourceCountersUI();
        StartCoroutine(Run());
    }

    void Update() {
        // TODO: If player presses esc, ask them if they want to quit. Then set hasQuit if true.
    }

    private IEnumerator Run() {
        while (gs.getNumPeople() > 0 && gs.dayNum <= 100 && !hasQuit) {
            Event e = Event.CreateRandom();
            // Interactive:
            if (e.IsInteractive()) {
                PresentInteractiveEventPanel(e);
                yield return new WaitUntil(() => EventPanelIsHidden());
            }

            // TODO: Update res

            // Outcome:
            UpdateResourceCountersUI();
            PresentEventOutcomePanel(e);
            yield return new WaitUntil(() => EventPanelIsHidden());
            yield return new WaitForSeconds(3);


            // Action time:
            PresentActionPanel();
            yield return new WaitUntil(() => !this.actionPanel.gameObject.activeSelf);
            // TODO: Update res
            UpdateResourceCountersUI();
            PresentActionOutcomePanel();
            yield return new WaitUntil(() => !this.actionOutcomePanel.gameObject.activeSelf);
            yield return new WaitForSeconds(3);

            EndDay();
        }
        
        // TODO: Display Game Over view (number of days survived, and maybe some interesting info like max number of survivors)
    }

    private void PresentEventOutcomePanel(Event e) {
        Debug.Log("Event Outcome Panel");

        this.eventOutcomePanel.gameObject.SetActive(true);

        Text title = (Text) this.eventOutcomePanel.Find("Title").GetComponent<Text>();
        Text desc = (Text) this.eventOutcomePanel.Find("Description").GetComponent<Text>();
        if (title != null && desc != null) {
            title.text = "Outcome: " + e.name;
            desc.text = "Outcome: " + e.description;
        }
    }

    private void PresentInteractiveEventPanel(Event e) {
        if (!e.IsInteractive()) {
            Debug.Log("Static event's should only go through outcome presentation");
            return;
        }
        Debug.Log("Event Panel");

        this.interactiveEventPanel.gameObject.SetActive(true);

        Text title = (Text) this.interactiveEventPanel.Find("Title").GetComponent<Text>();
        Text desc = (Text) this.interactiveEventPanel.Find("Description").GetComponent<Text>();
        if (title != null && desc != null) {
            title.text = e.name;
            desc.text = e.description;
        }

        // TODO: populate buttons w/ choices

  
    }

    private void PresentActionPanel() {
        List<Action> actions = Actions.CreateRandomActions();

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

    // TODO - access the chosen action in here to populate outcome info
    private void PresentActionOutcomePanel() {
        Debug.Log("Action Outcome Panel");
        this.actionOutcomePanel.gameObject.SetActive(true);

        Text title = (Text) this.actionOutcomePanel.Find("Title").GetComponent<Text>();
        Text desc = (Text) this.actionOutcomePanel.Find("Description").GetComponent<Text>();
        if (title != null && desc != null) {
            title.text = "ACTION TITLE";
            desc.text = "ACTION DESC";
        }
    }

    private bool EventPanelIsHidden() {
        return !this.interactiveEventPanel.gameObject.activeSelf && 
            !this.eventOutcomePanel.gameObject.activeSelf;
    }

    public void CloseEventPanel(bool interactive) {
        if (interactive) {
            this.interactiveEventPanel.gameObject.SetActive(false);
        } else {
            this.eventOutcomePanel.gameObject.SetActive(false);
        }
    }

    public void CloseActionPanel() {
        this.actionPanel.gameObject.SetActive(false);
    }

    public void CloseActionOutcomePanel() {
        this.actionOutcomePanel.gameObject.SetActive(false);
    }

    private void GetPlayerEventResponse(Event e) {
        // TODO
    }

    private void EndDay() {
        gs.IncrementDayNum();
        UpdateDayCounterUI(this.gs.dayNum);
    }


    // PURE UI:
    private void UpdateDayCounterUI(int newDayNum) {
        this.dayNumTextUI.text = newDayNum.ToString();
    }

    private void UpdateResourceCountersUI() {
        this.numPeopleTextUI.text = this.gs.getNumPeople().ToString();
        this.defenseTextUI.text = this.gs.resources.defense.ToString();
        this.moraleTextUI.text = this.gs.resources.morale.ToString();
        this.suppliesTextUI.text = this.gs.resources.supplies.ToString();
    }
}

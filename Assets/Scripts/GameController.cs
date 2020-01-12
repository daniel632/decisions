using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("UI Elements")]

    [Header("Game Config")]
    public int MAX_DAYS = 100;
    public static int RNG_LEVEL = 5;

    [Header("Canvas")]
    public RectTransform canvas;

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
    public RectTransform endGamePanel;
    public RectTransform introInfoPanel;

    [Header("Event Choice Buttons")]
    public Button interactiveEventButton1;
    public Button interactiveEventButton2;
    public Button interactiveEventButton3;

    [Header("Action Choice Buttons")]
    public Button actionChoiceButton1;
    public Button actionChoiceButton2;
    public Button actionChoiceButton3;

    [Header("Environment Art")]
    public RectTransform lessPeople;
    public RectTransform morePeople;
    public RectTransform lessSupplies;
    public RectTransform moreSupplies;


    GameState gs;

    private bool hasQuit = false;
    private int interactiveEventOutcomeChoiceNumber = 0;
    private int actionChoiceNumber = 0;
    private Button[] interactiveEventChoiceButtons = null;  // event outcome buttons
    private Button[] actionChoiceButtons = null;  // event outcome buttons
    private int SMALL_PEOPLE_NUM = 10;
    private int SMALL_SUPPLIES_NUM = 15;
    private int MEDIUM_SUPPLIES_NUM = 50;
    private bool screenIsHidden = false;
    private bool morningTime = true;

    void Start() {
        this.gs = GameState.GetGameState();
        this.interactiveEventChoiceButtons = new Button[] { interactiveEventButton1, interactiveEventButton2, interactiveEventButton3 };
        this.actionChoiceButtons = new Button[] { actionChoiceButton1, actionChoiceButton2, actionChoiceButton3 };
        UpdateResourceCountersUI();
        UpdateDayCounterUI(1);
        if (!this.introInfoPanel.gameObject.activeSelf) {
            this.introInfoPanel.gameObject.SetActive(true);
        }
        this.lessPeople.gameObject.SetActive(true);

        StartCoroutine(Run());
    }

    void Update() {
        // TODO: If player presses esc, ask them if they want to quit. Then set hasQuit if true.
    }

    private IEnumerator Run() {
        yield return new WaitUntil(() => !this.introInfoPanel.gameObject.activeSelf);

        while (gs.getNumPeople() > 0 && gs.dayNum <= MAX_DAYS && !hasQuit) {
            int initPeople = gs.resources.people;
            int initSupplies = gs.resources.supplies;

            Event e = Event.CreateRandom();
            if (!(e is NothingEvent)) {
                // Interactive:
                if (e.IsInteractive()) {
                    PresentInteractiveEventPanel((InteractiveEvent) e);
                    yield return new WaitUntil(() => EventPanelIsHidden());
                    // Update res
                    ((InteractiveEvent) e).choiceNum = this.interactiveEventOutcomeChoiceNumber;
                    gs.UpdateResources(e.getOutcomeIfGenerated().resources);
                }
                // Outcome:
                UpdateResourceCountersUI();
                PresentEventOutcomePanel(e);
                yield return new WaitUntil(() => EventPanelIsHidden());
                yield return new WaitForSeconds(1);

                // Show more art appearing if supplies / people changes enough
                if (gs.resources.people != initPeople || gs.resources.supplies != initSupplies) {
                    TryUpdateBackgroundArt();
                }
                yield return new WaitForSeconds(1);
            }

            // Action time:
            List<Action> actions = Actions.CreateRandomActions();
            PresentActionPanel(actions);
            yield return new WaitUntil(() => !this.actionPanel.gameObject.activeSelf);
            // Update res
            Action action = actions[this.actionChoiceNumber];
            gs.UpdateResources(action.outcome.resources);
            UpdateResourceCountersUI();
            PresentActionOutcomePanel(action);
            yield return new WaitUntil(() => !this.actionOutcomePanel.gameObject.activeSelf);
            // yield return new WaitForSeconds(1);

            // Show more art appearing if supplies / people changes enough
            if (gs.resources.people != initPeople || gs.resources.supplies != initSupplies) {
                TryUpdateBackgroundArt();
            }

            this.morningTime = false;
            StartCoroutine(EndDay());
            yield return new WaitUntil(() => this.morningTime);
        }
        
        DisplayEndgamePanel();
    }

    private IEnumerator EndDay() {
        StartCoroutine(FadeScreen());
        yield return new WaitUntil(() => this.screenIsHidden);
        gs.IncrementDayNum();
        UpdateDayCounterUI(this.gs.dayNum);
        StartCoroutine(ShowScreen());
        yield return new WaitUntil(() => !this.screenIsHidden);
        this.morningTime = true;
    }

    private void PresentEventOutcomePanel(Event e) {
        this.eventOutcomePanel.gameObject.SetActive(true);

        Text title = (Text) this.eventOutcomePanel.Find("Title").GetComponent<Text>();
        Text outcome = (Text) this.eventOutcomePanel.Find("Description").GetComponent<Text>();
        if (title != null && outcome != null) {
            title.text = e.name;
            outcome.text = e.getOutcomeIfGenerated().information + "\n" + e.getOutcomeIfGenerated().ResourcesToString();
        }
    }

    private void PresentInteractiveEventPanel(InteractiveEvent e) {
        if (!e.IsInteractive()) {
            Debug.Log("Error: Static event's should only go through outcome presentation");
            return;
        }

        this.interactiveEventPanel.gameObject.SetActive(true);

        Text title = (Text) this.interactiveEventPanel.Find("Title").GetComponent<Text>();
        Text desc = (Text) this.interactiveEventPanel.Find("Description").GetComponent<Text>();
        if (title != null && desc != null) {
            title.text = e.name;
            desc.text = e.description;
        }

        // Populate buttons w/ choices
        for (int i = 0; i < this.interactiveEventChoiceButtons.Length; i++) {
            this.interactiveEventChoiceButtons[i].GetComponentInChildren<Text>().text = e.choices[i];
        }
  
    }

    private void PresentActionPanel(List<Action> actions) {
        this.actionPanel.gameObject.SetActive(true);
        
        // TODO - null check btns
        for (int i = 0; i < actionChoiceButtons.Length; i++) {
            actionChoiceButtons[i].gameObject.GetComponentInChildren<Text>().text = actions[i].description;
        }
    }

    // TODO - access the chosen action in here to populate outcome info
    private void PresentActionOutcomePanel(Action action) {
        this.actionOutcomePanel.gameObject.SetActive(true);

        Text title = (Text) this.actionOutcomePanel.Find("Title").GetComponent<Text>();
        Text desc = (Text) this.actionOutcomePanel.Find("Description").GetComponent<Text>();

        if (title != null && desc != null) {
            title.text = action.description;
            desc.text = action.outcome.ResourcesToString(); // TODO: use action.outcome to populate description
        }
    }

    // UI:
    public void TryUpdateBackgroundArt() {
        if (gs.resources.people > SMALL_PEOPLE_NUM) {
            this.morePeople.gameObject.SetActive(true);
        } else if (gs.resources.people > 0) {
            this.morePeople.gameObject.SetActive(false);
        } else {
            this.lessPeople.gameObject.SetActive(false);
        }

        if (gs.resources.supplies > MEDIUM_SUPPLIES_NUM) {
            this.lessSupplies.gameObject.SetActive(true);
            this.moreSupplies.gameObject.SetActive(true);
        } else if (gs.resources.supplies > SMALL_SUPPLIES_NUM) {
            this.lessSupplies.gameObject.SetActive(true);
            this.moreSupplies.gameObject.SetActive(false);
        } else {
            this.lessSupplies.gameObject.SetActive(false);
            this.moreSupplies.gameObject.SetActive(false);
        }
    }

    private bool EventPanelIsHidden() {
        return !this.interactiveEventPanel.gameObject.activeSelf && 
            !this.eventOutcomePanel.gameObject.activeSelf;
    }

    public void CloseInteractiveEventPanel() {
        this.interactiveEventPanel.gameObject.SetActive(false);
    }

    public void CloseEventOutcomePanel() {
        this.eventOutcomePanel.gameObject.SetActive(false);
    }

    public void CloseIntroInfoPanel() {
        this.introInfoPanel.gameObject.SetActive(false);
    }

    public void CloseActionPanel() {
        this.actionPanel.gameObject.SetActive(false);
    }

    public void CloseActionOutcomePanel() {
        this.actionOutcomePanel.gameObject.SetActive(false);
    }

    public void CloseApplication() {
        Application.Quit();
    }

    public void OnInteractiveEventChoiceButtonClicked(int btnNumber) { // TODO -- link in editor
        this.interactiveEventOutcomeChoiceNumber = btnNumber;
    }

    public void OnActionChoiceClicked(int btnNumber) { // TODO -- link in editor
        this.actionChoiceNumber = btnNumber;
    }

    // PURE UI:
    private void UpdateDayCounterUI(int newDayNum) {
        this.dayNumTextUI.text = "Day " + newDayNum.ToString();
    }

    private void UpdateResourceCountersUI() {
        // TODO: make the text red / green for decreasing / increasing resources
        this.numPeopleTextUI.text = this.gs.getNumPeople().ToString();
        this.defenseTextUI.text = this.gs.resources.defense.ToString();
        this.moraleTextUI.text = this.gs.resources.morale.ToString();
        this.suppliesTextUI.text = this.gs.resources.supplies.ToString();
    }

    IEnumerator FadeScreen() {
        CanvasGroup canvasGroup = this.canvas.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        this.screenIsHidden = true;
        yield return null;
    }

    IEnumerator ShowScreen() {
        CanvasGroup canvasGroup = this.canvas.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += Time.deltaTime * 2;
            yield return null;
        }
        canvasGroup.interactable = true;
        this.screenIsHidden = false;
        yield return null;
    }

    private void DisplayEndgamePanel() {
        this.endGamePanel.gameObject.SetActive(true);
        Text title = this.endGamePanel.Find("Title").GetComponent<Text>();
        Text desc = this.endGamePanel.Find("Description").GetComponent<Text>();
        if (gs.dayNum > MAX_DAYS && gs.resources.people > 0) {
            title.text = "You Survived!";
            desc.text = "Number of survivors: " + gs.resources.people;
        } else {
            title.text = "Game Over!";
            desc.text = "Try again...";
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveEvent : Event {
    // TODO: generate outcomes in each event implementation class
    public List<Outcome> outcomes = null;
    public List<string> choices = null;
    // TODO - link-up buttons to save the choicenum
    public int choiceNum = -1;
    public string description;

    public override Outcome getOutcomeIfGenerated() {
        if (this.choiceNum == -1) {
            Debug.Log("Error: Outcome not yet set");
            return null;
        }
        return outcomes[choiceNum];
    }
}
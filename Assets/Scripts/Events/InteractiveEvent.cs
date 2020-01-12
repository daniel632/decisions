using System.Collections.Generic;

public abstract class InteractiveEvent : Event {
    public List<Outcome> outcomes = null;
    public List<string> choices = null;
    // TODO - link-up buttons to save the choicenum
    public int choiceNum;
}
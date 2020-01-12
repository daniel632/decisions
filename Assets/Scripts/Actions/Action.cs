public class Action {
    // NOTE: Cost and Outcome are separate as the player will see first the cost, and then after 
    // accepting, will see the outcome.
    // public Cost cost;
    public Outcome outcome;
    public string description;

    public Action() {
        this.outcome = new Outcome();
        this.description = "";
    }

    // TODO - implement random information event / action outcome

}
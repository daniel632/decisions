public class StaticEvent : Event {
    // TODO: set outcome in constructors
    public Outcome outcome;

    public override Outcome getOutcomeIfGenerated() {
        return this.outcome;
    }

}
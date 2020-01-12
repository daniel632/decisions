using System.Collections.Generic;

public class BeggarEvent : InteractiveEvent {
    // TODO: link infiltrator
    public BeggarEvent() {
        this.name = "In Need Of Help";
        this.description = "An exhausted wanderer approaches in need of help";
        int rng = rnd.Next(GameController.RNG_LEVEL);
        this.choices = new List<string> {"Give some supplies", "Invite to stay", "Do nothing"};
        this.outcomes = new List<Outcome> {
            new Outcome(new Resources(0, rng, -rng, 0), "The wanderer was given some supplies and sent on their way."),
            new Outcome(new Resources(0, 0, -rng, rng), "The wanderer gladly accepted the offer to stay. There was great division in your settlement that day. Most thought it was the right thing to do, though some feared the wanderer might betray your trust."),
            new Outcome(new Resources(0, -2 * rng, 0, 0), "The wanderer didn't respond. He stood there looking defeated before disappearing into the wilderness. Your settlers were shocked, though some thought it was a smart to conserve resources.")
        };
    }

}
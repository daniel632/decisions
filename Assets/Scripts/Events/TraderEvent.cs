using System;

// Not used for now - Not sure what to trade with. Resources for resources??
public class TraderEvent : InteractiveEvent {

    public TraderEvent() {
        this.name = "A Trader Appears";
        this.description = "'Hello there would you be interested in buying some of my wares";
        int rng = rnd.Next(GameController.RNG_LEVEL);
        this.choices = null;
        this.outcomes = null;
    }

}
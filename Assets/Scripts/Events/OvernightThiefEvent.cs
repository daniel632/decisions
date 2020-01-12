using System;

public class OvernightThiefEvent : StaticEvent {

    public OvernightThiefEvent() {
        this.name = "The Thief";
        int rng = rnd.Next(GameController.RNG_LEVEL);
        this.outcome = new Outcome(new Resources(0, -rng, -2 * rng, 0), "You awake to the sound of a dispute. It seems that someone has stolen supplies. Whether this was an angry settler or someone from the great wilderness, will likely never be known.");
    }

}
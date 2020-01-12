using System.Collections.Generic;

public class AttackEvent : InteractiveEvent {

    private static int DAMAGE_PER_ATTACKER = 5;
    private static int MAX_NUMBER_OF_ATTACKERS = 8;
    public int numberOfAttackers;

    public AttackEvent() {
        this.name = "Under Siege";
        this.description = "A band of raiders appear. Things escalate and they start attacking...";
        this.numberOfAttackers = rnd.Next(GameController.RNG_LEVEL + 1);
        this.choices = new List<string> {"Arm your settlers and fight back", "Cautiously defend", "Do nothing"};
        this.outcomes = new List<Outcome> {
            new Outcome(new Resources(-numberOfAttackers, -numberOfAttackers, -2 * numberOfAttackers, numberOfAttackers), "The raiders were courageously fought off, though many supplies were exhausted."),
            new Outcome(new Resources(-3 * numberOfAttackers, -numberOfAttackers, -3 * numberOfAttackers, 0), "It was a slow victory which caused great damage to your settlement and used many supplies. However, no settlers were lost."),
            new Outcome(new Resources(0, -3 * numberOfAttackers, -3 * numberOfAttackers, -3 * numberOfAttackers), "'What is this' the raiders wondered. They broke into the settlement unopposed, stealing from and killing many of your settlers.")
        };
    }



}
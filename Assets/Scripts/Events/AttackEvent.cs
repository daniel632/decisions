using System;

public class AttackEvent : Event {

    private static int DAMAGE_PER_ATTACKER = 5;
    private static int MAX_NUMBER_OF_ATTACKERS = 8;
    public int numberOfAttackers;

    public AttackEvent() {
        this.interactive = true;
        this.name = "Under Siege";
        this.description = "blah, blah blah blah";
        this.numberOfAttackers = rnd.Next(MAX_NUMBER_OF_ATTACKERS + 1);
    }



}
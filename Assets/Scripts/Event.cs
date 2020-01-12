using System;
using UnityEngine;

public abstract class Event {
    public enum EventType {
        Attack,
        OvernightThief,
        Beggar,
        Nothing
    }

    private static int INCREASE_NOTHING_CHANCE = 1;

    private static EventType[] eventTypes = new[] {
        EventType.Attack, EventType.OvernightThief, EventType.Beggar, EventType.Nothing
    };
    public static System.Random rnd = new System.Random();

    public String name;

    // If an event is interactive, it can have more than one outcome. If static, has at most one outcome (0 for an information based static event)

    public static Event CreateRandom() {
        int val = rnd.Next(eventTypes.Length) + INCREASE_NOTHING_CHANCE;
        int index = val < eventTypes.Length ? val : eventTypes.Length - 1;
        EventType type = eventTypes[index];
        switch(type) {
            // TODO: don't happen before turn 10
            case EventType.Attack:
                return new AttackEvent();
            // TODO: increase chance if morale is low, don't happen before turn 10
            case EventType.OvernightThief:
                return new OvernightThiefEvent();
            case EventType.Beggar:
                return new BeggarEvent();
            default:
                return new NothingEvent();
        }
    }

    public bool IsInteractive() {
        return this is InteractiveEvent;
    }

    public abstract Outcome getOutcomeIfGenerated();
}

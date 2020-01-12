using System;
using UnityEngine;

public abstract class Event {
    public enum EventType {
        Attack,
        Infiltrator,
        OvernightThief,
        Trader,
        Refugee,
        Beggar,
        Nothing
    }

    private static EventType[] eventTypes = new[] {
        EventType.Attack, EventType.Infiltrator, EventType.OvernightThief, EventType.Trader, 
        EventType.Refugee, EventType.Beggar, EventType.Nothing
    };
    public static System.Random rnd = new System.Random();

    public String name;

    // If an event is interactive, it can have more than one outcome. If static, has at most one outcome (0 for an information based static event)

    public static Event CreateRandom() {
        EventType type = eventTypes[rnd.Next(eventTypes.Length)];
        switch(type) {
            // don't happen before turn 10
            case EventType.Attack:
                return new AttackEvent();
            // increase chance if morale is low, don't happen before turn 10
            case EventType.OvernightThief:
                return new OvernightThiefEvent();
            case EventType.Trader:
                return new BeggarEvent();
            case EventType.Nothing:
                return new NothingEvent();
            default:
                Debug.Log("Invalid event type randomly generated");
                return new NothingEvent();
        }
    }

    public bool IsInteractive() {
        return this is InteractiveEvent;
    }

    public abstract Outcome getOutcomeIfGenerated();
}

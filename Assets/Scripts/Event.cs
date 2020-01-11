using System;

public class Event {
    public enum EventType {
        Attack,
        Infiltrator,
        OvernightThief,
        Trader,
        Refugee,
        Beggar,
        Nothing
    }

    private static EventType[] eventTypes = new[] {EventType.Attack, EventType.Infiltrator, 
        EventType.OvernightThief, EventType.Trader, EventType.Refugee, EventType.Beggar};
    public static Random rnd = new Random();
    public EventType type;

    public String name;
    public String description;

    private Event(EventType type) {
        this.type = type;
    }

    public static Event createRandom() {
        return new Event(eventTypes[rnd.Next(eventTypes.Length)]);
    }

}

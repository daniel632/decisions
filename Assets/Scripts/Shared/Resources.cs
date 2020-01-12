public class Resources {
    public int defense;
    public int morale;
    public int supplies;
    // People are less of a direct resource, and is often dependant on the 3 other res. However 
    // sometimes, people will be lost / gained from actions / outcomes.
    public int people; 

    public Resources(int defense, int morale, int supplies, int people) {
        this.defense = defense;
        this.morale = morale;
        this.supplies = supplies;
        this.people = people;
    }
}
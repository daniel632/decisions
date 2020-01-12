public class Outcome {
    public Resources resources;
    public string information;

    public Outcome() {
        
    }

    public Outcome (Resources resources, string information) {
        this.resources = resources;
        this.information = information;
    }

    public string ResourcesToString() {
        return "\nResource Change: \n\nDefense: " + resources.defense + "    Morale: " + resources.morale + "    Supplies: " + resources.supplies + "    People: " + resources.people;
    }
}
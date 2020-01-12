public class DefenseAction : Action {
    public DefenseAction() {
        // TODO - finetune, implement randomness
        // this.cost.res = new Resources(0, 5, 5);
        this.description = "Reinforce Defenses";
        this.outcome.resources = new Resources(10, -5, -5, 0);
    }

}
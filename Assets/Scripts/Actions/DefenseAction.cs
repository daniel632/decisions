public class DefenseAction : Action {
    public DefenseAction() {
        // TODO - finetune, implement randomness
        // this.cost.res = new Resources(0, 5, 5);
        this.description = "Defend";
        this.outcome.res = new Resources(10, -5, -5, 0);
    }

}
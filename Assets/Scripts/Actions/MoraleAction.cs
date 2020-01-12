public class MoraleAction : Action {
    public MoraleAction() {
        // TODO - finetune, implement randomness
        // this.cost.res = new Resources(-5, 5, -5);
        this.description = "Make sure everyone is doing ok";
        this.outcome.res = new Resources(-5, 10, -5);
    }
}
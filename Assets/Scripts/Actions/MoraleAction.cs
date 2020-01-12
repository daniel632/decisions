public class MoraleAction : Action {
    public MoraleAction() {
        // TODO - finetune, implement randomness
        // this.cost.res = new Resources(-5, 5, -5);
        this.description = "Make sure everyone is doing ok";
        this.outcome.resources = new Resources(-5, 10, -5, 0);
    }


    // variants:
    // - good: survivors far and wide hear about how well everyone is treated (lose resources, people gained, etc)
    // - neutral:
    // - bad:   
}
public class SuppliesAction : Action {
    public SuppliesAction() {
        // TODO - finetune, implement randomness
        // this.cost.res = new Resources(-5, 5, -5);
        this.description = "Send a scavenging party out for supplies";
        this.outcome.resources = new Resources(-5, 5, 10, 0);
    }

    // variants:
    // - shitshow - people lost: morale (and people??), and res decrease
    // - successful: res increases, morale increases
    // - moderate: resources expended, mayve someone dies, no real gain nor loss
}
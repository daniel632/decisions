using System.Collections.Generic;
using UnityEngine;

// This class generates n actionable (i.e. they each have defined impact) actions
public class Actions {
    public enum ActionTypes {
        Defense,
        Morale,
        Supplies
    }

    public int NUM_ACTIONS_PER_CALL = 3;
    public static System.Random rnd = new System.Random();

    // NOTE: Creates three random actions (covering each possible type)
    public static List<Action> CreateRandomActions() {
        List<Action> actions = new List<Action>();
        actions.Add(new DefenseAction());
        actions.Add(new MoraleAction());
        actions.Add(new SuppliesAction());
        return Utils.ShuffleList(actions);
    }
}
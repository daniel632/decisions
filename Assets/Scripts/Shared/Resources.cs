public class Resources {
    private int _defense;
    public int defense {
        get { return _defense; }
        set { _defense = value >= 0 ? value : 0; }
    }
    private int _morale;
    public int morale {
        get { return _morale; }
        set { _morale = value >= 0 ? value : 0; }
    }
    private int _supplies;
    public int supplies {
        get { return _supplies; }
        set { _supplies = value >= 0 ? value : 0; }
    }
    // People is less of a direct resource, and is often dependant on the 3 other res. However 
    // sometimes, people will be lost / gained from actions / outcomes.
    private int _people;
    public int people {
        get { return _people; }
        set { _people = value >= 0 ? value : 0; }
    }

    public Resources(int defense, int morale, int supplies, int people) {
        this._defense = defense;
        this._morale = morale;
        this._supplies = supplies;
        this._people = people;
    }
}
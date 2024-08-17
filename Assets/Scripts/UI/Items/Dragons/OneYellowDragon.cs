using UnityEngine;

public class OneYellowDragon : Item
{
    public override string GiveName()
    {
        return "OneYellowDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/YellowDragon");
    }

    public override int GiveStar()
    { return 1; }
    public override int GiveColour() { return 2; }

    public override int GiveConfidence() { return 98; }
}

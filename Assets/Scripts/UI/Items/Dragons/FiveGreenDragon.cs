using UnityEngine;

public class FiveGreenDragon : Item
{
    public override string GiveName()
    {
        return "FiveGreenDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/GreenDragon");
    }

    public override int GiveStar()
    { return 5; }
    public override int GiveColour() { return 1; }

    public override int GiveConfidence() { return 98; }
}

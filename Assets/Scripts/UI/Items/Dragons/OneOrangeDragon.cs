using UnityEngine;

public class OneOrangeDragon : Item
{
    public override string GiveName()
    {
        return "OneOrangeDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/OrangeDragon");
    }

    public override int GiveStar()
    { return 1; }
    public override int GiveColour() { return 3; }

    public override int GiveConfidence() { return 98; }
}

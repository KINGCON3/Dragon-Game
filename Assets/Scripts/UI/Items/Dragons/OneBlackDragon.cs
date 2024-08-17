using UnityEngine;

public class OneBlackDragon : Item
{
    public override string GiveName()
    {
        return "OneBlackDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/BlackDragon");
    }

    public override int GiveStar()
    { return 1; }
    public override int GiveColour() { return 5; }

    public override int GiveConfidence() { return 98; }
}

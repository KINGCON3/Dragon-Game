using UnityEngine;

public class OneWhiteDragon : Item
{
    public override string GiveName()
    {
        return "OneWhiteDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/WhiteDragon");
    }

    public override int GiveStar()
    { return 1; }
    public override int GiveColour() { return 6; }

    public override int GiveConfidence() { return 98; }
}

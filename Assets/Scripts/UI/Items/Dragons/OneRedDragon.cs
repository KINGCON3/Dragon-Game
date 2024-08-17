using UnityEngine;

public class OneRedDragon : Item
{
    public override string GiveName()
    {
        return "OneRedDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/RedDragon");
    }

    public override int GiveStar()
    { return 1; }
    public override int GiveColour() { return 4; }

    public override int GiveConfidence() { return 98; }
}

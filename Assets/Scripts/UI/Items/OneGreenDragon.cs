using UnityEngine;

public class OneGreenDragon : Item
{
    public override string GiveName()
    {
        return "OneGreenDragon";
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/GreenDragon");
    }
}

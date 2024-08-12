using UnityEngine;

public class Egg : Item
{
    public override string GiveName()
    {
        return "Egg";
    }

    public override int MaxStacks()
    {
        return 9999;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/eggfinal");
    }
}

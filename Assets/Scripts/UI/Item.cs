using UnityEngine;

[System.Serializable]

public abstract class Item
{
    public abstract string GiveName();

    public virtual int GiveStar()
    { return 0; }
    public virtual int GiveColour() { return 0; }

    public virtual int GiveConfidence() { return 0; }

    //Colours:
    //Green, yellow, orange, red, black, white
    //1,2,3,4,5,6

    public virtual int MaxStacks()
    {
        return 30;
    }
    public virtual Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/No Item Image Icon.png");
    }

    public virtual GameObject DropObject()
    {
        return Resources.Load<GameObject>("Pickup Items/Default Item");
    }
}

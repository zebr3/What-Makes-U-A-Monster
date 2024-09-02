using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [TextArea(2, 10)]
    public string itemname;
    public int itemid;
    public int combinationID;
    public bool allowCombination = false;
    public Sprite Image;
    [TextArea(3, 10)]
    public string description;
    public bool stackable = false;
    public bool usable = true;
    public bool pickedUp = false;
    

  
   
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New NPCMetaData", menuName = "NPCMetaData")]
public class NPCMetaData: ScriptableObject
{
    public string npcName;
    public int hearts;

    public NPCMetaData(string name, int heartValue)
    {
        npcName = name;
        hearts = heartValue;
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCManager", menuName = "Custom/NPCManager")]
public class NPCManagerAsset : ScriptableObject
{
    private static NPCManagerAsset instance;
    public static NPCManagerAsset Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<NPCManagerAsset>("NPCManager");
                if (instance == null)
                {
                    Debug.LogError("NPCManager asset not found in Resources folder. Create a ScriptableObject asset with the name 'NPCManager' and assign it to the NPCManager field in the Inspector.");
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private List<NPCMetaData> npcDataList = new List<NPCMetaData>();

    private Dictionary<string, NPCMetaData> npcDataDictionary;

    private void OnEnable()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        npcDataDictionary = new Dictionary<string, NPCMetaData>();
        foreach (NPCMetaData npcData in npcDataList)
        {
            if (npcData != null && !npcDataDictionary.ContainsKey(npcData.npcName))
            {
                npcDataDictionary.Add(npcData.npcName, npcData);
            }
        }
    }

    public void SetNPC(string id, NPCMetaData npcData)
    {
        if (npcDataDictionary.ContainsKey(id))
        {
            npcDataDictionary[id] = npcData;
        }
        else
        {
            Debug.LogWarning("NPC with ID '" + id + "' does not exist in NPCManager.");
        }
    }

    public NPCMetaData GetNPC(string id)
    {
        if (npcDataDictionary.TryGetValue(id, out NPCMetaData npcData))
        {
            return npcData;
        }

        Debug.LogWarning("NPC with ID '" + id + "' does not exist in NPCManager.");
        return null;
    }
}

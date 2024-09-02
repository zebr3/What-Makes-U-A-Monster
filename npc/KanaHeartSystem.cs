using UnityEngine;
using UnityEngine.UI;

public class KanaHeartSystem : MonoBehaviour
{
    public Image[] hearts;
    public Image[] secondHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite evilHeart;

    public NPCManagerAsset npcManager;
    public string npcId = "Kana";

    private NPCMetaData npcData;

    private void Start()
    {
        npcData = npcManager.GetNPC(npcId);
    }

    private void Update()
    {
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        npcData = npcManager.GetNPC(npcId);

        if (npcData != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < Mathf.Abs(npcData.hearts))
                {
                    if (npcData.hearts >= 0)
                    {
                        hearts[i].sprite = fullHeart;
                    }
                    else
                    {
                        hearts[i].sprite = evilHeart;
                    }
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }

        if (npcData != null)
        {
            for (int l = 0; l < secondHearts.Length; l++)
            {
                if (l < Mathf.Abs(npcData.hearts))
                {
                    if (npcData.hearts >= 0)
                    {
                        secondHearts[l].sprite = fullHeart;
                    }
                    else
                    {
                        secondHearts[l].sprite = evilHeart;
                    }
                }
                else
                {
                    secondHearts[l].sprite = emptyHeart;
                }
            }
        }
    }

    public void AddHeartPoints()
    {
        npcData = npcManager.GetNPC(npcId);

        if (npcData != null)
        {
            if (npcData.hearts < 5)
            {
                npcData.hearts++;
                npcManager.SetNPC(npcId, npcData);
            }
        }
    }

    public void SubtractHeartPoints()
    {
        npcData = npcManager.GetNPC(npcId);

        if (npcData != null)
        {
            if (npcData.hearts > -5)
            {
                npcData.hearts--;
                npcManager.SetNPC(npcId, npcData);
            }
        }
    }
}

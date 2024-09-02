using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    public float soundDelay;

    private bool canPlaySound = true; // Flag to control sound playback

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != button)
        {
            selectedTab = button;

            if (canPlaySound)
            {
                AudioManager.instance.PlaySFX(button.soundName); // Play the button's sound
                StartCoroutine(ResetSoundDelay());
            }

            ResetTabs();
            button.background.sprite = tabActive;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }
    }

    private IEnumerator ResetSoundDelay()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(soundDelay); // Adjust the delay duration as needed
        canPlaySound = true;
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }
}

using UnityEngine;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
    public TextAsset inkJsonAsset;
    private Story inkStory;

    private void Start()
    {
        inkStory = new Story(inkJsonAsset.text);
    }

    public void CallInkFunction(string functionName)
    {
        inkStory.CallExternalFunction(functionName, 1);
    }
}

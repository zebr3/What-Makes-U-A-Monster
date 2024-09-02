using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisuelCue : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject cue;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        cue.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cue.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

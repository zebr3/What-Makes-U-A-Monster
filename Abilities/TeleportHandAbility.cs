using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandAbility : MonoBehaviour
{
    [HideInInspector] public bool abilitySelected = true;
    [SerializeField] GameObject handPrefab;
    bool allowAbility = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(abilitySelected || Input.GetMouseButtonDown(1) && allowAbility && GameManager.instance.allowAbility)
        {
            StartCoroutine(Activate());
            allowAbility = false;
        }
    }

    public IEnumerator Activate()
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(handPrefab, targetPosition, Quaternion.identity);
        yield return new WaitForSeconds(2);
        allowAbility = true;
    }
}

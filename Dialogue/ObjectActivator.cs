using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    public static ObjectActivator instance;
    void Awake()
    {
        instance = this;

    }

    // Method to activate an object by its name
    public void ActivateObject(string objectName)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj.name.Equals(objectName))
            {
                obj.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("Object with name '" + objectName + "' not found in the array.");
    }

    public void DeactivateObject(string objectName)
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj.name.Equals(objectName))
            {
                obj.SetActive(false);
                return;
            }
        }

        Debug.LogWarning("Object with name '" + objectName + "' not found in the array.");
    }
}

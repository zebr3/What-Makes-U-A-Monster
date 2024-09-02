using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private static CanvasScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour
{
    public string musicToPlay;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(musicToPlay);
    }

    // Update is called once per frame
    void Update()
    {

    }

}

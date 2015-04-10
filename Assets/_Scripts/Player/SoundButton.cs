using UnityEngine;
using System.Collections;

public class SoundButton : MonoBehaviour
{

    public void PlaySound()
    {
        if (Config.isSound_On)
            audio.Play();
    }
}

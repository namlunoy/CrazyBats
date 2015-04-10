using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy musicGame = null;
    void Awake()
    {
        if (musicGame != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            musicGame = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

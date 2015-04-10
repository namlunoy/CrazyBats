using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject loadingPanel;
    public GameObject aboutPanel;
    public GameObject quitPanel;
    public Button btSound;
    public Button btMusic;
    public Sprite music_On;
    public Sprite music_Off;
    public Sprite sound_On;
    public Sprite sound_Off;

    private GameObject MusicGameObject;
    void Start()
    {
        print(PlayerController.Instance.HightScore.ToString());
        loadingPanel.SetActive(false);

        AdsController.Instance.Show_Billboard();
        
        AdsController.Instance.Show_Banner(true);
        
        MusicGameObject = GameObject.FindGameObjectWithTag("MUSIC");
        UpdateGUI();
    }
    void UpdateGUI()
    {
        if (Config.isMusic_On)
        {
            btMusic.image.sprite = music_On;
            MusicGameObject.audio.volume = 1;
        }
        else
        {
            btMusic.image.sprite = music_Off;
            MusicGameObject.audio.volume = 0;
        }

        if (Config.isSound_On)
        {
            btSound.image.sprite = sound_On;
            audio.volume = 1;
        }
        else
        {
            btSound.image.sprite = sound_Off;
            audio.volume = 0;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ClickExit();
    }

    public void ClickStart()
    {
        loadingPanel.SetActive(true);
        audio.Play();
        Application.LoadLevel("MainScene");
    }

    public void ClickLeaderBoard() { audio.Play(); Application.LoadLevel("LeaderBoard"); }

    public void ClickStore() { audio.Play(); Application.LoadLevel("Store"); }

    public void ClickExit()
    {
        audio.Play();
        quitPanel.SetActive(true);
    }

    public void ClickQuit_Yes()
    {
        audio.Play();
        Application.Quit();
    }

    public void ClickQuit_No()
    {
        audio.Play();
        quitPanel.SetActive(false);
    }

    public void ClickRate()
    {
        audio.Play();
        //if (Application.platform == RuntimePlatform.Android)
        //    Application.OpenURL("https://play.google.com/store/apps/details?id=com.mgamestudio.evilbats");
        //else 
        Application.OpenURL("https://itunes.apple.com/us/app/ninja-hero-the-super-battle/id952186432");
    }

    public void ClickAbout()
    {
        audio.Play();
        if (aboutPanel.activeSelf)
            aboutPanel.SetActive(false);
        else aboutPanel.SetActive(true);
    }

    public void ClickMusic()
    {
        if (Config.isMusic_On)
        {
            MusicGameObject.audio.volume = 0;
            Config.isMusic_On = false;
            btMusic.image.sprite = music_Off;
        }
        else
        {
            MusicGameObject.audio.volume = 1;
            Config.isMusic_On = true;
            btMusic.image.sprite = music_On;
        }
    }

    public void ClickSound()
    {
        if (Config.isSound_On)
        {
            Config.isSound_On = false;
            btSound.image.sprite = sound_Off;
            audio.volume = 0;
        }
        else
        {
            audio.volume = 1;
            Config.isSound_On = true;
            btSound.image.sprite = sound_On;
        }
    }

}
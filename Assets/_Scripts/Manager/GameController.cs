using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 * - Quản lý các thành phần giao diện chung.
 *  + Nhấn nút pause
 *  + Nhấn nút back
 */
public class GameController : MonoBehaviour
{
    public Text txtName;
    public Transform centerPosition;
    public static Vector3 CoinUiPosition;
    public RectTransform menuPanel;
    public RectTransform pausePanel;
    public RectTransform SubmitPanel;
    public Vector3 pause_0, menu_0, submit_0;

    public AudioClip nhacNen;
    public AudioClip nhacOver;




    public Text txt_coin;
    public Text txt_bat;
    public Button bt_Sound;
    public Button bt_Music;

    public Sprite Music_On;
    public Sprite Music_Off;
    public Sprite Sound_On;
    public Sprite Sound_Off;

    private AdsController ads;

    public static bool IsAlive { get { return PlayerController.Instance.IsAlive; } }

    public void Click_Sound()
    {
        Config.isSound_On = Config.isSound_On ? false : true;
    }

    public void Click_Music()
    {
        Config.isMusic_On = Config.isMusic_On ? false : true;
    }

    // Use this for initialization
    void Start()
    {

        ads = AdsController.Instance;

        GameObject music = GameObject.FindGameObjectWithTag("MUSIC");
        if (music != null)
            Destroy(music);





        audio.clip = nhacNen;
        audio.loop = true;
        audio.Play();
        Time.timeScale = 1;
        pause_0 = pausePanel.position;
        menu_0 = menuPanel.position;
        submit_0 = SubmitPanel.position;
        GameObject g = GameObject.FindGameObjectWithTag("COIN_UI");
        if (g == null)
            print("null");
        CoinUiPosition = Camera.main.ScreenToWorldPoint(g.transform.position);

        ads.Show_Banner(false);
    }

    // Update is called once per frame
    void Update()
    {
        bt_Music.image.sprite = Config.isMusic_On ? Music_On : Music_Off;
        bt_Sound.image.sprite = Config.isSound_On ? Sound_On : Sound_Off;
        audio.volume = Config.isMusic_On ? 1 : 0;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickPause();
        }
    }


    #region Các sự kiện trong game
    public void GameOver()
    {

        ads.Show_Banner(true);
        audio.clip = nhacOver;
        audio.loop = false;
        audio.Play();
        Vector3 p = Vector3.zero;
        iTween.MoveTo(menuPanel.gameObject, iTween.Hash("position", centerPosition.position, "time", 2, "easetype", iTween.EaseType.linear));

        //Time.timeScale = 0;
        txt_bat.text = PlayerController.Instance.CurrentScore.ToString();
        txt_coin.text = PlayerController.Instance.CurrentCoin.ToString();

        //Dừng các hoạt động lại
        GameObject[] bats = GameObject.FindGameObjectsWithTag("BAT");
        GameObject batGen = GameObject.FindGameObjectWithTag("GEN");
        GameObject[] boss = GameObject.FindGameObjectsWithTag("BOSS");
        if (bats != null)
            foreach (GameObject g in bats)
                g.SetActive(false);
        if (batGen != null)
            batGen.SetActive(false);

        if (boss != null)
            foreach (GameObject b in boss)
                b.SetActive(false);


        print("gameover...");
        print("name = " + PlayerController.Instance.UserName);
#if UNITY_METRO
#else
        if (PlayerController.Instance.UserName.Length < 1)
            iTween.MoveTo(SubmitPanel.gameObject, iTween.Hash("position", centerPosition.position));
#endif
    }

    #endregion


    #region Sự kiện tương tác với người dùng

    public void Click_SetTen()
    {
        PlayerController.Instance.UserName = txtName.text;
        iTween.MoveTo(SubmitPanel.gameObject, iTween.Hash("position", submit_0));
        MyLeaderBoard b = new MyLeaderBoard();

        if (PlayerController.Instance.UserName.Length > 0)
            b.SaveScore(PlayerController.Instance.UserName, PlayerController.Instance.CurrentScore);
    }

    public void Click_Replay()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);

        ads.Show_Banner(false);
    }

    public void Click_Menu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }

    public void ClickShare()
    {
        Time.timeScale = 1;
        string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=https://play.google.com/store/apps/details?id=com.mgamestudio.evilbats";
        if (Application.platform != RuntimePlatform.Android)
            facebookshare = "https://www.facebook.com/sharer/sharer.php?u=http://www.windowsphone.com/en-us/store/app/evil-bats/cf9148b7-e348-44a9-b156-fb424c73dff9";
        Application.OpenURL(facebookshare);
    }

    public void ClickPause()
    {

        ads.Show_Banner(true);
        audio.Pause();
        pausePanel.position = centerPosition.position;
        Time.timeScale = 0;

    }

    public void ClickResume()
    {
        Time.timeScale = 1;
        audio.Play();
        pausePanel.position = pause_0;

        ads.Show_Banner(false);
    }

    #endregion

}

using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine.UI;

public class Mlisitview : MonoBehaviour
{
    public GameObject item;
    public RectTransform rect;
    RectTransform r;
    MyLeaderBoard leaderboard;
    public Text txtThongBao;
    public RectTransform loadingPanel;
    public RectTransform othePosition;
    public Text username;
    public Text score;

    int count = 50;
    public float width;
    public float heigh;

    public GameObject imageQuay;

    public void ClickBack()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
        audio.Play();
        Application.LoadLevel(0);
    }

    void Start()
    {
        leaderboard = new MyLeaderBoard();
        rect = GetComponent<RectTransform>();
        r = item.GetComponent<RectTransform>();

        width = r.offsetMax.x - r.offsetMin.x;
        heigh = r.offsetMax.y - r.offsetMin.y;

        username.text = PlayerController.Instance.UserName;
        score.text = PlayerController.Instance.HightScore.ToString();

        leaderboard.GetLeaderBoard(count, new GetLeaderboardCallback(this));
    }
}



public class GetLeaderboardCallback : App42CallBack
{
    Mlisitview t;
    public GetLeaderboardCallback(Mlisitview t)
    {
        this.t = t;
    }
    public void OnSuccess(object response)
    {
        Game game = (Game)response;
        App42Log.Console("gameName is " + game.GetName());

        t.rect.offsetMax = new Vector2(t.width, 0);
        t.rect.offsetMin = new Vector2(0, -t.heigh * game.GetScoreList().Count);

        System.Collections.Generic.IList<Game.Score> list = game.GetScoreList();
        for (int i = 0; i < list.Count; i++)
        {
            MonoBehaviour.print("index__" + i);
            Game.Score score = list[i];
            App42Log.Console("userName is : " + score.GetUserName());
            App42Log.Console("score is : " + score.GetValue());

            GameObject g = (GameObject)MonoBehaviour.Instantiate(t.item, t.rect.position, Quaternion.identity);
           
            g.transform.parent = t.rect.transform;
            RectTransform R = g.GetComponent<RectTransform>();
            R.offsetMax = new Vector2(t.width, -t.heigh * i);
            R.offsetMin = new Vector2(0, -t.heigh * (i + 1));
            R.localScale = new Vector3(1, 1, 1);
            R.GetChild(0).GetComponent<Text>().text = score.GetUserName();
            R.GetChild(1).GetComponent<Text>().text = score.GetValue().ToString();
        }


        //for (int i = 0; i < game.GetScoreList().Count; i++)
        //{
        //    Game.Score score = game.GetScoreList()[i];
        //    App42Log.Console("userName is : " + score.GetUserName());
        //    App42Log.Console("score is : " + score.GetValue());

        //    GameObject g = (GameObject)Instantiate(t.item, t.rect.position, Quaternion.identity);
        //    g.transform.parent = t.rect.transform;
        //    RectTransform R = g.GetComponent<RectTransform>();
        //    R.offsetMax = new Vector2(t.width, -t.heigh * i);
        //    R.offsetMin = new Vector2(0, -t.heigh * (i + 1));

        //    R.GetChild(0).GetComponent<Text>().text = score.GetUserName();
        //    R.GetChild(1).GetComponent<Text>().text = score.GetValue().ToString();
        //}

        iTween.MoveTo(t.loadingPanel.gameObject, iTween.Hash("position", t.othePosition.position));
    }

    public void OnException(System.Exception ex)
    {
        t.imageQuay.GetComponent<Rotate>().enabled = false;
        t.txtThongBao.text = "ERROR!";
    }

}

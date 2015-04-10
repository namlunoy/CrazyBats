using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

//Level
public class MyLeaderBoard
{

    private const string API_KEY = "2d4dfa4b3add90b8ac1bcf1540f53895811bc07c8afc6833e06af458bb512f64";
    private const string SECRET_KEY = "3a296b9be61e021af1efb18de756c7f25a60f67cfc0485848bf938e9f71952a8";
    private const string GAME_NAME = "CrazyBats";
    ScoreBoardService scoreBoardService;

    public MyLeaderBoard()
    {
        App42Log.SetDebug(true);
        App42API.Initialize(API_KEY, SECRET_KEY);
        scoreBoardService = App42API.BuildScoreBoardService();
    }

    public void SaveScore(string username,int score)
    {
        scoreBoardService.SaveUserScore(GAME_NAME, username, score, new SaveCallback());
    }

    public void GetLeaderBoard(int count, App42CallBack callback)
    {
        if (scoreBoardService == null)
            scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetTopNRankers(GAME_NAME, count, callback);
    }

    public class SaveCallback : App42CallBack
    {
        public void OnSuccess(object response)
        {
            MonoBehaviour.print("Update điểm thành công!");
        }
        public void OnException(System.Exception e)
        {
            MonoBehaviour.print("Update điểm thất bại!");
        }
    }


}

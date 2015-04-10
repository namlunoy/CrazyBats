
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class AdsController : MonoBehaviour
{
    private static AdsController _instance = null;
    private RuntimePlatform platform;

    //Google
    BannerView bannerView;
    InterstitialAd interstitial;
    private string admob_banner_id = "ca-app-pub-5094882700262976/6934546248";
    private string admob_billboard_id = "ca-app-pub-5094882700262976/8411279447";

    public event Action<bool> ShowAdsWP;

    void Start()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this.gameObject);
    }

    public static AdsController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AdsController();
            return _instance;
        }
    }
    private AdsController()
    {
        platform = Application.platform;
        //if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        //{
        bannerView = new BannerView(admob_banner_id, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        interstitial = new InterstitialAd(admob_billboard_id);
        AdRequest request2 = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        //}

    }


    public void Show_Banner(bool isShow)
    {
        //if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        //{
        if (isShow)
            bannerView.Show();
        else bannerView.Hide();
        //}
        //else
        //{
        //    if (ShowAdsWP != null)
        //        ShowAdsWP(isShow);
        //}
    }

    public void Show_Billboard()
    {
        //if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        //{
        if (interstitial.IsLoaded())
            interstitial.Show();
        //}
    }
}


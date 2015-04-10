using UnityEngine;
using System.Collections;
using VservPlugin;
using System.Collections.Generic;

public class VserSampleUnityScript : MonoBehaviour {

	public VservPlugin.VservWP8Plugin pluginVservAd;

	public GUIStyle myLabelStyle;
	public GUIStyle myButtonStyle;
	public GUIStyle myToggleStyle;
	public GUIStyle myTextfieldStyle;

	//This is a zoneId for a sample banner.
	public string zoneId = "20846";

	public VservWP8Plugin.AdUxType type;

	private bool toggleBool1 = true;
	
	private bool toggleBool2 = false;

	public string eventString = "No Events";
	
	// Use this for initialization
	void Start () {
		pluginVservAd = new VservWP8Plugin();
	}
	
	void InitializeVservAd(string zoneId, VservWP8Plugin.AdUxType type)
	{
		pluginVservAd.SetZoneId (zoneId);
		pluginVservAd.SetAdUXType (type);

		//This api is of use in case of billboard ad requests 
		//to get the ad in the required orientation.
		pluginVservAd.SetRequestedOrientation (VservWP8Plugin.Orientation.Potrait);

		pluginVservAd.DidCacheAd -= HandleAdViewDidCacheAd;
		pluginVservAd.FailedToLoadAd -= HandleAdViewFailedToLoadAd;
		pluginVservAd.DidLoadAd -= HandleAdViewDidLoadAd;
		pluginVservAd.WillDismissOverlay -= HandleWillDismissOverlay;
		pluginVservAd.WillPresentOverlay -= HandleWillPresentOverlay;
		pluginVservAd.WillLeaveApp -= HandleWillLeaveApp;
		pluginVservAd.DidInteractWithAd -= HandleDidInteractWithAd;
		pluginVservAd.LoggingEvent -= HandleLoggingEvent;
		pluginVservAd.FailedToCacheAd -= HandleDidFailedToCacheAd;
				
		pluginVservAd.DidCacheAd += HandleAdViewDidCacheAd;
		pluginVservAd.FailedToLoadAd += HandleAdViewFailedToLoadAd;
		pluginVservAd.DidLoadAd += HandleAdViewDidLoadAd;
		pluginVservAd.WillDismissOverlay += HandleWillDismissOverlay;
		pluginVservAd.WillPresentOverlay += HandleWillPresentOverlay;
		pluginVservAd.WillLeaveApp += HandleWillLeaveApp;
		pluginVservAd.DidInteractWithAd += HandleDidInteractWithAd;
		pluginVservAd.LoggingEvent += HandleLoggingEvent;
		pluginVservAd.FailedToCacheAd += HandleDidFailedToCacheAd;
	}

	void HandleDidFailedToCacheAd (object sender, System.EventArgs e)
	{
		eventString = "DidloadAd";
	}

	void HandleAdViewDidLoadAd (object sender, System.EventArgs e)
	{
		eventString = "DidloadAd";
	}
	
	void HandleAdViewFailedToLoadAd (object sender, System.EventArgs e)
	{
		eventString = "FailedToLoadAd";
	}
	
	void HandleAdViewDidCacheAd (object sender, System.EventArgs e)
	{
		eventString = "DidCacheAd";
	}
	
	void HandleLoggingEvent (object sender, System.EventArgs e)
	{
		eventString = "LoggingEvent";
	}
	
	void HandleDidInteractWithAd (object sender, System.EventArgs e)
	{
		eventString = "DidInteractWithAd";
	}
	
	void HandleWillLeaveApp (object sender, System.EventArgs e)
	{
		eventString = "WillLeaveApp";
	}

	void HandleWillPresentOverlay (object sender, System.EventArgs e)
	{
		eventString = "WillPresentOverlay";
	}
	
	void HandleWillDismissOverlay (object sender, System.EventArgs e)
	{
		eventString = "WillDismissOverlay";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	void OnGUI(){

		myLabelStyle = new GUIStyle (GUI.skin.label);
		myLabelStyle.fontSize = 30;

		myButtonStyle = new GUIStyle (GUI.skin.button);
		myButtonStyle.fontSize = 30;

		myToggleStyle = new GUIStyle (GUI.skin.toggle);
		myToggleStyle.fontSize = 30;

		myTextfieldStyle = new GUIStyle (GUI.skin.textField);
		myTextfieldStyle.fontSize = 30;

		toggleBool1 = GUI.Toggle (new Rect (25, 25, 150, 50), toggleBool1, "Banner",myButtonStyle);
		if (toggleBool1) {
			toggleBool2 = false;
			type = VservWP8Plugin.AdUxType.Banner;
		}
		
		toggleBool2 = GUI.Toggle (new Rect (250, 25, 150, 50), toggleBool2, "Billboard",myButtonStyle);
		if (toggleBool2) {
			toggleBool1 = false;
			type = VservWP8Plugin.AdUxType.Interstitial;
		}

		GUI.Label(new Rect(25, 125, 250, 100),"Enter ZoneId :",myLabelStyle);
		zoneId = GUI.TextField (new Rect (250, 125, 200, 50), zoneId, myTextfieldStyle);
			
		if (GUI.Button(new Rect(10, 210, Screen.width - 20, 50), "Load Ad",myButtonStyle))
		{
			InitializeVservAd(zoneId,type);
			pluginVservAd.SetTimeOut(50);

			if(type == VservWP8Plugin.AdUxType.Banner)
			{
				pluginVservAd.SetAdPosition(8, 80);
			}

			pluginVservAd.SetEnableRefresh(true);
			pluginVservAd.SetRefreshRate(30);

			pluginVservAd.LoadAd();
		}
		
		GUI.Label(new Rect(10, 270, Screen.width, 100),"Either use load ad or use a combination of cache and show ads with refresh disabled.",myLabelStyle);
		
		if (GUI.Button(new Rect(10, 375, Screen.width - 20, 50), "Cache Ad",myButtonStyle))
		{
			InitializeVservAd(zoneId,type);

			//Set the refresh flag to false for caching.
			pluginVservAd.SetEnableRefresh(false);
			pluginVservAd.SetTimeOut(50);
			pluginVservAd.CacheAd();
		}
		
		if (GUI.Button(new Rect(10, 450, Screen.width - 20, 50), "Show Ad",myButtonStyle))
		{
			if(type == VservWP8Plugin.AdUxType.Banner)
			{
				pluginVservAd.SetAdPosition(8, 80);
			}
			pluginVservAd.ShowAd();
		}
	}
}

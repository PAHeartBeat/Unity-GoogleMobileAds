using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
// Orignal code writen by Google Mobile Ad SDK Team,
// Modified By: Ankur Ranpariya aka PAHeartBeat on 28th Jan 2015

//Here I added few test adUnitIds for iOS Test
// ca-app-pub-6569756320106809/4839131600	Banner Ad iOS
//
// ca-app-pub-6569756320106809/7792598009	Interstitial Ad - Text
// ca-app-pub-6569756320106809/9269331204	Interstitial Ad - Image
// ca-app-pub-6569756320106809/1746064404	Interstitial Ad - Video

public class GoogleMobileAdsScript : MonoBehaviour {
	private Dictionary<string,BannerView> bannerViews = new Dictionary<string, BannerView>();
	private Dictionary<string,InterstitialAd> interstitialAdViews = new Dictionary<string, InterstitialAd>();

	private BannerView banner;
	private InterstitialAd interstitial;
	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		name = "Google Ad Manager";
	}
	void OnDestroy() {
		foreach(KeyValuePair<string,BannerView> kvp in bannerViews) {
			if(kvp.Value != null) {
				kvp.Value.Destroy();
			}
		}
		foreach(KeyValuePair<string,InterstitialAd> kvp in interstitialAdViews) {
			if(kvp.Value != null) {
				kvp.Value.Destroy();
			}
		}
	
	}

	void OnGUI() {
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.04f * Screen.height);

		Rect requestBannerRect = new Rect(0.1f * Screen.width, 0.05f * Screen.height,
			                         0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestBannerRect, "Request Banner")) {
			RequestBanner("ca-app-pub-6569756320106809/4839131600");
		}

		Rect showBannerRect = new Rect(0.1f * Screen.width, 0.15f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showBannerRect, "Show Banner")) {
			banner.Show();
		}

		Rect hideBannerRect = new Rect(0.1f * Screen.width, 0.25f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(hideBannerRect, "Hide Banner")) {
			banner.Hide();
		}



		Rect requestInterstitialRect = new Rect(0.1f * Screen.width, 0.40f * Screen.height,
			                               0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect, "Request Interstitial 1 - Text")) {
			RequestInterstitial("ca-app-pub-6569756320106809/7792598009");
		}

		Rect showInterstitialRect = new Rect(0.1f * Screen.width, 0.49f * Screen.height,
			                            0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect, "Show Interstitial 1 - Text")) {
			ShowInterstitial("ca-app-pub-6569756320106809/7792598009");
		}

		Rect requestInterstitialRect1 = new Rect(0.1f * Screen.width, 0.58f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect1, "Request Interstitial 2 - Image")) {
			RequestInterstitial("ca-app-pub-6569756320106809/9269331204");
		}

		Rect showInterstitialRect1 = new Rect(0.1f * Screen.width, 0.67f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect1, "Show Interstitial 2 - Image")) {
			ShowInterstitial("ca-app-pub-6569756320106809/9269331204");
		}

		Rect requestInterstitialRect2 = new Rect(0.1f * Screen.width, 0.76f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect2, "Request Interstitial 2 - Video")) {
			RequestInterstitial("ca-app-pub-6569756320106809/1746064404");
		}

		Rect showInterstitialRect2 = new Rect(0.1f * Screen.width, 0.85f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect2, "Show Interstitial 2 - Video")) {
			ShowInterstitial("ca-app-pub-6569756320106809/1746064404");
		}
	}


	public void RequestBanner(string adUnitId) {
		// Create a 320x50 banner at the top of the screen.
		if(bannerViews.ContainsKey(adUnitId)) {
			banner = bannerViews[adUnitId];
		} else {
			banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

			// Register for ad events.
			banner.AdLoaded += HandleAdLoaded;
			banner.AdFailedToLoad += HandleAdFailedToLoad;
			banner.AdOpened += HandleAdOpened;
			banner.AdClosing += HandleAdClosing;
			banner.AdClosed += HandleAdClosed;
			banner.AdLeftApplication += HandleAdLeftApplication;

			bannerViews.Add(adUnitId, banner);
		}
		// Load a banner ad.
		banner.LoadAd(createAdRequest());
	}
	public void RequestInterstitial(string adUnitId) {
		// Create an interstitial.

		if(bannerViews.ContainsKey(adUnitId)) {
			interstitial = interstitialAdViews[adUnitId];
		} else {
			interstitial = new InterstitialAd(adUnitId);

			// Register for ad events.
			interstitial.AdLoaded += HandleInterstitialLoaded;
			interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
			interstitial.AdOpened += HandleInterstitialOpened;
			interstitial.AdClosing += HandleInterstitialClosing;
			interstitial.AdClosed += HandleInterstitialClosed;
			interstitial.AdLeftApplication += HandleInterstitialLeftApplication;

			interstitialAdViews.Add(adUnitId, interstitial);
		}
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}

	// Returns an ad request with custom ad targeting.
	AdRequest ar;
	private AdRequest createAdRequest() {
		//.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
		//.AddExtra("color_bg", "9B30FF")
		if(ar == null) {
			ar = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddKeyword("game")
			.SetGender(Gender.Male)
			.SetBirthday(new DateTime(1985, 1, 1))
			.TagForChildDirectedTreatment(false)
			.Build();
		}
		return ar;
	}
	private void ShowInterstitial(string adUnitId) {
		if(interstitialAdViews.ContainsKey(adUnitId)) {
			interstitial = interstitialAdViews[adUnitId];
			if(interstitial.IsLoaded()) {
				interstitial.Show();
				//interstitial.LoadAd(createAdRequest());
			} else {
				Debug.Log("Interstitial is not ready yet.");
			}
		} else {
			Debug.Log("Interstitial is not requested yet.");
			RequestInterstitial(adUnitId);
		}
	}

	#region Banner callback handlers

	public void HandleAdLoaded(object sender, EventArgs args) {
		Debug.Log("HandleAdLoaded event received. " + args.ToString());
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args) {
		Debug.Log("HandleAdOpened event received " + args.ToString());
	}

	void HandleAdClosing(object sender, EventArgs args) {
		Debug.Log("HandleAdClosing event received " + args.ToString());
	}

	public void HandleAdClosed(object sender, EventArgs args) {
		Debug.Log("HandleAdClosed event received " + args.ToString());
	}

	public void HandleAdLeftApplication(object sender, EventArgs args) {
		Debug.Log("HandleAdLeftApplication event received " + args.ToString());
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded(object sender, EventArgs args) {
		Debug.Log("HandleInterstitialLoaded event received. " + args.ToString());
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		Debug.Log("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args) {
		Debug.Log("HandleInterstitialOpened event received " + args.ToString());
	}

	void HandleInterstitialClosing(object sender, EventArgs args) {
		Debug.Log("HandleInterstitialClosing event received " + args.ToString());
	}

	public void HandleInterstitialClosed(object sender, EventArgs args) {
		Debug.Log("HandleInterstitialClosed event received " + args.ToString());
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args) {
		Debug.Log("HandleInterstitialLeftApplication event received " + args.ToString());
	}

	#endregion
}

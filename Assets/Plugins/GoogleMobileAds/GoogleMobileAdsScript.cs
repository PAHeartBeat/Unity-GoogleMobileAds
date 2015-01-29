using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

/* Example script showing how to invoke the Google Mobile Ads Unity plugin.
 * Orignal code writen by Google Mobile Ad SDK Team, 
 * 
 * Modified By: Ankur Ranpariya aka PAHeartBeat on 28th Jan 2015
 * Last Modified By: Ankur Ranpariya aka PAHeartBeat on 29th Jan 2015
 * 
*/

//Here I added few test adUnitIds for iOS Test
// ca-app-pub-6569756320106809/4839131600	Banner Ad - Text / Image
//
// ca-app-pub-6569756320106809/7792598009	Interstitial Ad - Text
// ca-app-pub-6569756320106809/9269331204	Interstitial Ad - Image
// ca-app-pub-6569756320106809/1746064404	Interstitial Ad - Video

//Here I added few test adUnitIds for Android Test
// ca-app-pub-6569756320106809/4263722000	Banner Ad - Text / Image
//
// ca-app-pub-6569756320106809/7217188402	Interstitial Ad - Text
// ca-app-pub-6569756320106809/5740455203	Interstitial Ad - Image
// ca-app-pub-6569756320106809/8693921603	Interstitial Ad - Video

public class GoogleMobileAdsScript : MonoBehaviour {
	#region Singleton settings

	private static GoogleMobileAdsScript inst;
	public static GoogleMobileAdsScript Me {
		private set{ inst = value; }
		get { 
			if(inst == null) {
				inst = new GameObject().AddComponent<GoogleMobileAdsScript>();
			}
			return inst;
		}
	}

	#endregion

	#region Events

	public static Action<string> BannerAdLoaded;
	public static Action<string,string> BannerAdFailedToLoad;
	public static Action<string> BannerAdOpened;
	public static Action<string> BannerAdClosing;
	public static Action<string> BannerAdClosed;
	public static Action<string> BannerAdLeftApplication;

	public static Action<string> InterstitialLoaded;
	public static Action<string,string> InterstitialFailedToLoad;
	public static Action<string> InterstitialOpened;
	public static Action<string> InterstitialClosing;
	public static Action<string> InterstitialClosed;
	public static Action<string> InterstitialLeftApplication;

	#endregion

	#region Internal fields

	private Dictionary<string,BannerView> bannerViews = new Dictionary<string, BannerView>();
	private Dictionary<string,InterstitialAd> interstitialAdViews = new Dictionary<string, InterstitialAd>();

	private BannerView banner;
	private InterstitialAd interstitial;
	private string interstitialAdId = string.Empty;

	#endregion

	#region Mono Actions

	void Awake() {
		inst = this;
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
		#if TESTMODE_GOOGLEADS
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.03f * Screen.height);
		Rect showBannerRect = new Rect(0.1f * Screen.width, 0.15f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showBannerRect, "Show Banner")) {
			#if UNITY_IOS
			ShowBanner("ca-app-pub-6569756320106809/4839131600");
			#elif UNITY_ANDROID
			ShowBanner("ca-app-pub-6569756320106809/4263722000");
			#endif
		}

		Rect hideBannerRect = new Rect(0.1f * Screen.width, 0.25f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(hideBannerRect, "Hide Banner")) {
			HideBanner();
		}



		Rect requestInterstitialRect = new Rect(0.1f * Screen.width, 0.40f * Screen.height,
			                               0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect, "Request Interstitial 1 - Text")) {
			#if UNITY_IOS
			RequestInterstitial("ca-app-pub-6569756320106809/7792598009");
			#elif UNITY_ANDROID
			RequestInterstitial("ca-app-pub-6569756320106809/7217188402");
			#endif
		}

		Rect showInterstitialRect = new Rect(0.1f * Screen.width, 0.49f * Screen.height,
			                            0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect, "Show Interstitial 1 - Text")) {
			#if UNITY_IOS
			ShowInterstitial("ca-app-pub-6569756320106809/7792598009");
			#elif UNITY_ANDROID
			ShowInterstitial("ca-app-pub-6569756320106809/7217188402");
			#endif
		}

		Rect requestInterstitialRect1 = new Rect(0.1f * Screen.width, 0.58f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect1, "Request Interstitial 2 - Image")) {
			#if UNITY_IOS
			RequestInterstitial("ca-app-pub-6569756320106809/9269331204");
			#elif UNITY_ANDROID
			RequestInterstitial("ca-app-pub-6569756320106809/5740455203");
			#endif
		}

		Rect showInterstitialRect1 = new Rect(0.1f * Screen.width, 0.67f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect1, "Show Interstitial 2 - Image")) {
			#if UNITY_IOS
			ShowInterstitial("ca-app-pub-6569756320106809/9269331204");
			#elif UNITY_ANDROID
			ShowInterstitial("ca-app-pub-6569756320106809/5740455203");
			#endif
		}

		Rect requestInterstitialRect2 = new Rect(0.1f * Screen.width, 0.76f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect2, "Request Interstitial 3 - Video")) {
			#if UNITY_IOS
			RequestInterstitial("ca-app-pub-6569756320106809/1746064404");
			#elif UNITY_ANDROID
			RequestInterstitial("ca-app-pub-6569756320106809/8693921603");
			#endif
		}

		Rect showInterstitialRect2 = new Rect(0.1f * Screen.width, 0.85f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect2, "Show Interstitial 3 - Video")) {
			#if UNITY_IOS
			ShowInterstitial("ca-app-pub-6569756320106809/1746064404");
			#elif UNITY_ANDROID
			ShowInterstitial("ca-app-pub-6569756320106809/8693921603");
			#endif
		}

		#endif
	}

	#endregion

	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest() {
		//.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
		return new AdRequest.Builder()
				.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddExtra("color_bg", "9B30FF")
				.AddKeyword("Unity3D")
				.AddKeyword("GoogleAdTesting")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.Build();
	}
	//
	private void RequestBanner(string adUnitId, AdPosition position = AdPosition.Bottom) {
		RequestBanner(adUnitId, AdSize.SmartBanner, position);
	}
	private void RequestBanner(string adUnitId, AdSize adSize,
		AdPosition position = AdPosition.Bottom) {
		// Create a 320x50 banner at the top of the screen.
		if(!bannerViews.ContainsKey(adUnitId)) {
			bannerViews.Add(adUnitId, new  BannerView(adUnitId, adSize, position));
			// Register for ad events.
			bannerViews[adUnitId].AdLoaded += HandleAdLoaded;
			bannerViews[adUnitId].AdFailedToLoad += HandleAdFailedToLoad;
			bannerViews[adUnitId].AdOpened += HandleAdOpened;
			bannerViews[adUnitId].AdClosing += HandleAdClosing;
			bannerViews[adUnitId].AdClosed += HandleAdClosed;
			bannerViews[adUnitId].AdLeftApplication += HandleAdLeftApplication;
		}
		// Load a banner ad.
		bannerViews[adUnitId].LoadAd(createAdRequest());
	}
	//
	//
	//
	private void RemoveInterstitialInstance(string adUnitId) {
		interstitialAdViews.Remove(adUnitId);
		AutoCacheInterstitial(adUnitId);
	}
	private void AutoCacheInterstitial(string adUnitId) {
		RequestInterstitial(adUnitId);
	}
	//
	public void ShowBanner(string adUnitId, AdPosition position = AdPosition.Bottom) {
		if(string.IsNullOrEmpty(adUnitId)) {
			Debug.LogWarning("Empty Ad Unit ID not Allowed...");
			return;
		}
		if(!bannerViews.ContainsKey(adUnitId)) {
			RequestBanner(adUnitId, position);
		} else {
			bannerViews[adUnitId].Show();
		}

	}
	public void HideBanner(string adUnitId = "") {
		if(!string.IsNullOrEmpty(adUnitId)) {
			bannerViews[adUnitId].Hide();
		} else {
			foreach(KeyValuePair<string,BannerView> kvp in bannerViews) {
				if(kvp.Value != null) {
					kvp.Value.Hide();
				}
			}
		}

	}
	//
	public void RequestInterstitial(string adUnitId) {
		// Create an interstitial.
		if(!interstitialAdViews.ContainsKey(adUnitId)) {
			interstitialAdViews.Add(adUnitId, new InterstitialAd(adUnitId));
			// Register for ad events.
			interstitialAdViews[adUnitId].AdLoaded += HandleInterstitialLoaded;
			interstitialAdViews[adUnitId].AdFailedToLoad += HandleInterstitialFailedToLoad;
			interstitialAdViews[adUnitId].AdOpened += HandleInterstitialOpened;
			interstitialAdViews[adUnitId].AdClosing += HandleInterstitialClosing;
			interstitialAdViews[adUnitId].AdClosed += HandleInterstitialClosed;
			interstitialAdViews[adUnitId].AdLeftApplication += HandleInterstitialLeftApplication;
		}
		// Load an interstitial ad.
		interstitialAdViews[adUnitId].LoadAd(createAdRequest());
	}
	public bool IsInterstitialReady(string adUnitId) {
		// Create an interstitial.
		if(!interstitialAdViews.ContainsKey(adUnitId)) {
			Debug.Log("Ad Unit not requested yet, it's requested now try after some time to check again");
			RequestInterstitial(adUnitId);
			return false;
		} else {
			return interstitialAdViews[adUnitId].IsLoaded();
		}
	}
	public void ShowInterstitial(string adUnitId) {
		if(string.IsNullOrEmpty(adUnitId)) {
			Debug.LogWarning("Empty Ad Unit ID not Allowed...");
			return;
		}
		if(!interstitialAdViews.ContainsKey(adUnitId)) {
			Debug.LogWarning("Intestitial Cache not requeste yet for Ad Unity Id, Please request first");
		} else if(interstitialAdViews[adUnitId].IsLoaded()) {
			interstitialAdViews[adUnitId].Show();
		} else {
			Debug.LogWarning("Intestitial is not downloaded yet, please wait");
		}
	}
	//
	//

	#region Banner callback handlers

	private void HandleAdLoaded(object sender, EventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleAdLoaded event received. " + banner.AdUnityID);
		if(BannerAdLoaded != null) {
			BannerAdLoaded.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
		if(BannerAdFailedToLoad != null) {
			BannerAdFailedToLoad.Invoke(banner.AdUnityID, args.Message);
		}
	}

	private void HandleAdOpened(object sender, EventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleAdOpened event received " + banner.AdUnityID);
		if(BannerAdOpened != null) {
			BannerAdOpened.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdClosing(object sender, EventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleAdClosing event received " + banner.AdUnityID);
		if(BannerAdClosing != null) {
			BannerAdClosing.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdClosed(object sender, EventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleAdClosed event received " + banner.AdUnityID);
		bannerViews.Remove(banner.AdUnityID);
		if(BannerAdClosed != null) {
			BannerAdClosed.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdLeftApplication(object sender, EventArgs args) {
		banner = (BannerView)sender;
		Debug.Log("HandleAdLeftApplication event received " + banner.AdUnityID);
		if(BannerAdLeftApplication != null) {
			BannerAdLeftApplication.Invoke(banner.AdUnityID);
		}
	}

	#endregion

	//

	#region Interstitial callback handlers

	private void HandleInterstitialLoaded(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		Debug.Log("HandleInterstitialLoaded event received. " + interstitial.AdUnityID);
		if(InterstitialLoaded != null) {
			InterstitialLoaded.Invoke(interstitial.AdUnityID);
		}

	}
	private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		interstitial = (InterstitialAd)sender;
		Debug.Log("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		if(InterstitialFailedToLoad != null) {
			InterstitialFailedToLoad.Invoke(interstitial.AdUnityID, args.Message);
		}
	}
	private void HandleInterstitialOpened(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		Debug.Log("HandleInterstitialOpened event received " + interstitial.AdUnityID);
		if(InterstitialOpened != null) {
			InterstitialOpened.Invoke(interstitial.AdUnityID);
		}
	}
	private void HandleInterstitialClosing(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		Debug.Log("HandleInterstitialClosing event received " + interstitial.AdUnityID);
		if(InterstitialClosing != null) {
			InterstitialClosing.Invoke(interstitial.AdUnityID);
		}
	}
	private void HandleInterstitialClosed(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		interstitialAdId = interstitial.AdUnityID;
		Debug.Log("HandleInterstitialClosed event received " + interstitialAdId);
		if(InterstitialClosed != null) {
			InterstitialClosed.Invoke(interstitial.AdUnityID);
		}
		RemoveInterstitialInstance(interstitialAdId);
	}
	private void HandleInterstitialLeftApplication(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		interstitialAdId = interstitial.AdUnityID;
		Debug.Log("HandleInterstitialLeftApplication event received " + interstitialAdId);
		if(InterstitialLeftApplication != null) {
			InterstitialLeftApplication.Invoke(interstitial.AdUnityID);
		}
	}

	#endregion
}
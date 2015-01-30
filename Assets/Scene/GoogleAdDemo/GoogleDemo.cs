using UnityEngine;
using System.Collections;

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
using System.Collections.Specialized;

public class GoogleDemo : MonoBehaviour {

	// Use this for initialization
	//void Start() {
	//}
	
	// Update is called once per frame
	//void Update() {
	//}
	void OnEnable() {
		GoogleMobileAdsScript.Me.BannerAdLoaded += GMAdBannerAdLoaded;
		GoogleMobileAdsScript.Me.BannerAdFailedToLoad += GMAdBannerAdFailedToLoad;
		GoogleMobileAdsScript.Me.BannerAdOpened += GMAdBannerAdOpened;
		GoogleMobileAdsScript.Me.BannerAdClosing += GMAdBannerAdClosing;
		GoogleMobileAdsScript.Me.BannerAdClosed += GMAdBannerAdClosed;
		GoogleMobileAdsScript.Me.BannerAdLeftApplication += GMAdBannerAdLeftApplication;

		GoogleMobileAdsScript.Me.InterstitialLoaded += GMAdInterstitialLoaded;
		GoogleMobileAdsScript.Me.InterstitialFailedToLoad += GMAdInterstitialFailedToLoad;
		GoogleMobileAdsScript.Me.InterstitialOpened += GMAdInterstitialOpened;
		GoogleMobileAdsScript.Me.InterstitialClosing += GMAdInterstitialClosing;
		GoogleMobileAdsScript.Me.InterstitialClosed += GMAdInterstitialClosed;
		GoogleMobileAdsScript.Me.InterstitialLeftApplication += GMAdInterstitialLeftApplication;
	}
	void OnDisable() {
		GoogleMobileAdsScript.Me.BannerAdLoaded -= GMAdBannerAdLoaded;
		GoogleMobileAdsScript.Me.BannerAdFailedToLoad -= GMAdBannerAdFailedToLoad;
		GoogleMobileAdsScript.Me.BannerAdOpened -= GMAdBannerAdOpened;
		GoogleMobileAdsScript.Me.BannerAdClosing -= GMAdBannerAdClosing;
		GoogleMobileAdsScript.Me.BannerAdClosed -= GMAdBannerAdClosed;
		GoogleMobileAdsScript.Me.BannerAdLeftApplication -= GMAdBannerAdLeftApplication;

		GoogleMobileAdsScript.Me.InterstitialLoaded -= GMAdInterstitialLoaded;
		GoogleMobileAdsScript.Me.InterstitialFailedToLoad -= GMAdInterstitialFailedToLoad;
		GoogleMobileAdsScript.Me.InterstitialOpened -= GMAdInterstitialOpened;
		GoogleMobileAdsScript.Me.InterstitialClosing -= GMAdInterstitialClosing;
		GoogleMobileAdsScript.Me.InterstitialClosed -= GMAdInterstitialClosed;
		GoogleMobileAdsScript.Me.InterstitialLeftApplication -= GMAdInterstitialLeftApplication;
	}

	void OnGUI() {
		#if TESTMODE_GOOGLEADS
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.03f * Screen.height);
		Rect showBannerRect = new Rect(0.1f * Screen.width, 0.15f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showBannerRect, "Show Banner")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.ShowBanner("ca-app-pub-6569756320106809/4839131600");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.ShowBanner("ca-app-pub-6569756320106809/4263722000");
			#endif
		}

		Rect hideBannerRect = new Rect(0.1f * Screen.width, 0.25f * Screen.height,
			                      0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(hideBannerRect, "Hide Banner")) {
			GoogleMobileAdsScript.Me.HideBanner();
		}



		Rect requestInterstitialRect = new Rect(0.1f * Screen.width, 0.40f * Screen.height,
			                               0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect, "Request Interstitial 1 - Text")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/7792598009");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/7217188402");
			#endif
		}

		Rect showInterstitialRect = new Rect(0.1f * Screen.width, 0.49f * Screen.height,
			                            0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect, "Show Interstitial 1 - Text")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/7792598009");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/7217188402");
			#endif
		}

		Rect requestInterstitialRect1 = new Rect(0.1f * Screen.width, 0.58f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect1, "Request Interstitial 2 - Image")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/9269331204");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/5740455203");
			#endif
		}

		Rect showInterstitialRect1 = new Rect(0.1f * Screen.width, 0.67f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect1, "Show Interstitial 2 - Image")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/9269331204");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/5740455203");
			#endif
		}

		Rect requestInterstitialRect2 = new Rect(0.1f * Screen.width, 0.76f * Screen.height,
			                                0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(requestInterstitialRect2, "Request Interstitial 3 - Video")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/1746064404");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.RequestInterstitial("ca-app-pub-6569756320106809/8693921603");
			#endif
		}

		Rect showInterstitialRect2 = new Rect(0.1f * Screen.width, 0.85f * Screen.height,
			                             0.8f * Screen.width, 0.075f * Screen.height);
		if(GUI.Button(showInterstitialRect2, "Show Interstitial 3 - Video")) {
			#if UNITY_IOS
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/1746064404");
			#elif UNITY_ANDROID
			GoogleMobileAdsScript.Me.ShowInterstitial("ca-app-pub-6569756320106809/8693921603");
			#endif
		}

		#endif
	}


	private void GMAdBannerAdLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdBannerAdFailedToLoad => adUnitId: " + adUnitId +
		" - ERROR: " + error);
	}
	private void GMAdBannerAdOpened(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdOpened => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdClosing(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdClosing => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdClosed => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdLeftApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdLeftApplication => adUnitId: " + adUnitId);
	}

	private void GMAdInterstitialLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdInterstitialFailedToLoad => adUnitId: " + adUnitId +
		" - ERROR: " + error);
	}
	private void GMAdInterstitialOpened(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialOpened => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialClosing(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialClosing => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialClosed => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialLeftApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialLeftApplication => adUnitId: " + adUnitId);
	}


}

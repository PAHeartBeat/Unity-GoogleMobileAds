using UnityEngine;
//
using iPAHeartBeat.Core.Utility;
//
using GoogleMobileAds.Api;
//

public class GoogleDemo : MonoBehaviour {
#if UNITY_IOS
	//Here I added few test adUnitIds for iOS Test
	// Banner Ad - Text / Image
	string banner = "ca-app-pub-6569756320106809/4839131600";

	// Interstitial Ad - Text
	string intText = "ca-app-pub-6569756320106809/7792598009";
	// Interstitial Ad - Image
	string intImg = "ca-app-pub-6569756320106809/9269331204";
	// Interstitial Ad - Video
	string intVid = "ca-app-pub-6569756320106809/1746064404";

	// Reward Video Ads
	string rewardVid = "ca-app-pub-6569756320106809/1382778804";
	// Native Ads
	string nativeAd = "ca-app-pub-6569756320106809/2859512002";
#endif

#if UNITY_ANDROID
	//Here I added few test adUnitIds for Android Test
	// Banner Ad - Text / Image
	string banner = "ca-app-pub-6569756320106809/4263722000";

	// Interstitial Ad - Text
	string intText = "ca-app-pub-6569756320106809/7217188402";
	// Interstitial Ad - Image
	string intImg = "ca-app-pub-6569756320106809/5740455203";
	// Interstitial Ad - Video
	string intVid = "ca-app-pub-6569756320106809/8693921603";

	// Reward Video Ads
string rewardVid = "ca-app-pub-6569756320106809/6092180005";
	// Native Ads
string nativeAd = "ca-app-pub-6569756320106809/1522379609";
#endif

	void OnEnable() {
		GoogleMobileAdsScript.Me.OnBannerAdLoaded += GMAdBannerAdLoaded;
		GoogleMobileAdsScript.Me.OnBannerAdFailedToLoad += GMAdBannerAdFailedToLoad;
		GoogleMobileAdsScript.Me.OnBannerAdOpening += GMAdBannerAdOpened;
		GoogleMobileAdsScript.Me.OnBannerAdClosed += GMAdBannerAdClosed;
		GoogleMobileAdsScript.Me.OnBannerAdLeftApplication += GMAdBannerAdLeavingApplication;

		GoogleMobileAdsScript.Me.OnInterstitialLoaded += GMAdInterstitialLoaded;
		GoogleMobileAdsScript.Me.OnInterstitialFailedToLoad += GMAdInterstitialFailedToLoad;
		GoogleMobileAdsScript.Me.OnInterstitialOpening += GMAdInterstitialOpening;
		GoogleMobileAdsScript.Me.OnInterstitialClosed += GMAdInterstitialClosed;
		GoogleMobileAdsScript.Me.OnInterstitialLeavingApplication += GMAdInterstitialLeavingApplication;

		GoogleMobileAdsScript.Me.OnRewardAdLoaded += GMAdRewardAdLoaded;
		GoogleMobileAdsScript.Me.OnRewardAdFailedToLoad += GMAdRewardAdFailedToLoad;
		GoogleMobileAdsScript.Me.OnRewardAdOpening += GMAdRewardAdOpening;
		GoogleMobileAdsScript.Me.OnRewardAdStarted += GMAdRewardAdStarted;
		GoogleMobileAdsScript.Me.OnRewardAdRewarded += GMAdRewardBasedVideoRewarded;
		GoogleMobileAdsScript.Me.OnRewardAdClosed += GMAdRewardAdClosed;
		GoogleMobileAdsScript.Me.OnRewardAdLeavingApplication += GMAdRewardAdLeavingApplication;

		GoogleMobileAdsScript.Me.OnNativeExperssAdLoaded += GMAdNativeExpressLoaded;
		GoogleMobileAdsScript.Me.OnNativeExperssAdFailedToLoad += GMAdNativeExpressFailedToLoad;
		GoogleMobileAdsScript.Me.OnNativeExperssAdOpening += GMAdNativeExpressOpening;
		GoogleMobileAdsScript.Me.OnNativeExperssAdClosed += GMAdNativeExpressClosed;
		GoogleMobileAdsScript.Me.OnNativeExperssAdLeavingApplication += GMNativeExpressLeaviingApplication;
	}
	void OnDisable() {
		GoogleMobileAdsScript.Me.OnBannerAdLoaded -= GMAdBannerAdLoaded;
		GoogleMobileAdsScript.Me.OnBannerAdFailedToLoad -= GMAdBannerAdFailedToLoad;
		GoogleMobileAdsScript.Me.OnBannerAdOpening -= GMAdBannerAdOpened;
		GoogleMobileAdsScript.Me.OnBannerAdClosed -= GMAdBannerAdClosed;
		GoogleMobileAdsScript.Me.OnBannerAdLeftApplication -= GMAdBannerAdLeavingApplication;

		GoogleMobileAdsScript.Me.OnInterstitialLoaded -= GMAdInterstitialLoaded;
		GoogleMobileAdsScript.Me.OnInterstitialFailedToLoad -= GMAdInterstitialFailedToLoad;
		GoogleMobileAdsScript.Me.OnInterstitialOpening -= GMAdInterstitialOpening;
		GoogleMobileAdsScript.Me.OnInterstitialClosed -= GMAdInterstitialClosed;
		GoogleMobileAdsScript.Me.OnInterstitialLeavingApplication -= GMAdInterstitialLeavingApplication;

		GoogleMobileAdsScript.Me.OnRewardAdLoaded -= GMAdRewardAdLoaded;
		GoogleMobileAdsScript.Me.OnRewardAdFailedToLoad -= GMAdRewardAdFailedToLoad;
		GoogleMobileAdsScript.Me.OnRewardAdOpening -= GMAdRewardAdOpening;
		GoogleMobileAdsScript.Me.OnRewardAdStarted -= GMAdRewardAdStarted;
		GoogleMobileAdsScript.Me.OnRewardAdRewarded -= GMAdRewardBasedVideoRewarded;
		GoogleMobileAdsScript.Me.OnRewardAdClosed -= GMAdRewardAdClosed;
		GoogleMobileAdsScript.Me.OnRewardAdLeavingApplication -= GMAdRewardAdLeavingApplication;

		GoogleMobileAdsScript.Me.OnNativeExperssAdLoaded -= GMAdNativeExpressLoaded;
		GoogleMobileAdsScript.Me.OnNativeExperssAdFailedToLoad -= GMAdNativeExpressFailedToLoad;
		GoogleMobileAdsScript.Me.OnNativeExperssAdOpening -= GMAdNativeExpressOpening;
		GoogleMobileAdsScript.Me.OnNativeExperssAdClosed -= GMAdNativeExpressClosed;
		GoogleMobileAdsScript.Me.OnNativeExperssAdLeavingApplication -= GMNativeExpressLeaviingApplication;
	}

	void OnGUI() {
#if TESTMODE
		GUIStyle a = GUI.skin.button;
		a.fixedHeight = Screen.height * 0.09f;
		a.fixedWidth = Screen.width * 0.25f * 0.95f;
		a.richText = true;
		a.alignment = TextAnchor.MiddleCenter;
		GUILayout.BeginArea(new Rect(Screen.width * 0.25f, 0, Screen.width * 0.25f, Screen.height));

		// Puts some basic buttons onto the screen.
		if(GUILayout.Button("Show Banner")) {
			GoogleMobileAdsScript.Me.ShowBanner(banner);
		}
		if(GUILayout.Button("Hide Banner")) {
			GoogleMobileAdsScript.Me.HideBanner();
		}

		if(GUILayout.Button("Request Interstitial 1 - Text")) {
			GoogleMobileAdsScript.Me.RequestInterstitial(intText);
		}
		if(GUILayout.Button("Show Interstitial 1 - Text")) {
			GoogleMobileAdsScript.Me.ShowInterstitial(intText);
		}
		if(GUILayout.Button("Request Interstitial 2 - Image")) {
			GoogleMobileAdsScript.Me.RequestInterstitial(intImg);
		}
		if(GUILayout.Button("Show Interstitial 2 - Image")) {
			GoogleMobileAdsScript.Me.ShowInterstitial(intImg);
		}
		if(GUILayout.Button("Request Interstitial 3 - Video")) {
			GoogleMobileAdsScript.Me.RequestInterstitial(intVid);
		}
		if(GUILayout.Button("Show Interstitial 3 - Video")) {
			GoogleMobileAdsScript.Me.ShowInterstitial(intVid);
		}

		if(GUILayout.Button("Request Reward Video")) {
			GoogleMobileAdsScript.Me.RequestInterstitial(rewardVid);
		}
		if(GUILayout.Button("Show Reward Video")) {
			GoogleMobileAdsScript.Me.ShowInterstitial(rewardVid);
		}

		if(GUILayout.Button("Request Native")) {
			GoogleMobileAdsScript.Me.RequestInterstitial(nativeAd);
		}
		if(GUILayout.Button("Show Native")) {
			GoogleMobileAdsScript.Me.ShowInterstitial(nativeAd);
		}
		GUILayout.EndArea();
#endif
	}


	#region Google Mobile Ads Banner Events Listners
	private void GMAdBannerAdLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdBannerAdFailedToLoad => adUnitId: " + adUnitId + " - ERROR: " + error);
	}
	private void GMAdBannerAdOpened(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdOpened => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdClosed => adUnitId: " + adUnitId);
	}
	private void GMAdBannerAdLeavingApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdBannerAdLeftApplication => adUnitId: " + adUnitId);
	}
	#endregion

	#region Google Interstitial Ad Evetns Listners
	private void GMAdInterstitialLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdInterstitialFailedToLoad => adUnitId: " + adUnitId + " - ERROR: " + error);
	}
	private void GMAdInterstitialOpening(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialOpened => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialClosed => adUnitId: " + adUnitId);
	}
	private void GMAdInterstitialLeavingApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdInterstitialLeftApplication => adUnitId: " + adUnitId);
	}
	#endregion

	#region Google Mobibe Reward Video Ad Event Listner
	private void GMAdRewardAdLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdRewardAdLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdRewardAdFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdRewardAdLoaded => adUnitId: " + adUnitId + " - ERROR: " + error);
	}
	private void GMAdRewardBasedVideoRewarded(string adUnitId, Reward reward) {
		MyDebug.Log(GetType() + "::GMAdRewardBasedVideoRewarded => adUnitId: " + adUnitId + " - Reward: " + reward.Amount +
					", type: " + reward.Type);
	}
	private void GMAdRewardAdOpening(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdRewardAdOpening => adUnitId: " + adUnitId);
	}
	private void GMAdRewardAdStarted(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdRewardAdStarted => adUnitId: " + adUnitId);
	}
	private void GMAdRewardAdClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdRewardAdClosed => adUnitId: " + adUnitId);
	}
	private void GMAdRewardAdLeavingApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdRewardAdLeftApplication => adUnitId: " + adUnitId);
	}
	#endregion

	#region Google Interstitial Ad Evetns Listners
	private void GMAdNativeExpressLoaded(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdNativeExpressLoaded => adUnitId: " + adUnitId);
	}
	private void GMAdNativeExpressFailedToLoad(string adUnitId, string error) {
		MyDebug.Log(GetType() + "::GMAdNativeExpressFailedToLoad => adUnitId: " + adUnitId + " - ERROR: " + error);
	}
	private void GMAdNativeExpressOpening(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdNativeExpressOpening => adUnitId: " + adUnitId);
	}
	private void GMAdNativeExpressClosed(string adUnitId) {
		MyDebug.Log(GetType() + "::GMAdNativeExpressClosed => adUnitId: " + adUnitId);
	}
	private void GMNativeExpressLeaviingApplication(string adUnitId) {
		MyDebug.Log(GetType() + "::GMNativeExpressLeftApplication => adUnitId: " + adUnitId);
	}
	#endregion

}

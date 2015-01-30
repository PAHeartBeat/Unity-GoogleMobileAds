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

	public Action<string> BannerAdLoaded;
	public Action<string,string> BannerAdFailedToLoad;
	public Action<string> BannerAdOpened;
	public Action<string> BannerAdClosing;
	public Action<string> BannerAdClosed;
	public Action<string> BannerAdLeftApplication;

	public Action<string> InterstitialLoaded;
	public Action<string,string> InterstitialFailedToLoad;
	public Action<string> InterstitialOpened;
	public Action<string> InterstitialClosing;
	public Action<string> InterstitialClosed;
	public Action<string> InterstitialLeftApplication;

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
			MyDebug.LogWarning("GoogleMobileAdsScript::ShowBanner => Empty adUnitId not Allowed...");
			if(BannerAdFailedToLoad != null) {
				BannerAdFailedToLoad.Invoke(adUnitId, ErrorCode.Empty_adUnitId + " - ");
			}
			return;
		}
		if(!bannerViews.ContainsKey(adUnitId)) {
			if(BannerAdFailedToLoad != null) {
				BannerAdFailedToLoad.Invoke(adUnitId, ErrorCode.NotRequested + " - ");
			}
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
			MyDebug.LogWarning("GoogleMobileAdsScript::IsInterstitialReady => adUnitId not requested yet");
			return false;
		} else {
			return interstitialAdViews[adUnitId].IsLoaded();
		}
	}
	public void ShowInterstitial(string adUnitId) {
		if(string.IsNullOrEmpty(adUnitId)) {
			MyDebug.LogWarning("GoogleMobileAdsScript::ShowInterstitial => Empty adUnitId not Allowed...");
			if(InterstitialFailedToLoad != null) {
				InterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.Empty_adUnitId + " - ");
			}
			return;
		}
		if(!interstitialAdViews.ContainsKey(adUnitId)) {
			MyDebug.LogWarning("GoogleMobileAdsScript::ShowInterstitial => adUnitId not requested yet");
			if(InterstitialFailedToLoad != null) {
				InterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.NotRequested + " - ");
			}
		} else if(interstitialAdViews[adUnitId].IsLoaded()) {
			interstitialAdViews[adUnitId].Show();
		} else {
			if(InterstitialFailedToLoad != null) {
				InterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.Downloaing + " - ");
			}
			MyDebug.LogWarning("GoogleMobileAdsScript::ShowInterstitial => adUnitId not downloaded yet, please yet");
		}
	}
	//
	//

	#region Banner callback handlers

	private void HandleAdLoaded(object sender, EventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleAdLoaded => " + banner.AdUnityID);
		if(BannerAdLoaded != null) {
			BannerAdLoaded.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleAdFailedToLoad => " + banner.AdUnityID + " - Message: " + args.Message);
		if(BannerAdFailedToLoad != null) {
			BannerAdFailedToLoad.Invoke(banner.AdUnityID, ErrorCode.NotRequested + " - " + args.Message);
		}
	}

	private void HandleAdOpened(object sender, EventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleAdOpened => " + banner.AdUnityID);
		if(BannerAdOpened != null) {
			BannerAdOpened.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdClosing(object sender, EventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleAdClosing => " + banner.AdUnityID);
		if(BannerAdClosing != null) {
			BannerAdClosing.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdClosed(object sender, EventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::GoogleMobileAdsScript => " + banner.AdUnityID);
		bannerViews.Remove(banner.AdUnityID);
		if(BannerAdClosed != null) {
			BannerAdClosed.Invoke(banner.AdUnityID);
		}
	}

	private void HandleAdLeftApplication(object sender, EventArgs args) {
		banner = (BannerView)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleAdLeftApplication => " + banner.AdUnityID);
		if(BannerAdLeftApplication != null) {
			BannerAdLeftApplication.Invoke(banner.AdUnityID);
		}
	}

	#endregion

	//

	#region Interstitial callback handlers

	private void HandleInterstitialLoaded(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialLoaded => " + interstitial.AdUnityID);
		if(InterstitialLoaded != null) {
			InterstitialLoaded.Invoke(interstitial.AdUnityID);
		}

	}
	private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		interstitial = (InterstitialAd)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialFailedToLoad => " + interstitial.AdUnityID +
		" - Message: " + args.Message);
		if(InterstitialFailedToLoad != null) {
			InterstitialFailedToLoad.Invoke(interstitial.AdUnityID, ErrorCode.NotRequested + " - " + args.Message);
		}
	}
	private void HandleInterstitialOpened(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialOpened =>" + interstitial.AdUnityID);
		if(InterstitialOpened != null) {
			InterstitialOpened.Invoke(interstitial.AdUnityID);
		}
	}
	private void HandleInterstitialClosing(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialClosing =>" + interstitial.AdUnityID);
		if(InterstitialClosing != null) {
			InterstitialClosing.Invoke(interstitial.AdUnityID);
		}
	}
	private void HandleInterstitialClosed(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		interstitialAdId = interstitial.AdUnityID;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialClosed =>" + interstitialAdId);
		if(InterstitialClosed != null) {
			InterstitialClosed.Invoke(interstitial.AdUnityID);
		}
		RemoveInterstitialInstance(interstitialAdId);
	}
	private void HandleInterstitialLeftApplication(object sender, EventArgs args) {
		interstitial = (InterstitialAd)sender;
		interstitialAdId = interstitial.AdUnityID;
		MyDebug.Log("GoogleMobileAdsScript::HandleInterstitialLeftApplication =>" + interstitialAdId);
		if(InterstitialLeftApplication != null) {
			InterstitialLeftApplication.Invoke(interstitial.AdUnityID);
		}
	}

	#endregion
}

public enum ErrorCode {
	Empty_adUnitId,
	NotRequested,
	Downloaing,
	SDKError
}
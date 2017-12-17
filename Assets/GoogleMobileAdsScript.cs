using System;
using System.Collections.Generic;
//
using UnityEngine;
//
using iPAHeartBeat.Core.Singleton;
using iPAHeartBeat.Core.Utility;
//
using GoogleMobileAds.Api;

//
//

/* Example script showing how to invoke the Google Mobile Ads Unity plugin.
 * Orignal code writen by Google Mobile Ad SDK Team, 
 * 
 * Modified By: Ankur Ranpariya aka PAHeartBeat on 28th Jan 2015
 * Last Modified By: Ankur Ranpariya aka PAHeartBeat on 3rd Jun 2017
 * 
*/


public class GoogleMobileAdsScript : SingletonAuto<GoogleMobileAdsScript> {
	public bool IsHideBanner = false;

	#region Events

#if ADMOB
	public Action<string> OnBannerAdLoaded;
	public Action<string, string> OnBannerAdFailedToLoad;
	public Action<string> OnBannerAdOpening;
	public Action<string> OnBannerAdClosed;
	public Action<string> OnBannerAdLeftApplication;

	public Action<string> OnInterstitialLoaded;
	public Action<string, string> OnInterstitialFailedToLoad;
	public Action<string> OnInterstitialOpening;
	public Action<string> OnInterstitialClosed;
	public Action<string> OnInterstitialLeavingApplication;

	public Action<string> OnRewardAdLoaded;
	public Action<string, string> OnRewardAdFailedToLoad;
	public Action<string> OnRewardAdOpening;
	public Action<string> OnRewardAdStarted;
	public Action<string, Reward> OnRewardAdRewarded;
	public Action<string> OnRewardAdClosed;
	public Action<string> OnRewardAdLeavingApplication;

	public Action<string> OnNativeExperssAdLoaded;
	public Action<string, string> OnNativeExperssAdFailedToLoad;
	public Action<string> OnNativeExperssAdOpening;
	public Action<string> OnNativeExperssAdClosed;
	public Action<string> OnNativeExperssAdLeavingApplication;
#endif
	#endregion

	#region Internal fields

#if ADMOB
	private Dictionary<string, BannerView> bannerViews = new Dictionary<string, BannerView>();
	private Dictionary<string, InterstitialAd> interstitialAdViews = new Dictionary<string, InterstitialAd>();
	private Dictionary<string, RewardBasedVideoAd> rewardVideoAds = new Dictionary<string, RewardBasedVideoAd>();
	private Dictionary<string, NativeExpressAdView> nativeAdViews = new Dictionary<string, NativeExpressAdView>();

	private BannerView banner;
	private InterstitialAd interstitial;
	private RewardBasedVideoAd rewardVideoAd;
	private NativeExpressAdView nativeAdView;
#endif
	#endregion

	#region Mono Actions

	void Awake() {
		Me = this;
		name = "Google Mobile Ads";
		MyDebug.Log("GoogleMobileAdsScripts::Awake =>");
	}

	#endregion

#if ADMOB
	private void RemoveInterstitial(string adUnitId) {
		this.interstitialAdViews.Remove(adUnitId);
		this.AutoCacheInterstitial(adUnitId);
	}

	private void AutoCacheInterstitial(string adUnitId) {
		this.RequestInterstitial(adUnitId);
	}
	//
	private void RemoveRewardAd(string adUnitId) {
		this.rewardVideoAds.Remove(adUnitId);
		this.AutoCacheRewardAd(adUnitId);
	}

	private void AutoCacheRewardAd(string adUnitId) {
		this.RequestRewardVideoAd(adUnitId);
	}
	//
	private void RemoveNativeAd(NativeExpressAdView adView) {
		this.nativeAdViews.Remove(adView.adUnitId);
		this.AutoCacheNativeAd(adView);
	}

	private void AutoCacheNativeAd(NativeExpressAdView adView) {
		if(adView.isFixedPostion)
			this.RequestNativeExpress(adView.adUnitId, adView.adSize, adView.adPosition);
		else
			this.RequestNativeExpress(adView.adUnitId, adView.adSize, adView.x, adView.y);

	}
	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest() {
		MyDebug.Log(string.Format("GMAS::CreateAdRequest"));
		// Google Test Ad Unit ID
		// ca-app-pub-3940256099942544/1033173712
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.Build();
		//
		//	.AddTestDevice("81769a040e7adaf577ac7b9012a34fa9") // iPad SKM 3rd Gen GSM		iOS 9.1
		//	.AddTestDevice("d55111b6e8a6776f81fc05b1d4846501") // iPad 2nd Gen				iOS 9.2
		//	.AddTestDevice("f4a07362ca0c2a18a829f08783ecc939") // iPhone 5					iOS 9.2
		//	.AddTestDevice("bf66b38bc1f66ba917476918bb20541f") // iPhone 6 (LBL 8)			iOS 9.2
		//
		//.AddExtra("color_bg", "9B30FF")
		//.AddKeyword("Unity3D")
		//.AddKeyword(GUtility.Me.APPNAME)
		//.SetGender(Gender.Male)
		//.SetBirthday(new DateTime(1985, 1, 1))
		//.TagForChildDirectedTreatment(false)

	}
	//
#endif
	//

	#region Banner Methods

#if ADMOB
	private void RequestBanner(string adUnitId, AdPosition position = AdPosition.Bottom) {
		this.RequestBanner(adUnitId, AdSize.SmartBanner, position);
	}

	private void RequestBanner(string adUnitId, AdSize adSize, AdPosition position = AdPosition.Bottom) {
		// Create a 320x50 banner at the top of the screen.
		if(!this.bannerViews.ContainsKey(adUnitId)) {
			this.bannerViews.Add(adUnitId, new BannerView(adUnitId, adSize, position));
			// Register for ad events.
			this.bannerViews[adUnitId].OnAdLoaded += this.HandleOnBannerAdLoaded;
			this.bannerViews[adUnitId].OnAdFailedToLoad += this.HandleOnBannerAdFailedToLoad;
			this.bannerViews[adUnitId].OnAdOpening += this.HandleOnBannerAdOpening;
			this.bannerViews[adUnitId].OnAdClosed += this.HandleOnBannerAdClosed;
			this.bannerViews[adUnitId].OnAdLeavingApplication += this.HandleOnBannerAdLeftApplication;
		}
		// Load a banner ad.
		this.bannerViews[adUnitId].LoadAd(this.CreateAdRequest());
	}
	//
	public void ShowBanner(string adUnitId, AdPosition position = AdPosition.Bottom) {
		if(string.IsNullOrEmpty(adUnitId)) {
			MyDebug.Log("GMAS::ShowBanner => Empty adUnitId not Allowed...");
			if(this.OnBannerAdFailedToLoad != null) {
				this.OnBannerAdFailedToLoad.Invoke(adUnitId, ErrorCode.EmptyAdUnitID + " - ");
			}
			return;
		}
		this.IsHideBanner = false;
		if(!this.bannerViews.ContainsKey(adUnitId)) {
			this.RequestBanner(adUnitId, position);
		} else {
			this.bannerViews[adUnitId].Show();
			this.HandleOnBannerAdLoaded(bannerViews[adUnitId], null);
		}
	}
#endif
	public void HideBanner(string adUnitId = "") {
		this.IsHideBanner = true;
#if ADMOB
		if(!string.IsNullOrEmpty(adUnitId) && this.bannerViews.ContainsKey(adUnitId)) {
			this.bannerViews[adUnitId].Hide();
		} else {
			foreach(KeyValuePair<string, BannerView> kvp in this.bannerViews) {
				if(kvp.Value != null) {
					kvp.Value.Hide();
				}
			}
		}
#endif
	}

	#region callback handlers for Banner

#if ADMOB
	private void HandleOnBannerAdLoaded(object sender, EventArgs args) {
		banner = (BannerView)sender;
		if(this.IsHideBanner) {
			banner.Hide();
		}

		MyDebug.Log("GMAS::HandleAdLoaded => " + banner.adUnitId);
		if(this.OnBannerAdLoaded != null) {
			this.OnBannerAdLoaded.Invoke(banner.adUnitId);
		}
	}

	private void HandleOnBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		this.banner = (BannerView)sender;
		MyDebug.Log("GMAS::HandleAdFailedToLoad => " + this.banner.adUnitId + " - Message: " + args.Message);
		this.bannerViews.Remove(banner.adUnitId);
		if(this.OnBannerAdFailedToLoad != null) {
			this.OnBannerAdFailedToLoad.Invoke(this.banner.adUnitId, ErrorCode.NotRequested + " - " + args.Message);
		}
	}

	private void HandleOnBannerAdOpening(object sender, EventArgs args) {
		this.banner = (BannerView)sender;
		MyDebug.Log("GMAS::HandleAdOpened => " + this.banner.adUnitId);
		if(this.OnBannerAdOpening != null) {
			this.OnBannerAdOpening.Invoke(this.banner.adUnitId);
		}
	}

	private void HandleOnBannerAdClosed(object sender, EventArgs args) {
		this.banner = (BannerView)sender;
		MyDebug.Log("GMAS::GoogleMobileAdsScript => " + this.banner.adUnitId);
		this.bannerViews.Remove(this.banner.adUnitId);
		if(this.OnBannerAdClosed != null) {
			this.OnBannerAdClosed.Invoke(this.banner.adUnitId);
		}
	}

	private void HandleOnBannerAdLeftApplication(object sender, EventArgs args) {
		this.banner = (BannerView)sender;
		MyDebug.Log("GMAS::HandleAdLeftApplication => " + this.banner.adUnitId);
		if(this.OnBannerAdLeftApplication != null) {
			this.OnBannerAdLeftApplication.Invoke(this.banner.adUnitId);
		}
	}
#endif
	#endregion

	#endregion

	#region Interstitial Methods

	public void RequestInterstitial(string adUnitId) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestInterstitial => Request Start for: {0}", adUnitId));
		if(!this.interstitialAdViews.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.interstitialAdViews.Add(adUnitId, new InterstitialAd(adUnitId));
			// Register for ad events.
			this.interstitialAdViews[adUnitId].OnAdLoaded += this.HandleOnInterstitialLoaded;
			this.interstitialAdViews[adUnitId].OnAdFailedToLoad += this.HandleOnInterstitialFailedToLoad;
			this.interstitialAdViews[adUnitId].OnAdOpening += this.HandleOnInterstitialOpening;
			this.interstitialAdViews[adUnitId].OnAdClosed += this.HandleOnInterstitialClosed;
			this.interstitialAdViews[adUnitId].OnAdLeavingApplication += this.HandleOnInterstitialLeavingApplication;

			this.interstitialAdViews[adUnitId].LoadAd(this.CreateAdRequest());
			MyDebug.Log(string.Format("GMAS::RequestInterstitial => adUnitId: {0} requested", adUnitId));
		} else {
			MyDebug.Log(string.Format("GMAS::RequestInterstitial => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public bool IsInterstitialReady(string adUnitId) {
		bool retVal = false;
#if ADMOB
		if(!this.interstitialAdViews.ContainsKey(adUnitId)) {
			MyDebug.Log(string.Format("GMAS::IsInterstitialReady => adUnitId: {0} not requested yet", adUnitId));
			this.RequestInterstitial(adUnitId);
			retVal = false;
		} else {
			retVal = this.interstitialAdViews[adUnitId].IsLoaded();
		}
#endif
		return retVal;
	}

	public void ShowInterstitial(string adUnitId) {
#if ADMOB
		if(string.IsNullOrEmpty(adUnitId)) {
			MyDebug.Log("GMAS::ShowInterstitial => Empty adUnitId not Allowed...");
			if(this.OnInterstitialFailedToLoad != null) {
				this.OnInterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.EmptyAdUnitID + " - ");
			}
			return;
		}
		if(!this.interstitialAdViews.ContainsKey(adUnitId)) {
			MyDebug.Log(string.Format("GMAS::ShowInterstitial => adUnitId: {0} not requested yet", adUnitId));
			if(this.OnInterstitialFailedToLoad != null) {
				this.OnInterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.NotRequested + " - ");
			}
		} else if(this.interstitialAdViews[adUnitId].IsLoaded()) {
			this.interstitialAdViews[adUnitId].Show();
		} else {
			if(this.OnInterstitialFailedToLoad != null) {
				this.OnInterstitialFailedToLoad.Invoke(adUnitId, ErrorCode.Downloaing + " - ");
			}
			MyDebug.Log(string.Format("GMAS::ShowInterstitial => adUnitId: {0} not downloaded yet, please yet", adUnitId));
		}
#endif
	}

	#region callback handlers for this.interstitial

#if ADMOB
	private void HandleOnInterstitialLoaded(object sender, EventArgs args) {
		this.interstitial = (InterstitialAd)sender;
		MyDebug.Log("GMAS::HandleInterstitialLoaded => " + this.interstitial.adUnitId);
		if(this.OnInterstitialLoaded != null) {
			this.OnInterstitialLoaded.Invoke(this.interstitial.adUnitId);
		}
	}

	private void HandleOnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		this.interstitial = (InterstitialAd)sender;
		this.interstitialAdViews.Remove(this.interstitial.adUnitId);
		MyDebug.Log("GMAS::HandleInterstitialFailedToLoad => " + this.interstitial.adUnitId + " - Message: " + args.Message);
		if(this.OnInterstitialFailedToLoad != null) {
			this.OnInterstitialFailedToLoad.Invoke(this.interstitial.adUnitId, ErrorCode.NotRequested + " - " + args.Message);
		}
	}

	private void HandleOnInterstitialOpening(object sender, EventArgs args) {
		this.interstitial = (InterstitialAd)sender;
		MyDebug.Log("GMAS::HandleInterstitialOpened =>" + this.interstitial.adUnitId);
		if(this.OnInterstitialOpening != null) {
			this.OnInterstitialOpening.Invoke(this.interstitial.adUnitId);
		}
	}

	private void HandleOnInterstitialClosed(object sender, EventArgs args) {
		this.interstitial = (InterstitialAd)sender;
		MyDebug.Log("GMAS::HandleInterstitialClosed =>" + this.interstitial.adUnitId);
		if(this.OnInterstitialClosed != null) {
			this.OnInterstitialClosed.Invoke(this.interstitial.adUnitId);
		}
		this.RemoveInterstitial(this.interstitial.adUnitId);
	}

	private void HandleOnInterstitialLeavingApplication(object sender, EventArgs args) {
		this.interstitial = (InterstitialAd)sender;
		MyDebug.Log("GMAS::HandleInterstitialLeftApplication =>" + this.interstitial.adUnitId);
		if(this.OnInterstitialLeavingApplication != null) {
			this.OnInterstitialLeavingApplication.Invoke(this.interstitial.adUnitId);
		}
	}
#endif
	#endregion

	#endregion

	#region Reward Video Ads Method

	public void RequestRewardVideoAd(string adUnitId) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestRewardVideoAd => Request Start for: {0}", adUnitId));
		if(!this.rewardVideoAds.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.rewardVideoAds.Add(adUnitId, new RewardBasedVideoAd(adUnitId));
			// Register for ad events.
			this.rewardVideoAds[adUnitId].OnAdLoaded += this.HandleRewardAdLoaded;
			this.rewardVideoAds[adUnitId].OnAdFailedToLoad += this.HandleRewardAdFailedToLoad;
			this.rewardVideoAds[adUnitId].OnAdStarted += this.HandleRewardAdStarted;

			this.rewardVideoAds[adUnitId].OnAdOpening += this.HandleRewardAdOpening;
			this.rewardVideoAds[adUnitId].OnAdRewarded += this.HandleRewardBasedVideoRewarded;
			this.rewardVideoAds[adUnitId].OnAdClosed += this.HandleRewardAdClosed;
			this.rewardVideoAds[adUnitId].OnAdLeavingApplication += this.HandleRewardAdLeftApplication;
			// Load an this.interstitial ad.
			this.rewardVideoAds[adUnitId].LoadAd(this.CreateAdRequest());
			MyDebug.Log(string.Format("GMAS::RequestRewardVideoAd => adUnitId: {0} requested", adUnitId));
		} else {
			MyDebug.Log(string.Format("GMAS::RequestRewardVideoAd=> adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public bool IsRewardVideoAdReady(string adUnitId) {
		bool retVal = false;
#if ADMOB
		// Create an this.interstitial.
		if(!this.rewardVideoAds.ContainsKey(adUnitId)) {
			MyDebug.Log(string.Format("GMAS::IsRewardVideoAdReady => adUnitId: {0} not requested yet", adUnitId));
			RequestRewardVideoAd(adUnitId);
			retVal = false;
		} else {
			retVal = this.rewardVideoAds[adUnitId].IsLoaded();
		}
#endif
		return retVal;
	}

	public void ShowRewardVideoAd(string adUnitId) {
#if ADMOB
		if(string.IsNullOrEmpty(adUnitId)) {
			MyDebug.Log("GMAS::ShowRewardVideoAd => Empty adUnitId not Allowed...");
			if(this.OnRewardAdFailedToLoad != null) {
				this.OnRewardAdFailedToLoad.Invoke(adUnitId, ErrorCode.EmptyAdUnitID + " - ");
			}
			return;
		}
		if(!this.rewardVideoAds.ContainsKey(adUnitId)) {
			MyDebug.Log(string.Format("GMAS::ShowRewardVideoAd => adUnitId: {0} not requested yet", adUnitId));
			if(this.OnRewardAdFailedToLoad != null) {
				this.OnRewardAdFailedToLoad.Invoke(adUnitId, ErrorCode.NotRequested + " - ");
			}
		} else if(this.rewardVideoAds[adUnitId].IsLoaded()) {
			this.rewardVideoAds[adUnitId].Show();
		} else {
			if(this.OnRewardAdFailedToLoad != null) {
				this.OnRewardAdFailedToLoad.Invoke(adUnitId, ErrorCode.Downloaing + " - ");
			}
			MyDebug.Log(string.Format("GMAS::ShowRewardVideoAd => adUnitId: {0} not downloaded yet, please yet", adUnitId));
		}
#endif
	}

	#region callback handlers for Reward video

#if ADMOB
	private void HandleRewardAdLoaded(object sender, EventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardAdLoaded => " + this.rewardVideoAd.adUnitId);
		if(this.OnRewardAdLoaded != null) {
			this.OnRewardAdLoaded.Invoke(this.rewardVideoAd.adUnitId);
		}
	}

	private void HandleRewardAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		this.rewardVideoAds.Remove(this.rewardVideoAd.adUnitId);
		MyDebug.Log("GMAS::HandleRewardAdFailedToLoad => " + this.rewardVideoAd.adUnitId + " - Message: " + args.Message);
		if(this.OnRewardAdFailedToLoad != null) {
			this.OnRewardAdFailedToLoad.Invoke(this.rewardVideoAd.adUnitId, ErrorCode.NotRequested + " - " + args.Message);
		}
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardBasedVideoRewarded =>" + this.rewardVideoAd.adUnitId + ", Type: " + args.Type + ", Amount: " + args.Amount);
		if(this.OnRewardAdRewarded != null) {
			this.OnRewardAdRewarded.Invoke(this.rewardVideoAd.adUnitId, args);
		}
	}

	private void HandleRewardAdOpening(object sender, EventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardAdOpened =>" + this.rewardVideoAd.adUnitId);
		if(this.OnRewardAdOpening != null) {
			this.OnRewardAdOpening.Invoke(this.rewardVideoAd.adUnitId);
		}
	}

	private void HandleRewardAdStarted(object sender, EventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardAdStarted =>" + this.rewardVideoAd.adUnitId);
		if(this.OnRewardAdStarted != null) {
			this.OnRewardAdStarted.Invoke(this.rewardVideoAd.adUnitId);
		}
	}

	private void HandleRewardAdClosed(object sender, EventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardAdClosed =>" + this.rewardVideoAd.adUnitId);
		if(this.OnRewardAdClosed != null) {
			this.OnRewardAdClosed.Invoke(this.rewardVideoAd.adUnitId);
		}
	}

	private void HandleRewardAdLeftApplication(object sender, EventArgs args) {
		this.rewardVideoAd = (RewardBasedVideoAd)sender;
		MyDebug.Log("GMAS::HandleRewardAdLeftApplication =>" + this.rewardVideoAd.adUnitId);
		if(this.OnRewardAdLeavingApplication != null) {
			this.OnRewardAdLeavingApplication.Invoke(this.rewardVideoAd.adUnitId);
		}
	}
#endif
	#endregion

	#endregion

	#region Native Express Methods

	public void RequestNativeExpress(string adUnitId) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public void RequestNativeExpress(string adUnitId, AdSize size) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create a this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, size, AdPosition.Bottom));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already request", adUnitId));
		}


#endif
	}

	public void RequestNativeExpress(string adUnitId, AdPosition posiition) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create a this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, AdSize.MediumRectangle, posiition));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public void RequestNativeExpress(string adUnitId, AdSize size, AdPosition position) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, size, position));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public void RequestNativeExpress(string adUnitId, int x, int y) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, AdSize.MediumRectangle, x, y));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	public void RequestNativeExpress(string adUnitId, AdSize size, int x, int y) {
#if ADMOB
		MyDebug.Log(string.Format("GMAS::RequestNativeExpress => Request Start for: {0}", adUnitId));
		if(!this.nativeAdViews.ContainsKey(adUnitId)) {
			// Create an this.interstitial.
			this.nativeAdViews.Add(adUnitId, new NativeExpressAdView(adUnitId, size, x, y));
			this.BindNLoadNativeExpressAdView(adUnitId);
		} else {
			MyDebug.Log(string.Format("GMAS::RequestNativeExpress => adUnitId: {0} already requested", adUnitId));
		}
#endif
	}

	private void BindNLoadNativeExpressAdView(string adUnitId) {
#if ADMOB
		// Register for ad events.
		this.nativeAdViews[adUnitId].OnAdLoaded += this.HandleOnNativeExpressLoaded;
		this.nativeAdViews[adUnitId].OnAdFailedToLoad += this.HandleOnNativeExpressFailedToLoad;
		this.nativeAdViews[adUnitId].OnAdOpening += this.HandleOnNativeExpressOpening;
		this.nativeAdViews[adUnitId].OnAdClosed += this.HandleOnNativeExpressClosed;
		this.nativeAdViews[adUnitId].OnAdLeavingApplication += this.HandleOnNativeExpressLeavingApplication;

		this.nativeAdViews[adUnitId].LoadAd(this.CreateAdRequest());
		MyDebug.Log(string.Format("GMAS::BindNLoadNativeExpressAdView => adUnitId: {0} requested", adUnitId));
#endif
	}

	public void ShowNativeExpress(string adUnitId) {
#if ADMOB
		if(string.IsNullOrEmpty(adUnitId)) {
			MyDebug.Log("GMAS::ShowNativeExpress => Empty adUnitId not Allowed...");
			if(this.OnNativeExperssAdFailedToLoad != null) {
				this.OnNativeExperssAdFailedToLoad.Invoke(adUnitId, ErrorCode.EmptyAdUnitID + " - ");
			}
			return;
		}
		if(!interstitialAdViews.ContainsKey(adUnitId)) {
			MyDebug.Log(string.Format("GMAS::ShowNativeExpress => adUnitId: {0} not requested yet", adUnitId));
			if(this.OnNativeExperssAdFailedToLoad != null) {
				this.OnNativeExperssAdFailedToLoad.Invoke(adUnitId, ErrorCode.NotRequested + " - ");
			}
		} else if(this.nativeAdViews[adUnitId].IsLoaded()) {
			this.nativeAdViews[adUnitId].Show();
		} else {
			if(this.OnNativeExperssAdFailedToLoad != null) {
				this.OnNativeExperssAdFailedToLoad.Invoke(adUnitId, ErrorCode.Downloaing + " - ");
			}
			MyDebug.Log(string.Format("GMAS::ShowNativeExpress => adUnitId: {0} not downloaded yet, please yet", adUnitId));
		}
#endif
	}

	#region callback handlers for this.interstitial

#if ADMOB
	private void HandleOnNativeExpressLoaded(object sender, EventArgs args) {
		this.nativeAdView = (NativeExpressAdView)sender;
		MyDebug.Log("GMAS::HandleOnNativeExpressLoaded => " + this.nativeAdView.adUnitId);
		if(this.OnNativeExperssAdLoaded != null) {
			this.OnNativeExperssAdLoaded.Invoke(this.nativeAdView.adUnitId);
		}
	}

	private void HandleOnNativeExpressFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		this.nativeAdView = (NativeExpressAdView)sender;
		this.nativeAdViews.Remove(this.nativeAdView.adUnitId);
		MyDebug.Log("GMAS::HandleOnNativeExpressFailedToLoad => " + this.nativeAdView.adUnitId + " - Message: " + args.Message);
		if(this.OnNativeExperssAdFailedToLoad != null) {
			this.OnNativeExperssAdFailedToLoad.Invoke(this.nativeAdView.adUnitId, ErrorCode.NotRequested + " - " + args.Message);
		}
	}

	private void HandleOnNativeExpressOpening(object sender, EventArgs args) {
		nativeAdView = (NativeExpressAdView)sender;
		MyDebug.Log("GMAS::HandleOnNativeExpressOpening =>" + this.nativeAdView.adUnitId);
		if(this.OnNativeExperssAdOpening != null) {
			this.OnNativeExperssAdOpening.Invoke(this.nativeAdView.adUnitId);
		}
	}

	private void HandleOnNativeExpressClosed(object sender, EventArgs args) {
		this.nativeAdView = (NativeExpressAdView)sender;
		MyDebug.Log("GMAS::HandleOnNativeExpressClosed =>" + this.nativeAdView.adUnitId);
		if(this.OnNativeExperssAdClosed != null) {
			this.OnNativeExperssAdClosed.Invoke(this.nativeAdView.adUnitId);
		}
		this.RemoveNativeAd(nativeAdView);
	}

	private void HandleOnNativeExpressLeavingApplication(object sender, EventArgs args) {
		this.nativeAdView = (NativeExpressAdView)sender;
		MyDebug.Log("GMAS::HandleOnNativeExpressLeavingApplication =>" + this.nativeAdView.adUnitId);
		if(this.OnNativeExperssAdLeavingApplication != null) {
			this.OnNativeExperssAdLeavingApplication.Invoke(this.nativeAdView.adUnitId);
		}
	}
#endif
	#endregion

	#endregion
}
namespace GoogleMobileAds.Api {
	public enum ErrorCode {
		EmptyAdUnitID,
		NotRequested,
		Downloaing,
		SDKError
	}
}
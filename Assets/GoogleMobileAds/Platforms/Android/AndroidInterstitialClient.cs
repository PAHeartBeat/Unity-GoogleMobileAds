#if UNITY_ANDROID

using System;
using System.Collections.Generic;

using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Android
{
	internal class AndroidInterstitialClient : IGoogleMobileAdsInterstitialClient
	{
		private AndroidJavaObject interstitial;

		public AndroidInterstitialClient (IAdListener listener)
		{
			AndroidJavaClass playerClass = new AndroidJavaClass (Utils.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject> ("currentActivity");
			this.interstitial = new AndroidJavaObject (
				Utils.InterstitialClassName, activity, new AdListener (listener));
		}

		#region IGoogleMobileAdsInterstitialClient implementation

		public void CreateInterstitialAd (string adUnitId)
		{
			this.interstitial.Call ("create", adUnitId);
		}

		public void LoadAd (AdRequest request)
		{
			this.interstitial.Call ("loadAd", Utils.GetAdRequestJavaObject (request));
		}

		public bool IsLoaded ()
		{
			return this.interstitial.Call<bool> ("isLoaded");
		}

		public void ShowInterstitial ()
		{
			this.interstitial.Call ("show");
		}

		public void DestroyInterstitial ()
		{
			this.interstitial.Call ("destroy");
		}

		#endregion
	}
}

#endif

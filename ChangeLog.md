#GoogleAdsUnity
Test Google Ads for Unity3D

##Change Log
============
###v1.2.0
- Modfid bellow files for GoogleAdMob iOS SDK 7.0.0 
	- `Assets/Plugins/iOS/GADUBanner.m`
	- `Assets/Plugins/iOS/GADUInterstitial.h`
	- `Assets/Plugins/iOS/GADUInterstitial.m`
	- `Assets/Plugins/iOS/GADURequest.h`
	- `Assets/Plugins/iOS/GADURequest.h`
- Updated `UserGuide.md`
- This commit also need few change in GoogleAdMob iOS SDK v7.0.0 file. please read [User Guide.md from assets folder](https://github.com/PAHeartBeat/Unity-GoogleMobileAds/tree/master/Assets/UserGuide.md)
- May be this commit will not work with AdMob iOS SDK v6.12.2
- If you are still using Google AdMob iOS SDK 6.12.2 please use this commit  PAHeartBeat/Unity-GoogleMobileAds@f2ce9cf8bb676a1fc2ee0aa426e4cd2e7ab76502 
- No change made for Android Platform

###v1.1.2
- Minor Bugfixing
- Show Banner Ad is auto requeste Banner Ad if not requseted yet, but it also give error event, Now error event removes from Show Banner Ad
- Demo Script Modified with GUILayout insted-of using rects for GUI Button

###v1.1.1
- Minor Changes
- Created  seprate Demo script
- removed OnGUI and test adUnitId from script `GoogleMobileAdsScript`
- Also placed `UserGuide.md` file in `Asset` folder

###v1.1
- Android Support Add
- Android SDK used API 21
- Playservices used Rev. 22
- Event listenr added
- Singleton setting adds now developer access all public method via `GoogleMobileAdsScript.Me.<<Method>>`
- Preprocessor `TESTMODE_GOOGLEADS` used to display OnGUI buttons
	- For final build remove 'TESTMODE_GOOGLEADS' from Unity Build setting `Script define symbols`
- Fully functional (with only Google Ads package, other google / apple / 3rd party package not tested with cross package support) in iOS and Android
- `AndroidManifest_GoogleAdsBackup.xm`l placed if another package import in this Unity project and it will may be overwrite current `AndroidManifest.xml`


###v1.0.1
- Working Code
- Multiple Interstitial cache support
- Auto Cache when Interstitial closed
- Not Emplimeted yet to give back event info to user via custom event (using Unity `Action`)
- Updated `BannerView.cs` class
	- Add a extra Field `public string AdUnityID`;
	- Modified constructer n add a line 'AdUnityID = adUnitId;'
- Updated `InterstitialAd.cs` class
	- Add a extra Field `public string AdUnityID`;
	- Modified constructer n add a line 'AdUnityID = adUnitId;'


###v1.0
- Date: 2015-01-28
- Initial commit,
- Not working on xCode it;s throw some error after adding Google AdMob iOS SDK (as per step https://developers.google.com/mobile-ads-sdk/docs/admob/ios/quick-start#manually_using_the_sdk_download)




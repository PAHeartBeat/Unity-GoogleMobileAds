#GoogleAdsUnity
Test Google Ads for Unity3D

##Change Log
============
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




#GoogleAdsUnity
Test Google Ads for Unity3D

Modified: Feb 7th, 2015
iOS SDK Version: 7.0.0
Android: API v21 Playservices Rev. 22
Current Package: v1.2.0

##Guide
###iOS
1. Export XCode Folder from Unity3D
2. Open Project in XCode
3. Right Click on Unity-iPhone in XCode "Project Navigator"
4. Select `Add Files to Unity-iPhone` and select AdMob SKD 7.0.0 folder (you can download it from https://developers.google.com/mobile-ads-sdk/download#downloadios)
5. Check `Copy item if needed` and select `Create Group` in XCode Selection popup and click on Add
6. Select `Unity-iPhone` in project
7. Now Select Genral from top for `Unity-iPhone` in Tagets
8. Goes to Frameworks and Libraries Group
9. Now Add Missing Framework and Library files adn set as Required
	1. AdSupport
	2. AudioToolbox
	3. AVFoundation
	4. CoreGraphics
	5. CoreTelephony
	6. EventKit
	7. EventKitUI
	8. MessageUI
	9. StoreKit
	10. SystemConfiguration
	11. GoogleMobileAds
10. Open `GADRequest.h` file from GoogleAdMob SDK 7.0.0 (<<XCode Project Forlder>>/<<AdMobSDKFolder>>/GoogleMobileAds.framework/Headers/
11. Add this two line and save hader file after `@protocol GADAdNetworkExtras;` line (without single quote)
`// Add this constant to the testDevices property's array to receive test ads on the simulator.`
`#define GAD_SIMULATOR_ID @"Simulator"`
12. Now Hit `Cmd+B` and validate XCode Configuration


Note:
Perfrom Step 10 and 11 on AdMob SDK files before Import in project, so you can skip step 10 and 11 each project you develope unity project in future 

Optional Steps:
13. if its throw `Apple Mach-O Linker Error` for file not found `-liPhone-lib` goes 
Unity-iPhone in XCode "Project Navigator" and expand `Libraries` folder
14. Find `libiPhone-lib.a` file and remove referance using right click on file and `delete` option
15. select 'Remove Referance` from popup.
16. Now Right Click on `Libraries` folder and Select `Add Files to Unity-iPhone`
17. Now Add Referance of `libiPhone-lib.a` file
18. Now Clean project and rebuild again.

Wowww... it's done and you can check it with your iOS Device (I not tested it in simulator)

###Andorid
- Just Build Project and export it to APK it's ready to run project and tested as per `README.md`


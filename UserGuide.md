#GoogleAdsUnity
Test Google Ads for Unity3D

Modified: Jan 29th, 2015
iOS SDK Version: 6.12.2
Android: API v21 Playservices Rev. 22
Current Package: v1.1

##Guide
=========
###iOS
------
1. Export XCode Folder from Unity3D
2. Open Project in XCode
3. Right Click on Unity-iPhone in XCode "Project Navigator"
4. Select `Add Files to Unity-iPhone` and select AdMob SKD 6.12.12 (you can download it from https://developers.google.com/mobile-ads-sdk/download#downloadios)
5. Check `Copy item if needed` and select `Create Group` in XCode Selection popup and click on Add
6. Select `Unity-iPhone` in project
7. Add `-ObjC` in other Linker flag under Linking Group of Build Settings
8. Select `Unity-iPhone` in Tagets
9. Folow Step 7
10. Now Select Genral from top for `Unity-iPhone` in Tagets
11. Goes to Frameworks and Libraries Group
12. Now Add Missing Framework and Library files adn set as Required
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
	11. libGoogleAdMobAds.a
13. Now Hit `Cmd+B` and validate XCode Configuration

Optional Steps:

14. if its throw `Apple Mach-O Linker Error` for file not found `-liPhone-lib` goes 
Unity-iPhone in XCode "Project Navigator" and expand `Libraries` folder
15. Find `libiPhone-lib.a` file and remove referance using right click on file and `delete` option
16. select 'Remove Referance` from popup.
17. Now Right Click on `Libraries` folder and Select `Add Files to Unity-iPhone`
18. Now Add Referance of `libiPhone-lib.a` file
19. Now Clean project and rebuild again.

Wowww... it's done and you can check it with your iOS Device (I not tested it in simulator)

###Andorid
----------
- Just Build Project and export it to APK it's ready to run project and tested as per `README.md`


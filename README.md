## Adcash&reg; Plugin for Unity
---

### Getting started with the Adcash&reg; SDK
Supported platforms:

* Android
* iOS

The plugin features include:

* Mock ad calls when running inside Unity editor
* Support for Banner Ads
* Support for Interstitial Ads
* Banner positions(top/bottom)
* Banner ad events listeners
* A sample project to demonstrate the plugin integration

The plugin contains a `.unitypackage` file for to support easy import
of the plugin.

#### Requirements:

* Unity IDE 5
* Zone id (click [here](https://www.adcash.com/console/scripts.php) to create one).
* To deploy on **Android**:    
    * Adcash&reg; Android SDK library as [.JAR](https://www.adcash.com/downloadsv4/AdcashAndroidSDK_Files_Eclipse_v12.zip) or as [.AAR](https://www.adcash.com/downloadsv4/AdcashAndroidSDK_Files_Studio_v12.zip)
    * [Android SDK](https://developer.android.com/sdk/index.html#Other)
    * [Google Play services](http://developer.android.com/google/play-services/index.html) 4.0 or higher
    * Google Support Library v4
* To deploy on **iOS**:
    * Deployment target of iOS 6.0 or higher
    * Xcode 5.1 or higher 

### Integrate the Plugin into your Game
1. Open your project in the Unity editor.
2. Navigate to **Assets > Import Package > Custom Package...**
3. Select the `.unitypackage` file.
4. Import all of the files for the plugins by selecting **Import**. 
5. Make sure to check for any conflicts with files.

### Android Specific Setup
1. Add the `google-play-services_lib` folder located at: `ANDROID_SDK_LOCATION/extras/google/google_play_services/libproject` into the `Plugins/Android` folder of your project.
2. For users running a version of Unity earlier than 5.0:

    Navigate to `Temp/StagingArea` of your project directory and copy `AndroidManifest.xml` to `Assets/Plugins/Android`. Then add the following `meta-data` tag to the AndroidManifest.xml file:
   
    ```xml
    <activity android:name="com.unity3d.player.UnityPlayerActivity">
        <meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" 
            android:value="true" />
    </activity>
    ```

3. If you use Interstitial in your app, add next code in your Manifest file:

    ```xml
    <activity android:name=".InterstitialDemoActivity"
        android:label="@string/app_name" >
        <intent-filter>
            <action android:name="android.intent.action.MAIN" />
            <category android:name="android.intent.category.LAUNCHER" />
        </intent-filter>
    </activity>
    ```

### Adcash&reg; SDK Unity API
The remainder of this guide assumes you are now attempting to write your own
code to integrate the Adcash&reg; SDK into your game.

### Banner
Here is a snippet showing how to create a banner:

```csharp
using AdcashSDK.Api;
...
// Create a 320x50 banner at the top of the screen.
BannerView bannerView = new BannerView("YOUR_ZONE_ID", AdSize.AdSizeSmartBanner, AdPosition.Top);
// Load the banner with the request.
bannerView.LoadAd(request);
```

The `AdPosition` enum specifies where to place the banner. The following constants are the possible ad positions:

* `AdPosition.Top`
* `AdPosition.Bottom`

The banner lifecycle is fairly straightforward:

* To load a banner:

	```csharp
	bannerView.LoadAd();
	```

* By default, banners are visible. To temporarily hide a banner:

    ```csharp
    bannerView.Hide();
    ```

* To show it again:

    ```csharp
    bannerView.Show();
    ```

* When you are finished with a banner make sure to destroy it before dropping
your reference to it. This lets the plugin know you no longer need the object and can do any necessary cleanup on your behalf:

    ```csharp
    bannerView.Destroy();
    ```

### Interstitial
The following is a snippet to create an interstitial:

```csharp
using Plugin.Api;
...
// Initialize an Interstitial.
Interstitial interstitial = new Interstitial("MY_AD_UNIT_ID"); 
   
// Load the interstitial with the request.
interstitial.LoadAd();
```

Unlike banners, the interstitials need to be explicitly shown. At an appropriate point in your app you should check that the interstitial is ready before showing it:

```csharp
if (interstitial.IsLoaded()) {
    interstitial.Show();
}
```

Similar to banners, the interstitials also have a destroy method:

```csharp
interstitial.Destroy();
```

### Ad Events
Both `BannerView` and `Interstitial` classes contain the same ad events that you can
register for. These events are of type
[EventHandler](http://msdn.microsoft.com/en-us/library/db0etb8x%28v=vs.110%29.aspx).
Here is an example of how to register ad events on a banner:

```csharp
using AdcashSDK.Api;
...

BannerView bannerView = new BannerView("YOUR_ZONE_ID", AdSize.AdSizeSmartBanner, AdPosition.Top);
bannerView.LoadAd();

// Called when an ad request has successfully loaded.
bannerView.AdLoaded += HandleAdLoaded;
// Called when an ad request failed to load.
bannerView.AdFailedToLoad += HandleAdFailedToLoad;
// Called when an ad is clicked.
bannerView.AdOpened += HandleAdOpened;
// Called when the user is about to return to the app after an ad click.
bannerView.AdClosing += HandleAdClosing;
// Called when the user returned from the app after an ad click.
bannerView.AdClosed += HandleAdClosed;
// Called when the ad click caused the user to leave the application.
bannerView.AdLeftApplication += HandleAdLeftApplication;
...

public void HandleAdLoaded(object sender, EventArgs args)
{
    print("HandleAdLoaded event received.");
    // Handle the ad loaded event.
}
```

The only event with special event arguments is `AdFailedToLoad`. It passes an
instance of `AdFailedToLoadEventArgs` with a `Message` describing the error:

```csharp
public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
  print("Interstitial Failed to load: " + args.Message);
  // Handle the ad failed to load event.
};
```

You only have to register for the events you need to use.

### Conversion tracking
If you are an advertiser, you might want to send us some information when your app has been opened so that we can **track a successful conversion**. Keep in mind that only the first time the app is opened is considered a successful conversion. 

You can do this using the `ConversionTracker` class:

```csharp
using AdcashSDK.Api;

...
void OnGUI()
{
	...
	ConversionTracker tracker = new ConversionTracker();
	tracker.ReportAppOpen();
}
```

### Running the sample project
1. Clone the repo:

	```bash
	git clone https://github.com/adcash/adcash-unity-sdk.git
	```
2. From the **Unity IDE** select **Open Project** and choose the `adcash-unity-sdk/HelloWorld` folder.
3. From the menu select **Assets**->**Import package**->**Custom package..**, choose `AdcashUnitySDK.unitypackage` and click **Import**
4. Select **File**->**Build & Run** (`⌘ + B`).
5. Press **Add Current** to add the current scene for build
6. Select your platform (iOS or Android).
7. Press **Build and Run** (`⌘ + B`).
8. (iOS) Wait for Xcode to start and load.
9. (iOS) Build and run (`⌘ + R`) in Xcode to start the app on your device.
10. Enjoy :)

### Support
If you need any support or assistance you can contact us by sending email to <mobile@adcash.com>.
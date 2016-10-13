Supported platforms:

* Android
* iOS

The plugin features include:

* Mock ad calls when running inside Unity editor
* Support for Banner Ads
* Support for Interstitial Ads
* Support for Video Interstitial Ads
* Banner positions (top/bottom)
* Banner ad events listeners

The plugin contains a `.unitypackage` file for to support easy import of the plugin.

## Requirements

* Unity IDE 5
* Zone id (click [here](https://www.adcash.com/console/scripts.php) to create one).
* To deploy on **Android**:    
    * [Android SDK](https://developer.android.com/sdk/index.html#Other)
    * Deployment target of Android API 9 or higher
* To deploy on **iOS**:
    * Xcode 5.1 or higher
    * Deployment target of iOS 7.0 or higher

## Integrate the Plugin

#### Android Specific Setup
**If** your project already contains a **AndroidManifest.xml** file in the Assets/Plugins/Android folder, then there is no need to import it in the next step. Only thing you need to do, if you plan to use interstitial/video ads is to add next lines **<application>** to body of your existing **AndroidManifest.xml**:    
```xml    
<activity
    android:name="com.adcash.mobileads.ui.AdcashActivity"
    android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize" 
    android:theme="@android:style/Theme.Translucent"
    android:hardwareAccelerated="true" /> 
```

#### Import Adcash package
1. Open your project in the Unity editor.
2. Navigate to **Assets > Import Package > Custom Package...**
3. Select the `.unitypackage` file.
4. Import all of the files for the plugins by selecting **Import**. 
5. Make sure to check for any conflicts with files.

#### Adcash SDK Unity API
The remainder of this guide assumes you are now attempting to write your own code to integrate the Adcash SDK into your game.

## Banner
Here is a snippet showing how to create a banner:

```csharp
using AdcashSDK.Api;
...
// Create a banner at the top of the screen.
BannerView bannerView = new BannerView("YOUR_ZONE_ID", AdPosition.Top);
// Load the banner with the request.
bannerView.LoadAd();
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

* When you are finished with a banner make sure to destroy it before dropping your reference to it. This lets the plugin know you no longer need the object and can do any necessary cleanup on your behalf:

    ```csharp
    bannerView.Destroy();
    ```

## Interstitial
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

## Video 
 > (**Video calls on Android as same as Interstitials**)

The following is a snippet to create a video on iOS:

```csharp
using Plugin.Api;
...
// Initialize and load a Video.
VideoIOS video = new VideoIOS("MY_AD_UNIT_ID"); 
```

At an appropriate point in your app you should check that the video is ready before showing it:


```csharp
if (video != null)) {
    video.Play();
}
```

Alternatively, to play video just after loading completed;

```csharp
public void HandleVideoLoaded (object sender, EventArgs args){
	video.Play();
}
```

## Ad Events
`BannerView`, `Interstitial` and `Video` classes contain the same ad events that you can register for. These events are of type [EventHandler](http://msdn.microsoft.com/en-us/library/db0etb8x%28v=vs.110%29.aspx).   
Here is an example of how to register ad events on a banner:

```csharp
using AdcashSDK.Api;
...

BannerView bannerView = new BannerView("YOUR_ZONE_ID", AdPosition.Top);
bannerView.LoadAd();

// Called when an ad request has successfully loaded.
bannerView.AdLoaded += HandleAdLoaded;
// Called when an ad request failed to load.
bannerView.AdFailedToLoad += HandleAdFailedToLoad;
// Called when an ad is clicked.
bannerView.AdOpened += HandleAdOpened;
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

The only event with special event arguments is `AdFailedToLoad`. It passes an instance of `AdFailedToLoadEventArgs` with a `Message` describing the error:

```csharp
public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
  print("Interstitial Failed to load: " + args.Message);
  // Handle the ad failed to load event.
};
```

You only have to register for the events you need to use.

## Support
If you need any support or assistance you can contact us by sending email to <mobile@adcash.com>.
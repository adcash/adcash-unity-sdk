## Supported platforms:

* Android
* iOS

## The plugin features include:

* Support for Banner Ads
* Support for Interstitial Ads
* Support for Rewarded Video Ads
* Banner positions (top/bottom)

The plugin contains a `.unitypackage` file to support easy import of the plugin.

## Requirements

* Unity IDE 5
* Zone id (click [here](https://www.adcash.com/console/scripts.php) to create one).
* [Adcash Unity plugin](https://github.com/adcash/adcash-unity-sdk/raw/master/AdcashSDK.unitypackage).  
* To deploy on **Android**:    
    * [Android SDK](https://developer.android.com/sdk/index.html#Other)
    * Deployment target of Android API 9 or higher
* To deploy on **iOS**:
    * Xcode 5.1 or higher
    * Deployment target of iOS 7.0 or higher

## Integrate the Plugin

### Import Adcash package

##### Android Specific Setup
<b>If</b> your project already contains a <b>AndroidManifest.xml</b> file in the Assets/Plugins/Android folder, then there is no need to import it in the next step, just make this changes:
1. Add the following `uses-permission` tags: 
    * INTERNET - required to allow the Adcash SDK to make ad requests.
    * ACCESS_NETWORK_STATE - used to check the Network connection availability.  
```xml  
<uses-permission android:name="android.permission.INTERNET"/>
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
```

2. Add <b>AdcashActivity</b> for full screen ads to work (interstitial and rewarded video)  
```xml
<activity
    android:name="com.adcash.mobileads.ui.AdcashActivity"  
    android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize" 
    android:theme="@style/AdcashTheme"
    android:hardwareAccelerated="true" /> 
```

##### General setup

1. Open your project in the Unity editor.
2. Navigate to <b>Assets > Import Package > Custom Package...</b>
3. Select the `.unitypackage` file.
4. Import all of the files for the plugins by selecting <b>Import</b>. 
5. Make sure to check for any conflicts with files.

## Adcash SDK Unity API
The remainder of this guide assumes you are now attempting to write your own code to integrate the Adcash SDK into your game.


### Banner
Here is a snippet showing how to create a banner:

```csharp
using AdcashSDK.Api;
...
// Create a banner at the top of the screen.
BannerView bannerView = new BannerView("YOUR_ZONE_ID", AdPosition.Top);
// Load the banner with the request.
bannerView.LoadAd();
// Don't forget to call bannerView.Destroy() when you don't need bannerView any more.
```

**More detailed description**

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

### Interstitial
The following is a snippet to create an interstitial:

```csharp
using AdcashSDK.Api;
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

### Rewarded Video

The following is a snippet to create a rewarded video:

```csharp
using AdcashSDK.Api;
...
// Initialize and load a Rewarded Video.
RewardedVideo rewardedVideo = new RewardedVideo ("MY_AD_UNIT_ID"); 
rewardedVideo.AdLoaded += HandleVideoLoaded;
rewardedVideo.AdReward += HandleVideoRewarded;
rewardedVideo.LoadAd();
```

To play video just after loading completed as well as to give reward;

```csharp
public void HandleVideoLoaded (object sender, EventArgs args){
    rewardedVideo.Show();
}

public void HandleVideoRewarded (object sender, AdRewardEventArgs args){
    // Give user reward here
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

public void HandleAdLoaded(object sender, EventArgs args) {
    print("HandleAdLoaded event received.");
    // Handle the ad loaded event.
}
```

There are two special events that are with arguments.
First is `AdFailedToLoad`. It passes an instance of `AdFailedToLoadEventArgs` with a `Message` describing the error:
```csharp
public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
  print("Ad failed to load: " + args.Message);
  // Handle the ad failed to load event.
};
```

Second is `AdReward` event. This event is only for `RewardedVideo` ad type and it contains reward information if it was specified in publisher panel:
```csharp
public void HandleVideoReward(object sender, AdRewardEventArgs args) {
    print("You received " + args.Amount + " " + args.Name);
    // Handle rewarding your user.
};
```

You only have to register for the events you need to use.

## Support
If you need any support or assistance you can contact us by sending email to <mobile@adcash.com>.
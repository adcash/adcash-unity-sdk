using System;
using UnityEngine;
using AdcashSDK;
using AdcashSDK.Api;


public class AdcashDemoScript : MonoBehaviour
{

	private BannerView bannerView;
	private Interstitial interstitial;
	private RewardedVideo rewardedVideo;

	static float buttonHeight;
	static float buttonWidth;
	static float buttonPaddingVertical;
	static float buttonPaddingHorizontal;
	static float buttonsCount;
	static bool isLoading = false;

	void OnGUI ()
	{
		GUI.skin.button.fontSize = (int)(0.04f * Screen.height);
		GUI.skin.label.fontSize = (int)(0.03f * Screen.height);
		buttonHeight = 0.08f * Screen.height;
		buttonWidth = 0.8f * Screen.width;
		buttonPaddingVertical = 0.02f * Screen.height;
		buttonPaddingHorizontal = (Screen.width - buttonWidth) / 2f;
		buttonsCount = 0f;

		Rect showBannerRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (showBannerRect, "Show Banner")) {
			if (bannerView == null) {
				bannerView = InitBanner ();
			}
			bannerView.Show ();
		}

		Rect hideBannerRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (hideBannerRect, "Hide Banner")) {
			bannerView.Hide ();
		}

		Rect destroyBannerRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (destroyBannerRect, "Destroy Banner")) {
			bannerView.Destroy ();
			bannerView = null;
		}

		Rect showInterstitialRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (showInterstitialRect, "Show Interstitial")) {
			isLoading = true;
			RequestInterstitial ();
		}

		Rect loadVideoRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (loadVideoRect, "Load Rewarded Video")) {
			isLoading = true;
			RequestRewardedVideo ();
		}

		Rect showVideoRect = CreateNewButtonPlaceHolder ();
		if (GUI.Button (showVideoRect, "Show Rewarded Video")) {
			ShowVideo ();
		}

		DisplayStatus ();
	}

	private Rect CreateNewButtonPlaceHolder ()
	{
		float positionY;
		positionY = buttonHeight + buttonPaddingVertical;
		positionY = positionY * buttonsCount;
		positionY = positionY + buttonPaddingVertical * 2.5f;
		buttonsCount = buttonsCount + 1f;

		// return new Rect(buttonPaddingHorizontal, (buttonsCount*(buttonHeight+buttonPaddingVertical))+buttonPaddingVertical, buttonWidth, buttonHeight);
		return new Rect (buttonPaddingHorizontal, positionY,
			buttonWidth, buttonHeight);
	}

	private void DisplayStatus() {
		string message;
		if (isLoading) {
			message = "Loading...";
		} else {
			message = "";
		}
		GUI.Label(new Rect(0, 0, Screen.width, buttonHeight), message);
	}

	private void RequestRewardedVideo ()
	{
		#if UNITY_ANDROID
		string adUnitId = "1461193";
		#elif UNITY_IOS
		string adUnitId = "1587947";
		#else
		string adUnitId = "unused";
		#endif

		isLoading = true;
		rewardedVideo = new RewardedVideo (adUnitId);
		// Register for ad events.
		rewardedVideo.AdLoaded += HandleVideoLoaded;
		rewardedVideo.AdFailedToLoad += HandleVideoFailedToLoad;
		rewardedVideo.AdOpened += HandleVideoOpened;
		rewardedVideo.AdReward += HandleVideoRewarded;
		rewardedVideo.AdClosed += HandleVideoClosed;
		rewardedVideo.AdLeftApplication += HandleVideoLeftApplication;
	}

	private BannerView InitBanner ()
	{
		isLoading = true;

		#if UNITY_ANDROID
		string adUnitId = "1461185";
		#elif UNITY_IOS
		string adUnitId = "1461197";
		#else
		string adUnitId = "unused";
		#endif

		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView (adUnitId, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd ();

		return bannerView;
	}

	private void RequestInterstitial ()
		{
		isLoading = true;

		#if UNITY_ANDROID
		string adUnitId = "1461177";
		#elif UNITY_IOS
		string adUnitId = "1587903";
		#else
		string adUnitId = "unused";
		#endif

		// Create an interstitial.
		interstitial = new Interstitial (adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd ();
	}


	private void ShowInterstitial ()
	{
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} else {
			print ("Interstitial is not ready yet.");
		}
	}

	private void ShowVideo ()
	{
		if (rewardedVideo != null) {
		    rewardedVideo.Show ();
		}

	}

	#region Banner callback handlers

	public void HandleAdLoaded (object sender, EventArgs args)
	{
		print ("HandleAdLoaded event received.");
		isLoading = false;
	}

	public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		print ("HandleFailedToReceiveAd event received with message: " + args.Message);
		isLoading = false;
	}

	public void HandleAdOpened (object sender, EventArgs args)
	{
		print ("HandleAdOpened event received");
	}

	public void HandleAdClosed (object sender, EventArgs args)
	{
		print ("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication (object sender, EventArgs args)
	{
		print ("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded (object sender, EventArgs args)
	{
		print ("HandleInterstitialLoaded event received.");
		ShowInterstitial ();
		isLoading = false;
	}

	public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		print ("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		isLoading = false;
	}

	public void HandleInterstitialOpened (object sender, EventArgs args)
	{
		print ("HandleInterstitialOpened event received");
	}

	public void HandleInterstitialClosed (object sender, EventArgs args)
	{
		print ("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication (object sender, EventArgs args)
	{
		print ("HandleInterstitialLeftApplication event received");
	}

	#endregion

	#region Video callback handlers

	public void HandleVideoLoaded (object sender, EventArgs args)
	{
		print ("HandleVideoLoaded event received.");
		isLoading = false;
	}

	public void HandleVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		print ("HandleVideoFailedToLoad event received with message: " + args.Message);
		isLoading = false;
	}

	public void HandleVideoOpened (object sender, EventArgs args)
	{
		print ("HandleVideoOpened event received");
	}

	public void HandleVideoRewarded (object sender, AdRewardEventArgs args)
	{
		print ("HandleVideoReward event received " + args.Amount + " " + args.Name);
	}

	public void HandleVideoClosed (object sender, EventArgs args)
	{
		print ("HandleVideoClosed event received");
	}

	public void HandleVideoLeftApplication (object sender, EventArgs args)
	{
		print ("HandleVideoLeftApplication event received");
	}

	#endregion
}

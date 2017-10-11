#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AdcashSDK.Api;

namespace AdcashSDK.iOS
{
	// Externs used by the iOS component.
	internal class Externs
	{
		#region Common externs

		[DllImport("__Internal")]
		internal static extern void ACURelease(IntPtr obj);

		#endregion

		#region Banner externs

		[DllImport("__Internal")]
		internal static extern IntPtr ACUCreateSmartBannerView(
			IntPtr bannerClient, string adUnitId, int positionAtTop);
		/*
		[DllImport("__Internal")]
		internal static extern IntPtr ACUCreateSmartBannerView(
			IntPtr bannerClient, string adUnitId, int positionAtTop);
		*/
		[DllImport("__Internal")]
		internal static extern void ACUSetBannerCallbacks(
			IntPtr bannerView,
			IOSBannerClient.ACUAdViewDidReceiveAdCallback adReceivedCallback,
			IOSBannerClient.ACUAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback,
			IOSBannerClient.ACUAdViewWillPresentScreenCallback willPresentCallback,
			IOSBannerClient.ACUAdViewWillDismissScreenCallback willDismissCallback,
			IOSBannerClient.ACUAdViewWillLeaveApplicationCallback willLeaveCallback);

		[DllImport("__Internal")]
		internal static extern void ACUHideBannerView(IntPtr bannerView);

		[DllImport("__Internal")]
		internal static extern void ACUShowBannerView(IntPtr bannerView);

		[DllImport("__Internal")]
		internal static extern void ACURemoveBannerView(IntPtr bannerView);

		[DllImport("__Internal")]
		internal static extern void ACURequestBannerAd(IntPtr bannerView);

		#endregion

		#region Interstitial externs

		[DllImport("__Internal")]
		internal static extern IntPtr ACUCreateInterstitial(
			IntPtr interstitialClient, string adUnitId);

		[DllImport("__Internal")]
		internal static extern void ACUSetInterstitialCallbacks(
			IntPtr interstitial,
			IOSInterstitialClient.ACUInterstitialDidReceiveAdCallback adReceivedCallback,
			IOSInterstitialClient.ACUInterstitialDidFailToReceiveAdWithErrorCallback
			adFailedCallback,
			IOSInterstitialClient.ACUInterstitialWillPresentScreenCallback willPresentCallback,
			IOSInterstitialClient.ACUInterstitialWillDismissScreenCallback willDismissCallback,
			IOSInterstitialClient.ACUInterstitialWillLeaveApplicationCallback
			willLeaveCallback);

		[DllImport("__Internal")]
		internal static extern bool ACUInterstitialReady(IntPtr interstitial);

		[DllImport("__Internal")]
		internal static extern void ACUShowInterstitial(IntPtr interstitial);

		[DllImport("__Internal")]
		internal static extern void ACURequestInterstitial(IntPtr interstitial);

		#endregion

		#region Conversion tracker externs
/*
		[DllImport("__Internal")]
		internal static extern void ACUReportAppOpenConversion(int campaignId, string conversionType, Dictionary<string,string> otherParams);
*/
		#endregion

/*
		#region Video externs

		[DllImport("__Internal")]
		internal static extern void ACUShowVideo (IntPtr video);

		[DllImport("__Internal")]
		internal static extern IntPtr ACUCreateVideo(IntPtr videoClient, string zoneID);

		[DllImport("__Internal")]
		internal static extern IntPtr ACUSetVideoCallbacks(
		IntPtr video,
		IOSVideoClient.ACUVideoDidReceiveAdCallback adReceivedCallback,
		IOSVideoClient.ACUVideoDidFailToReceiveAdCallback adFailedCallback,
		IOSVideoClient.ACUVideoWillPresentScreenCallback willPresentCallback,
		IOSVideoClient.ACUVideoWillDismissScreenCallback willDismissCallback,
		IOSVideoClient.ACUVideoDidDismissScreenCallback didDismissCallback,
		IOSVideoClient.ACUVideoWillLeaveApplicationCallback willLeaveCallback);
		#endregion
*/

		#region Rewarded Video externs

		[DllImport("__Internal")]
		internal static extern void ACUShowRewardedVideo (IntPtr rewardedVideo);

		[DllImport("__Internal")]
		internal static extern IntPtr ACUCreateRewardedVideo(IntPtr rewardedVideoClient, string zoneID);

		[DllImport("__Internal")]
		internal static extern IntPtr ACUSetRewardedVideoCallbacks(
		IntPtr rewardedVideo,
		IOSRewardedVideoClient.ACURewardedVideoDidReceiveAdCallback adReceivedCallback,
		IOSRewardedVideoClient.ACURewardedVideoDidFailToReceiveAdCallback adFailedCallback,
		IOSRewardedVideoClient.ACURewardedVideoWillPresentScreenCallback willPresentCallback,
		IOSRewardedVideoClient.ACURewardedVideoWillDismissScreenCallback willDismissCallback,
		IOSRewardedVideoClient.ACURewardedVideoDidDismissScreenCallback didDismissCallback,
		IOSRewardedVideoClient.ACURewardedVideoWillLeaveApplicationCallback willLeaveCallback,
		IOSRewardedVideoClient.ACURewardedVideoDidCompleteWithRewardCallback completedWithRewardCallback);
		#endregion
	}
}
#endif

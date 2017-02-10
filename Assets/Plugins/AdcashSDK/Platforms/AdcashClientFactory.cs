using System;
using AdcashSDK.Common;

namespace AdcashSDK
{
	internal class AdcashClientFactory
	{
		internal static IAdcashBannerClient GetAdcashBannerClient(
			IAdListener listener)
		{
			#if UNITY_ANDROID
			return new AdcashSDK.Android.AndroidBannerClient(listener);
			#elif UNITY_IOS
			return new AdcashSDK.iOS.IOSBannerClient(listener);
			#else
			return new AdcashSDK.Common.DummyClient(listener);
			#endif
		}

		internal static IAdcashInterstitialClient GetAdcashInterstitialClient(
			IAdListener listener)
		{
			#if UNITY_ANDROID
			return new AdcashSDK.Android.AndroidInterstitialClient(listener);
			#elif UNITY_IOS
			return new AdcashSDK.iOS.IOSInterstitialClient(listener);
			#else
			return new AdcashSDK.Common.DummyClient(listener);
			#endif
		}


		internal static IAdcashRewardedVideoClient GetAdcashRewardedVideoClient( IAdListener listener)
		{
			#if UNITY_ANDROID
			return new AdcashSDK.Android.AndroidRewardedVideoClient(listener);
			#elif UNITY_IOS
			return new AdcashSDK.iOS.IOSRewardedVideoClient(listener);
			#else
			return new AdcashSDK.Common.DummyClient();
			#endif
		}
	}
}

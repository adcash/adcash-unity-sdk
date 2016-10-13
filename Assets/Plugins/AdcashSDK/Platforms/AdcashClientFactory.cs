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

		internal static IAdcashConversionTrackerClient GetAdcashConversionTrackerClient()
		{
			#if UNITY_ANDROID
			return new AdcashSDK.Android.AndroidConversionTrackerClient();
			#elif UNITY_IOS
			return new AdcashSDK.iOS.IOSConversionTrackerClient();
			#else
			return new AdcashSDK.Common.DummyClient();
			#endif
		}


		internal static IAdcashVideoClient GetAdcashVideoClient( IAdListener listener)
		{
			#if UNITY_ANDROID
			return new AdcashSDK.Android.AndroidVideoClient(listener);
			#elif UNITY_IOS
			return new AdcashSDK.iOS.IOSVideoClient(listener);
			#else
			return new AdcashSDK.Common.DummyClient();
			#endif
		}
	}
}

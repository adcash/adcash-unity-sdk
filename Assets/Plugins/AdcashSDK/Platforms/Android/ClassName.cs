#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;
using AdcashSDK.Api;

namespace AdcashSDK.Android
{
	internal class ClassName
	{
		#region Fully-qualified class names
		
		#region Adcash Mobile Ads SDK class names	
		/*public const string AdListenerClassName = "com.google.android.gms.ads.AdListener";*/
		#endregion
		
		#region Adcash Mobile Ads Unity Plugin class names		
		public const string BannerViewClassName = "com.adcash.unity.mobileads.Banner";
		public const string InterstitialClassName = "com.adcash.unity.mobileads.Interstitial";
		public const string RewardedVideoClassName = "com.adcash.unity.mobileads.RewardedVideo";
		public const string UnityAdListenerClassName = "com.adcash.unity.mobileads.UnityAdListener";
		public const string UnityRewardedAdListenerClassName = "com.adcash.unity.mobileads.UnityRewardedAdListener";
		#endregion
		
		#region Unity class names	
		public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";		
		#endregion
		
		#endregion
	}
}
#endif
#if UNITY_ANDROID
using System;
using System.Collections.Generic;

using UnityEngine;

using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AndroidVideoClient : IAdcashVideoClient
	{
		private AndroidJavaObject interstitial;
		
		public AndroidVideoClient(IAdListener listener)
		{
			AndroidJavaClass playerClass = new AndroidJavaClass(ClassName.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			interstitial = new AndroidJavaObject(
				ClassName.InterstitialClassName, activity, new AdListener(listener));
		}
		
		#region IAdcashVideoClient implementation
		
		public void CreateVideoAd(string adUnitId) {
			interstitial.Call("create", adUnitId);
			LoadAd();
		}

		public void Play() {
			if (IsLoaded()) {
				ShowVideo();
			}
		}

		public void LoadAd() {
			interstitial.Call("loadAd");
		}
		
		public bool IsLoaded() {
			return interstitial.Call<bool>("isLoaded");
		}
		
		public void ShowVideo() {
			interstitial.Call("showAd");
		}
		
		public void Destroy() {
			interstitial.Call("destroy");
		}
		
		#endregion
	}
}
#endif
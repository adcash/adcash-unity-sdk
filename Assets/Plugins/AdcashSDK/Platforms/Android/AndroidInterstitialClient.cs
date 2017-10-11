#if UNITY_ANDROID
using System;
using System.Collections.Generic;

using UnityEngine;

using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AndroidInterstitialClient : IAdcashInterstitialClient
	{
		private AndroidJavaObject interstitial;
		
		public AndroidInterstitialClient(IAdListener listener)
		{
			AndroidJavaClass playerClass = new AndroidJavaClass(ClassName.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			interstitial = new AndroidJavaObject(
				ClassName.InterstitialClassName, activity, new AdListener(listener));
		}
		
		#region IAdcashInterstitialClient implementation
		
		public void CreateInterstitialAd(string adUnitId) {
			interstitial.Call("create", adUnitId);
		}
		
		public void LoadAd() {
			interstitial.Call("loadAd");
		}
		
		public bool IsLoaded() {
			return interstitial.Call<bool>("isLoaded");
		}
		
		public void ShowInterstitial() {
			interstitial.Call("show");
		}
		
		public void DestroyInterstitial() {
			interstitial.Call("destroy");
		}
		
		#endregion
	}
}
#endif
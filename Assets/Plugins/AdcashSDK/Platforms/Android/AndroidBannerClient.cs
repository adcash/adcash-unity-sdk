#if UNITY_ANDROID
using System;
using System.Collections.Generic;

using UnityEngine;

using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AndroidBannerClient : IAdcashBannerClient
	{
		private AndroidJavaObject bannerView;
		
		public AndroidBannerClient(IAdListener listener)
		{
			AndroidJavaClass playerClass = new AndroidJavaClass(ClassName.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			bannerView = new AndroidJavaObject(
				ClassName.BannerViewClassName, activity, new AdListener(listener));
		}
		
		// Creates a banner view.
		public void CreateBannerView(String adUnitId, AdPosition position) {
			bannerView.Call("create",
			                new object[2] { adUnitId, (int)position });
		}
		
		// Load an ad.
		public void LoadAd()
		{
			bannerView.Call("loadAd");
		}
		
		// Show the banner view on the screen.
		public void ShowBannerView() {
			bannerView.Call("show");
		}
		
		// Hide the banner view from the screen.
		public void HideBannerView()
		{
			bannerView.Call("hide");
		}
		
		public void DestroyBannerView()
		{
			bannerView.Call("destroy");
		}
	}
}
#endif
#if UNITY_ANDROID
using System;
using System.Collections.Generic;

using UnityEngine;

using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AndroidRewardedVideoClient : IAdcashRewardedVideoClient
	{
		private AndroidJavaObject rewardedVideo;
		
		public AndroidRewardedVideoClient(IAdListener listener)
		{
			AndroidJavaClass playerClass = new AndroidJavaClass(ClassName.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			rewardedVideo = new AndroidJavaObject(
				ClassName.RewardedVideoClassName, activity, new RewardedAdListener(listener));
		}
		
		#region IAdcashRewardedVideoClient implementation
		
		public void CreateVideoAd(string adUnitId) {
			rewardedVideo.Call("create", adUnitId);
			LoadAd();
		}

		public void Play() {
			if (IsLoaded()) {
				ShowVideo();
			}
		}

		public void LoadAd() {
			rewardedVideo.Call("loadAd");
		}
		
		public bool IsLoaded() {
			return rewardedVideo.Call<bool>("isLoaded");
		}
		
		public void ShowVideo() {
			rewardedVideo.Call("show");
		}
		
		public void Destroy() {
			rewardedVideo.Call("destroy");
		}
		
		#endregion
	}
}
#endif
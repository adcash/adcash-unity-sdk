#if UNITY_ANDROID
using UnityEngine;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class RewardedAdListener : AndroidJavaProxy
	{
		private IAdListener listener;
		internal RewardedAdListener(IAdListener listener)
			: base(ClassName.UnityRewardedAdListenerClassName)
		{
			this.listener = listener;
		}
		
		void onAdLoaded() {
			listener.FireAdLoaded();
		}
		
		void onAdFailedToLoad(int errorReason) {
			listener.FireAdFailedToLoad(errorReason);
		}
		
		void onAdOpened() {
			listener.FireAdOpened();
		}

		void onAdReward(string rewardName, int rewardAmount) {
			listener.FireAdReward(rewardName, rewardAmount);
		}
		
		void onAdClosed() {
			listener.FireAdClosed();
		}
		
		void onAdLeftApplication() {
			listener.FireAdLeftApplication();
		}
	}
}
#endif
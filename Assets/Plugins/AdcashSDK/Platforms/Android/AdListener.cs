#if UNITY_ANDROID
using UnityEngine;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AdListener : AndroidJavaProxy
	{
		private IAdListener listener;
		internal AdListener(IAdListener listener)
			: base(ClassName.UnityAdListenerClassName)
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
		
		void onAdClosed() {
			listener.FireAdClosed();
		}
		
		void onAdLeftApplication() {
			listener.FireAdLeftApplication();
		}
	}
}
#endif
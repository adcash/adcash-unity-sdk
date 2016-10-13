using System;

namespace AdcashSDK.Common
{
	// Interface for the methods to be invoked by the native plugin.
	internal interface IAdListener
	{
		void FireAdLoaded();
		void FireAdFailedToLoad(int message);
		void FireAdOpened();
		void FireAdClosing();
		void FireAdClosed();
		void FireAdLeftApplication();
	}
}
#if UNITY_ANDROID
using System;
using AdcashSDK.Common;
using AdcashSDK.Android;

namespace AdcashSDK.Api
{
	public class VideoAndroid : IAdListener
	{
		private AndroidInterstitialClient client;

		// These are the ad callback events that can be hooked into.
		public event EventHandler<EventArgs> AdLoaded = delegate {};
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
		public event EventHandler<EventArgs> AdOpened = delegate {};
		public event EventHandler<EventArgs> AdClosing = delegate {};
		public event EventHandler<EventArgs> AdClosed = delegate {};
		public event EventHandler<EventArgs> AdLeftApplication = delegate {};

		public VideoAndroid (string adUnitId)
		{
			// Create a Video player and add it to the view hierarchy.
			client = new AndroidInterstitialClient(this);
			client.CreateInterstitialAd(adUnitId);
		}

		public void LoadAd()
		{
			client.LoadAd ();
		}

		public bool IsLoaded()
		{
			return client.IsLoaded();
		}

		public void Show()
		{
			client.ShowInterstitial();
		}

		public void Destroy()
		{
			client.DestroyInterstitial ();
		}

		#region IAdListener implementation

		// The following methods are invoked from an IAdcashVideoClient. Forward
		// these calls to the developer.
		void IAdListener.FireAdLoaded()
		{
			AdLoaded(this, EventArgs.Empty);
		}

		void IAdListener.FireAdFailedToLoad(int message)
		{
			AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs() {
				Message = message
			};
			AdFailedToLoad(this, args);
		}

		void IAdListener.FireAdOpened()
		{
			AdOpened(this, EventArgs.Empty);
		}

		void IAdListener.FireAdClosing()
		{
			AdClosing(this, EventArgs.Empty);
		}

		void IAdListener.FireAdClosed()
		{
			AdClosed(this, EventArgs.Empty);
		}

		void IAdListener.FireAdLeftApplication()
		{
			AdLeftApplication(this, EventArgs.Empty);
		}

		#endregion
	}
}

#endif
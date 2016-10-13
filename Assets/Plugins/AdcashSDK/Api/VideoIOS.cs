#if UNITY_IOS
using System;
using AdcashSDK.Common;
using AdcashSDK.iOS;

namespace AdcashSDK.Api
{
	public class VideoIOS : IAdListener
	{
		//private IOSVideoClient client;
		private IAdcashVideoClient client;

		// These are ad callback events that can be hooked into.
		public event EventHandler<EventArgs> AdLoaded = delegate {};
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
		public event EventHandler<EventArgs> AdOpened = delegate {};
		public event EventHandler<EventArgs> AdClosing = delegate {};
		public event EventHandler<EventArgs> AdClosed = delegate {};
		public event EventHandler<EventArgs> AdLeftApplication = delegate {};

		// Creates an VideoAd.
		public VideoIOS (string zoneID)
		{
			// Create a Video player and add it to the view hierarchy.
			//client = new IOSVideoClient(this);
			client = AdcashClientFactory.GetAdcashVideoClient(this);
			client.CreateVideoAd (zoneID);
		}

		public void Play()
		{
			client.Play ();
		}
			
		public void Destroy ()
		{
			client.Destroy ();
		}

		#region IAdListener implementation

		// The following methods are invoked from an IAdcashVideoClient. Forward these
		// calls to developer.
		void IAdListener.FireAdLoaded()
		{
			AdLoaded (this, EventArgs.Empty);
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

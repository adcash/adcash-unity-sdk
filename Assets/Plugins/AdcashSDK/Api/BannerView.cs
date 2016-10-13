using System;
using AdcashSDK.Common;

namespace AdcashSDK.Api
{
	public class BannerView : IAdListener
	{
		private IAdcashBannerClient client;
		
		// These are the ad callback events that can be hooked into.
		public event EventHandler<EventArgs> AdLoaded = delegate {};
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
		public event EventHandler<EventArgs> AdOpened = delegate {};
		public event EventHandler<EventArgs> AdClosing = delegate {};
		public event EventHandler<EventArgs> AdClosed = delegate {};
		public event EventHandler<EventArgs> AdLeftApplication = delegate {};
		
		// Create a BannerView and add it into the view hierarchy.
		public BannerView(string adUnitId, AdPosition position)
		{
			client = AdcashClientFactory.GetAdcashBannerClient(this);
			client.CreateBannerView(adUnitId, position);
		}
		
		// Load an ad into the BannerView.
		public void LoadAd()
		{
			client.LoadAd();
		}
		
		// Hide the BannerView from the screen.
		public void Hide()
		{
			client.HideBannerView();
		}
		
		// Show the BannerView on the screen.
		public void Show()
		{
			client.ShowBannerView();
		}
		
		// Destroy the BannerView.
		public void Destroy()
		{
			client.DestroyBannerView();
		}
		
		#region IAdListener implementation
		
		// The following methods are invoked from an IAdcashBannerClient. Forward these calls
		// to the developer.
		void IAdListener.FireAdLoaded()
		{
			AdLoaded(this, EventArgs.Empty);
		}
		
		void IAdListener.FireAdFailedToLoad(int message)
		{
			AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs();
			args.Message = message;
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
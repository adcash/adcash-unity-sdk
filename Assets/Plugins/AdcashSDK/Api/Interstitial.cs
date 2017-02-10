using System;
using AdcashSDK.Common;

namespace AdcashSDK.Api
{
	public class Interstitial : IAdListener
	{
		private IAdcashInterstitialClient client;
		
		// These are the ad callback events that can be hooked into.
		public event EventHandler<EventArgs> AdLoaded = delegate {};
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
		public event EventHandler<EventArgs> AdOpened = delegate {};
		public event EventHandler<AdRewardEventArgs> AdReward = delegate {};
		public event EventHandler<EventArgs> AdClosed = delegate {};
		public event EventHandler<EventArgs> AdLeftApplication = delegate {};
		
		// Creates an InsterstitialAd.
		public Interstitial(string adUnitId)
		{
			client = AdcashClientFactory.GetAdcashInterstitialClient(this);
			client.CreateInterstitialAd(adUnitId);
		}
		
		// Loads a new interstitial request
		public void LoadAd()
		{
			client.LoadAd();
		}
		
		// Determines whether the InterstitialAd has loaded.
		public bool IsLoaded()
		{
			return client.IsLoaded();
		}
		
		// Shows the InterstitialAd.
		public void Show()
		{
			client.ShowInterstitial();
		}
		
		// Destroy the InterstitialAd.
		public void Destroy()
		{
			client.DestroyInterstitial();
		}
		
		#region IAdListener implementation
		
		// The following methods are invoked from an IAdcashInterstitialClient. Forward
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

		void IAdListener.FireAdReward(string name, int amount){
			AdRewardEventArgs args = new AdRewardEventArgs () {
				Amount = amount,
				Name = name
			};
			AdReward (this, args);
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
using System;
using AdcashSDK.Common;

namespace AdcashSDK.Api
{
	public class RewardedVideo : IAdListener
	{
		private IAdcashRewardedVideoClient client;

		// These are the ad callback events that can be hooked into.
		public event EventHandler<EventArgs> AdLoaded = delegate {};
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
		public event EventHandler<EventArgs> AdOpened = delegate {};
		public event EventHandler<AdRewardEventArgs> AdReward = delegate {};
		public event EventHandler<EventArgs> AdClosed = delegate {};
		public event EventHandler<EventArgs> AdLeftApplication = delegate {};

		public RewardedVideo (string adUnitId)
		{
			// Create a Video player and add it to the view hierarchy.
			client = AdcashClientFactory.GetAdcashRewardedVideoClient(this);
			client.CreateVideoAd(adUnitId);
		}


		/*
		public void LoadAd()
		{
			client.LoadAd ();
		}
		*/

		public bool IsLoaded()
		{
			return client.IsLoaded();
		}

		public void Show()
		{
			client.Play();
		}

		public void Destroy()
		{
			client.Destroy();
		}

		#region IAdListener implementation

		// The following methods are invoked from an IAdcashRewardedVideoClient. Forward
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

		void IAdListener.FireAdReward(string rewardName, int rewardAmount)
		{
			AdRewardEventArgs args = new AdRewardEventArgs () {
				Amount = rewardAmount, Name = rewardName
			};
			AdReward(this, args);
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
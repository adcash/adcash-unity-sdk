using AdcashSDK.Api;

namespace AdcashSDK.Common
{
	internal interface IAdcashRewardedVideoClient
	{
		// Loads a new video request.
		void CreateVideoAd (string zoneID);

		// Plays video ad.
		void Play();

		//Destroys video.
		void Destroy();

		// Returns true if ad is loaded.
		bool IsLoaded();
	}
}

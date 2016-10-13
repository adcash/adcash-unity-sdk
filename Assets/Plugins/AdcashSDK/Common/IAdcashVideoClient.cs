using AdcashSDK.Api;

namespace AdcashSDK.Common
{
	internal interface IAdcashVideoClient
	{
		// Loads a new video request.
		void CreateVideoAd (string zoneID);

		// Plays video ad.
		void Play();

		//Destroys video.
		void Destroy();
	}
}

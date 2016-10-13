using AdcashSDK.Api;

namespace AdcashSDK.Common {
	internal interface IAdcashInterstitialClient {
		// Creates an InterstitialAd.
		void CreateInterstitialAd(string adUnitId);
		
		// Loads a new interstitial request.
		void LoadAd();
		
		// Determines whether the interstitial has loaded.
		bool IsLoaded();
		
		// Shows the InterstitialAd when it is loaded.
		void ShowInterstitial();
		
		// Destroys an InterstitialAd to free up memory.
		void DestroyInterstitial();
	}
}
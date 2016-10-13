using AdcashSDK.Api;
using System;
using System.Collections.Generic;


namespace AdcashSDK.Common
{
	internal class DummyClient : IAdcashBannerClient, IAdcashInterstitialClient, IAdcashConversionTrackerClient, IAdcashVideoClient
	{
		public DummyClient()
		{
			Console.WriteLine("Created DummyClient");
		}

		public DummyClient(IAdListener listener)
		{
			Console.WriteLine("Created DummyClient");
		}

		public void CreateBannerView(string adUnitId, AdPosition position)
		{
			Console.WriteLine("Dummy CreateBannerView");
		}

		public void CreateVideoAd(string adUnitId) {
			Console.WriteLine("Dummy CreateVideoAd");
		}
		
		public void LoadAd()
		{
			Console.WriteLine("Dummy LoadAd");
		}
		
		public void ShowBannerView()
		{
			Console.WriteLine("Dummy ShowBannerView");
		}
		
		public void HideBannerView()
		{
			Console.WriteLine("Dummy HideBannerView");
		}
		
		public void DestroyBannerView()
		{
			Console.WriteLine("Dummy DestroyBannerView");
		}
			

		public void CreateInterstitialAd(string adUnitId) {
			Console.WriteLine("Dummy CreateIntersitialAd");
		}
		
		public bool IsLoaded() {
			Console.WriteLine("Dummy IsLoaded");
			return true;
		}
		
		public void ShowInterstitial() {
			Console.WriteLine("Dummy ShowInterstitial");
		}
		
		public void DestroyInterstitial() {
			Console.WriteLine("Dummy DestroyInterstitial");
		}

		public void ReportAppOpen() {
			Console.WriteLine("Dummy ReportAppOpen");
		}
		
		public void ReportConversion(int campaignId, string conversionType, Dictionary<string, string> otherParams) {
			Console.WriteLine("Dummy ReportConversion");
		}

		public void Play() {
			Console.WriteLine("Dummy Play");
		}

		public void Destroy() {
			Console.WriteLine("Dummy DestroyVideo");
		}
	}
}
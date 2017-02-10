#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.iOS
{
	internal class IOSBannerClient : IAdcashBannerClient
	{
		#region Banner callback types
		
		internal delegate void ACUAdViewDidReceiveAdCallback(IntPtr bannerClient);
		internal delegate void ACUAdViewDidFailToReceiveAdWithErrorCallback(
			IntPtr bannerClient, int error);
		internal delegate void ACUAdViewWillPresentScreenCallback(IntPtr bannerClient);
		internal delegate void ACUAdViewWillDismissScreenCallback(IntPtr bannerClient);
		internal delegate void ACUAdViewWillLeaveApplicationCallback(IntPtr bannerClient);
		
		#endregion
		
		private IAdListener listener;
		private IntPtr bannerViewPtr;
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;
		
		public IOSBannerClient(IAdListener listener)
		{
			this.listener = listener;
		}
		
		// This property should be used when setting the bannerViewPtr.
		private IntPtr BannerViewPtr
		{
			get
			{
				return bannerViewPtr;
			}
			set
			{
				Externs.ACURelease(bannerViewPtr);
				bannerViewPtr = value;
			}
		}
		
		#region IAdcashBannerClient implementation
		
		// Creates a banner view.
		public void CreateBannerView(string adUnitId, AdPosition position) {
			IntPtr bannerClientPtr = (IntPtr) GCHandle.Alloc(this);
			
			BannerViewPtr = Externs.ACUCreateSmartBannerView(
					bannerClientPtr, adUnitId, (int)position);
			Externs.ACUSetBannerCallbacks(
				BannerViewPtr,
				AdViewDidReceiveAdCallback,
				AdViewDidFailToReceiveAdWithErrorCallback,
				AdViewWillPresentScreenCallback,
				AdViewWillDismissScreenCallback,
				AdViewWillLeaveApplicationCallback);
		}
		
		// Load an ad.
		public void LoadAd()
		{
			Externs.ACURequestBannerAd(BannerViewPtr);
		}
		
		// Show the banner view on the screen.
		public void ShowBannerView() {
			Externs.ACUShowBannerView(BannerViewPtr);
		}
		
		// Hide the banner view from the screen.
		public void HideBannerView()
		{
			Externs.ACUHideBannerView(BannerViewPtr);
		}
		
		public void DestroyBannerView()
		{
			Externs.ACURemoveBannerView(BannerViewPtr);
			BannerViewPtr = IntPtr.Zero;
		}
		
		#endregion
		
		#region Banner callback methods
		
		[MonoPInvokeCallback(typeof(ACUAdViewDidReceiveAdCallback))]
		private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdLoaded();
		}
		
		[MonoPInvokeCallback(typeof(ACUAdViewDidFailToReceiveAdWithErrorCallback))]
		private static void AdViewDidFailToReceiveAdWithErrorCallback(
			IntPtr bannerClient, int errorCode)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdFailedToLoad(errorCode);
		}
		
		[MonoPInvokeCallback(typeof(ACUAdViewWillPresentScreenCallback))]
		private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdOpened();
		}
		
		[MonoPInvokeCallback(typeof(ACUAdViewWillDismissScreenCallback))]
		private static void AdViewWillDismissScreenCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdClosed();
		}
		
		[MonoPInvokeCallback(typeof(ACUAdViewWillLeaveApplicationCallback))]
		private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdLeftApplication();
		}
		
		private static IOSBannerClient IntPtrToBannerClient(IntPtr bannerClient)
		{
			GCHandle handle = (GCHandle) bannerClient;
			return handle.Target as IOSBannerClient;
		}
		
		#endregion
	}
}
#endif
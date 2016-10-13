#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.iOS
{
	internal class IOSInterstitialClient : IAdcashInterstitialClient
	{
		#region Interstitial callback types
		
		internal delegate void ACUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);
		internal delegate void ACUInterstitialDidFailToReceiveAdWithErrorCallback(
			IntPtr interstitialClient, int errorCode);
		internal delegate void ACUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);
		internal delegate void ACUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);
		internal delegate void ACUInterstitialWillLeaveApplicationCallback(
			IntPtr interstitialClient);
		
		#endregion
		
		private IAdListener listener;
		private IntPtr interstitialPtr;
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;
		
		// This property should be used when setting the interstitialPtr.
		private IntPtr InterstitialPtr
		{
			get
			{
				return interstitialPtr;
			}
			set
			{
				Externs.ACURelease(interstitialPtr);
				interstitialPtr = value;
			}
		}
		
		public IOSInterstitialClient(IAdListener listener)
		{
			this.listener = listener;
		}
		
		#region IGoogleMobileAdsInterstitialClient implementation
		
		public void CreateInterstitialAd(string adUnitId) {
			IntPtr interstitialClientPtr = (IntPtr) GCHandle.Alloc(this);
			InterstitialPtr = Externs.ACUCreateInterstitial(interstitialClientPtr, adUnitId);
			Externs.ACUSetInterstitialCallbacks(
				InterstitialPtr,
				InterstitialDidReceiveAdCallback,
				InterstitialDidFailToReceiveAdWithErrorCallback,
				InterstitialWillPresentScreenCallback,
				InterstitialWillDismissScreenCallback,
				InterstitialWillLeaveApplicationCallback);
		}
		
		public void LoadAd()
		{
			Externs.ACURequestInterstitial(InterstitialPtr);
		}
		
		public bool IsLoaded() {
			return Externs.ACUInterstitialReady(InterstitialPtr);
		}
		
		public void ShowInterstitial() {
			Externs.ACUShowInterstitial(InterstitialPtr);
		}
		
		public void DestroyInterstitial() {
			InterstitialPtr = IntPtr.Zero;
		}
		
		#endregion
		
		#region Interstitial callback methods
		
		[MonoPInvokeCallback(typeof(ACUInterstitialDidReceiveAdCallback))]
		private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
		}
		
		[MonoPInvokeCallback(typeof(ACUInterstitialDidFailToReceiveAdWithErrorCallback))]
		private static void InterstitialDidFailToReceiveAdWithErrorCallback(
			IntPtr interstitialClient, int errorCode)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(errorCode);
		}
		
		[MonoPInvokeCallback(typeof(ACUInterstitialWillPresentScreenCallback))]
		private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
		}
		
		[MonoPInvokeCallback(typeof(ACUInterstitialWillDismissScreenCallback))]
		private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
		}
		
		[MonoPInvokeCallback(typeof(ACUInterstitialWillLeaveApplicationCallback))]
		private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
		}
		
		private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
		{
			GCHandle handle = (GCHandle) interstitialClient;
			return handle.Target as IOSInterstitialClient;
		}
		
		#endregion
	}
}
#endif
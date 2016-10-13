#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.iOS
{
	internal class IOSVideoClient : IAdcashVideoClient
	{

		#region Video callback types

		internal delegate void ACUVideoDidReceiveAdCallback(IntPtr videoClient);
		internal delegate void ACUVideoDidFailToReceiveAdCallback(IntPtr videoClient, int errorCode);
		internal delegate void ACUVideoWillPresentScreenCallback(IntPtr videoClient);
		internal delegate void ACUVideoWillDismissScreenCallback(IntPtr videoClient);
		internal delegate void ACUVideoDidDismissScreenCallback(IntPtr videoClient);
		internal delegate void ACUVideoWillLeaveApplicationCallback(IntPtr videoClient);

		#endregion
		private IAdListener listener;
		private IntPtr videoPtr;
		private static Dictionary<IntPtr, IOSVideoClient> videoClients;

		// This property should be used when setting the videoPtr.
		private IntPtr VideoPtr
		{
			get
			{
				return videoPtr;
			}
			set
			{
				Externs.ACURelease (videoPtr);
				videoPtr = value;
			}
		}

		public IOSVideoClient(IAdListener listener)
		{
			this.listener = listener;
		}

		#region AdcashVideoClient implementation

		//Loads the video.
		public void CreateVideoAd(string zoneID)
		{
			IntPtr videoClientPtr = (IntPtr) GCHandle.Alloc(this);
			VideoPtr = Externs.ACUCreateVideo(videoClientPtr, zoneID);
			Externs.ACUSetVideoCallbacks (
				VideoPtr,
				VideoDidReceiveAdCallback,
				VideoDidFailToReceiveAdCallback,
				VideoWillPresentScreenCallback,
				VideoWillDismissScreenCallback,
				VideoDidDismissScreenCallback,
				VideoWillLeaveApplicationCallback);

		}
			
		public void Play()
		{
			Externs.ACUShowVideo (VideoPtr);
		}

		public void Destroy()
		{
			VideoPtr = IntPtr.Zero;
		}
			
		#endregion

		#region Video callback methods

		[MonoPInvokeCallback(typeof(ACUVideoDidReceiveAdCallback))]
		private static void VideoDidReceiveAdCallback(IntPtr videoClient)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdLoaded();
		}

		[MonoPInvokeCallback(typeof(ACUVideoDidFailToReceiveAdCallback))]
		private static void VideoDidFailToReceiveAdCallback(IntPtr videoClient, int errorCode)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdFailedToLoad(errorCode);
		}

		[MonoPInvokeCallback(typeof(ACUVideoWillPresentScreenCallback))]
		private static void VideoWillPresentScreenCallback(IntPtr videoClient)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdOpened();
		}

		[MonoPInvokeCallback(typeof(ACUVideoWillDismissScreenCallback))]
		private static void VideoWillDismissScreenCallback(IntPtr videoClient)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdClosing();
		}

		[MonoPInvokeCallback(typeof(ACUVideoDidDismissScreenCallback))]
		private static void VideoDidDismissScreenCallback(IntPtr videoClient)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdClosed();
		}

		[MonoPInvokeCallback(typeof(ACUVideoWillLeaveApplicationCallback))]
		private static void VideoWillLeaveApplicationCallback(IntPtr videoClient)
		{
			IntPtrToVideoClient(videoClient).listener.FireAdLeftApplication();
		}

		private static IOSVideoClient IntPtrToVideoClient(IntPtr videoClient)
		{
			GCHandle handle = (GCHandle) videoClient;
			return handle.Target as IOSVideoClient;
		}	
		#endregion
	}
}
#endif

#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.iOS
{
	internal class IOSRewardedVideoClient : IAdcashRewardedVideoClient
	{

		#region Video callback types

		internal delegate void ACURewardedVideoDidReceiveAdCallback(IntPtr rewardedVideoClient);
		internal delegate void ACURewardedVideoDidFailToReceiveAdCallback(IntPtr rewardedVideoClient, int errorCode);
		internal delegate void ACURewardedVideoWillPresentScreenCallback(IntPtr rewardedVideoClient);
		internal delegate void ACURewardedVideoWillDismissScreenCallback(IntPtr rewardedVideoClient);
		internal delegate void ACURewardedVideoDidDismissScreenCallback(IntPtr rewardedVideoClient);
		internal delegate void ACURewardedVideoWillLeaveApplicationCallback(IntPtr rewardedVideoClient);
		internal delegate void ACURewardedVideoDidCompleteWithRewardCallback(IntPtr rewardedVideoClient, string rewardName, int amount);

		#endregion
		private static bool isReady;
		private IAdListener listener;
		private IntPtr rewardedVideoPtr;
		private static Dictionary<IntPtr, IOSRewardedVideoClient> rewardedVideoClients;

		// This property should be used when setting the videoPtr.
		private IntPtr RewardedVideoPtr
		{
			get
			{
				return rewardedVideoPtr;
			}
			set
			{
				Externs.ACURelease (rewardedVideoPtr);
				rewardedVideoPtr = value;
			}
		}

		public IOSRewardedVideoClient(IAdListener listener)
		{
			this.listener = listener;
		}

		#region AdcashRewardedVideoClient implementation

		//Loads the video.
		public void CreateVideoAd(string zoneID)
		{
			isReady = false;
			IntPtr rewardedVideoClientPtr = (IntPtr) GCHandle.Alloc(this);
			RewardedVideoPtr = Externs.ACUCreateRewardedVideo(rewardedVideoClientPtr, zoneID);
			Externs.ACUSetRewardedVideoCallbacks (
				RewardedVideoPtr,
				RewardedVideoDidReceiveAdCallback,
				RewardedVideoDidFailToReceiveAdCallback,
				RewardedVideoWillPresentScreenCallback,
				RewardedVideoWillDismissScreenCallback,
				RewardedVideoDidDismissScreenCallback,
				RewardedVideoWillLeaveApplicationCallback,
				RewardedVideoDidCompleteWithRewardCallback);

		}

		public void Play()
		{
			if (IsLoaded()){
				Show();
			}		
		}
			
		public void Show()
		{
			Externs.ACUShowRewardedVideo (RewardedVideoPtr);
		}

		public void Destroy()
		{
			RewardedVideoPtr = IntPtr.Zero;
		}

		public bool IsLoaded()
		{
			return isReady;
		}
		#endregion

		#region Video callback methods

		[MonoPInvokeCallback(typeof(ACURewardedVideoDidReceiveAdCallback))]
		private static void RewardedVideoDidReceiveAdCallback(IntPtr rewardedVideoClient)
		{
			isReady = true;
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdLoaded();
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoDidFailToReceiveAdCallback))]
		private static void RewardedVideoDidFailToReceiveAdCallback(IntPtr rewardedVideoClient, int errorCode)
		{
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdFailedToLoad(errorCode);
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoWillPresentScreenCallback))]
		private static void RewardedVideoWillPresentScreenCallback(IntPtr rewardedVideoClient)
		{
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdOpened();
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoWillDismissScreenCallback))]
		private static void RewardedVideoWillDismissScreenCallback(IntPtr rewardedVideoClient)
		{
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdClosed();
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoDidDismissScreenCallback))]
		private static void RewardedVideoDidDismissScreenCallback(IntPtr rewardedVideoClient)
		{
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdClosed();
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoWillLeaveApplicationCallback))]
		private static void RewardedVideoWillLeaveApplicationCallback(IntPtr rewardedVideoClient)
		{
			IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdLeftApplication();
		}

		[MonoPInvokeCallback(typeof(ACURewardedVideoDidCompleteWithRewardCallback))]
		private static void RewardedVideoDidCompleteWithRewardCallback(IntPtr rewardedVideoClient, string rewardName, int amount)
		{
		IntPtrToRewardedVideoClient(rewardedVideoClient).listener.FireAdReward(rewardName,amount);
		}

		private static IOSRewardedVideoClient IntPtrToRewardedVideoClient(IntPtr rewardedVideoClient)
		{
			GCHandle handle = (GCHandle) rewardedVideoClient;
			return handle.Target as IOSRewardedVideoClient;
		}	
		#endregion
	}
}
#endif

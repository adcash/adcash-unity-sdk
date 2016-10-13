#if UNITY_ANDROID

using System;
using System.Collections.Generic;
using UnityEngine;
using AdcashSDK;
using AdcashSDK.Api;
using AdcashSDK.Common;

namespace AdcashSDK.Android
{
	internal class AndroidConversionTrackerClient : IAdcashConversionTrackerClient
	{
		private AndroidJavaObject tracker;
		
		public AndroidConversionTrackerClient()
		{
			AndroidJavaClass playerClass = new AndroidJavaClass(ClassName.UnityActivityClassName);
			AndroidJavaObject activity =
				playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			tracker = new AndroidJavaObject(
				ClassName.TrackerClassName, activity);
		}

		// Load an ad.
		public void ReportConversion(int campaignID, string conversionType, Dictionary<string, string> otherParams)
		{
			Debug.Log ("Conversion type: " + conversionType);
			tracker.Call("reportConversion", new object[3] { campaignID, conversionType, otherParams });
		}

	}
}
#endif
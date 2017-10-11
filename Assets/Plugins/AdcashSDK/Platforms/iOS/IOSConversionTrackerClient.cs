/*
#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using AdcashSDK.Common;

namespace AdcashSDK.iOS
{
	internal class IOSConversionTrackerClient : IAdcashConversionTrackerClient
	{
		public IOSConversionTrackerClient() {}


		public void ReportConversion(int campaignId, string conversionType, Dictionary<string, string> otherParams) {
			Externs.ACUReportAppOpenConversion (campaignId,conversionType,otherParams);
		}

		// Load an ad.

		public void ReportConversion(String campaign, String payout)
		{
			Console.WriteLine ("<Adcash>: Custom campaign conversion is not supported in iOS. The method call will be ignored.");
		}

	}
}
#endif
*/

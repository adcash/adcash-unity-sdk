using System;
using AdcashSDK.Common;
using System.Collections.Generic;

namespace AdcashSDK.Api
{
	public class ConversionTracker
	{
		private IAdcashConversionTrackerClient client;
		
		public ConversionTracker()
		{
			client = AdcashClientFactory.GetAdcashConversionTrackerClient();
		}

		// Report conversions
		public void ReportConversion(int a, String b, Dictionary<string, string> c)
		{
			client.ReportConversion(a, b, c);
		}
	}
}
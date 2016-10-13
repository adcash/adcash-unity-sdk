using AdcashSDK.Api;
using System.Collections.Generic;

namespace AdcashSDK.Common {
	internal interface IAdcashConversionTrackerClient {
		
		// Reports conversion
		void ReportConversion(int campaignId, string conversionType, Dictionary<string, string> otherParams);
	}
}
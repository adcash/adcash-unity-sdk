using System;
using AdcashSDK.Common;

namespace AdcashSDK.Api
{
	// Event that occurs when an ad fails to load.
	public class AdRewardEventArgs : EventArgs
	{
		public int Amount { get; set; }
		public string Name { get; set; }
	}
}

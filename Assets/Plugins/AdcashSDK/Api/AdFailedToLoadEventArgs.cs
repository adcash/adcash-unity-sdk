using System;
using AdcashSDK.Common;

namespace AdcashSDK.Api
{
	// Event that occurs when an ad fails to load.
	public class AdFailedToLoadEventArgs : EventArgs
	{
		public int Message { get; set; }
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a parameter. This class cannot be inherited.</summary>
	// Token: 0x020007D8 RID: 2008
	[AttributeUsage(AttributeTargets.Parameter)]
	[ComVisible(true)]
	public sealed class SoapParameterAttribute : SoapAttribute
	{
	}
}

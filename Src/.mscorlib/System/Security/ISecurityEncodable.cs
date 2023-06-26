using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Defines the methods that convert permission object state to and from XML element representation.</summary>
	// Token: 0x020001D3 RID: 467
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001C73 RID: 7283
		SecurityElement ToXml();

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06001C74 RID: 7284
		void FromXml(SecurityElement e);
	}
}

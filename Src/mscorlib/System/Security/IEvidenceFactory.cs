using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Gets an object's <see cref="T:System.Security.Policy.Evidence" />.</summary>
	// Token: 0x020001D1 RID: 465
	[ComVisible(true)]
	public interface IEvidenceFactory
	{
		/// <summary>Gets <see cref="T:System.Security.Policy.Evidence" /> that verifies the current object's identity.</summary>
		/// <returns>
		///   <see cref="T:System.Security.Policy.Evidence" /> of the current object's identity.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001C6D RID: 7277
		Evidence Evidence { get; }
	}
}

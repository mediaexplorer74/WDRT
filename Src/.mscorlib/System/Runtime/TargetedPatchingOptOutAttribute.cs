using System;

namespace System.Runtime
{
	/// <summary>Indicates that the .NET Framework class library method to which this attribute is applied is unlikely to be affected by servicing releases, and therefore is eligible to be inlined across Native Image Generator (NGen) images.</summary>
	// Token: 0x02000717 RID: 1815
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" /> class.</summary>
		/// <param name="reason">The reason why the method to which the <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" /> attribute is applied is considered to be eligible for inlining across Native Image Generator (NGen) images.</param>
		// Token: 0x06005149 RID: 20809 RVA: 0x0011FDEE File Offset: 0x0011DFEE
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.m_reason = reason;
		}

		/// <summary>Gets the reason why the method to which this attribute is applied is considered to be eligible for inlining across Native Image Generator (NGen) images.</summary>
		/// <returns>The reason why the method is considered to be eligible for inlining across NGen images.</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x0011FDFD File Offset: 0x0011DFFD
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0011FE05 File Offset: 0x0011E005
		private TargetedPatchingOptOutAttribute()
		{
		}

		// Token: 0x04002404 RID: 9220
		private string m_reason;
	}
}

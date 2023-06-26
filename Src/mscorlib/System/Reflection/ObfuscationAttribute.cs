using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x0200060E RID: 1550
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>
		///   <see langword="true" /> if an obfuscation tool should remove the attribute after processing; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x00107573 File Offset: 0x00105773
		// (set) Token: 0x0600480C RID: 18444 RVA: 0x0010757B File Offset: 0x0010577B
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>
		///   <see langword="true" /> if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x00107584 File Offset: 0x00105784
		// (set) Token: 0x0600480E RID: 18446 RVA: 0x0010758C File Offset: 0x0010578C
		public bool Exclude
		{
			get
			{
				return this.m_exclude;
			}
			set
			{
				this.m_exclude = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is to apply to the members of the type; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x00107595 File Offset: 0x00105795
		// (set) Token: 0x06004810 RID: 18448 RVA: 0x0010759D File Offset: 0x0010579D
		public bool ApplyToMembers
		{
			get
			{
				return this.m_applyToMembers;
			}
			set
			{
				this.m_applyToMembers = value;
			}
		}

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options.</summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x001075A6 File Offset: 0x001057A6
		// (set) Token: 0x06004812 RID: 18450 RVA: 0x001075AE File Offset: 0x001057AE
		public string Feature
		{
			get
			{
				return this.m_feature;
			}
			set
			{
				this.m_feature = value;
			}
		}

		// Token: 0x04001DCD RID: 7629
		private bool m_strip = true;

		// Token: 0x04001DCE RID: 7630
		private bool m_exclude = true;

		// Token: 0x04001DCF RID: 7631
		private bool m_applyToMembers = true;

		// Token: 0x04001DD0 RID: 7632
		private string m_feature = "all";
	}
}

using System;

namespace System.Security
{
	/// <summary>Indicates the set of security rules the common language runtime should enforce for an assembly.</summary>
	// Token: 0x020001CC RID: 460
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityRulesAttribute" /> class using the specified rule set value.</summary>
		/// <param name="ruleSet">One of the enumeration values that specifies the transparency rules set.</param>
		// Token: 0x06001C24 RID: 7204 RVA: 0x00060E61 File Offset: 0x0005F061
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		/// <summary>Determines whether fully trusted transparent code should skip Microsoft intermediate language (MSIL) verification.</summary>
		/// <returns>
		///   <see langword="true" /> if MSIL verification should be skipped; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00060E70 File Offset: 0x0005F070
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x00060E78 File Offset: 0x0005F078
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		/// <summary>Gets the rule set to be applied.</summary>
		/// <returns>One of the enumeration values that specifies the transparency rules to be applied.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00060E81 File Offset: 0x0005F081
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x040009CA RID: 2506
		private SecurityRuleSet m_ruleSet;

		// Token: 0x040009CB RID: 2507
		private bool m_skipVerificationInFullTrust;
	}
}

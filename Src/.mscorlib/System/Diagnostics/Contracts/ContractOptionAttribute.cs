using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables you to set contract and tool options at assembly, type, or method granularity.</summary>
	// Token: 0x02000412 RID: 1042
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractOptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and enable/disable value.</summary>
		/// <param name="category">The category for the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="enabled">
		///   <see langword="true" /> to enable the option; <see langword="false" /> to disable the option.</param>
		// Token: 0x06003413 RID: 13331 RVA: 0x000C7F6B File Offset: 0x000C616B
		[__DynamicallyInvokable]
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and value.</summary>
		/// <param name="category">The category of the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="value">The value for the setting.</param>
		// Token: 0x06003414 RID: 13332 RVA: 0x000C7F88 File Offset: 0x000C6188
		[__DynamicallyInvokable]
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		/// <summary>Gets the category of the option.</summary>
		/// <returns>The category of the option.</returns>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000C7FA5 File Offset: 0x000C61A5
		[__DynamicallyInvokable]
		public string Category
		{
			[__DynamicallyInvokable]
			get
			{
				return this._category;
			}
		}

		/// <summary>Gets the setting for the option.</summary>
		/// <returns>The setting for the option.</returns>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000C7FAD File Offset: 0x000C61AD
		[__DynamicallyInvokable]
		public string Setting
		{
			[__DynamicallyInvokable]
			get
			{
				return this._setting;
			}
		}

		/// <summary>Determines if an option is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the option is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000C7FB5 File Offset: 0x000C61B5
		[__DynamicallyInvokable]
		public bool Enabled
		{
			[__DynamicallyInvokable]
			get
			{
				return this._enabled;
			}
		}

		/// <summary>Gets the value for the option.</summary>
		/// <returns>The value for the option.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000C7FBD File Offset: 0x000C61BD
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001713 RID: 5907
		private string _category;

		// Token: 0x04001714 RID: 5908
		private string _setting;

		// Token: 0x04001715 RID: 5909
		private bool _enabled;

		// Token: 0x04001716 RID: 5910
		private string _value;
	}
}

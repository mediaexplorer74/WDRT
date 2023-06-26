using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies whether the type should be marshaled using the Automation marshaler or a custom proxy and stub.</summary>
	// Token: 0x02000936 RID: 2358
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" /> class.</summary>
		/// <param name="val">
		///   <see langword="true" /> if the class should be marshaled using the Automation Marshaler; <see langword="false" /> if a proxy stub marshaler should be used.</param>
		// Token: 0x06006062 RID: 24674 RVA: 0x0014D614 File Offset: 0x0014B814
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		/// <summary>Gets a value indicating the type of marshaler to use.</summary>
		/// <returns>
		///   <see langword="true" /> if the class should be marshaled using the Automation Marshaler; <see langword="false" /> if a proxy stub marshaler should be used.</returns>
		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06006063 RID: 24675 RVA: 0x0014D623 File Offset: 0x0014B823
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B2A RID: 11050
		internal bool _val;
	}
}

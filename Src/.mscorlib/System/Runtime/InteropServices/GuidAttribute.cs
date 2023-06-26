using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Supplies an explicit <see cref="T:System.Guid" /> when an automatic GUID is undesirable.</summary>
	// Token: 0x0200092B RID: 2347
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class GuidAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> class with the specified GUID.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> to be assigned.</param>
		// Token: 0x06006041 RID: 24641 RVA: 0x0014D1FE File Offset: 0x0014B3FE
		[__DynamicallyInvokable]
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		/// <summary>Gets the <see cref="T:System.Guid" /> of the class.</summary>
		/// <returns>The <see cref="T:System.Guid" /> of the class.</returns>
		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x0014D20D File Offset: 0x0014B40D
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B10 RID: 11024
		internal string _val;
	}
}

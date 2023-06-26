using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the COM dispatch identifier (DISPID) of a method, field, or property.</summary>
	// Token: 0x02000910 RID: 2320
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DispIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="DispIdAttribute" /> class with the specified DISPID.</summary>
		/// <param name="dispId">The DISPID for the member.</param>
		// Token: 0x0600600C RID: 24588 RVA: 0x0014CDB3 File Offset: 0x0014AFB3
		[__DynamicallyInvokable]
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		/// <summary>Gets the DISPID for the member.</summary>
		/// <returns>The DISPID for the member.</returns>
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x0014CDC2 File Offset: 0x0014AFC2
		[__DynamicallyInvokable]
		public int Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A6C RID: 10860
		internal int _val;
	}
}

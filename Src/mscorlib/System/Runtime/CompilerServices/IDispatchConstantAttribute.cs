using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />, where the <see cref="P:System.Runtime.InteropServices.DispatchWrapper.WrappedObject" /> is <see langword="null" />.</summary>
	// Token: 0x020008FA RID: 2298
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the <see langword="IDispatch" /> constant stored in this attribute.</summary>
		/// <returns>The <see langword="IDispatch" /> constant stored in this attribute. Only <see langword="null" /> is allowed for an <see langword="IDispatch" /> constant value.</returns>
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x0014CC8B File Offset: 0x0014AE8B
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.UnknownWrapper" />, where the <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> is <see langword="null" />. This class cannot be inherited.</summary>
	// Token: 0x020008FB RID: 2299
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the <see langword="IUnknown" /> constant stored in this attribute.</summary>
		/// <returns>The <see langword="IUnknown" /> constant stored in this attribute. Only <see langword="null" /> is allowed for an <see langword="IUnknown" /> constant value.</returns>
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x0014CC9B File Offset: 0x0014AE9B
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}

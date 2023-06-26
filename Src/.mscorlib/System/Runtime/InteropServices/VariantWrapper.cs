using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type <see langword="VT_VARIANT | VT_BYREF" /> from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200095F RID: 2399
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class VariantWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> class for the specified <see cref="T:System.Object" /> parameter.</summary>
		/// <param name="obj">The object to marshal.</param>
		// Token: 0x06006239 RID: 25145 RVA: 0x00150EC6 File Offset: 0x0014F0C6
		[__DynamicallyInvokable]
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</returns>
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600623A RID: 25146 RVA: 0x00150ED5 File Offset: 0x0014F0D5
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B98 RID: 11160
		private object m_WrappedObject;
	}
}

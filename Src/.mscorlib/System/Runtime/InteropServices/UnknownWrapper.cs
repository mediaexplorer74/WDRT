using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_UNKNOWN" />.</summary>
	// Token: 0x0200095E RID: 2398
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class UnknownWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> class with the object to be wrapped.</summary>
		/// <param name="obj">The object being wrapped.</param>
		// Token: 0x06006237 RID: 25143 RVA: 0x00150EAF File Offset: 0x0014F0AF
		[__DynamicallyInvokable]
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object contained by this wrapper.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x00150EBE File Offset: 0x0014F0BE
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B97 RID: 11159
		private object m_WrappedObject;
	}
}

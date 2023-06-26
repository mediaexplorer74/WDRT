using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type <see langword="VT_BSTR" /> from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200095A RID: 2394
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class BStrWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.String" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as <see langword="VT_BSTR" />.</param>
		// Token: 0x0600622B RID: 25131 RVA: 0x00150DAB File Offset: 0x0014EFAB
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.Object" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as <see langword="VT_BSTR" />.</param>
		// Token: 0x0600622C RID: 25132 RVA: 0x00150DBA File Offset: 0x0014EFBA
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(object value)
		{
			this.m_WrappedObject = (string)value;
		}

		/// <summary>Gets the wrapped <see cref="T:System.String" /> object to marshal as type <see langword="VT_BSTR" />.</summary>
		/// <returns>The object that is wrapped by <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.</returns>
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600622D RID: 25133 RVA: 0x00150DCE File Offset: 0x0014EFCE
		[__DynamicallyInvokable]
		public string WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B93 RID: 11155
		private string m_WrappedObject;
	}
}

using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_DISPATCH" />.</summary>
	// Token: 0x0200095C RID: 2396
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DispatchWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> class with the object being wrapped.</summary>
		/// <param name="obj">The object to be wrapped and converted to <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a class or an array.  
		/// -or-  
		/// <paramref name="obj" /> does not support <see langword="IDispatch" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="obj" /> parameter was marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> attribute that was passed a value of <see langword="false" />.  
		///  -or-  
		///  The <paramref name="obj" /> parameter inherits from a type marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> attribute that was passed a value of <see langword="false" />.</exception>
		// Token: 0x06006231 RID: 25137 RVA: 0x00150E20 File Offset: 0x0014F020
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				IntPtr idispatchForObject = Marshal.GetIDispatchForObject(obj);
				Marshal.Release(idispatchForObject);
			}
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</returns>
		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x00150E4B File Offset: 0x0014F04B
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B95 RID: 11157
		private object m_WrappedObject;
	}
}

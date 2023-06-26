using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps a managed object holding a handle to a resource that is passed to unmanaged code using platform invoke.</summary>
	// Token: 0x02000948 RID: 2376
	[ComVisible(true)]
	public struct HandleRef
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleRef" /> class with the object to wrap and a handle to the resource used by unmanaged code.</summary>
		/// <param name="wrapper">A managed object that should not be finalized until the platform invoke call returns.</param>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that indicates a handle to a resource.</param>
		// Token: 0x060060BD RID: 24765 RVA: 0x0014E1CD File Offset: 0x0014C3CD
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.m_wrapper = wrapper;
			this.m_handle = handle;
		}

		/// <summary>Gets the object holding the handle to a resource.</summary>
		/// <returns>The object holding the handle to a resource.</returns>
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x0014E1DD File Offset: 0x0014C3DD
		public object Wrapper
		{
			get
			{
				return this.m_wrapper;
			}
		}

		/// <summary>Gets the handle to a resource.</summary>
		/// <returns>The handle to a resource.</returns>
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x0014E1E5 File Offset: 0x0014C3E5
		public IntPtr Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		/// <summary>Returns the handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <param name="value">The object that needs a handle.</param>
		/// <returns>The handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		// Token: 0x060060C0 RID: 24768 RVA: 0x0014E1ED File Offset: 0x0014C3ED
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.HandleRef" /> object to retrieve an internal integer representation from.</param>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		// Token: 0x060060C1 RID: 24769 RVA: 0x0014E1F5 File Offset: 0x0014C3F5
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x04002B5A RID: 11098
		internal object m_wrapper;

		// Token: 0x04002B5B RID: 11099
		internal IntPtr m_handle;
	}
}

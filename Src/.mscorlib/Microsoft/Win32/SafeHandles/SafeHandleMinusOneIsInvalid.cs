using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 safe handle implementations in which the value of -1 indicates an invalid handle.</summary>
	// Token: 0x02000023 RID: 35
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandleMinusOneIsInvalid : SafeHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid" /> class, specifying whether the handle is to be reliably released.</summary>
		/// <param name="ownsHandle">
		///   <see langword="true" /> to reliably release the handle during the finalization phase; <see langword="false" /> to prevent reliable release (not recommended).</param>
		// Token: 0x06000173 RID: 371 RVA: 0x00004869 File Offset: 0x00002A69
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleMinusOneIsInvalid(bool ownsHandle)
			: base(new IntPtr(-1), ownsHandle)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is not valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004878 File Offset: 0x00002A78
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}

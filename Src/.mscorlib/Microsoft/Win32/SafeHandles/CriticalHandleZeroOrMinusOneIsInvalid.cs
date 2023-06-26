using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 critical handle implementations in which the value of either 0 or -1 indicates an invalid handle.</summary>
	// Token: 0x02000024 RID: 36
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleZeroOrMinusOneIsInvalid : CriticalHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleZeroOrMinusOneIsInvalid" /> class.</summary>
		// Token: 0x06000175 RID: 373 RVA: 0x0000488B File Offset: 0x00002A8B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleZeroOrMinusOneIsInvalid()
			: base(IntPtr.Zero)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is not valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004898 File Offset: 0x00002A98
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle.IsNull() || this.handle == new IntPtr(-1);
			}
		}
	}
}

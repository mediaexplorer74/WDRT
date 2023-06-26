using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 critical handle implementations in which the value of -1 indicates an invalid handle.</summary>
	// Token: 0x02000025 RID: 37
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid" /> class.</summary>
		// Token: 0x06000177 RID: 375 RVA: 0x000048BA File Offset: 0x00002ABA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleMinusOneIsInvalid()
			: base(new IntPtr(-1))
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is not valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000048C8 File Offset: 0x00002AC8
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

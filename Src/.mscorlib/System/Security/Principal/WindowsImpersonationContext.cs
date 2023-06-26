using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	/// <summary>Represents the Windows user prior to an impersonation operation.</summary>
	// Token: 0x0200032F RID: 815
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x00098418 File Offset: 0x00096618
		[SecurityCritical]
		private WindowsImpersonationContext()
		{
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0009842C File Offset: 0x0009662C
		[SecurityCritical]
		internal WindowsImpersonationContext(SafeAccessTokenHandle safeTokenHandle, WindowsIdentity wi, bool isImpersonating, FrameSecurityDescriptor fsd)
		{
			if (safeTokenHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (isImpersonating)
			{
				if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), safeTokenHandle, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
				this.m_wi = wi;
			}
			this.m_fsd = fsd;
		}

		/// <summary>Reverts the user context to the Windows user represented by this object.</summary>
		/// <exception cref="T:System.Security.SecurityException">An attempt is made to use this method for any purpose other than to revert identity to self.</exception>
		// Token: 0x06002901 RID: 10497 RVA: 0x000984A0 File Offset: 0x000966A0
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
			}
			else
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
				if (num < 0)
				{
					throw new SecurityException(Win32Native.GetMessage(num));
				}
			}
			WindowsIdentity.UpdateThreadWI(this.m_wi);
			if (this.m_fsd != null)
			{
				this.m_fsd.SetTokenHandles(null, null);
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00098524 File Offset: 0x00096724
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			bool flag = false;
			try
			{
				int num;
				if (this.m_safeTokenHandle.IsInvalid)
				{
					num = Win32.RevertToSelf();
					if (num < 0)
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				else
				{
					num = Win32.RevertToSelf();
					if (num >= 0)
					{
						num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
					}
					else
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				flag = num >= 0;
				if (this.m_fsd != null)
				{
					this.m_fsd.SetTokenHandles(null, null);
				}
			}
			catch (Exception ex)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), ex);
				}
				flag = false;
			}
			return flag;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002903 RID: 10499 RVA: 0x000985C8 File Offset: 0x000967C8
		[SecuritySafeCritical]
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
			{
				this.Undo();
				this.m_safeTokenHandle.Dispose();
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" />.</summary>
		// Token: 0x06002904 RID: 10500 RVA: 0x000985F3 File Offset: 0x000967F3
		[ComVisible(false)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04001087 RID: 4231
		[SecurityCritical]
		private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04001088 RID: 4232
		private WindowsIdentity m_wi;

		// Token: 0x04001089 RID: 4233
		private FrameSecurityDescriptor m_fsd;
	}
}

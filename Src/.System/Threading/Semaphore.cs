using System;
using System.IO.Ports;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Limits the number of threads that can access a resource or pool of resources concurrently.</summary>
	// Token: 0x020003D4 RID: 980
	[ComVisible(false)]
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class Semaphore : WaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x060025A3 RID: 9635 RVA: 0x000AEEFE File Offset: 0x000AD0FE
		[SecuritySafeCritical]
		[global::__DynamicallyInvokable]
		public Semaphore(int initialCount, int maximumCount)
			: this(initialCount, maximumCount, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, and optionally specifying the name of a system semaphore object.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x060025A4 RID: 9636 RVA: 0x000AEF0C File Offset: 0x000AD10C
		[global::__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("Argument_SemaphoreInitialMaximum"));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			SafeWaitHandle safeWaitHandle = SafeNativeMethods.CreateSemaphore(null, initialCount, maximumCount, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
				}
				InternalResources.WinIOError();
			}
			base.SafeWaitHandle = safeWaitHandle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, optionally specifying the name of a system semaphore object, and specifying a variable that receives a value indicating whether a new system semaphore was created.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local semaphore was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system semaphore was created; <see langword="false" /> if the specified named system semaphore already existed. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x060025A5 RID: 9637 RVA: 0x000AEFD2 File Offset: 0x000AD1D2
		[global::__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew)
			: this(initialCount, maximumCount, name, out createdNew, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, optionally specifying the name of a system semaphore object, specifying a variable that receives a value indicating whether a new system semaphore was created, and specifying security access control for the system semaphore.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local semaphore was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system semaphore was created; <see langword="false" /> if the specified named system semaphore already existed. This parameter is passed uninitialized.</param>
		/// <param name="semaphoreSecurity">A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security to be applied to the named system semaphore.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x060025A6 RID: 9638 RVA: 0x000AEFE0 File Offset: 0x000AD1E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public unsafe Semaphore(int initialCount, int maximumCount, string name, out bool createdNew, SemaphoreSecurity semaphoreSecurity)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("Argument_SemaphoreInitialMaximum"));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			SafeWaitHandle safeWaitHandle;
			if (semaphoreSecurity != null)
			{
				Microsoft.Win32.NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new Microsoft.Win32.NativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = semaphoreSecurity.GetSecurityDescriptorBinaryForm();
				byte[] array;
				byte* ptr;
				if ((array = securityDescriptorBinaryForm) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				security_ATTRIBUTES.lpSecurityDescriptor = new SafeLocalMemHandle((IntPtr)((void*)ptr), false);
				safeWaitHandle = SafeNativeMethods.CreateSemaphore(security_ATTRIBUTES, initialCount, maximumCount, name);
				array = null;
			}
			else
			{
				safeWaitHandle = SafeNativeMethods.CreateSemaphore(null, initialCount, maximumCount, name);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeWaitHandle.IsInvalid)
			{
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
				}
				InternalResources.WinIOError();
			}
			createdNew = lastWin32Error != 183;
			base.SafeWaitHandle = safeWaitHandle;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000AF113 File Offset: 0x000AD313
		private Semaphore(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		/// <summary>Opens the specified named semaphore, if it already exists.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <returns>An object that represents the named system semaphore.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060025A8 RID: 9640 RVA: 0x000AF122 File Offset: 0x000AD322
		[global::__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name)
		{
			return Semaphore.OpenExisting(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize);
		}

		/// <summary>Opens the specified named semaphore, if it already exists, with the desired security access.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <returns>An object that represents the named system semaphore.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the desired security access rights.</exception>
		// Token: 0x060025A9 RID: 9641 RVA: 0x000AF130 File Offset: 0x000AD330
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name, SemaphoreRights rights)
		{
			Semaphore semaphore;
			switch (Semaphore.OpenExistingWorker(name, rights, out semaphore))
			{
			case Semaphore.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case Semaphore.OpenExistingResult.PathNotFound:
				InternalResources.WinIOError(3, string.Empty);
				return semaphore;
			case Semaphore.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
			default:
				return semaphore;
			}
		}

		/// <summary>Opens the specified named semaphore, if it already exists, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Semaphore" /> object that represents the named semaphore if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named semaphore was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060025AA RID: 9642 RVA: 0x000AF18B File Offset: 0x000AD38B
		[global::__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize, out result) == Semaphore.OpenExistingResult.Success;
		}

		/// <summary>Opens the specified named semaphore, if it already exists, with the desired security access, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Semaphore" /> object that represents the named semaphore if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named semaphore was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060025AB RID: 9643 RVA: 0x000AF19C File Offset: 0x000AD39C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, SemaphoreRights rights, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, rights, out result) == Semaphore.OpenExistingResult.Success;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000AF1AC File Offset: 0x000AD3AC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static Semaphore.OpenExistingResult OpenExistingWorker(string name, SemaphoreRights rights, out Semaphore result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "name" }), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			result = null;
			SafeWaitHandle safeWaitHandle = SafeNativeMethods.OpenSemaphore((int)rights, false, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (2 == lastWin32Error || 123 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.NameNotFound;
				}
				if (3 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.NameInvalid;
				}
				InternalResources.WinIOError();
			}
			result = new Semaphore(safeWaitHandle);
			return Semaphore.OpenExistingResult.Success;
		}

		/// <summary>Exits the semaphore and returns the previous count.</summary>
		/// <returns>The count on the semaphore before the <see cref="Overload:System.Threading.Semaphore.Release" /> method was called.</returns>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The semaphore count is already at the maximum value.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred with a named semaphore.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current semaphore represents a named system semaphore, but the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" />.  
		///  -or-  
		///  The current semaphore represents a named system semaphore, but it was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" />.</exception>
		// Token: 0x060025AD RID: 9645 RVA: 0x000AF25C File Offset: 0x000AD45C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[PrePrepareMethod]
		[global::__DynamicallyInvokable]
		public int Release()
		{
			return this.Release(1);
		}

		/// <summary>Exits the semaphore a specified number of times and returns the previous count.</summary>
		/// <param name="releaseCount">The number of times to exit the semaphore.</param>
		/// <returns>The count on the semaphore before the <see cref="Overload:System.Threading.Semaphore.Release" /> method was called.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="releaseCount" /> is less than 1.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The semaphore count is already at the maximum value.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred with a named semaphore.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current semaphore represents a named system semaphore, but the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" /> rights.  
		///  -or-  
		///  The current semaphore represents a named system semaphore, but it was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" /> rights.</exception>
		// Token: 0x060025AE RID: 9646 RVA: 0x000AF268 File Offset: 0x000AD468
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[global::__DynamicallyInvokable]
		public int Release(int releaseCount)
		{
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			int num;
			if (!SafeNativeMethods.ReleaseSemaphore(base.SafeWaitHandle, releaseCount, out num))
			{
				throw new SemaphoreFullException();
			}
			return num;
		}

		/// <summary>Gets the access control security for a named system semaphore.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security for the named system semaphore.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.Semaphore" /> object represents a named system semaphore, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.ReadPermissions" /> rights.  
		///  -or-  
		///  The current <see cref="T:System.Threading.Semaphore" /> object represents a named system semaphore and was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.ReadPermissions" /> rights.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x060025AF RID: 9647 RVA: 0x000AF2A5 File Offset: 0x000AD4A5
		public SemaphoreSecurity GetAccessControl()
		{
			return new SemaphoreSecurity(base.SafeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Sets the access control security for a named system semaphore.</summary>
		/// <param name="semaphoreSecurity">A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security to be applied to the named system semaphore.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="semaphoreSecurity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.ChangePermissions" /> rights.  
		///  -or-  
		///  The semaphore was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.ChangePermissions" /> rights.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Threading.Semaphore" /> object does not represent a named system semaphore.</exception>
		// Token: 0x060025B0 RID: 9648 RVA: 0x000AF2B4 File Offset: 0x000AD4B4
		public void SetAccessControl(SemaphoreSecurity semaphoreSecurity)
		{
			if (semaphoreSecurity == null)
			{
				throw new ArgumentNullException("semaphoreSecurity");
			}
			semaphoreSecurity.Persist(base.SafeWaitHandle);
		}

		// Token: 0x0400204B RID: 8267
		private const int MAX_PATH = 260;

		// Token: 0x0200080D RID: 2061
		private new enum OpenExistingResult
		{
			// Token: 0x0400355A RID: 13658
			Success,
			// Token: 0x0400355B RID: 13659
			NameNotFound,
			// Token: 0x0400355C RID: 13660
			PathNotFound,
			// Token: 0x0400355D RID: 13661
			NameInvalid
		}
	}
}

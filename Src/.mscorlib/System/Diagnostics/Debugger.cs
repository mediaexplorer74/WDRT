using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Enables communication with a debugger. This class cannot be inherited.</summary>
	// Token: 0x020003E4 RID: 996
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Debugger
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Debugger" /> class.</summary>
		// Token: 0x06003313 RID: 13075 RVA: 0x000C62C1 File Offset: 0x000C44C1
		[Obsolete("Do not create instances of the Debugger class.  Call the static methods directly on this type instead", true)]
		public Debugger()
		{
		}

		/// <summary>Signals a breakpoint to an attached debugger.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to break into the debugger.</exception>
		// Token: 0x06003314 RID: 13076 RVA: 0x000C62CC File Offset: 0x000C44CC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Break()
		{
			if (!Debugger.IsAttached)
			{
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				}
				catch (SecurityException)
				{
					return;
				}
			}
			Debugger.BreakInternal();
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000C6308 File Offset: 0x000C4508
		[SecuritySafeCritical]
		private static void BreakCanThrow()
		{
			if (!Debugger.IsAttached)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			Debugger.BreakInternal();
		}

		// Token: 0x06003316 RID: 13078
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BreakInternal();

		/// <summary>Launches and attaches a debugger to the process.</summary>
		/// <returns>
		///   <see langword="true" /> if the startup is successful or if the debugger is already attached; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to start the debugger.</exception>
		// Token: 0x06003317 RID: 13079 RVA: 0x000C6324 File Offset: 0x000C4524
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool Launch()
		{
			if (Debugger.IsAttached)
			{
				return true;
			}
			try
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return Debugger.LaunchInternal();
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000C6364 File Offset: 0x000C4564
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void NotifyOfCrossThreadDependencySlow()
		{
			Debugger.CrossThreadDependencyNotification crossThreadDependencyNotification = new Debugger.CrossThreadDependencyNotification();
			Debugger.CustomNotification(crossThreadDependencyNotification);
			if (Debugger.s_triggerThreadAbortExceptionForDebugger)
			{
				throw new ThreadAbortException();
			}
		}

		/// <summary>Notifies a debugger that execution is about to enter a path that involves a cross-thread dependency.</summary>
		// Token: 0x06003319 RID: 13081 RVA: 0x000C638A File Offset: 0x000C458A
		[ComVisible(false)]
		public static void NotifyOfCrossThreadDependency()
		{
			if (Debugger.IsAttached)
			{
				Debugger.NotifyOfCrossThreadDependencySlow();
			}
		}

		// Token: 0x0600331A RID: 13082
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LaunchInternal();

		/// <summary>Gets a value that indicates whether a debugger is attached to the process.</summary>
		/// <returns>
		///   <see langword="true" /> if a debugger is attached; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600331B RID: 13083
		[__DynamicallyInvokable]
		public static extern bool IsAttached
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Posts a message for the attached debugger.</summary>
		/// <param name="level">A description of the importance of the message.</param>
		/// <param name="category">The category of the message.</param>
		/// <param name="message">The message to show.</param>
		// Token: 0x0600331C RID: 13084
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Log(int level, string category, string message);

		/// <summary>Checks to see if logging is enabled by an attached debugger.</summary>
		/// <returns>
		///   <see langword="true" /> if a debugger is attached and logging is enabled; otherwise, <see langword="false" />. The attached debugger is the registered managed debugger in the <see langword="DbgManagedDebugger" /> registry key. For more information on this key, see Enabling JIT-Attach Debugging.</returns>
		// Token: 0x0600331D RID: 13085
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		// Token: 0x0600331E RID: 13086
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CustomNotification(ICustomDebuggerNotification data);

		// Token: 0x040016A8 RID: 5800
		private static bool s_triggerThreadAbortExceptionForDebugger;

		/// <summary>Represents the default category of message with a constant.</summary>
		// Token: 0x040016A9 RID: 5801
		public static readonly string DefaultCategory;

		// Token: 0x02000B7B RID: 2939
		private class CrossThreadDependencyNotification : ICustomDebuggerNotification
		{
		}
	}
}

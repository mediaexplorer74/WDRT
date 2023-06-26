using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path. This class cannot be inherited.</summary>
	// Token: 0x02000889 RID: 2185
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x06005CC3 RID: 23747 RVA: 0x0014674C File Offset: 0x0014494C
		private CallContext()
		{
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00146754 File Offset: 0x00144954
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		/// <summary>Empties a data slot with the specified name.</summary>
		/// <param name="name">The name of the data slot to empty.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CC5 RID: 23749 RVA: 0x0014677C File Offset: 0x0014497C
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
		}

		/// <summary>Retrieves an object with the specified name from the logical call context.</summary>
		/// <param name="name">The name of the item in the logical call context.</param>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CC6 RID: 23750 RVA: 0x001467AC File Offset: 0x001449AC
		[SecurityCritical]
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x001467D4 File Offset: 0x001449D4
		private static object IllogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x001467FC File Offset: 0x001449FC
		// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x00146823 File Offset: 0x00144A23
		internal static IPrincipal Principal
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
			}
			[SecurityCritical]
			set
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
			}
		}

		/// <summary>Gets or sets the host context associated with the current thread.</summary>
		/// <returns>The host context associated with the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x0014683C File Offset: 0x00144A3C
		// (set) Token: 0x06005CCB RID: 23755 RVA: 0x00146878 File Offset: 0x00144A78
		public static object HostContext
		{
			[SecurityCritical]
			get
			{
				ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
				object obj = executionContextReader.IllogicalCallContext.HostContext;
				if (obj == null)
				{
					obj = executionContextReader.LogicalCallContext.HostContext;
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				if (value is ILogicalThreadAffinative)
				{
					mutableExecutionContext.IllogicalCallContext.HostContext = null;
					mutableExecutionContext.LogicalCallContext.HostContext = value;
					return;
				}
				mutableExecutionContext.IllogicalCallContext.HostContext = value;
				mutableExecutionContext.LogicalCallContext.HostContext = null;
			}
		}

		/// <summary>Retrieves an object with the specified name from the <see cref="T:System.Runtime.Remoting.Messaging.CallContext" />.</summary>
		/// <param name="name">The name of the item in the call context.</param>
		/// <returns>The object in the call context associated with the specified name.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CCC RID: 23756 RVA: 0x001468CC File Offset: 0x00144ACC
		[SecurityCritical]
		public static object GetData(string name)
		{
			object obj = CallContext.LogicalGetData(name);
			if (obj == null)
			{
				return CallContext.IllogicalGetData(name);
			}
			return obj;
		}

		/// <summary>Stores a given object and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context.</param>
		/// <param name="data">The object to store in the call context.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CCD RID: 23757 RVA: 0x001468EC File Offset: 0x00144AEC
		[SecurityCritical]
		public static void SetData(string name, object data)
		{
			if (data is ILogicalThreadAffinative)
			{
				CallContext.LogicalSetData(name, data);
				return;
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.SetData(name, data);
		}

		/// <summary>Stores a given object in the logical call context and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the logical call context.</param>
		/// <param name="data">The object to store in the logical call context, this object must be serializable.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CCE RID: 23758 RVA: 0x00146930 File Offset: 0x00144B30
		[SecurityCritical]
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}

		/// <summary>Returns the headers that are sent along with the method call.</summary>
		/// <returns>The headers that are sent along with the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CCF RID: 23759 RVA: 0x00146964 File Offset: 0x00144B64
		[SecurityCritical]
		public static Header[] GetHeaders()
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			return logicalCallContext.InternalGetHeaders();
		}

		/// <summary>Sets the headers that are sent along with the method call.</summary>
		/// <param name="headers">A <see cref="T:System.Runtime.Remoting.Messaging.Header" /> array of the headers that are to be sent along with the method call.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005CD0 RID: 23760 RVA: 0x00146988 File Offset: 0x00144B88
		[SecurityCritical]
		public static void SetHeaders(Header[] headers)
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			logicalCallContext.InternalSetHeaders(headers);
		}
	}
}

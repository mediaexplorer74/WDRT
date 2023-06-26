using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Microsoft.Win32;
using Windows.Foundation.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x0200057E RID: 1406
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x06004255 RID: 16981 RVA: 0x000F8093 File Offset: 0x000F6293
		internal static void EnableToETW(bool enabled)
		{
			if (enabled)
			{
				AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.ETW;
				return;
			}
			AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.ETW;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x000F80B5 File Offset: 0x000F62B5
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return AsyncCausalityTracer.f_LoggingOn > (AsyncCausalityTracer.Loggers)0;
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000F80C0 File Offset: 0x000F62C0
		[SecuritySafeCritical]
		static AsyncCausalityTracer()
		{
			if (!Environment.IsWinRTSupported)
			{
				return;
			}
			string text = "Windows.Foundation.Diagnostics.AsyncCausalityTracer";
			Guid guid = new Guid(1350896422, 9854, 17691, 168, 144, 171, 106, 55, 2, 69, 238);
			object obj = null;
			try
			{
				int num = Microsoft.Win32.UnsafeNativeMethods.RoGetActivationFactory(text, ref guid, out obj);
				if (num >= 0 && obj != null)
				{
					AsyncCausalityTracer.s_TracerFactory = (IAsyncCausalityTracerStatics)obj;
					EventRegistrationToken eventRegistrationToken = AsyncCausalityTracer.s_TracerFactory.add_TracingStatusChanged(new EventHandler<TracingStatusChangedEventArgs>(AsyncCausalityTracer.TracingStatusChangedHandler));
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000F8194 File Offset: 0x000F6394
		[SecuritySafeCritical]
		private static void TracingStatusChangedHandler(object sender, TracingStatusChangedEventArgs args)
		{
			if (args.Enabled)
			{
				AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.CausalityTracer;
				return;
			}
			AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.CausalityTracer;
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000F81BC File Offset: 0x000F63BC
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationBegin(taskId, operationName, (long)relatedContext);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationCreation((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), operationName, relatedContext);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000F821C File Offset: 0x000F641C
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationEnd(taskId, status);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationCompletion((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (AsyncCausalityStatus)status);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000F827C File Offset: 0x000F647C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationRelation(taskId, relation);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationRelation((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (CausalityRelation)relation);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000F82DC File Offset: 0x000F64DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceSynchronousWorkBegin(taskId, work);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkStart((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (CausalitySynchronousWork)work);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000F833C File Offset: 0x000F653C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceSynchronousWorkEnd(work);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkCompletion((CausalityTraceLevel)traceLevel, CausalitySource.Library, (CausalitySynchronousWork)work);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000F8390 File Offset: 0x000F6590
		private static void LogAndDisable(Exception ex)
		{
			AsyncCausalityTracer.f_LoggingOn = (AsyncCausalityTracer.Loggers)0;
			Debugger.Log(0, "AsyncCausalityTracer", ex.ToString());
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000F83A9 File Offset: 0x000F65A9
		private static ulong GetOperationId(uint taskId)
		{
			return (ulong)(((long)AppDomain.CurrentDomain.Id << 32) + (long)((ulong)taskId));
		}

		// Token: 0x04001B94 RID: 7060
		private static readonly Guid s_PlatformId = new Guid(1258385830U, 62416, 16800, 155, 51, 2, 85, 6, 82, 185, 149);

		// Token: 0x04001B95 RID: 7061
		private const CausalitySource s_CausalitySource = CausalitySource.Library;

		// Token: 0x04001B96 RID: 7062
		private static IAsyncCausalityTracerStatics s_TracerFactory;

		// Token: 0x04001B97 RID: 7063
		private static AsyncCausalityTracer.Loggers f_LoggingOn;

		// Token: 0x02000C1F RID: 3103
		[Flags]
		private enum Loggers : byte
		{
			// Token: 0x040036DB RID: 14043
			CausalityTracer = 1,
			// Token: 0x040036DC RID: 14044
			ETW = 2
		}
	}
}

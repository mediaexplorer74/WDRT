using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Services
{
	/// <summary>Provides a way to register, unregister, and obtain a list of tracking handlers.</summary>
	// Token: 0x02000805 RID: 2053
	[SecurityCritical]
	[ComVisible(true)]
	public class TrackingServices
	{
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x0600588D RID: 22669 RVA: 0x00139694 File Offset: 0x00137894
		private static object TrackingServicesSyncObject
		{
			get
			{
				if (TrackingServices.s_TrackingServicesSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref TrackingServices.s_TrackingServicesSyncObject, obj, null);
				}
				return TrackingServices.s_TrackingServicesSyncObject;
			}
		}

		/// <summary>Registers a new tracking handler with the <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The tracking handler to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is already registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</exception>
		// Token: 0x0600588E RID: 22670 RVA: 0x001396C0 File Offset: 0x001378C0
		[SecurityCritical]
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				if (-1 != TrackingServices.Match(handler))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_TrackingHandlerAlreadyRegistered", new object[] { "handler" }));
				}
				if (TrackingServices._Handlers == null || TrackingServices._Size == TrackingServices._Handlers.Length)
				{
					ITrackingHandler[] array = new ITrackingHandler[TrackingServices._Size * 2 + 4];
					if (TrackingServices._Handlers != null)
					{
						Array.Copy(TrackingServices._Handlers, array, TrackingServices._Size);
					}
					TrackingServices._Handlers = array;
				}
				Volatile.Write<ITrackingHandler>(ref TrackingServices._Handlers[TrackingServices._Size++], handler);
			}
		}

		/// <summary>Unregisters the specified tracking handler from <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The handler to unregister.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is not registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</exception>
		// Token: 0x0600588F RID: 22671 RVA: 0x001397A4 File Offset: 0x001379A4
		[SecurityCritical]
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				int num = TrackingServices.Match(handler);
				if (-1 == num)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_HandlerNotRegistered", new object[] { handler }));
				}
				Array.Copy(TrackingServices._Handlers, num + 1, TrackingServices._Handlers, num, TrackingServices._Size - num - 1);
				TrackingServices._Size--;
			}
		}

		/// <summary>Gets an array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</summary>
		/// <returns>An array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06005890 RID: 22672 RVA: 0x00139844 File Offset: 0x00137A44
		public static ITrackingHandler[] RegisteredHandlers
		{
			[SecurityCritical]
			get
			{
				object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
				ITrackingHandler[] array;
				lock (trackingServicesSyncObject)
				{
					if (TrackingServices._Size == 0)
					{
						array = new ITrackingHandler[0];
					}
					else
					{
						ITrackingHandler[] array2 = new ITrackingHandler[TrackingServices._Size];
						for (int i = 0; i < TrackingServices._Size; i++)
						{
							array2[i] = TrackingServices._Handlers[i];
						}
						array = array2;
					}
				}
				return array;
			}
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x001398C4 File Offset: 0x00137AC4
		[SecurityCritical]
		internal static void MarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).MarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00139914 File Offset: 0x00137B14
		[SecurityCritical]
		internal static void UnmarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).UnmarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x00139964 File Offset: 0x00137B64
		[SecurityCritical]
		internal static void DisconnectedObject(object obj)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).DisconnectedObject(obj);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x001399B4 File Offset: 0x00137BB4
		private static int Match(ITrackingHandler handler)
		{
			int num = -1;
			for (int i = 0; i < TrackingServices._Size; i++)
			{
				if (TrackingServices._Handlers[i] == handler)
				{
					num = i;
					break;
				}
			}
			return num;
		}

		// Token: 0x0400285E RID: 10334
		private static volatile ITrackingHandler[] _Handlers = new ITrackingHandler[0];

		// Token: 0x0400285F RID: 10335
		private static volatile int _Size = 0;

		// Token: 0x04002860 RID: 10336
		private static object s_TrackingServicesSyncObject = null;
	}
}

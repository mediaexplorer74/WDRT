using System;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides methods that enable .NET Framework delegates that handle events to be added and removed from COM objects.</summary>
	// Token: 0x020009AA RID: 2474
	[__DynamicallyInvokable]
	public static class ComEventsHelper
	{
		/// <summary>Adds a delegate to the invocation list of events originating from a COM object.</summary>
		/// <param name="rcw">The COM object that triggers the events the caller would like to respond to.</param>
		/// <param name="iid">The identifier of the source interface used by the COM object to trigger events.</param>
		/// <param name="dispid">The dispatch identifier of the method on the source interface.</param>
		/// <param name="d">The delegate to invoke when the COM event is fired.</param>
		// Token: 0x06006327 RID: 25383 RVA: 0x0015322C File Offset: 0x0015142C
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
		{
			rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
			object obj = rcw;
			lock (obj)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.FromObject(rcw);
				ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid);
				if (comEventsSink == null)
				{
					comEventsSink = comEventsInfo.AddSink(ref iid);
				}
				ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid);
				if (comEventsMethod == null)
				{
					comEventsMethod = comEventsSink.AddMethod(dispid);
				}
				comEventsMethod.AddDelegate(d);
			}
		}

		/// <summary>Removes a delegate from the invocation list of events originating from a COM object.</summary>
		/// <param name="rcw">The COM object the delegate is attached to.</param>
		/// <param name="iid">The identifier of the source interface used by the COM object to trigger events.</param>
		/// <param name="dispid">The dispatch identifier of the method on the source interface.</param>
		/// <param name="d">The delegate to remove from the invocation list.</param>
		/// <returns>The delegate that was removed from the invocation list.</returns>
		// Token: 0x06006328 RID: 25384 RVA: 0x001532A4 File Offset: 0x001514A4
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
		{
			rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
			object obj = rcw;
			Delegate @delegate;
			lock (obj)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
				if (comEventsInfo == null)
				{
					@delegate = null;
				}
				else
				{
					ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid);
					if (comEventsSink == null)
					{
						@delegate = null;
					}
					else
					{
						ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid);
						if (comEventsMethod == null)
						{
							@delegate = null;
						}
						else
						{
							comEventsMethod.RemoveDelegate(d);
							if (comEventsMethod.Empty)
							{
								comEventsMethod = comEventsSink.RemoveMethod(comEventsMethod);
							}
							if (comEventsMethod == null)
							{
								comEventsSink = comEventsInfo.RemoveSink(comEventsSink);
							}
							if (comEventsSink == null)
							{
								Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), null);
								GC.SuppressFinalize(comEventsInfo);
							}
							@delegate = d;
						}
					}
				}
			}
			return @delegate;
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x0015335C File Offset: 0x0015155C
		[SecurityCritical]
		internal static object UnwrapIfTransparentProxy(object rcw)
		{
			if (RemotingServices.IsTransparentProxy(rcw))
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(rcw);
				try
				{
					rcw = Marshal.GetObjectForIUnknown(iunknownForObject);
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			return rcw;
		}
	}
}

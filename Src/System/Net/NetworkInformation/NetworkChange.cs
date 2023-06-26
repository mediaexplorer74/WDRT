using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows applications to receive notification when the Internet Protocol (IP) address of a network interface, also called a network card or adapter, changes.</summary>
	// Token: 0x020002DC RID: 732
	[global::__DynamicallyInvokable]
	public class NetworkChange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkChange" /> class.</summary>
		// Token: 0x060019CE RID: 6606 RVA: 0x0007E042 File Offset: 0x0007C242
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public NetworkChange()
		{
		}

		/// <summary>Registers a network change instance to receive network change events.</summary>
		/// <param name="nc">The instance to register.</param>
		// Token: 0x060019CF RID: 6607 RVA: 0x0007E04A File Offset: 0x0007C24A
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterNetworkChange(NetworkChange nc)
		{
		}

		/// <summary>Occurs when the availability of the network changes.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060019D0 RID: 6608 RVA: 0x0007E04C File Offset: 0x0007C24C
		// (remove) Token: 0x060019D1 RID: 6609 RVA: 0x0007E054 File Offset: 0x0007C254
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				NetworkChange.AvailabilityChangeListener.Start(value);
			}
			remove
			{
				NetworkChange.AvailabilityChangeListener.Stop(value);
			}
		}

		/// <summary>Occurs when the IP address of a network interface changes.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060019D2 RID: 6610 RVA: 0x0007E05C File Offset: 0x0007C25C
		// (remove) Token: 0x060019D3 RID: 6611 RVA: 0x0007E064 File Offset: 0x0007C264
		[global::__DynamicallyInvokable]
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			[global::__DynamicallyInvokable]
			add
			{
				NetworkChange.AddressChangeListener.Start(value);
			}
			[global::__DynamicallyInvokable]
			remove
			{
				NetworkChange.AddressChangeListener.Stop(value);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0007E06C File Offset: 0x0007C26C
		internal static bool CanListenForNetworkChanges
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A39 RID: 6713
		private static readonly object s_globalLock = new object();

		// Token: 0x04001A3A RID: 6714
		private static readonly object s_protectCallbackLock = new object();

		// Token: 0x020007A3 RID: 1955
		internal static class AvailabilityChangeListener
		{
			// Token: 0x060042ED RID: 17133 RVA: 0x00118845 File Offset: 0x00116A45
			private static void RunHandlerCallback(object state)
			{
				((NetworkAvailabilityChangedEventHandler)state)(null, new NetworkAvailabilityEventArgs(NetworkChange.AvailabilityChangeListener.isAvailable));
			}

			// Token: 0x060042EE RID: 17134 RVA: 0x00118860 File Offset: 0x00116A60
			private static void ChangedAddress(object sender, EventArgs eventArgs)
			{
				DictionaryEntry[] array = null;
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					bool flag2 = SystemNetworkInterface.InternalGetIsNetworkAvailable();
					if (flag2 != NetworkChange.AvailabilityChangeListener.isAvailable)
					{
						NetworkChange.AvailabilityChangeListener.isAvailable = flag2;
						array = new DictionaryEntry[NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count];
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.CopyTo(array, 0);
					}
				}
				if (array != null)
				{
					object s_protectCallbackLock = NetworkChange.s_protectCallbackLock;
					lock (s_protectCallbackLock)
					{
						foreach (DictionaryEntry dictionaryEntry in array)
						{
							NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = (NetworkAvailabilityChangedEventHandler)dictionaryEntry.Key;
							ExecutionContext executionContext = (ExecutionContext)dictionaryEntry.Value;
							if (executionContext == null)
							{
								networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(NetworkChange.AvailabilityChangeListener.isAvailable));
							}
							else
							{
								ExecutionContext.Run(executionContext.CreateCopy(), NetworkChange.AvailabilityChangeListener.s_RunHandlerCallback, networkAvailabilityChangedEventHandler);
							}
						}
					}
				}
			}

			// Token: 0x060042EF RID: 17135 RVA: 0x0011896C File Offset: 0x00116B6C
			internal static void Start(NetworkAvailabilityChangedEventHandler caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count == 0)
					{
						NetworkChange.AvailabilityChangeListener.isAvailable = NetworkInterface.GetIsNetworkAvailable();
						NetworkChange.AddressChangeListener.UnsafeStart(NetworkChange.AvailabilityChangeListener.addressChange);
					}
					if (caller != null && !NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Contains(caller))
					{
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Add(caller, ExecutionContext.Capture());
					}
				}
			}

			// Token: 0x060042F0 RID: 17136 RVA: 0x001189E8 File Offset: 0x00116BE8
			internal static void Stop(NetworkAvailabilityChangedEventHandler caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Remove(caller);
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count == 0)
					{
						NetworkChange.AddressChangeListener.Stop(NetworkChange.AvailabilityChangeListener.addressChange);
					}
				}
			}

			// Token: 0x040033B7 RID: 13239
			private static ListDictionary s_availabilityCallerArray = new ListDictionary();

			// Token: 0x040033B8 RID: 13240
			private static NetworkAddressChangedEventHandler addressChange = new NetworkAddressChangedEventHandler(NetworkChange.AvailabilityChangeListener.ChangedAddress);

			// Token: 0x040033B9 RID: 13241
			private static volatile bool isAvailable = false;

			// Token: 0x040033BA RID: 13242
			private static ContextCallback s_RunHandlerCallback = new ContextCallback(NetworkChange.AvailabilityChangeListener.RunHandlerCallback);
		}

		// Token: 0x020007A4 RID: 1956
		internal static class AddressChangeListener
		{
			// Token: 0x060042F2 RID: 17138 RVA: 0x00118A7C File Offset: 0x00116C7C
			private static void AddressChangedCallback(object stateObject, bool signaled)
			{
				DictionaryEntry[] array = null;
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AddressChangeListener.s_isPending = false;
					if (!NetworkChange.AddressChangeListener.s_isListening)
					{
						return;
					}
					NetworkChange.AddressChangeListener.s_isListening = false;
					if (NetworkChange.AddressChangeListener.s_callerArray.Count > 0)
					{
						array = new DictionaryEntry[NetworkChange.AddressChangeListener.s_callerArray.Count];
						NetworkChange.AddressChangeListener.s_callerArray.CopyTo(array, 0);
					}
					try
					{
						NetworkChange.AddressChangeListener.StartHelper(null, false, (StartIPOptions)stateObject);
					}
					catch (NetworkInformationException ex)
					{
						if (Logging.On)
						{
							Logging.Exception(Logging.Web, "AddressChangeListener", "AddressChangedCallback", ex);
						}
					}
				}
				if (array != null)
				{
					object s_protectCallbackLock = NetworkChange.s_protectCallbackLock;
					lock (s_protectCallbackLock)
					{
						foreach (DictionaryEntry dictionaryEntry in array)
						{
							NetworkAddressChangedEventHandler networkAddressChangedEventHandler = (NetworkAddressChangedEventHandler)dictionaryEntry.Key;
							ExecutionContext executionContext = (ExecutionContext)dictionaryEntry.Value;
							if (executionContext == null)
							{
								networkAddressChangedEventHandler(null, EventArgs.Empty);
							}
							else
							{
								ExecutionContext.Run(executionContext.CreateCopy(), NetworkChange.AddressChangeListener.s_runHandlerCallback, networkAddressChangedEventHandler);
							}
						}
					}
				}
			}

			// Token: 0x060042F3 RID: 17139 RVA: 0x00118BC8 File Offset: 0x00116DC8
			private static void RunHandlerCallback(object state)
			{
				((NetworkAddressChangedEventHandler)state)(null, EventArgs.Empty);
			}

			// Token: 0x060042F4 RID: 17140 RVA: 0x00118BDB File Offset: 0x00116DDB
			internal static void Start(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, true, StartIPOptions.Both);
			}

			// Token: 0x060042F5 RID: 17141 RVA: 0x00118BE5 File Offset: 0x00116DE5
			internal static void UnsafeStart(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, false, StartIPOptions.Both);
			}

			// Token: 0x060042F6 RID: 17142 RVA: 0x00118BF0 File Offset: 0x00116DF0
			private static void StartHelper(NetworkAddressChangedEventHandler caller, bool captureContext, StartIPOptions startIPOptions)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					if (NetworkChange.AddressChangeListener.s_ipv4Socket == null)
					{
						Socket.InitializeSockets();
						if (Socket.OSSupportsIPv4)
						{
							int num = -1;
							NetworkChange.AddressChangeListener.s_ipv4Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP, true, false);
							UnsafeNclNativeMethods.OSSOCK.ioctlsocket(NetworkChange.AddressChangeListener.s_ipv4Socket, -2147195266, ref num);
							NetworkChange.AddressChangeListener.s_ipv4WaitHandle = NetworkChange.AddressChangeListener.s_ipv4Socket.GetEventHandle();
						}
						if (Socket.OSSupportsIPv6)
						{
							int num = -1;
							NetworkChange.AddressChangeListener.s_ipv6Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.IP, true, false);
							UnsafeNclNativeMethods.OSSOCK.ioctlsocket(NetworkChange.AddressChangeListener.s_ipv6Socket, -2147195266, ref num);
							NetworkChange.AddressChangeListener.s_ipv6WaitHandle = NetworkChange.AddressChangeListener.s_ipv6Socket.GetEventHandle();
						}
					}
					if (caller != null && !NetworkChange.AddressChangeListener.s_callerArray.Contains(caller))
					{
						NetworkChange.AddressChangeListener.s_callerArray.Add(caller, captureContext ? ExecutionContext.Capture() : null);
					}
					if (!NetworkChange.AddressChangeListener.s_isListening && NetworkChange.AddressChangeListener.s_callerArray.Count != 0)
					{
						if (!NetworkChange.AddressChangeListener.s_isPending)
						{
							if (Socket.OSSupportsIPv4 && (startIPOptions & StartIPOptions.StartIPv4) != StartIPOptions.None)
							{
								NetworkChange.AddressChangeListener.s_registeredWait = ThreadPool.UnsafeRegisterWaitForSingleObject(NetworkChange.AddressChangeListener.s_ipv4WaitHandle, new WaitOrTimerCallback(NetworkChange.AddressChangeListener.AddressChangedCallback), StartIPOptions.StartIPv4, -1, true);
								int num2;
								SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(NetworkChange.AddressChangeListener.s_ipv4Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num2, SafeNativeOverlapped.Zero, IntPtr.Zero);
								if (socketError != SocketError.Success)
								{
									NetworkInformationException ex = new NetworkInformationException();
									if ((long)ex.ErrorCode != 10035L)
									{
										throw ex;
									}
								}
								socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(NetworkChange.AddressChangeListener.s_ipv4Socket, NetworkChange.AddressChangeListener.s_ipv4Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
								if (socketError != SocketError.Success)
								{
									throw new NetworkInformationException();
								}
							}
							if (Socket.OSSupportsIPv6 && (startIPOptions & StartIPOptions.StartIPv6) != StartIPOptions.None)
							{
								NetworkChange.AddressChangeListener.s_registeredWait = ThreadPool.UnsafeRegisterWaitForSingleObject(NetworkChange.AddressChangeListener.s_ipv6WaitHandle, new WaitOrTimerCallback(NetworkChange.AddressChangeListener.AddressChangedCallback), StartIPOptions.StartIPv6, -1, true);
								int num2;
								SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(NetworkChange.AddressChangeListener.s_ipv6Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num2, SafeNativeOverlapped.Zero, IntPtr.Zero);
								if (socketError != SocketError.Success)
								{
									NetworkInformationException ex2 = new NetworkInformationException();
									if ((long)ex2.ErrorCode != 10035L)
									{
										throw ex2;
									}
								}
								socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(NetworkChange.AddressChangeListener.s_ipv6Socket, NetworkChange.AddressChangeListener.s_ipv6Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
								if (socketError != SocketError.Success)
								{
									throw new NetworkInformationException();
								}
							}
						}
						NetworkChange.AddressChangeListener.s_isListening = true;
						NetworkChange.AddressChangeListener.s_isPending = true;
					}
				}
			}

			// Token: 0x060042F7 RID: 17143 RVA: 0x00118E50 File Offset: 0x00117050
			internal static void Stop(object caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AddressChangeListener.s_callerArray.Remove(caller);
					if (NetworkChange.AddressChangeListener.s_callerArray.Count == 0 && NetworkChange.AddressChangeListener.s_isListening)
					{
						NetworkChange.AddressChangeListener.s_isListening = false;
					}
				}
			}

			// Token: 0x040033BB RID: 13243
			private static ListDictionary s_callerArray = new ListDictionary();

			// Token: 0x040033BC RID: 13244
			private static ContextCallback s_runHandlerCallback = new ContextCallback(NetworkChange.AddressChangeListener.RunHandlerCallback);

			// Token: 0x040033BD RID: 13245
			private static RegisteredWaitHandle s_registeredWait;

			// Token: 0x040033BE RID: 13246
			private static bool s_isListening = false;

			// Token: 0x040033BF RID: 13247
			private static bool s_isPending = false;

			// Token: 0x040033C0 RID: 13248
			private static SafeCloseSocketAndEvent s_ipv4Socket = null;

			// Token: 0x040033C1 RID: 13249
			private static SafeCloseSocketAndEvent s_ipv6Socket = null;

			// Token: 0x040033C2 RID: 13250
			private static WaitHandle s_ipv4WaitHandle = null;

			// Token: 0x040033C3 RID: 13251
			private static WaitHandle s_ipv6WaitHandle = null;
		}
	}
}

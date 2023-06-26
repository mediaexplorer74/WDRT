using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000115 RID: 277
	internal static class NclUtilities
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0003CBFC File Offset: 0x0003ADFC
		internal static bool IsThreadPoolLow()
		{
			if (ComNetOS.IsAspNetServer)
			{
				return false;
			}
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return num < 2 || num2 < 2;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0003CC25 File Offset: 0x0003AE25
		internal static bool HasShutdownStarted
		{
			get
			{
				return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		internal static bool IsCredentialFailure(SecurityStatus error)
		{
			return error == SecurityStatus.LogonDenied || error == SecurityStatus.UnknownCredentials || error == SecurityStatus.NoImpersonation || error == SecurityStatus.NoAuthenticatingAuthority || error == SecurityStatus.UntrustedRoot || error == SecurityStatus.CertExpired || error == SecurityStatus.SmartcardLogonRequired || error == SecurityStatus.BadBinding;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		internal static bool IsClientFault(SecurityStatus error)
		{
			return error == SecurityStatus.InvalidToken || error == SecurityStatus.CannotPack || error == SecurityStatus.QopNotSupported || error == SecurityStatus.NoCredentials || error == SecurityStatus.MessageAltered || error == SecurityStatus.OutOfSequence || error == SecurityStatus.IncompleteMessage || error == SecurityStatus.IncompleteCredentials || error == SecurityStatus.WrongPrincipal || error == SecurityStatus.TimeSkew || error == SecurityStatus.IllegalMessage || error == SecurityStatus.CertUnknown || error == SecurityStatus.AlgorithmMismatch || error == SecurityStatus.SecurityQosFailed || error == SecurityStatus.UnsupportedPreauth;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0003CD13 File Offset: 0x0003AF13
		internal static ContextCallback ContextRelativeDemandCallback
		{
			get
			{
				if (NclUtilities.s_ContextRelativeDemandCallback == null)
				{
					NclUtilities.s_ContextRelativeDemandCallback = new ContextCallback(NclUtilities.DemandCallback);
				}
				return NclUtilities.s_ContextRelativeDemandCallback;
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0003CD38 File Offset: 0x0003AF38
		private static void DemandCallback(object state)
		{
			((CodeAccessPermission)state).Demand();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003CD48 File Offset: 0x0003AF48
		internal static bool GuessWhetherHostIsLoopback(string host)
		{
			string text = host.ToLowerInvariant();
			if (text == "localhost" || text == "loopback")
			{
				return true;
			}
			IPGlobalProperties ipglobalProperties = IPGlobalProperties.InternalGetIPGlobalProperties();
			string text2 = ipglobalProperties.HostName.ToLowerInvariant();
			return text == text2 || text == text2 + "." + ipglobalProperties.DomainName.ToLowerInvariant();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003CDB1 File Offset: 0x0003AFB1
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0003CDD4 File Offset: 0x0003AFD4
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				if (NclUtilities.s_AddressChange != null && NclUtilities.s_AddressChange.CheckAndReset())
				{
					return NclUtilities._LocalAddresses = NclUtilities.GetLocalAddresses();
				}
				if (NclUtilities._LocalAddresses != null)
				{
					return NclUtilities._LocalAddresses;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] array;
				lock (localAddressesLock)
				{
					if (NclUtilities._LocalAddresses != null)
					{
						array = NclUtilities._LocalAddresses;
					}
					else
					{
						NclUtilities.s_AddressChange = new NetworkAddressChangePolled();
						array = (NclUtilities._LocalAddresses = NclUtilities.GetLocalAddresses());
					}
				}
				return array;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0003CE74 File Offset: 0x0003B074
		private static IPAddress[] GetLocalAddresses()
		{
			ArrayList arrayList = new ArrayList(16);
			int num = 0;
			SafeLocalFree safeLocalFree = null;
			GetAdaptersAddressesFlags getAdaptersAddressesFlags = GetAdaptersAddressesFlags.SkipAnycast | GetAdaptersAddressesFlags.SkipMulticast | GetAdaptersAddressesFlags.SkipDnsServer | GetAdaptersAddressesFlags.SkipFriendlyName;
			uint num2 = 0U;
			uint num3 = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(AddressFamily.Unspecified, (uint)getAdaptersAddressesFlags, IntPtr.Zero, SafeLocalFree.Zero, ref num2);
			while (num3 == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)num2);
					num3 = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(AddressFamily.Unspecified, (uint)getAdaptersAddressesFlags, IntPtr.Zero, safeLocalFree, ref num2);
					if (num3 == 0U)
					{
						IntPtr intPtr = safeLocalFree.DangerousGetHandle();
						while (intPtr != IntPtr.Zero)
						{
							IpAdapterAddresses ipAdapterAddresses = (IpAdapterAddresses)Marshal.PtrToStructure(intPtr, typeof(IpAdapterAddresses));
							if (ipAdapterAddresses.firstUnicastAddress != IntPtr.Zero)
							{
								UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = SystemUnicastIPAddressInformation.MarshalUnicastIpAddressInformationCollection(ipAdapterAddresses.firstUnicastAddress);
								num += unicastIPAddressInformationCollection.Count;
								arrayList.Add(unicastIPAddressInformationCollection);
							}
							intPtr = ipAdapterAddresses.next;
						}
					}
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
					safeLocalFree = null;
				}
			}
			if (num3 != 0U && num3 != 232U)
			{
				throw new NetworkInformationException((int)num3);
			}
			IPAddress[] array = new IPAddress[num];
			uint num4 = 0U;
			foreach (object obj in arrayList)
			{
				UnicastIPAddressInformationCollection unicastIPAddressInformationCollection2 = (UnicastIPAddressInformationCollection)obj;
				foreach (IPAddressInformation ipaddressInformation in unicastIPAddressInformationCollection2)
				{
					array[(int)num4++] = ipaddressInformation.Address;
				}
			}
			return array;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0003D010 File Offset: 0x0003B210
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0003D040 File Offset: 0x0003B240
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x04000F51 RID: 3921
		private static volatile ContextCallback s_ContextRelativeDemandCallback;

		// Token: 0x04000F52 RID: 3922
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x04000F53 RID: 3923
		private static object _LocalAddressesLock;

		// Token: 0x04000F54 RID: 3924
		private static volatile NetworkAddressChangePolled s_AddressChange;
	}
}

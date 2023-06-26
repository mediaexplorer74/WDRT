using System;
using System.Collections;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Provides simple domain name resolution functionality.</summary>
	// Token: 0x020000DF RID: 223
	public static class Dns
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00029EFC File Offset: 0x000280FC
		private static IPHostEntry NativeToHostEntry(IntPtr nativePointer)
		{
			hostent hostent = (hostent)Marshal.PtrToStructure(nativePointer, typeof(hostent));
			IPHostEntry iphostEntry = new IPHostEntry();
			if (hostent.h_name != IntPtr.Zero)
			{
				iphostEntry.HostName = Marshal.PtrToStringAnsi(hostent.h_name);
			}
			ArrayList arrayList = new ArrayList();
			IntPtr intPtr = hostent.h_addr_list;
			nativePointer = Marshal.ReadIntPtr(intPtr);
			while (nativePointer != IntPtr.Zero)
			{
				int num = Marshal.ReadInt32(nativePointer);
				arrayList.Add(new IPAddress(num));
				intPtr = IntPtrHelper.Add(intPtr, IntPtr.Size);
				nativePointer = Marshal.ReadIntPtr(intPtr);
			}
			iphostEntry.AddressList = new IPAddress[arrayList.Count];
			arrayList.CopyTo(iphostEntry.AddressList, 0);
			arrayList.Clear();
			intPtr = hostent.h_aliases;
			nativePointer = Marshal.ReadIntPtr(intPtr);
			while (nativePointer != IntPtr.Zero)
			{
				string text = Marshal.PtrToStringAnsi(nativePointer);
				arrayList.Add(text);
				intPtr = IntPtrHelper.Add(intPtr, IntPtr.Size);
				nativePointer = Marshal.ReadIntPtr(intPtr);
			}
			iphostEntry.Aliases = new string[arrayList.Count];
			arrayList.CopyTo(iphostEntry.Aliases, 0);
			return iphostEntry;
		}

		/// <summary>Gets the DNS information for the specified DNS host name.</summary>
		/// <param name="hostName">The DNS name of the host.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains host information for the address specified in <paramref name="hostName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />.</exception>
		// Token: 0x0600078C RID: 1932 RVA: 0x0002A024 File Offset: 0x00028224
		[Obsolete("GetHostByName is obsoleted for this type, please use GetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			Dns.s_DnsPermission.Demand();
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				return Dns.GetUnresolveAnswer(ipaddress);
			}
			return Dns.InternalGetHostByName(hostName, false);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002A061 File Offset: 0x00028261
		internal static IPHostEntry InternalGetHostByName(string hostName)
		{
			return Dns.InternalGetHostByName(hostName, true);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002A06C File Offset: 0x0002826C
		internal static IPHostEntry InternalGetHostByName(string hostName, bool includeIPv6)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostByName", hostName);
			}
			if (hostName.Length > 255 || (hostName.Length == 255 && hostName[254] != '.'))
			{
				throw new ArgumentOutOfRangeException("hostName", SR.GetString("net_toolong", new object[]
				{
					"hostName",
					255.ToString(NumberFormatInfo.CurrentInfo)
				}));
			}
			IPHostEntry iphostEntry;
			if (Socket.LegacySupportsIPv6 || includeIPv6)
			{
				iphostEntry = Dns.GetAddrInfo(hostName);
			}
			else
			{
				IntPtr intPtr = UnsafeNclNativeMethods.OSSOCK.gethostbyname(hostName);
				if (intPtr == IntPtr.Zero)
				{
					SocketException ex = new SocketException();
					IPAddress ipaddress;
					if (IPAddress.TryParse(hostName, out ipaddress))
					{
						iphostEntry = Dns.GetUnresolveAnswer(ipaddress);
						if (Logging.On)
						{
							Logging.Exit(Logging.Sockets, "DNS", "GetHostByName", iphostEntry);
						}
						return iphostEntry;
					}
					throw ex;
				}
				else
				{
					iphostEntry = Dns.NativeToHostEntry(intPtr);
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostByName", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from an IP address.</summary>
		/// <param name="address">An IP address.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		// Token: 0x0600078F RID: 1935 RVA: 0x0002A17C File Offset: 0x0002837C
		[Obsolete("GetHostByAddress is obsoleted for this type, please use GetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry GetHostByAddress(string address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostByAddress", address);
			}
			Dns.s_DnsPermission.Demand();
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			IPHostEntry iphostEntry = Dns.InternalGetHostByAddress(IPAddress.Parse(address), false);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostByAddress", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from the specified <see cref="T:System.Net.IPAddress" />.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />.</exception>
		// Token: 0x06000790 RID: 1936 RVA: 0x0002A1E8 File Offset: 0x000283E8
		[Obsolete("GetHostByAddress is obsoleted for this type, please use GetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry GetHostByAddress(IPAddress address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostByAddress", "");
			}
			Dns.s_DnsPermission.Demand();
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			IPHostEntry iphostEntry = Dns.InternalGetHostByAddress(address, false);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostByAddress", iphostEntry);
			}
			return iphostEntry;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002A254 File Offset: 0x00028454
		internal static IPHostEntry InternalGetHostByAddress(IPAddress address, bool includeIPv6)
		{
			SocketError socketError = SocketError.Success;
			Exception ex;
			if (Socket.LegacySupportsIPv6 || includeIPv6)
			{
				string text = Dns.TryGetNameInfo(address, out socketError);
				if (socketError == SocketError.Success)
				{
					IPHostEntry iphostEntry;
					socketError = Dns.TryGetAddrInfo(text, out iphostEntry);
					if (socketError == SocketError.Success)
					{
						return iphostEntry;
					}
					if (Logging.On)
					{
						Logging.Exception(Logging.Sockets, "DNS", "InternalGetHostByAddress", new SocketException(socketError));
					}
					return iphostEntry;
				}
				else
				{
					ex = new SocketException(socketError);
				}
			}
			else
			{
				if (address.AddressFamily == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.ProtocolNotSupported);
				}
				int num = (int)address.m_Address;
				IntPtr intPtr = UnsafeNclNativeMethods.OSSOCK.gethostbyaddr(ref num, Marshal.SizeOf(typeof(int)), ProtocolFamily.InterNetwork);
				if (intPtr != IntPtr.Zero)
				{
					return Dns.NativeToHostEntry(intPtr);
				}
				ex = new SocketException();
			}
			if (Logging.On)
			{
				Logging.Exception(Logging.Sockets, "DNS", "InternalGetHostByAddress", ex);
			}
			throw ex;
		}

		/// <summary>Gets the host name of the local computer.</summary>
		/// <returns>A string that contains the DNS host name of the local computer.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		// Token: 0x06000792 RID: 1938 RVA: 0x0002A324 File Offset: 0x00028524
		public static string GetHostName()
		{
			Dns.s_DnsPermission.Demand();
			Socket.InitializeSockets();
			StringBuilder stringBuilder = new StringBuilder(256);
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.gethostname(stringBuilder, 256);
			if (socketError != SocketError.Success)
			{
				throw new SocketException();
			}
			return stringBuilder.ToString();
		}

		/// <summary>Resolves a DNS host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <param name="hostName">A DNS-style host name or IP address.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />.</exception>
		// Token: 0x06000793 RID: 1939 RVA: 0x0002A368 File Offset: 0x00028568
		[Obsolete("Resolve is obsoleted for this type, please use GetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry Resolve(string hostName)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "Resolve", hostName);
			}
			Dns.s_DnsPermission.Demand();
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			IPAddress ipaddress;
			IPHostEntry iphostEntry;
			if (IPAddress.TryParse(hostName, out ipaddress) && (ipaddress.AddressFamily != AddressFamily.InterNetworkV6 || Socket.LegacySupportsIPv6))
			{
				try
				{
					iphostEntry = Dns.InternalGetHostByAddress(ipaddress, false);
					goto IL_8D;
				}
				catch (SocketException ex)
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.Sockets, "DNS", "DNS.Resolve", ex.Message);
					}
					iphostEntry = Dns.GetUnresolveAnswer(ipaddress);
					goto IL_8D;
				}
			}
			iphostEntry = Dns.InternalGetHostByName(hostName, false);
			IL_8D:
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "Resolve", iphostEntry);
			}
			return iphostEntry;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0002A430 File Offset: 0x00028630
		private static IPHostEntry GetUnresolveAnswer(IPAddress address)
		{
			return new IPHostEntry
			{
				HostName = address.ToString(),
				Aliases = new string[0],
				AddressList = new IPAddress[] { address }
			};
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002A46C File Offset: 0x0002866C
		internal static bool TryInternalResolve(string hostName, out IPHostEntry result)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "TryInternalResolve", hostName);
			}
			if (string.IsNullOrEmpty(hostName) || hostName.Length > 255)
			{
				result = null;
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				result = Dns.GetUnresolveAnswer(ipaddress);
				return true;
			}
			IPHostEntry iphostEntry;
			if (Dns.TryGetAddrInfo(hostName, AddressInfoHints.AI_CANONNAME, out iphostEntry) != SocketError.Success)
			{
				result = null;
				return false;
			}
			result = iphostEntry;
			if (!ComNetOS.IsWin7Sp1orLater)
			{
				return true;
			}
			if (Dns.CompareHosts(hostName, iphostEntry.HostName))
			{
				return true;
			}
			IPHostEntry iphostEntry2;
			if (Dns.TryGetAddrInfo(hostName, AddressInfoHints.AI_FQDN, out iphostEntry2) != SocketError.Success)
			{
				return true;
			}
			if (Dns.CompareHosts(iphostEntry.HostName, iphostEntry2.HostName))
			{
				return true;
			}
			iphostEntry.isTrustedHost = false;
			return true;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0002A520 File Offset: 0x00028720
		private static bool CompareHosts(string host1, string host2)
		{
			string text;
			string text2;
			if (Dns.TryNormalizeHost(host1, out text) && Dns.TryNormalizeHost(host2, out text2))
			{
				return text.Equals(text2, StringComparison.OrdinalIgnoreCase);
			}
			return host1.Equals(host2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0002A554 File Offset: 0x00028754
		private static bool TryNormalizeHost(string host, out string result)
		{
			Uri uri;
			if (Uri.TryCreate(Uri.UriSchemeHttp + Uri.SchemeDelimiter + host, UriKind.Absolute, out uri))
			{
				result = uri.GetComponents(UriComponents.NormalizedHost, UriFormat.SafeUnescaped);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002A590 File Offset: 0x00028790
		private static void ResolveCallback(object context)
		{
			Dns.ResolveAsyncResult resolveAsyncResult = (Dns.ResolveAsyncResult)context;
			IPHostEntry iphostEntry;
			try
			{
				if (resolveAsyncResult.address != null)
				{
					iphostEntry = Dns.InternalGetHostByAddress(resolveAsyncResult.address, resolveAsyncResult.includeIPv6);
				}
				else
				{
					iphostEntry = Dns.InternalGetHostByName(resolveAsyncResult.hostName, resolveAsyncResult.includeIPv6);
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is ThreadAbortException || ex is StackOverflowException)
				{
					throw;
				}
				resolveAsyncResult.InvokeCallback(ex);
				return;
			}
			resolveAsyncResult.InvokeCallback(iphostEntry);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002A610 File Offset: 0x00028810
		private static IAsyncResult HostResolutionBeginHelper(string hostName, bool justReturnParsedIp, bool flowContext, bool includeIPv6, bool throwOnIPAny, AsyncCallback requestCallback, object state)
		{
			Dns.s_DnsPermission.Demand();
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			IPAddress ipaddress;
			Dns.ResolveAsyncResult resolveAsyncResult;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				if (throwOnIPAny && (ipaddress.Equals(IPAddress.Any) || ipaddress.Equals(IPAddress.IPv6Any)))
				{
					throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "hostNameOrAddress");
				}
				resolveAsyncResult = new Dns.ResolveAsyncResult(ipaddress, null, includeIPv6, state, requestCallback);
				if (justReturnParsedIp)
				{
					IPHostEntry unresolveAnswer = Dns.GetUnresolveAnswer(ipaddress);
					resolveAsyncResult.StartPostingAsyncOp(false);
					resolveAsyncResult.InvokeCallback(unresolveAnswer);
					resolveAsyncResult.FinishPostingAsyncOp();
					return resolveAsyncResult;
				}
			}
			else
			{
				resolveAsyncResult = new Dns.ResolveAsyncResult(hostName, null, includeIPv6, state, requestCallback);
			}
			if (flowContext)
			{
				resolveAsyncResult.StartPostingAsyncOp(false);
			}
			ThreadPool.UnsafeQueueUserWorkItem(Dns.resolveCallback, resolveAsyncResult);
			resolveAsyncResult.FinishPostingAsyncOp();
			return resolveAsyncResult;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002A6D0 File Offset: 0x000288D0
		private static IAsyncResult HostResolutionBeginHelper(IPAddress address, bool flowContext, bool includeIPv6, AsyncCallback requestCallback, object state)
		{
			Dns.s_DnsPermission.Demand();
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			Dns.ResolveAsyncResult resolveAsyncResult = new Dns.ResolveAsyncResult(address, null, includeIPv6, state, requestCallback);
			if (flowContext)
			{
				resolveAsyncResult.StartPostingAsyncOp(false);
			}
			ThreadPool.UnsafeQueueUserWorkItem(Dns.resolveCallback, resolveAsyncResult);
			resolveAsyncResult.FinishPostingAsyncOp();
			return resolveAsyncResult;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002A750 File Offset: 0x00028950
		private static IPHostEntry HostResolutionEndHelper(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Dns.ResolveAsyncResult resolveAsyncResult = asyncResult as Dns.ResolveAsyncResult;
			if (resolveAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (resolveAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndResolve" }));
			}
			resolveAsyncResult.InternalWaitForCompletion();
			resolveAsyncResult.EndCalled = true;
			Exception ex = resolveAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
			return (IPHostEntry)resolveAsyncResult.Result;
		}

		/// <summary>Begins an asynchronous request for <see cref="T:System.Net.IPHostEntry" /> information about the specified DNS host name.</summary>
		/// <param name="hostName">The DNS name of the host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered executing the DNS query.</exception>
		// Token: 0x0600079C RID: 1948 RVA: 0x0002A7DC File Offset: 0x000289DC
		[Obsolete("BeginGetHostByName is obsoleted for this type, please use BeginGetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static IAsyncResult BeginGetHostByName(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "BeginGetHostByName", hostName);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(hostName, true, true, false, false, requestCallback, stateObject);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "BeginGetHostByName", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostByName(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x0600079D RID: 1949 RVA: 0x0002A830 File Offset: 0x00028A30
		[Obsolete("EndGetHostByName is obsoleted for this type, please use EndGetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry EndGetHostByName(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "EndGetHostByName", asyncResult);
			}
			IPHostEntry iphostEntry = Dns.HostResolutionEndHelper(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "EndGetHostByName", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address.</exception>
		// Token: 0x0600079E RID: 1950 RVA: 0x0002A880 File Offset: 0x00028A80
		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostEntry", hostNameOrAddress);
			}
			Dns.s_DnsPermission.Demand();
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			IPAddress ipaddress;
			IPHostEntry iphostEntry;
			if (IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				if (ipaddress.Equals(IPAddress.Any) || ipaddress.Equals(IPAddress.IPv6Any))
				{
					throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "hostNameOrAddress");
				}
				iphostEntry = Dns.InternalGetHostByAddress(ipaddress, true);
			}
			else
			{
				iphostEntry = Dns.InternalGetHostByName(hostNameOrAddress, true);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostEntry", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <param name="address">An IP address.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x0600079F RID: 1951 RVA: 0x0002A92C File Offset: 0x00028B2C
		public static IPHostEntry GetHostEntry(IPAddress address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostEntry", "");
			}
			Dns.s_DnsPermission.Demand();
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			IPHostEntry iphostEntry = Dns.InternalGetHostByAddress(address, true);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostEntry", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x060007A0 RID: 1952 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "GetHostAddresses", hostNameOrAddress);
			}
			Dns.s_DnsPermission.Demand();
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			IPAddress ipaddress;
			IPAddress[] array;
			if (IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				if (ipaddress.Equals(IPAddress.Any) || ipaddress.Equals(IPAddress.IPv6Any))
				{
					throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "hostNameOrAddress");
				}
				array = new IPAddress[] { ipaddress };
			}
			else
			{
				array = Dns.InternalGetHostByName(hostNameOrAddress, true).AddressList;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "GetHostAddresses", array);
			}
			return array;
		}

		/// <summary>Asynchronously resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x060007A1 RID: 1953 RVA: 0x0002AA7C File Offset: 0x00028C7C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static IAsyncResult BeginGetHostEntry(string hostNameOrAddress, AsyncCallback requestCallback, object stateObject)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "BeginGetHostEntry", hostNameOrAddress);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(hostNameOrAddress, false, true, true, true, requestCallback, stateObject);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "BeginGetHostEntry", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Asynchronously resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <param name="address">The IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x060007A2 RID: 1954 RVA: 0x0002AAD0 File Offset: 0x00028CD0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static IAsyncResult BeginGetHostEntry(IPAddress address, AsyncCallback requestCallback, object stateObject)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "BeginGetHostEntry", address);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(address, true, true, requestCallback, stateObject);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "BeginGetHostEntry", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to an <see cref="Overload:System.Net.Dns.BeginGetHostEntry" /> method.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x060007A3 RID: 1955 RVA: 0x0002AB24 File Offset: 0x00028D24
		public static IPHostEntry EndGetHostEntry(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "EndGetHostEntry", asyncResult);
			}
			IPHostEntry iphostEntry = Dns.HostResolutionEndHelper(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "EndGetHostEntry", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Asynchronously returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x060007A4 RID: 1956 RVA: 0x0002AB74 File Offset: 0x00028D74
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static IAsyncResult BeginGetHostAddresses(string hostNameOrAddress, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "BeginGetHostAddresses", hostNameOrAddress);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(hostNameOrAddress, true, true, true, true, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "BeginGetHostAddresses", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host specified by the <paramref name="hostNameOrAddress" /> parameter of <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" />.</returns>
		// Token: 0x060007A5 RID: 1957 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		public static IPAddress[] EndGetHostAddresses(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "EndGetHostAddresses", asyncResult);
			}
			IPHostEntry iphostEntry = Dns.HostResolutionEndHelper(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "EndGetHostAddresses", iphostEntry);
			}
			return iphostEntry.AddressList;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002AC1C File Offset: 0x00028E1C
		internal static IAsyncResult UnsafeBeginGetHostAddresses(string hostName, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "UnsafeBeginGetHostAddresses", hostName);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(hostName, true, false, true, true, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "UnsafeBeginGetHostAddresses", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request to resolve a DNS host name or IP address to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <param name="hostName">The DNS name of the host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The caller does not have permission to access DNS information.</exception>
		// Token: 0x060007A7 RID: 1959 RVA: 0x0002AC70 File Offset: 0x00028E70
		[Obsolete("BeginResolve is obsoleted for this type, please use BeginGetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static IAsyncResult BeginResolve(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "BeginResolve", hostName);
			}
			IAsyncResult asyncResult = Dns.HostResolutionBeginHelper(hostName, false, true, false, false, requestCallback, stateObject);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "BeginResolve", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginResolve(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x060007A8 RID: 1960 RVA: 0x0002ACC4 File Offset: 0x00028EC4
		[Obsolete("EndResolve is obsoleted for this type, please use EndGetHostEntry instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IPHostEntry EndResolve(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "DNS", "EndResolve", asyncResult);
			}
			IPHostEntry iphostEntry;
			try
			{
				iphostEntry = Dns.HostResolutionEndHelper(asyncResult);
			}
			catch (SocketException ex)
			{
				IPAddress address = ((Dns.ResolveAsyncResult)asyncResult).address;
				if (address == null)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Sockets, "DNS", "DNS.EndResolve", ex.Message);
				}
				iphostEntry = Dns.GetUnresolveAnswer(address);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "DNS", "EndResolve", iphostEntry);
			}
			return iphostEntry;
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host as an asynchronous operation.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x060007A9 RID: 1961 RVA: 0x0002AD60 File Offset: 0x00028F60
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress)
		{
			return Task<IPAddress[]>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostAddresses), new Func<IAsyncResult, IPAddress[]>(Dns.EndGetHostAddresses), hostNameOrAddress, null);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <param name="address">An IP address.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x060007AA RID: 1962 RVA: 0x0002AD86 File Offset: 0x00028F86
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static Task<IPHostEntry> GetHostEntryAsync(IPAddress address)
		{
			return Task<IPHostEntry>.Factory.FromAsync<IPAddress>(new Func<IPAddress, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), address, null);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address.</exception>
		// Token: 0x060007AB RID: 1963 RVA: 0x0002ADAC File Offset: 0x00028FAC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
		{
			return Task<IPHostEntry>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), hostNameOrAddress, null);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002ADD4 File Offset: 0x00028FD4
		private static IPHostEntry GetAddrInfo(string name)
		{
			IPHostEntry iphostEntry;
			SocketError socketError = Dns.TryGetAddrInfo(name, out iphostEntry);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return iphostEntry;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002ADF5 File Offset: 0x00028FF5
		private static SocketError TryGetAddrInfo(string name, out IPHostEntry hostinfo)
		{
			return Dns.TryGetAddrInfo(name, AddressInfoHints.AI_CANONNAME, out hostinfo);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002AE00 File Offset: 0x00029000
		private unsafe static SocketError TryGetAddrInfo(string name, AddressInfoHints flags, out IPHostEntry hostinfo)
		{
			SafeFreeAddrInfo safeFreeAddrInfo = null;
			ArrayList arrayList = new ArrayList();
			string text = null;
			AddressInfo addressInfo = default(AddressInfo);
			addressInfo.ai_flags = flags;
			addressInfo.ai_family = AddressFamily.Unspecified;
			try
			{
				SocketError addrInfo = (SocketError)SafeFreeAddrInfo.GetAddrInfo(name, null, ref addressInfo, out safeFreeAddrInfo);
				if (addrInfo != SocketError.Success)
				{
					hostinfo = new IPHostEntry();
					hostinfo.HostName = name;
					hostinfo.Aliases = new string[0];
					hostinfo.AddressList = new IPAddress[0];
					return addrInfo;
				}
				for (AddressInfo* ptr = (AddressInfo*)(void*)safeFreeAddrInfo.DangerousGetHandle(); ptr != null; ptr = ptr->ai_next)
				{
					if (text == null && ptr->ai_canonname != null)
					{
						text = Marshal.PtrToStringUni((IntPtr)((void*)ptr->ai_canonname));
					}
					if (ptr->ai_family == AddressFamily.InterNetwork || (ptr->ai_family == AddressFamily.InterNetworkV6 && Socket.OSSupportsIPv6))
					{
						SocketAddress socketAddress = new SocketAddress(ptr->ai_family, ptr->ai_addrlen);
						for (int i = 0; i < ptr->ai_addrlen; i++)
						{
							socketAddress.m_Buffer[i] = ptr->ai_addr[i];
						}
						if (ptr->ai_family == AddressFamily.InterNetwork)
						{
							arrayList.Add(((IPEndPoint)IPEndPoint.Any.Create(socketAddress)).Address);
						}
						else
						{
							arrayList.Add(((IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress)).Address);
						}
					}
				}
			}
			finally
			{
				if (safeFreeAddrInfo != null)
				{
					safeFreeAddrInfo.Close();
				}
			}
			hostinfo = new IPHostEntry();
			hostinfo.HostName = ((text != null) ? text : name);
			hostinfo.Aliases = new string[0];
			hostinfo.AddressList = new IPAddress[arrayList.Count];
			arrayList.CopyTo(hostinfo.AddressList);
			return SocketError.Success;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002AFCC File Offset: 0x000291CC
		internal static string TryGetNameInfo(IPAddress addr, out SocketError errorCode)
		{
			SocketAddress socketAddress = new IPEndPoint(addr, 0).Serialize();
			StringBuilder stringBuilder = new StringBuilder(1025);
			int num = 4;
			Socket.InitializeSockets();
			errorCode = UnsafeNclNativeMethods.OSSOCK.GetNameInfoW(socketAddress.m_Buffer, socketAddress.m_Size, stringBuilder, stringBuilder.Capacity, null, 0, num);
			if (errorCode != SocketError.Success)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D23 RID: 3363
		private const int HostNameBufferLength = 256;

		// Token: 0x04000D24 RID: 3364
		private static DnsPermission s_DnsPermission = new DnsPermission(PermissionState.Unrestricted);

		// Token: 0x04000D25 RID: 3365
		private const int MaxHostName = 255;

		// Token: 0x04000D26 RID: 3366
		private static WaitCallback resolveCallback = new WaitCallback(Dns.ResolveCallback);

		// Token: 0x020006F6 RID: 1782
		private class ResolveAsyncResult : ContextAwareResult
		{
			// Token: 0x0600405E RID: 16478 RVA: 0x0010DD91 File Offset: 0x0010BF91
			internal ResolveAsyncResult(string hostName, object myObject, bool includeIPv6, object myState, AsyncCallback myCallBack)
				: base(myObject, myState, myCallBack)
			{
				this.hostName = hostName;
				this.includeIPv6 = includeIPv6;
			}

			// Token: 0x0600405F RID: 16479 RVA: 0x0010DDAC File Offset: 0x0010BFAC
			internal ResolveAsyncResult(IPAddress address, object myObject, bool includeIPv6, object myState, AsyncCallback myCallBack)
				: base(myObject, myState, myCallBack)
			{
				this.includeIPv6 = includeIPv6;
				this.address = address;
			}

			// Token: 0x04003086 RID: 12422
			internal readonly string hostName;

			// Token: 0x04003087 RID: 12423
			internal bool includeIPv6;

			// Token: 0x04003088 RID: 12424
			internal IPAddress address;
		}
	}
}

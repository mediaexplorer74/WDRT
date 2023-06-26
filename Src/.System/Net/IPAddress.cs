using System;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x0200014B RID: 331
	[global::__DynamicallyInvokable]
	[Serializable]
	public class IPAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36".</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newAddress" /> &lt; 0 or  
		/// <paramref name="newAddress" /> &gt; 0x00000000FFFFFFFF</exception>
		// Token: 0x06000B6A RID: 2922 RVA: 0x0003E5AD File Offset: 0x0003C7AD
		[global::__DynamicallyInvokable]
		public IPAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("newAddress");
			}
			this.m_Address = newAddress;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address.</param>
		/// <param name="scopeid">The long value of the scope identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or  
		/// <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF</exception>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0003E5E4 File Offset: 0x0003C7E4
		[global::__DynamicallyInvokable]
		public IPAddress(byte[] address, long scopeid)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length != 16)
			{
				throw new ArgumentException(SR.GetString("dns_bad_ip_address"), "address");
			}
			this.m_Family = AddressFamily.InterNetworkV6;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)((int)address[i * 2] * 256 + (int)address[i * 2 + 1]);
			}
			if (scopeid < 0L || scopeid > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("scopeid");
			}
			this.m_ScopeId = scopeid;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0003E680 File Offset: 0x0003C880
		private IPAddress(ushort[] address, uint scopeid)
		{
			this.m_Family = AddressFamily.InterNetworkV6;
			this.m_Numbers = address;
			this.m_ScopeId = (long)((ulong)scopeid);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address.</exception>
		// Token: 0x06000B6D RID: 2925 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		[global::__DynamicallyInvokable]
		public IPAddress(byte[] address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length != 4 && address.Length != 16)
			{
				throw new ArgumentException(SR.GetString("dns_bad_ip_address"), "address");
			}
			if (address.Length == 4)
			{
				this.m_Family = AddressFamily.InterNetwork;
				this.m_Address = (long)(((int)address[3] << 24) | ((int)address[2] << 16) | ((int)address[1] << 8) | (int)address[0]) & (long)((ulong)(-1));
				return;
			}
			this.m_Family = AddressFamily.InterNetworkV6;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)((int)address[i * 2] * 256 + (int)address[i * 2 + 1]);
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003E769 File Offset: 0x0003C969
		internal IPAddress(int newAddress)
		{
			this.m_Address = (long)newAddress & (long)((ulong)(-1));
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="ipString" /> was able to be parsed as an IP address; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null.</exception>
		// Token: 0x06000B6F RID: 2927 RVA: 0x0003E78F File Offset: 0x0003C98F
		[global::__DynamicallyInvokable]
		public static bool TryParse(string ipString, out IPAddress address)
		{
			address = IPAddress.InternalParse(ipString, true);
			return address != null;
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6.</param>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address.</exception>
		// Token: 0x06000B70 RID: 2928 RVA: 0x0003E79F File Offset: 0x0003C99F
		[global::__DynamicallyInvokable]
		public static IPAddress Parse(string ipString)
		{
			return IPAddress.InternalParse(ipString, false);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		private unsafe static IPAddress InternalParse(string ipString, bool tryParse)
		{
			if (ipString == null)
			{
				if (tryParse)
				{
					return null;
				}
				throw new ArgumentNullException("ipString");
			}
			else
			{
				if (ipString.IndexOf(':') != -1)
				{
					SocketException ex;
					if (Socket.OSSupportsIPv6)
					{
						byte[] array = new byte[16];
						SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetworkV6, 28);
						if (UnsafeNclNativeMethods.OSSOCK.WSAStringToAddress(ipString, AddressFamily.InterNetworkV6, IntPtr.Zero, socketAddress.m_Buffer, ref socketAddress.m_Size) == SocketError.Success)
						{
							for (int i = 0; i < 16; i++)
							{
								array[i] = socketAddress[i + 8];
							}
							long num = (long)(((int)socketAddress[27] << 24) + ((int)socketAddress[26] << 16) + ((int)socketAddress[25] << 8) + (int)socketAddress[24]);
							return new IPAddress(array, num);
						}
						if (tryParse)
						{
							return null;
						}
						ex = new SocketException();
					}
					else
					{
						int num2 = 0;
						if (ipString[0] != '[')
						{
							ipString += "]";
						}
						else
						{
							num2 = 1;
						}
						int length = ipString.Length;
						fixed (string text = ipString)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							if (IPv6AddressHelper.IsValidStrict(ptr, num2, ref length) || length != ipString.Length)
							{
								ushort[] array2 = new ushort[8];
								string text2 = null;
								ushort[] array3;
								ushort* ptr2;
								if ((array3 = array2) == null || array3.Length == 0)
								{
									ptr2 = null;
								}
								else
								{
									ptr2 = &array3[0];
								}
								IPv6AddressHelper.Parse(ipString, ptr2, 0, ref text2);
								array3 = null;
								if (text2 == null || text2.Length == 0)
								{
									return new IPAddress(array2, 0U);
								}
								text2 = text2.Substring(1);
								uint num3;
								if (uint.TryParse(text2, NumberStyles.None, null, out num3))
								{
									return new IPAddress(array2, num3);
								}
							}
						}
						if (tryParse)
						{
							return null;
						}
						ex = new SocketException(SocketError.InvalidArgument);
					}
					throw new FormatException(SR.GetString("dns_bad_ip_address"), ex);
				}
				Socket.InitializeSockets();
				int length2 = ipString.Length;
				long num4;
				fixed (string text3 = ipString)
				{
					char* ptr3 = text3;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					num4 = IPv4AddressHelper.ParseNonCanonical(ptr3, 0, ref length2, true);
				}
				if (num4 != -1L && length2 == ipString.Length)
				{
					num4 = ((num4 & 255L) << 24) | (((num4 & 65280L) << 8) | (((num4 & 16711680L) >> 8) | ((num4 & (long)((ulong)(-16777216))) >> 24)));
					return new IPAddress(num4);
				}
				if (tryParse)
				{
					return null;
				}
				throw new FormatException(SR.GetString("dns_bad_ip_address"));
			}
		}

		/// <summary>An Internet Protocol (IP) address.</summary>
		/// <returns>The long value of the IP address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0003E9F4 File Offset: 0x0003CBF4
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0003EA11 File Offset: 0x0003CC11
		[Obsolete("This property has been deprecated. It is address family dependent. Please use IPAddress.Equals method to perform comparisons. http://go.microsoft.com/fwlink/?linkid=14202")]
		public long Address
		{
			get
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return this.m_Address;
			}
			set
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (this.m_Address != value)
				{
					this.m_ToString = null;
					this.m_Address = value;
				}
			}
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06000B74 RID: 2932 RVA: 0x0003EA40 File Offset: 0x0003CC40
		[global::__DynamicallyInvokable]
		public byte[] GetAddressBytes()
		{
			byte[] array;
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				array = new byte[16];
				int num = 0;
				for (int i = 0; i < 8; i++)
				{
					array[num++] = (byte)((this.m_Numbers[i] >> 8) & 255);
					array[num++] = (byte)(this.m_Numbers[i] & 255);
				}
			}
			else
			{
				array = new byte[]
				{
					(byte)this.m_Address,
					(byte)(this.m_Address >> 8),
					(byte)(this.m_Address >> 16),
					(byte)(this.m_Address >> 24)
				};
			}
			return array;
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0003EAD5 File Offset: 0x0003CCD5
		[global::__DynamicallyInvokable]
		public AddressFamily AddressFamily
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <see langword="AddressFamily" /> = <see langword="InterNetwork" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0  
		/// -or-
		///
		/// <paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF</exception>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0003EADD File Offset: 0x0003CCDD
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0003EAFC File Offset: 0x0003CCFC
		[global::__DynamicallyInvokable]
		public long ScopeId
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_Family == AddressFamily.InterNetwork)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return this.m_ScopeId;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (this.m_Family == AddressFamily.InterNetwork)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.m_ScopeId != value)
				{
					this.m_Address = value;
					this.m_ScopeId = value;
					this.m_ToString = null;
				}
			}
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad.</exception>
		// Token: 0x06000B78 RID: 2936 RVA: 0x0003EB50 File Offset: 0x0003CD50
		[global::__DynamicallyInvokable]
		public unsafe override string ToString()
		{
			if (this.m_ToString == null)
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					int num = 256;
					StringBuilder stringBuilder = new StringBuilder(num);
					if (Socket.OSSupportsIPv6)
					{
						SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetworkV6, 28);
						int num2 = 8;
						for (int i = 0; i < 8; i++)
						{
							socketAddress[num2++] = (byte)(this.m_Numbers[i] >> 8);
							socketAddress[num2++] = (byte)this.m_Numbers[i];
						}
						if (this.m_ScopeId > 0L)
						{
							socketAddress[24] = (byte)this.m_ScopeId;
							socketAddress[25] = (byte)(this.m_ScopeId >> 8);
							socketAddress[26] = (byte)(this.m_ScopeId >> 16);
							socketAddress[27] = (byte)(this.m_ScopeId >> 24);
						}
						SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAAddressToString(socketAddress.m_Buffer, socketAddress.m_Size, IntPtr.Zero, stringBuilder, ref num);
						if (socketError != SocketError.Success)
						{
							throw new SocketException();
						}
					}
					else
					{
						string text = string.Format(CultureInfo.InvariantCulture, "{0:x4}:{1:x4}:{2:x4}:{3:x4}:{4:x4}:{5:x4}:{6}.{7}.{8}.{9}", new object[]
						{
							this.m_Numbers[0],
							this.m_Numbers[1],
							this.m_Numbers[2],
							this.m_Numbers[3],
							this.m_Numbers[4],
							this.m_Numbers[5],
							(this.m_Numbers[6] >> 8) & 255,
							(int)(this.m_Numbers[6] & 255),
							(this.m_Numbers[7] >> 8) & 255,
							(int)(this.m_Numbers[7] & 255)
						});
						stringBuilder.Append(text);
						if (this.m_ScopeId != 0L)
						{
							stringBuilder.Append('%').Append((uint)this.m_ScopeId);
						}
					}
					this.m_ToString = stringBuilder.ToString();
				}
				else
				{
					int num3 = 15;
					char* ptr = stackalloc char[(UIntPtr)30];
					int num4 = (int)((this.m_Address >> 24) & 255L);
					do
					{
						ptr[(IntPtr)(--num3) * 2] = (char)(48 + num4 % 10);
						num4 /= 10;
					}
					while (num4 > 0);
					ptr[(IntPtr)(--num3) * 2] = '.';
					num4 = (int)((this.m_Address >> 16) & 255L);
					do
					{
						ptr[(IntPtr)(--num3) * 2] = (char)(48 + num4 % 10);
						num4 /= 10;
					}
					while (num4 > 0);
					ptr[(IntPtr)(--num3) * 2] = '.';
					num4 = (int)((this.m_Address >> 8) & 255L);
					do
					{
						ptr[(IntPtr)(--num3) * 2] = (char)(48 + num4 % 10);
						num4 /= 10;
					}
					while (num4 > 0);
					ptr[(IntPtr)(--num3) * 2] = '.';
					num4 = (int)(this.m_Address & 255L);
					do
					{
						ptr[(IntPtr)(--num3) * 2] = (char)(48 + num4 % 10);
						num4 /= 10;
					}
					while (num4 > 0);
					this.m_ToString = new string(ptr, num3, 15 - num3);
				}
			}
			return this.m_ToString;
		}

		/// <summary>Converts a long value from host byte order to network byte order.</summary>
		/// <param name="host">The number to convert, expressed in host byte order.</param>
		/// <returns>A long value, expressed in network byte order.</returns>
		// Token: 0x06000B79 RID: 2937 RVA: 0x0003EE7F File Offset: 0x0003D07F
		[global::__DynamicallyInvokable]
		public static long HostToNetworkOrder(long host)
		{
			return (((long)IPAddress.HostToNetworkOrder((int)host) & (long)((ulong)(-1))) << 32) | ((long)IPAddress.HostToNetworkOrder((int)(host >> 32)) & (long)((ulong)(-1)));
		}

		/// <summary>Converts an integer value from host byte order to network byte order.</summary>
		/// <param name="host">The number to convert, expressed in host byte order.</param>
		/// <returns>An integer value, expressed in network byte order.</returns>
		// Token: 0x06000B7A RID: 2938 RVA: 0x0003EE9E File Offset: 0x0003D09E
		[global::__DynamicallyInvokable]
		public static int HostToNetworkOrder(int host)
		{
			return (((int)IPAddress.HostToNetworkOrder((short)host) & 65535) << 16) | ((int)IPAddress.HostToNetworkOrder((short)(host >> 16)) & 65535);
		}

		/// <summary>Converts a short value from host byte order to network byte order.</summary>
		/// <param name="host">The number to convert, expressed in host byte order.</param>
		/// <returns>A short value, expressed in network byte order.</returns>
		// Token: 0x06000B7B RID: 2939 RVA: 0x0003EEC1 File Offset: 0x0003D0C1
		[global::__DynamicallyInvokable]
		public static short HostToNetworkOrder(short host)
		{
			return (short)(((int)(host & 255) << 8) | ((host >> 8) & 255));
		}

		/// <summary>Converts a long value from network byte order to host byte order.</summary>
		/// <param name="network">The number to convert, expressed in network byte order.</param>
		/// <returns>A long value, expressed in host byte order.</returns>
		// Token: 0x06000B7C RID: 2940 RVA: 0x0003EED7 File Offset: 0x0003D0D7
		[global::__DynamicallyInvokable]
		public static long NetworkToHostOrder(long network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts an integer value from network byte order to host byte order.</summary>
		/// <param name="network">The number to convert, expressed in network byte order.</param>
		/// <returns>An integer value, expressed in host byte order.</returns>
		// Token: 0x06000B7D RID: 2941 RVA: 0x0003EEDF File Offset: 0x0003D0DF
		[global::__DynamicallyInvokable]
		public static int NetworkToHostOrder(int network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts a short value from network byte order to host byte order.</summary>
		/// <param name="network">The number to convert, expressed in network byte order.</param>
		/// <returns>A short value, expressed in host byte order.</returns>
		// Token: 0x06000B7E RID: 2942 RVA: 0x0003EEE7 File Offset: 0x0003D0E7
		[global::__DynamicallyInvokable]
		public static short NetworkToHostOrder(short network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <param name="address">An IP address.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="address" /> is the loopback address; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B7F RID: 2943 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
		[global::__DynamicallyInvokable]
		public static bool IsLoopback(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.m_Family == AddressFamily.InterNetworkV6)
			{
				return address.Equals(IPAddress.IPv6Loopback);
			}
			return (address.m_Address & 255L) == (IPAddress.Loopback.m_Address & 255L);
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0003EF41 File Offset: 0x0003D141
		internal bool IsBroadcast
		{
			get
			{
				return this.m_Family != AddressFamily.InterNetworkV6 && this.m_Address == IPAddress.Broadcast.m_Address;
			}
		}

		/// <summary>Gets whether the address is an IPv6 multicast global address.</summary>
		/// <returns>
		///   <see langword="true" /> if the IP address is an IPv6 multicast global address; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0003EF61 File Offset: 0x0003D161
		[global::__DynamicallyInvokable]
		public bool IsIPv6Multicast
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65280) == 65280;
			}
		}

		/// <summary>Gets whether the address is an IPv6 link local address.</summary>
		/// <returns>
		///   <see langword="true" /> if the IP address is an IPv6 link local address; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0003EF84 File Offset: 0x0003D184
		[global::__DynamicallyInvokable]
		public bool IsIPv6LinkLocal
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65472) == 65152;
			}
		}

		/// <summary>Gets whether the address is an IPv6 site local address.</summary>
		/// <returns>
		///   <see langword="true" /> if the IP address is an IPv6 site local address; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0003EFA7 File Offset: 0x0003D1A7
		[global::__DynamicallyInvokable]
		public bool IsIPv6SiteLocal
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65472) == 65216;
			}
		}

		/// <summary>Gets whether the address is an IPv6 Teredo address.</summary>
		/// <returns>
		///   <see langword="true" /> if the IP address is an IPv6 Teredo address; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0003EFCA File Offset: 0x0003D1CA
		[global::__DynamicallyInvokable]
		public bool IsIPv6Teredo
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && this.m_Numbers[0] == 8193 && this.m_Numbers[1] == 0;
			}
		}

		/// <summary>Gets whether the IP address is an IPv4-mapped IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.  
		///  <see langword="true" /> if the IP address is an IPv4-mapped IPv6 address; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0003EFF4 File Offset: 0x0003D1F4
		[global::__DynamicallyInvokable]
		public bool IsIPv4MappedToIPv6
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					return false;
				}
				for (int i = 0; i < 5; i++)
				{
					if (this.m_Numbers[i] != 0)
					{
						return false;
					}
				}
				return this.m_Numbers[5] == ushort.MaxValue;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003F034 File Offset: 0x0003D234
		internal bool Equals(object comparandObj, bool compareScopeId)
		{
			IPAddress ipaddress = comparandObj as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.m_Family != ipaddress.m_Family)
			{
				return false;
			}
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				for (int i = 0; i < 8; i++)
				{
					if (ipaddress.m_Numbers[i] != this.m_Numbers[i])
					{
						return false;
					}
				}
				return ipaddress.m_ScopeId == this.m_ScopeId || !compareScopeId;
			}
			return ipaddress.m_Address == this.m_Address;
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the two addresses are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B87 RID: 2951 RVA: 0x0003F0AC File Offset: 0x0003D2AC
		[global::__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			return this.Equals(comparand, true);
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06000B88 RID: 2952 RVA: 0x0003F0B6 File Offset: 0x0003D2B6
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				if (this.m_HashCode == 0)
				{
					this.m_HashCode = StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
				}
				return this.m_HashCode;
			}
			return (int)this.m_Address;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003F0F0 File Offset: 0x0003D2F0
		internal IPAddress Snapshot()
		{
			AddressFamily family = this.m_Family;
			if (family == AddressFamily.InterNetwork)
			{
				return new IPAddress(this.m_Address);
			}
			if (family != AddressFamily.InterNetworkV6)
			{
				throw new InternalException();
			}
			return new IPAddress(this.m_Numbers, (uint)this.m_ScopeId);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.  
		///  An IPv6 address.</returns>
		// Token: 0x06000B8A RID: 2954 RVA: 0x0003F134 File Offset: 0x0003D334
		[global::__DynamicallyInvokable]
		public IPAddress MapToIPv6()
		{
			if (this.AddressFamily == AddressFamily.InterNetworkV6)
			{
				return this;
			}
			return new IPAddress(new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				ushort.MaxValue,
				(ushort)(((this.m_Address & 65280L) >> 8) | ((this.m_Address & 255L) << 8)),
				(ushort)(((this.m_Address & (long)((ulong)(-16777216))) >> 24) | ((this.m_Address & 16711680L) >> 8))
			}, 0U);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv4 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.  
		///  An IPv4 address.</returns>
		// Token: 0x06000B8B RID: 2955 RVA: 0x0003F1AC File Offset: 0x0003D3AC
		[global::__DynamicallyInvokable]
		public IPAddress MapToIPv4()
		{
			if (this.AddressFamily == AddressFamily.InterNetwork)
			{
				return this;
			}
			long num = (long)((ulong)(((uint)(this.m_Numbers[6] & 65280) >> 8) | (uint)((uint)(this.m_Numbers[6] & 255) << 8) | ((((uint)(this.m_Numbers[7] & 65280) >> 8) | (uint)((uint)(this.m_Numbers[7] & 255) << 8)) << 16)));
			return new IPAddress(num);
		}

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x040010EA RID: 4330
		[global::__DynamicallyInvokable]
		public static readonly IPAddress Any = new IPAddress(0);

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x040010EB RID: 4331
		[global::__DynamicallyInvokable]
		public static readonly IPAddress Loopback = new IPAddress(16777343);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x040010EC RID: 4332
		[global::__DynamicallyInvokable]
		public static readonly IPAddress Broadcast = new IPAddress((long)((ulong)(-1)));

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x040010ED RID: 4333
		[global::__DynamicallyInvokable]
		public static readonly IPAddress None = IPAddress.Broadcast;

		// Token: 0x040010EE RID: 4334
		internal const long LoopbackMask = 255L;

		// Token: 0x040010EF RID: 4335
		internal long m_Address;

		// Token: 0x040010F0 RID: 4336
		[NonSerialized]
		internal string m_ToString;

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x040010F1 RID: 4337
		[global::__DynamicallyInvokable]
		public static readonly IPAddress IPv6Any = new IPAddress(new byte[16], 0L);

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x040010F2 RID: 4338
		[global::__DynamicallyInvokable]
		public static readonly IPAddress IPv6Loopback = new IPAddress(new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 1
		}, 0L);

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x040010F3 RID: 4339
		[global::__DynamicallyInvokable]
		public static readonly IPAddress IPv6None = new IPAddress(new byte[16], 0L);

		// Token: 0x040010F4 RID: 4340
		private AddressFamily m_Family = AddressFamily.InterNetwork;

		// Token: 0x040010F5 RID: 4341
		private ushort[] m_Numbers = new ushort[8];

		// Token: 0x040010F6 RID: 4342
		private long m_ScopeId;

		// Token: 0x040010F7 RID: 4343
		private int m_HashCode;

		// Token: 0x040010F8 RID: 4344
		internal const int IPv4AddressBytes = 4;

		// Token: 0x040010F9 RID: 4345
		internal const int IPv6AddressBytes = 16;

		// Token: 0x040010FA RID: 4346
		internal const int NumberOfLabels = 8;
	}
}

using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	/// <summary>Stores serialized information from <see cref="T:System.Net.EndPoint" /> derived classes.</summary>
	// Token: 0x02000161 RID: 353
	[global::__DynamicallyInvokable]
	public class SocketAddress
	{
		/// <summary>Gets the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value of the current <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated values.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00043A78 File Offset: 0x00041C78
		[global::__DynamicallyInvokable]
		public AddressFamily Family
		{
			[global::__DynamicallyInvokable]
			get
			{
				return (AddressFamily)((int)this.m_Buffer[0] | ((int)this.m_Buffer[1] << 8));
			}
		}

		/// <summary>Gets the underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>The underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00043A9A File Offset: 0x00041C9A
		[global::__DynamicallyInvokable]
		public int Size
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Size;
			}
		}

		/// <summary>Gets or sets the specified index element in the underlying buffer.</summary>
		/// <param name="offset">The array index element of the desired information.</param>
		/// <returns>The value of the specified index element in the underlying buffer.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist in the buffer.</exception>
		// Token: 0x170002EE RID: 750
		[global::__DynamicallyInvokable]
		public byte this[int offset]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Buffer[offset];
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.m_Buffer[offset] != value)
				{
					this.m_changed = true;
				}
				this.m_Buffer[offset] = value;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class for the given address family.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value.</param>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00043AEF File Offset: 0x00041CEF
		[global::__DynamicallyInvokable]
		public SocketAddress(AddressFamily family)
			: this(family, 32)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class using the specified address family and buffer size.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value.</param>
		/// <param name="size">The number of bytes to allocate for the underlying buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 2. These 2 bytes are needed to store <paramref name="family" />.</exception>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00043AFC File Offset: 0x00041CFC
		[global::__DynamicallyInvokable]
		public SocketAddress(AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.m_Size = size;
			this.m_Buffer = new byte[(size / IntPtr.Size + 2) * IntPtr.Size];
			this.m_Buffer[0] = (byte)family;
			this.m_Buffer[1] = (byte)(family >> 8);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00043B5C File Offset: 0x00041D5C
		internal SocketAddress(IPAddress ipAddress)
			: this(ipAddress.AddressFamily, (ipAddress.AddressFamily == AddressFamily.InterNetwork) ? 16 : 28)
		{
			this.m_Buffer[2] = 0;
			this.m_Buffer[3] = 0;
			if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				this.m_Buffer[4] = 0;
				this.m_Buffer[5] = 0;
				this.m_Buffer[6] = 0;
				this.m_Buffer[7] = 0;
				long scopeId = ipAddress.ScopeId;
				this.m_Buffer[24] = (byte)scopeId;
				this.m_Buffer[25] = (byte)(scopeId >> 8);
				this.m_Buffer[26] = (byte)(scopeId >> 16);
				this.m_Buffer[27] = (byte)(scopeId >> 24);
				byte[] addressBytes = ipAddress.GetAddressBytes();
				for (int i = 0; i < addressBytes.Length; i++)
				{
					this.m_Buffer[8 + i] = addressBytes[i];
				}
				return;
			}
			this.m_Buffer[4] = (byte)ipAddress.m_Address;
			this.m_Buffer[5] = (byte)(ipAddress.m_Address >> 8);
			this.m_Buffer[6] = (byte)(ipAddress.m_Address >> 16);
			this.m_Buffer[7] = (byte)(ipAddress.m_Address >> 24);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00043C69 File Offset: 0x00041E69
		internal SocketAddress(IPAddress ipaddress, int port)
			: this(ipaddress)
		{
			this.m_Buffer[2] = (byte)(port >> 8);
			this.m_Buffer[3] = (byte)port;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00043C88 File Offset: 0x00041E88
		internal IPAddress GetIPAddress()
		{
			if (this.Family == AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.m_Buffer[i + 8];
				}
				long num = (long)(((int)this.m_Buffer[27] << 24) + ((int)this.m_Buffer[26] << 16) + ((int)this.m_Buffer[25] << 8) + (int)this.m_Buffer[24]);
				return new IPAddress(array, num);
			}
			if (this.Family == AddressFamily.InterNetwork)
			{
				long num2 = (long)((int)(this.m_Buffer[4] & byte.MaxValue) | (((int)this.m_Buffer[5] << 8) & 65280) | (((int)this.m_Buffer[6] << 16) & 16711680) | ((int)this.m_Buffer[7] << 24)) & (long)((ulong)(-1));
				return new IPAddress(num2);
			}
			throw new SocketException(SocketError.AddressFamilyNotSupported);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00043D58 File Offset: 0x00041F58
		internal IPEndPoint GetIPEndPoint()
		{
			IPAddress ipaddress = this.GetIPAddress();
			int num = (((int)this.m_Buffer[2] << 8) & 65280) | (int)this.m_Buffer[3];
			return new IPEndPoint(ipaddress, num);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00043D90 File Offset: 0x00041F90
		internal void CopyAddressSizeIntoBuffer()
		{
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size] = (byte)this.m_Size;
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 1] = (byte)(this.m_Size >> 8);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 2] = (byte)(this.m_Size >> 16);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 3] = (byte)(this.m_Size >> 24);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00043E1B File Offset: 0x0004201B
		internal int GetAddressSizeOffset()
		{
			return this.m_Buffer.Length - IntPtr.Size;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00043E2B File Offset: 0x0004202B
		internal unsafe void SetSize(IntPtr ptr)
		{
			this.m_Size = *(int*)(void*)ptr;
		}

		/// <summary>Determines whether the specified <see langword="Object" /> is equal to the current <see langword="Object" />.</summary>
		/// <param name="comparand">The <see cref="T:System.Object" /> to compare with the current <see langword="Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see langword="Object" /> is equal to the current <see langword="Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000CBD RID: 3261 RVA: 0x00043E3C File Offset: 0x0004203C
		[global::__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			SocketAddress socketAddress = comparand as SocketAddress;
			if (socketAddress == null || this.Size != socketAddress.Size)
			{
				return false;
			}
			for (int i = 0; i < this.Size; i++)
			{
				if (this[i] != socketAddress[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000CBE RID: 3262 RVA: 0x00043E88 File Offset: 0x00042088
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_changed)
			{
				this.m_changed = false;
				this.m_hash = 0;
				int num = this.Size & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.m_hash ^= (int)this.m_Buffer[i] | ((int)this.m_Buffer[i + 1] << 8) | ((int)this.m_Buffer[i + 2] << 16) | ((int)this.m_Buffer[i + 3] << 24);
				}
				if ((this.Size & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.Size)
					{
						num2 |= (int)this.m_Buffer[i] << num3;
						num3 += 8;
						i++;
					}
					this.m_hash ^= num2;
				}
			}
			return this.m_hash;
		}

		/// <summary>Returns information about the socket address.</summary>
		/// <returns>A string that contains information about the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x06000CBF RID: 3263 RVA: 0x00043F48 File Offset: 0x00042148
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 2; i < this.Size; i++)
			{
				if (i > 2)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this[i].ToString(NumberFormatInfo.InvariantInfo));
			}
			return string.Concat(new string[]
			{
				this.Family.ToString(),
				":",
				this.Size.ToString(NumberFormatInfo.InvariantInfo),
				":{",
				stringBuilder.ToString(),
				"}"
			});
		}

		// Token: 0x040011A4 RID: 4516
		internal const int IPv6AddressSize = 28;

		// Token: 0x040011A5 RID: 4517
		internal const int IPv4AddressSize = 16;

		// Token: 0x040011A6 RID: 4518
		internal int m_Size;

		// Token: 0x040011A7 RID: 4519
		internal byte[] m_Buffer;

		// Token: 0x040011A8 RID: 4520
		private const int WriteableOffset = 2;

		// Token: 0x040011A9 RID: 4521
		private const int MaxSize = 32;

		// Token: 0x040011AA RID: 4522
		private bool m_changed = true;

		// Token: 0x040011AB RID: 4523
		private int m_hash;
	}
}

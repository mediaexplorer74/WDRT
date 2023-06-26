using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x020002EC RID: 748
	public class PingOptions
	{
		// Token: 0x06001A44 RID: 6724 RVA: 0x0007F992 File Offset: 0x0007DB92
		internal PingOptions(IPOptions options)
		{
			this.ttl = (int)options.ttl;
			this.dontFragment = (options.flags & 2) > 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">
		///   <see langword="true" /> to prevent data sent to the remote host from being fragmented; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl" /> is less than or equal to zero.</exception>
		// Token: 0x06001A45 RID: 6725 RVA: 0x0007F9C6 File Offset: 0x0007DBC6
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x06001A46 RID: 6726 RVA: 0x0007F9F6 File Offset: 0x0007DBF6
		public PingOptions()
		{
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0007FA09 File Offset: 0x0007DC09
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x0007FA11 File Offset: 0x0007DC11
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>
		///   <see langword="true" /> if the data cannot be sent in multiple packets; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0007FA29 File Offset: 0x0007DC29
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0007FA31 File Offset: 0x0007DC31
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x04001A71 RID: 6769
		private const int DontFragmentFlag = 2;

		// Token: 0x04001A72 RID: 6770
		private int ttl = 128;

		// Token: 0x04001A73 RID: 6771
		private bool dontFragment;
	}
}

using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Transmission Control Protocol (TCP) statistical data.</summary>
	// Token: 0x02000307 RID: 775
	[global::__DynamicallyInvokable]
	public abstract class TcpStatistics
	{
		/// <summary>Gets the number of accepted Transmission Control Protocol (TCP) connection requests.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connection requests accepted.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001B5C RID: 7004
		[global::__DynamicallyInvokable]
		public abstract long ConnectionsAccepted
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) connection requests made by clients.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections initiated by clients.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001B5D RID: 7005
		[global::__DynamicallyInvokable]
		public abstract long ConnectionsInitiated
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Specifies the total number of Transmission Control Protocol (TCP) connections established.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of connections established.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001B5E RID: 7006
		[global::__DynamicallyInvokable]
		public abstract long CumulativeConnections
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of current Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of current TCP connections.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001B5F RID: 7007
		[global::__DynamicallyInvokable]
		public abstract long CurrentConnections
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) errors received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP errors received.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001B60 RID: 7008
		[global::__DynamicallyInvokable]
		public abstract long ErrorsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of failed Transmission Control Protocol (TCP) connection attempts.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of failed TCP connection attempts.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001B61 RID: 7009
		[global::__DynamicallyInvokable]
		public abstract long FailedConnectionAttempts
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the maximum number of supported Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections that can be supported.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001B62 RID: 7010
		[global::__DynamicallyInvokable]
		public abstract long MaximumConnections
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the maximum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001B63 RID: 7011
		[global::__DynamicallyInvokable]
		public abstract long MaximumTransmissionTimeout
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the minimum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the minimum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001B64 RID: 7012
		[global::__DynamicallyInvokable]
		public abstract long MinimumTransmissionTimeout
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of RST packets received by Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of reset TCP connections.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001B65 RID: 7013
		[global::__DynamicallyInvokable]
		public abstract long ResetConnections
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments received.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001B66 RID: 7014
		[global::__DynamicallyInvokable]
		public abstract long SegmentsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments re-sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments retransmitted.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001B67 RID: 7015
		[global::__DynamicallyInvokable]
		public abstract long SegmentsResent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001B68 RID: 7016
		[global::__DynamicallyInvokable]
		public abstract long SegmentsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent with the reset flag set.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent with the reset flag set.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001B69 RID: 7017
		[global::__DynamicallyInvokable]
		public abstract long ResetsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> class.</summary>
		// Token: 0x06001B6A RID: 7018 RVA: 0x00081E14 File Offset: 0x00080014
		[global::__DynamicallyInvokable]
		protected TcpStatistics()
		{
		}
	}
}

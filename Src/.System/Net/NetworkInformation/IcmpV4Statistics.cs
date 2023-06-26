using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for IPv4 (ICMPv4) statistical data for the local computer.</summary>
	// Token: 0x0200029C RID: 668
	[global::__DynamicallyInvokable]
	public abstract class IcmpV4Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were received.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060018B9 RID: 6329
		[global::__DynamicallyInvokable]
		public abstract long AddressMaskRepliesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were sent.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060018BA RID: 6330
		[global::__DynamicallyInvokable]
		public abstract long AddressMaskRepliesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were received.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060018BB RID: 6331
		[global::__DynamicallyInvokable]
		public abstract long AddressMaskRequestsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were sent.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060018BC RID: 6332
		[global::__DynamicallyInvokable]
		public abstract long AddressMaskRequestsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages that were received.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060018BD RID: 6333
		[global::__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060018BE RID: 6334
		[global::__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were received.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060018BF RID: 6335
		[global::__DynamicallyInvokable]
		public abstract long EchoRepliesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were sent.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060018C0 RID: 6336
		[global::__DynamicallyInvokable]
		public abstract long EchoRepliesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were received.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060018C1 RID: 6337
		[global::__DynamicallyInvokable]
		public abstract long EchoRequestsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were sent.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060018C2 RID: 6338
		[global::__DynamicallyInvokable]
		public abstract long EchoRequestsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages that were received.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060018C3 RID: 6339
		[global::__DynamicallyInvokable]
		public abstract long ErrorsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP error messages that were sent.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060018C4 RID: 6340
		[global::__DynamicallyInvokable]
		public abstract long ErrorsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were received.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060018C5 RID: 6341
		[global::__DynamicallyInvokable]
		public abstract long MessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were sent.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060018C6 RID: 6342
		[global::__DynamicallyInvokable]
		public abstract long MessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were received.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060018C7 RID: 6343
		[global::__DynamicallyInvokable]
		public abstract long ParameterProblemsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were sent.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060018C8 RID: 6344
		[global::__DynamicallyInvokable]
		public abstract long ParameterProblemsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were received.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060018C9 RID: 6345
		[global::__DynamicallyInvokable]
		public abstract long RedirectsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were sent.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060018CA RID: 6346
		[global::__DynamicallyInvokable]
		public abstract long RedirectsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were received.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060018CB RID: 6347
		[global::__DynamicallyInvokable]
		public abstract long SourceQuenchesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were sent.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060018CC RID: 6348
		[global::__DynamicallyInvokable]
		public abstract long SourceQuenchesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were received.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060018CD RID: 6349
		[global::__DynamicallyInvokable]
		public abstract long TimeExceededMessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were sent.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060018CE RID: 6350
		[global::__DynamicallyInvokable]
		public abstract long TimeExceededMessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were received.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060018CF RID: 6351
		[global::__DynamicallyInvokable]
		public abstract long TimestampRepliesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were sent.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060018D0 RID: 6352
		[global::__DynamicallyInvokable]
		public abstract long TimestampRepliesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were received.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060018D1 RID: 6353
		[global::__DynamicallyInvokable]
		public abstract long TimestampRequestsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were sent.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060018D2 RID: 6354
		[global::__DynamicallyInvokable]
		public abstract long TimestampRequestsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> class.</summary>
		// Token: 0x060018D3 RID: 6355 RVA: 0x0007DA61 File Offset: 0x0007BC61
		[global::__DynamicallyInvokable]
		protected IcmpV4Statistics()
		{
		}
	}
}

using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for Internet Protocol version 6 (ICMPv6) statistical data for the local computer.</summary>
	// Token: 0x0200029D RID: 669
	[global::__DynamicallyInvokable]
	public abstract class IcmpV6Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages received.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060018D4 RID: 6356
		[global::__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060018D5 RID: 6357
		[global::__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages received.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060018D6 RID: 6358
		[global::__DynamicallyInvokable]
		public abstract long EchoRepliesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages sent.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060018D7 RID: 6359
		[global::__DynamicallyInvokable]
		public abstract long EchoRepliesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages received.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060018D8 RID: 6360
		[global::__DynamicallyInvokable]
		public abstract long EchoRequestsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages sent.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060018D9 RID: 6361
		[global::__DynamicallyInvokable]
		public abstract long EchoRequestsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages received.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060018DA RID: 6362
		[global::__DynamicallyInvokable]
		public abstract long ErrorsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages sent.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060018DB RID: 6363
		[global::__DynamicallyInvokable]
		public abstract long ErrorsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages received.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060018DC RID: 6364
		[global::__DynamicallyInvokable]
		public abstract long MembershipQueriesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages sent.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060018DD RID: 6365
		[global::__DynamicallyInvokable]
		public abstract long MembershipQueriesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages received.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060018DE RID: 6366
		[global::__DynamicallyInvokable]
		public abstract long MembershipReductionsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages sent.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060018DF RID: 6367
		[global::__DynamicallyInvokable]
		public abstract long MembershipReductionsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages received.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060018E0 RID: 6368
		[global::__DynamicallyInvokable]
		public abstract long MembershipReportsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages sent.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060018E1 RID: 6369
		[global::__DynamicallyInvokable]
		public abstract long MembershipReportsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages received.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060018E2 RID: 6370
		[global::__DynamicallyInvokable]
		public abstract long MessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages sent.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060018E3 RID: 6371
		[global::__DynamicallyInvokable]
		public abstract long MessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Neighbor Advertisement messages received.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060018E4 RID: 6372
		[global::__DynamicallyInvokable]
		public abstract long NeighborAdvertisementsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Advertisement messages sent.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060018E5 RID: 6373
		[global::__DynamicallyInvokable]
		public abstract long NeighborAdvertisementsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages received.</returns>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060018E6 RID: 6374
		[global::__DynamicallyInvokable]
		public abstract long NeighborSolicitsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages sent.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060018E7 RID: 6375
		[global::__DynamicallyInvokable]
		public abstract long NeighborSolicitsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages received.</returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060018E8 RID: 6376
		[global::__DynamicallyInvokable]
		public abstract long PacketTooBigMessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages sent.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060018E9 RID: 6377
		[global::__DynamicallyInvokable]
		public abstract long PacketTooBigMessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages received.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060018EA RID: 6378
		[global::__DynamicallyInvokable]
		public abstract long ParameterProblemsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages sent.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060018EB RID: 6379
		[global::__DynamicallyInvokable]
		public abstract long ParameterProblemsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages received.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060018EC RID: 6380
		[global::__DynamicallyInvokable]
		public abstract long RedirectsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages sent.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060018ED RID: 6381
		[global::__DynamicallyInvokable]
		public abstract long RedirectsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages received.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060018EE RID: 6382
		[global::__DynamicallyInvokable]
		public abstract long RouterAdvertisementsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages sent.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060018EF RID: 6383
		[global::__DynamicallyInvokable]
		public abstract long RouterAdvertisementsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages received.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060018F0 RID: 6384
		[global::__DynamicallyInvokable]
		public abstract long RouterSolicitsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages sent.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060018F1 RID: 6385
		[global::__DynamicallyInvokable]
		public abstract long RouterSolicitsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages received.</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060018F2 RID: 6386
		[global::__DynamicallyInvokable]
		public abstract long TimeExceededMessagesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages sent.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060018F3 RID: 6387
		[global::__DynamicallyInvokable]
		public abstract long TimeExceededMessagesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> class.</summary>
		// Token: 0x060018F4 RID: 6388 RVA: 0x0007DA69 File Offset: 0x0007BC69
		[global::__DynamicallyInvokable]
		protected IcmpV6Statistics()
		{
		}
	}
}

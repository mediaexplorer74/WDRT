using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies types of network interfaces.</summary>
	// Token: 0x0200029E RID: 670
	[global::__DynamicallyInvokable]
	public enum NetworkInterfaceType
	{
		/// <summary>The interface type is not known.</summary>
		// Token: 0x0400189A RID: 6298
		[global::__DynamicallyInvokable]
		Unknown = 1,
		/// <summary>The network interface uses an Ethernet connection. Ethernet is defined in IEEE standard 802.3.</summary>
		// Token: 0x0400189B RID: 6299
		[global::__DynamicallyInvokable]
		Ethernet = 6,
		/// <summary>The network interface uses a Token-Ring connection. Token-Ring is defined in IEEE standard 802.5.</summary>
		// Token: 0x0400189C RID: 6300
		[global::__DynamicallyInvokable]
		TokenRing = 9,
		/// <summary>The network interface uses a Fiber Distributed Data Interface (FDDI) connection. FDDI is a set of standards for data transmission on fiber optic lines in a local area network.</summary>
		// Token: 0x0400189D RID: 6301
		[global::__DynamicallyInvokable]
		Fddi = 15,
		/// <summary>The network interface uses a basic rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x0400189E RID: 6302
		[global::__DynamicallyInvokable]
		BasicIsdn = 20,
		/// <summary>The network interface uses a primary rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x0400189F RID: 6303
		[global::__DynamicallyInvokable]
		PrimaryIsdn,
		/// <summary>The network interface uses a Point-To-Point protocol (PPP) connection. PPP is a protocol for data transmission using a serial device.</summary>
		// Token: 0x040018A0 RID: 6304
		[global::__DynamicallyInvokable]
		Ppp = 23,
		/// <summary>The network interface is a loopback adapter. Such interfaces are often used for testing; no traffic is sent over the wire.</summary>
		// Token: 0x040018A1 RID: 6305
		[global::__DynamicallyInvokable]
		Loopback,
		/// <summary>The network interface uses an Ethernet 3 megabit/second connection. This version of Ethernet is defined in IETF RFC 895.</summary>
		// Token: 0x040018A2 RID: 6306
		[global::__DynamicallyInvokable]
		Ethernet3Megabit = 26,
		/// <summary>The network interface uses a Serial Line Internet Protocol (SLIP) connection. SLIP is defined in IETF RFC 1055.</summary>
		// Token: 0x040018A3 RID: 6307
		[global::__DynamicallyInvokable]
		Slip = 28,
		/// <summary>The network interface uses asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040018A4 RID: 6308
		[global::__DynamicallyInvokable]
		Atm = 37,
		/// <summary>The network interface uses a modem.</summary>
		// Token: 0x040018A5 RID: 6309
		[global::__DynamicallyInvokable]
		GenericModem = 48,
		/// <summary>The network interface uses a Fast Ethernet connection over twisted pair and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-T.</summary>
		// Token: 0x040018A6 RID: 6310
		[global::__DynamicallyInvokable]
		FastEthernetT = 62,
		/// <summary>The network interface uses a connection configured for ISDN and the X.25 protocol. X.25 allows computers on public networks to communicate using an intermediary computer.</summary>
		// Token: 0x040018A7 RID: 6311
		[global::__DynamicallyInvokable]
		Isdn,
		/// <summary>The network interface uses a Fast Ethernet connection over optical fiber and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-FX.</summary>
		// Token: 0x040018A8 RID: 6312
		[global::__DynamicallyInvokable]
		FastEthernetFx = 69,
		/// <summary>The network interface uses a wireless LAN connection (IEEE 802.11 standard).</summary>
		// Token: 0x040018A9 RID: 6313
		[global::__DynamicallyInvokable]
		Wireless80211 = 71,
		/// <summary>The network interface uses an Asymmetric Digital Subscriber Line (ADSL).</summary>
		// Token: 0x040018AA RID: 6314
		[global::__DynamicallyInvokable]
		AsymmetricDsl = 94,
		/// <summary>The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).</summary>
		// Token: 0x040018AB RID: 6315
		[global::__DynamicallyInvokable]
		RateAdaptDsl,
		/// <summary>The network interface uses a Symmetric Digital Subscriber Line (SDSL).</summary>
		// Token: 0x040018AC RID: 6316
		[global::__DynamicallyInvokable]
		SymmetricDsl,
		/// <summary>The network interface uses a Very High Data Rate Digital Subscriber Line (VDSL).</summary>
		// Token: 0x040018AD RID: 6317
		[global::__DynamicallyInvokable]
		VeryHighSpeedDsl,
		/// <summary>The network interface uses the Internet Protocol (IP) in combination with asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040018AE RID: 6318
		[global::__DynamicallyInvokable]
		IPOverAtm = 114,
		/// <summary>The network interface uses a gigabit Ethernet connection and provides a data rate of 1,000 megabits per second (1 gigabit per second).</summary>
		// Token: 0x040018AF RID: 6319
		[global::__DynamicallyInvokable]
		GigabitEthernet = 117,
		/// <summary>The network interface uses a tunnel connection.</summary>
		// Token: 0x040018B0 RID: 6320
		[global::__DynamicallyInvokable]
		Tunnel = 131,
		/// <summary>The network interface uses a Multirate Digital Subscriber Line.</summary>
		// Token: 0x040018B1 RID: 6321
		[global::__DynamicallyInvokable]
		MultiRateSymmetricDsl = 143,
		/// <summary>The network interface uses a High Performance Serial Bus.</summary>
		// Token: 0x040018B2 RID: 6322
		[global::__DynamicallyInvokable]
		HighPerformanceSerialBus,
		/// <summary>The network interface uses a mobile broadband interface for WiMax devices.</summary>
		// Token: 0x040018B3 RID: 6323
		[global::__DynamicallyInvokable]
		Wman = 237,
		/// <summary>The network interface uses a mobile broadband interface for GSM-based devices.</summary>
		// Token: 0x040018B4 RID: 6324
		[global::__DynamicallyInvokable]
		Wwanpp = 243,
		/// <summary>The network interface uses a mobile broadband interface for CDMA-based devices.</summary>
		// Token: 0x040018B5 RID: 6325
		[global::__DynamicallyInvokable]
		Wwanpp2
	}
}

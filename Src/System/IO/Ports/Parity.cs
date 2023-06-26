using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200040A RID: 1034
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x040020E2 RID: 8418
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x040020E3 RID: 8419
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x040020E4 RID: 8420
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x040020E5 RID: 8421
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x040020E6 RID: 8422
		Space
	}
}

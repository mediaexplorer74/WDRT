using System;
using System.Text;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides the Media Access Control (MAC) address for a network interface (adapter).</summary>
	// Token: 0x020002E7 RID: 743
	[global::__DynamicallyInvokable]
	public class PhysicalAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> class.</summary>
		/// <param name="address">A <see cref="T:System.Byte" /> array containing the address.</param>
		// Token: 0x06001A05 RID: 6661 RVA: 0x0007E5E5 File Offset: 0x0007C7E5
		[global::__DynamicallyInvokable]
		public PhysicalAddress(byte[] address)
		{
			this.address = address;
		}

		/// <summary>Returns the hash value of a physical address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06001A06 RID: 6662 RVA: 0x0007E5FC File Offset: 0x0007C7FC
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.changed)
			{
				this.changed = false;
				this.hash = 0;
				int num = this.address.Length & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.hash ^= (int)this.address[i] | ((int)this.address[i + 1] << 8) | ((int)this.address[i + 2] << 16) | ((int)this.address[i + 3] << 24);
				}
				if ((this.address.Length & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.address.Length)
					{
						num2 |= (int)this.address[i] << num3;
						num3 += 8;
						i++;
					}
					this.hash ^= num2;
				}
			}
			return this.hash;
		}

		/// <summary>Compares two <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instances.</summary>
		/// <param name="comparand">The <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if this instance and the specified instance contain the same address; otherwise <see langword="false" />.</returns>
		// Token: 0x06001A07 RID: 6663 RVA: 0x0007E6C4 File Offset: 0x0007C8C4
		[global::__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			PhysicalAddress physicalAddress = comparand as PhysicalAddress;
			if (physicalAddress == null)
			{
				return false;
			}
			if (this.address.Length != physicalAddress.address.Length)
			{
				return false;
			}
			for (int i = 0; i < physicalAddress.address.Length; i++)
			{
				if (this.address[i] != physicalAddress.address[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.String" /> representation of the address of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the address contained in this instance.</returns>
		// Token: 0x06001A08 RID: 6664 RVA: 0x0007E71C File Offset: 0x0007C91C
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.address)
			{
				int num = (b >> 4) & 15;
				for (int j = 0; j < 2; j++)
				{
					if (num < 10)
					{
						stringBuilder.Append((char)(num + 48));
					}
					else
					{
						stringBuilder.Append((char)(num + 55));
					}
					num = (int)(b & 15);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns the address of the current instance.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the address.</returns>
		// Token: 0x06001A09 RID: 6665 RVA: 0x0007E790 File Offset: 0x0007C990
		[global::__DynamicallyInvokable]
		public byte[] GetAddressBytes()
		{
			byte[] array = new byte[this.address.Length];
			Buffer.BlockCopy(this.address, 0, array, 0, this.address.Length);
			return array;
		}

		/// <summary>Parses the specified <see cref="T:System.String" /> and stores its contents as the address bytes of the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> returned by this method.</summary>
		/// <param name="address">A <see cref="T:System.String" /> containing the address that will be used to initialize the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance returned by this method.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance with the specified address.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> contains an illegal hardware address or contains a string in the incorrect format.</exception>
		// Token: 0x06001A0A RID: 6666 RVA: 0x0007E7C4 File Offset: 0x0007C9C4
		[global::__DynamicallyInvokable]
		public static PhysicalAddress Parse(string address)
		{
			int num = 0;
			bool flag = false;
			if (address == null)
			{
				return PhysicalAddress.None;
			}
			byte[] array;
			if (address.IndexOf('-') >= 0)
			{
				flag = true;
				array = new byte[(address.Length + 1) / 3];
			}
			else
			{
				if (address.Length % 2 > 0)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				array = new byte[address.Length / 2];
			}
			int num2 = 0;
			int i = 0;
			while (i < address.Length)
			{
				int num3 = (int)address[i];
				if (num3 >= 48 && num3 <= 57)
				{
					num3 -= 48;
					goto IL_C3;
				}
				if (num3 >= 65 && num3 <= 70)
				{
					num3 -= 55;
					goto IL_C3;
				}
				if (num3 != 45)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				if (num != 2)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				num = 0;
				IL_100:
				i++;
				continue;
				IL_C3:
				if (flag && num >= 2)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				if (num % 2 == 0)
				{
					array[num2] = (byte)(num3 << 4);
				}
				else
				{
					byte[] array2 = array;
					int num4 = num2++;
					array2[num4] |= (byte)num3;
				}
				num++;
				goto IL_100;
			}
			if (num < 2)
			{
				throw new FormatException(SR.GetString("net_bad_mac_address"));
			}
			return new PhysicalAddress(array);
		}

		// Token: 0x04001A54 RID: 6740
		private byte[] address;

		// Token: 0x04001A55 RID: 6741
		private bool changed = true;

		// Token: 0x04001A56 RID: 6742
		private int hash;

		/// <summary>Returns a new <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance with a zero length address. This field is read-only.</summary>
		// Token: 0x04001A57 RID: 6743
		[global::__DynamicallyInvokable]
		public static readonly PhysicalAddress None = new PhysicalAddress(new byte[0]);
	}
}

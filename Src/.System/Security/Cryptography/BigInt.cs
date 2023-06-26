using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x02000450 RID: 1104
	internal sealed class BigInt
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x000BAB4B File Offset: 0x000B8D4B
		internal BigInt()
		{
			this.m_elements = new byte[128];
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000BAB63 File Offset: 0x000B8D63
		internal BigInt(byte b)
		{
			this.m_elements = new byte[128];
			this.SetDigit(0, b);
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x000BAB83 File Offset: 0x000B8D83
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x000BAB8B File Offset: 0x000B8D8B
		internal int Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				if (value > 128)
				{
					this.m_size = 128;
				}
				if (value < 0)
				{
					this.m_size = 0;
				}
				this.m_size = value;
			}
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000BABB2 File Offset: 0x000B8DB2
		internal byte GetDigit(int index)
		{
			if (index < 0 || index >= this.m_size)
			{
				return 0;
			}
			return this.m_elements[index];
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000BABCC File Offset: 0x000B8DCC
		internal void SetDigit(int index, byte digit)
		{
			if (index >= 0 && index < 128)
			{
				this.m_elements[index] = digit;
				if (index >= this.m_size && digit != 0)
				{
					this.m_size = index + 1;
				}
				if (index == this.m_size - 1 && digit == 0)
				{
					this.m_size--;
				}
			}
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000BAC1F File Offset: 0x000B8E1F
		internal void SetDigit(int index, byte digit, ref int size)
		{
			if (index >= 0 && index < 128)
			{
				this.m_elements[index] = digit;
				if (index >= size && digit != 0)
				{
					size = index + 1;
				}
				if (index == size - 1 && digit == 0)
				{
					size--;
				}
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000BAC54 File Offset: 0x000B8E54
		public static bool operator <(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return true;
			}
			if (value2 == null)
			{
				return false;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return size < size2;
			}
			while (size-- > 0)
			{
				if (value1.m_elements[size] != value2.m_elements[size])
				{
					return value1.m_elements[size] < value2.m_elements[size];
				}
			}
			return false;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000BACBC File Offset: 0x000B8EBC
		public static bool operator >(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return false;
			}
			if (value2 == null)
			{
				return true;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return size > size2;
			}
			while (size-- > 0)
			{
				if (value1.m_elements[size] != value2.m_elements[size])
				{
					return value1.m_elements[size] > value2.m_elements[size];
				}
			}
			return false;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000BAD24 File Offset: 0x000B8F24
		public static bool operator ==(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return value2 == null;
			}
			if (value2 == null)
			{
				return value1 == null;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return false;
			}
			for (int i = 0; i < size; i++)
			{
				if (value1.m_elements[i] != value2.m_elements[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000BAD76 File Offset: 0x000B8F76
		public static bool operator !=(BigInt value1, BigInt value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000BAD82 File Offset: 0x000B8F82
		public override bool Equals(object obj)
		{
			return obj is BigInt && this == (BigInt)obj;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000BAD9C File Offset: 0x000B8F9C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.m_size; i++)
			{
				num += (int)this.GetDigit(i);
			}
			return num;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000BADC8 File Offset: 0x000B8FC8
		internal static void Add(BigInt a, byte b, ref BigInt c)
		{
			byte b2 = b;
			int size = a.Size;
			int num = 0;
			for (int i = 0; i < size; i++)
			{
				int num2 = (int)(a.GetDigit(i) + b2);
				c.SetDigit(i, (byte)(num2 & 255), ref num);
				b2 = (byte)((num2 >> 8) & 255);
			}
			if (b2 != 0)
			{
				c.SetDigit(a.Size, b2, ref num);
			}
			c.Size = num;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000BAE38 File Offset: 0x000B9038
		internal static void Negate(ref BigInt a)
		{
			int num = 0;
			for (int i = 0; i < 128; i++)
			{
				a.SetDigit(i, ~a.GetDigit(i) & byte.MaxValue, ref num);
			}
			for (int j = 0; j < 128; j++)
			{
				a.SetDigit(j, a.GetDigit(j) + 1, ref num);
				if ((a.GetDigit(j) & 255) != 0)
				{
					break;
				}
				a.SetDigit(j, a.GetDigit(j) & byte.MaxValue, ref num);
			}
			a.Size = num;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000BAEC8 File Offset: 0x000B90C8
		internal static void Subtract(BigInt a, BigInt b, ref BigInt c)
		{
			byte b2 = 0;
			if (a < b)
			{
				BigInt.Subtract(b, a, ref c);
				BigInt.Negate(ref c);
				return;
			}
			int size = a.Size;
			int num = 0;
			for (int i = 0; i < size; i++)
			{
				int num2 = (int)(a.GetDigit(i) - b.GetDigit(i) - b2);
				b2 = 0;
				if (num2 < 0)
				{
					num2 += 256;
					b2 = 1;
				}
				c.SetDigit(i, (byte)(num2 & 255), ref num);
			}
			c.Size = num;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000BAF48 File Offset: 0x000B9148
		private void Multiply(int b)
		{
			if (b == 0)
			{
				this.Clear();
				return;
			}
			int num = 0;
			int size = this.Size;
			int num2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num3 = b * (int)this.GetDigit(i) + num;
				num = num3 / 256;
				this.SetDigit(i, (byte)(num3 % 256), ref num2);
			}
			if (num != 0)
			{
				byte[] bytes = BitConverter.GetBytes(num);
				for (int j = 0; j < bytes.Length; j++)
				{
					this.SetDigit(size + j, bytes[j], ref num2);
				}
			}
			this.Size = num2;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000BAFDC File Offset: 0x000B91DC
		private static void Multiply(BigInt a, int b, ref BigInt c)
		{
			if (b == 0)
			{
				c.Clear();
				return;
			}
			int num = 0;
			int size = a.Size;
			int num2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num3 = b * (int)a.GetDigit(i) + num;
				num = num3 / 256;
				c.SetDigit(i, (byte)(num3 % 256), ref num2);
			}
			if (num != 0)
			{
				byte[] bytes = BitConverter.GetBytes(num);
				for (int j = 0; j < bytes.Length; j++)
				{
					c.SetDigit(size + j, bytes[j], ref num2);
				}
			}
			c.Size = num2;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000BB074 File Offset: 0x000B9274
		private void Divide(int b)
		{
			int num = 0;
			int size = this.Size;
			int num2 = 0;
			while (size-- > 0)
			{
				int num3 = 256 * num + (int)this.GetDigit(size);
				num = num3 % b;
				this.SetDigit(size, (byte)(num3 / b), ref num2);
			}
			this.Size = num2;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000BB0C0 File Offset: 0x000B92C0
		internal static void Divide(BigInt numerator, BigInt denominator, ref BigInt quotient, ref BigInt remainder)
		{
			if (numerator < denominator)
			{
				quotient.Clear();
				remainder.CopyFrom(numerator);
				return;
			}
			if (numerator == denominator)
			{
				quotient.Clear();
				quotient.SetDigit(0, 1);
				remainder.Clear();
				return;
			}
			BigInt bigInt = new BigInt();
			bigInt.CopyFrom(numerator);
			BigInt bigInt2 = new BigInt();
			bigInt2.CopyFrom(denominator);
			uint num = 0U;
			while (bigInt2.Size < bigInt.Size)
			{
				bigInt2.Multiply(256);
				num += 1U;
			}
			if (bigInt2 > bigInt)
			{
				bigInt2.Divide(256);
				num -= 1U;
			}
			BigInt bigInt3 = new BigInt();
			quotient.Clear();
			int num2 = 0;
			while ((long)num2 <= (long)((ulong)num))
			{
				int num3 = ((bigInt.Size == bigInt2.Size) ? ((int)bigInt.GetDigit(bigInt.Size - 1)) : (256 * (int)bigInt.GetDigit(bigInt.Size - 1) + (int)bigInt.GetDigit(bigInt.Size - 2)));
				int digit = (int)bigInt2.GetDigit(bigInt2.Size - 1);
				int num4 = num3 / digit;
				if (num4 >= 256)
				{
					num4 = 255;
				}
				BigInt.Multiply(bigInt2, num4, ref bigInt3);
				while (bigInt3 > bigInt)
				{
					num4--;
					BigInt.Multiply(bigInt2, num4, ref bigInt3);
				}
				quotient.Multiply(256);
				BigInt.Add(quotient, (byte)num4, ref quotient);
				BigInt.Subtract(bigInt, bigInt3, ref bigInt);
				bigInt2.Divide(256);
				num2++;
			}
			remainder.CopyFrom(bigInt);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000BB24A File Offset: 0x000B944A
		internal void CopyFrom(BigInt a)
		{
			Array.Copy(a.m_elements, this.m_elements, 128);
			this.m_size = a.m_size;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000BB270 File Offset: 0x000B9470
		internal bool IsZero()
		{
			for (int i = 0; i < this.m_size; i++)
			{
				if (this.m_elements[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000BB29C File Offset: 0x000B949C
		internal byte[] ToByteArray()
		{
			byte[] array = new byte[this.Size];
			Array.Copy(this.m_elements, array, this.Size);
			return array;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000BB2C8 File Offset: 0x000B94C8
		internal void Clear()
		{
			this.m_size = 0;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000BB2D4 File Offset: 0x000B94D4
		internal void FromHexadecimal(string hexNum)
		{
			byte[] array = System.Security.Cryptography.X509Certificates.X509Utils.DecodeHexString(hexNum);
			Array.Reverse(array);
			int hexArraySize = System.Security.Cryptography.X509Certificates.X509Utils.GetHexArraySize(array);
			Array.Copy(array, this.m_elements, hexArraySize);
			this.Size = hexArraySize;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000BB30C File Offset: 0x000B950C
		internal void FromDecimal(string decNum)
		{
			BigInt bigInt = new BigInt();
			BigInt bigInt2 = new BigInt();
			int length = decNum.Length;
			for (int i = 0; i < length; i++)
			{
				if (decNum[i] <= '9' && decNum[i] >= '0')
				{
					BigInt.Multiply(bigInt, 10, ref bigInt2);
					BigInt.Add(bigInt2, (byte)(decNum[i] - '0'), ref bigInt);
				}
			}
			this.CopyFrom(bigInt);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000BB374 File Offset: 0x000B9574
		internal string ToDecimal()
		{
			if (this.IsZero())
			{
				return "0";
			}
			BigInt bigInt = new BigInt(10);
			BigInt bigInt2 = new BigInt();
			BigInt bigInt3 = new BigInt();
			BigInt bigInt4 = new BigInt();
			bigInt2.CopyFrom(this);
			char[] array = new char[(int)Math.Ceiling((double)(this.m_size * 2) * 1.21)];
			int num = 0;
			do
			{
				BigInt.Divide(bigInt2, bigInt, ref bigInt3, ref bigInt4);
				array[num++] = BigInt.decValues[(int)(bigInt4.IsZero() ? 0 : bigInt4.m_elements[0])];
				bigInt2.CopyFrom(bigInt3);
			}
			while (!bigInt3.IsZero());
			Array.Reverse(array, 0, num);
			return new string(array, 0, num);
		}

		// Token: 0x0400226D RID: 8813
		private byte[] m_elements;

		// Token: 0x0400226E RID: 8814
		private const int m_maxbytes = 128;

		// Token: 0x0400226F RID: 8815
		private const int m_base = 256;

		// Token: 0x04002270 RID: 8816
		private int m_size;

		// Token: 0x04002271 RID: 8817
		private static readonly char[] decValues = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	}
}

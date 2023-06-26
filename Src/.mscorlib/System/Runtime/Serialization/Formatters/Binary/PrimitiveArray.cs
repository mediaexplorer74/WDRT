using System;
using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079B RID: 1947
	internal sealed class PrimitiveArray
	{
		// Token: 0x06005477 RID: 21623 RVA: 0x0012AC8C File Offset: 0x00128E8C
		internal PrimitiveArray(InternalPrimitiveTypeE code, Array array)
		{
			this.Init(code, array);
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0012AC9C File Offset: 0x00128E9C
		internal void Init(InternalPrimitiveTypeE code, Array array)
		{
			this.code = code;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA = (bool[])array;
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				this.charA = (char[])array;
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA = (double[])array;
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A = (short[])array;
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A = (int[])array;
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A = (long[])array;
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA = (sbyte[])array;
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA = (float[])array;
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A = (ushort[])array;
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A = (uint[])array;
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A = (ulong[])array;
				break;
			default:
				return;
			}
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0012AD88 File Offset: 0x00128F88
		internal void SetValue(string value, int index)
		{
			switch (this.code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA[index] = bool.Parse(value);
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				if (value[0] == '_' && value.Equals("_0x00_"))
				{
					this.charA[index] = '\0';
					return;
				}
				this.charA[index] = char.Parse(value);
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA[index] = double.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A[index] = short.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A[index] = int.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A[index] = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA[index] = sbyte.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA[index] = float.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A[index] = ushort.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A[index] = uint.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A[index] = ulong.Parse(value, CultureInfo.InvariantCulture);
				break;
			default:
				return;
			}
		}

		// Token: 0x04002679 RID: 9849
		private InternalPrimitiveTypeE code;

		// Token: 0x0400267A RID: 9850
		private bool[] booleanA;

		// Token: 0x0400267B RID: 9851
		private char[] charA;

		// Token: 0x0400267C RID: 9852
		private double[] doubleA;

		// Token: 0x0400267D RID: 9853
		private short[] int16A;

		// Token: 0x0400267E RID: 9854
		private int[] int32A;

		// Token: 0x0400267F RID: 9855
		private long[] int64A;

		// Token: 0x04002680 RID: 9856
		private sbyte[] sbyteA;

		// Token: 0x04002681 RID: 9857
		private float[] singleA;

		// Token: 0x04002682 RID: 9858
		private ushort[] uint16A;

		// Token: 0x04002683 RID: 9859
		private uint[] uint32A;

		// Token: 0x04002684 RID: 9860
		private ulong[] uint64A;
	}
}

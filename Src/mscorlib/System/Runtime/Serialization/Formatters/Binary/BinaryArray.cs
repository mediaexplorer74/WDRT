using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078C RID: 1932
	internal sealed class BinaryArray : IStreamable
	{
		// Token: 0x06005426 RID: 21542 RVA: 0x0012998A File Offset: 0x00127B8A
		internal BinaryArray()
		{
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x00129992 File Offset: 0x00127B92
		internal BinaryArray(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x001299A4 File Offset: 0x00127BA4
		internal void Set(int objectId, int rank, int[] lengthA, int[] lowerBoundA, BinaryTypeEnum binaryTypeEnum, object typeInformation, BinaryArrayTypeEnum binaryArrayTypeEnum, int assemId)
		{
			this.objectId = objectId;
			this.binaryArrayTypeEnum = binaryArrayTypeEnum;
			this.rank = rank;
			this.lengthA = lengthA;
			this.lowerBoundA = lowerBoundA;
			this.binaryTypeEnum = binaryTypeEnum;
			this.typeInformation = typeInformation;
			this.assemId = assemId;
			this.binaryHeaderEnum = BinaryHeaderEnum.Array;
			if (binaryArrayTypeEnum == BinaryArrayTypeEnum.Single)
			{
				if (binaryTypeEnum == BinaryTypeEnum.Primitive)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySinglePrimitive;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.String)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleString;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.Object)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleObject;
				}
			}
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00129A24 File Offset: 0x00127C24
		public void Write(__BinaryWriter sout)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				sout.WriteByte((byte)((InternalPrimitiveTypeE)this.typeInformation));
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			case BinaryHeaderEnum.ArraySingleString:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			default:
			{
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteByte((byte)this.binaryArrayTypeEnum);
				sout.WriteInt32(this.rank);
				for (int i = 0; i < this.rank; i++)
				{
					sout.WriteInt32(this.lengthA[i]);
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						sout.WriteInt32(this.lowerBoundA[j]);
					}
				}
				sout.WriteByte((byte)this.binaryTypeEnum);
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnum, this.typeInformation, this.assemId, sout);
				return;
			}
			}
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x00129B8C File Offset: 0x00127D8C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Primitive;
				this.typeInformation = (InternalPrimitiveTypeE)input.ReadByte();
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Object;
				this.typeInformation = null;
				return;
			case BinaryHeaderEnum.ArraySingleString:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.String;
				this.typeInformation = null;
				return;
			default:
			{
				this.objectId = input.ReadInt32();
				this.binaryArrayTypeEnum = (BinaryArrayTypeEnum)input.ReadByte();
				this.rank = input.ReadInt32();
				this.lengthA = new int[this.rank];
				this.lowerBoundA = new int[this.rank];
				for (int i = 0; i < this.rank; i++)
				{
					this.lengthA[i] = input.ReadInt32();
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						this.lowerBoundA[j] = input.ReadInt32();
					}
				}
				this.binaryTypeEnum = (BinaryTypeEnum)input.ReadByte();
				this.typeInformation = BinaryConverter.ReadTypeInfo(this.binaryTypeEnum, input, out this.assemId);
				return;
			}
			}
		}

		// Token: 0x040025FE RID: 9726
		internal int objectId;

		// Token: 0x040025FF RID: 9727
		internal int rank;

		// Token: 0x04002600 RID: 9728
		internal int[] lengthA;

		// Token: 0x04002601 RID: 9729
		internal int[] lowerBoundA;

		// Token: 0x04002602 RID: 9730
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002603 RID: 9731
		internal object typeInformation;

		// Token: 0x04002604 RID: 9732
		internal int assemId;

		// Token: 0x04002605 RID: 9733
		private BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002606 RID: 9734
		internal BinaryArrayTypeEnum binaryArrayTypeEnum;
	}
}

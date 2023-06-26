using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078B RID: 1931
	internal sealed class BinaryObjectWithMapTyped : IStreamable
	{
		// Token: 0x06005421 RID: 21537 RVA: 0x00129717 File Offset: 0x00127917
		internal BinaryObjectWithMapTyped()
		{
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0012971F File Offset: 0x0012791F
		internal BinaryObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x00129730 File Offset: 0x00127930
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, int assemId)
		{
			this.objectId = objectId;
			this.assemId = assemId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.memberAssemIds = memberAssemIds;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTypedAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTyped;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x00129798 File Offset: 0x00127998
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				sout.WriteByte((byte)this.binaryTypeEnumA[j]);
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnumA[k], this.typeInformationA[k], this.memberAssemIds[k], sout);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0012985C File Offset: 0x00127A5C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			this.binaryTypeEnumA = new BinaryTypeEnum[this.numMembers];
			this.typeInformationA = new object[this.numMembers];
			this.memberAssemIds = new int[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				this.binaryTypeEnumA[j] = (BinaryTypeEnum)input.ReadByte();
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				if (this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUrt && this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUser)
				{
					this.typeInformationA[k] = BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
				else
				{
					BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x040025F5 RID: 9717
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040025F6 RID: 9718
		internal int objectId;

		// Token: 0x040025F7 RID: 9719
		internal string name;

		// Token: 0x040025F8 RID: 9720
		internal int numMembers;

		// Token: 0x040025F9 RID: 9721
		internal string[] memberNames;

		// Token: 0x040025FA RID: 9722
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x040025FB RID: 9723
		internal object[] typeInformationA;

		// Token: 0x040025FC RID: 9724
		internal int[] memberAssemIds;

		// Token: 0x040025FD RID: 9725
		internal int assemId;
	}
}

using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078A RID: 1930
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x0600541A RID: 21530 RVA: 0x001295A5 File Offset: 0x001277A5
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x001295AD File Offset: 0x001277AD
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x001295BC File Offset: 0x001277BC
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x001295F8 File Offset: 0x001277F8
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
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0012966C File Offset: 0x0012786C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x001296E2 File Offset: 0x001278E2
		public void Dump()
		{
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x001296E4 File Offset: 0x001278E4
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x040025EF RID: 9711
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040025F0 RID: 9712
		internal int objectId;

		// Token: 0x040025F1 RID: 9713
		internal string name;

		// Token: 0x040025F2 RID: 9714
		internal int numMembers;

		// Token: 0x040025F3 RID: 9715
		internal string[] memberNames;

		// Token: 0x040025F4 RID: 9716
		internal int assemId;
	}
}

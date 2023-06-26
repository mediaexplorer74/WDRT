using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078F RID: 1935
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x06005438 RID: 21560 RVA: 0x00129E39 File Offset: 0x00128039
		internal ObjectNull()
		{
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00129E41 File Offset: 0x00128041
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00129E4C File Offset: 0x0012804C
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00129EA2 File Offset: 0x001280A2
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00129EB0 File Offset: 0x001280B0
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00129EFC File Offset: 0x001280FC
		public void Dump()
		{
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00129EFE File Offset: 0x001280FE
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY") && this.nullCount != 1)
			{
				int num = this.nullCount;
			}
		}

		// Token: 0x0400260A RID: 9738
		internal int nullCount;
	}
}

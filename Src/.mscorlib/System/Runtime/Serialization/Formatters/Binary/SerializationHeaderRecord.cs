using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000780 RID: 1920
	internal sealed class SerializationHeaderRecord : IStreamable
	{
		// Token: 0x060053DD RID: 21469 RVA: 0x00128623 File Offset: 0x00126823
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00128632 File Offset: 0x00126832
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00128668 File Offset: 0x00126868
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x001286CA File Offset: 0x001268CA
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | ((int)buffer[index + 1] << 8) | ((int)buffer[index + 2] << 16) | ((int)buffer[index + 3] << 24);
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x001286EC File Offset: 0x001268EC
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFormat", new object[] { BitConverter.ToString(array) }));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0012877A File Offset: 0x0012697A
		public void Dump()
		{
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x0012877C File Offset: 0x0012697C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025C2 RID: 9666
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x040025C3 RID: 9667
		internal int binaryFormatterMinorVersion;

		// Token: 0x040025C4 RID: 9668
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040025C5 RID: 9669
		internal int topId;

		// Token: 0x040025C6 RID: 9670
		internal int headerId;

		// Token: 0x040025C7 RID: 9671
		internal int majorVersion;

		// Token: 0x040025C8 RID: 9672
		internal int minorVersion;
	}
}

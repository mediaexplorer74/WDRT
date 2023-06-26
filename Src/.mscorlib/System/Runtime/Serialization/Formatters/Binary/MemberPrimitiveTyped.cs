using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000789 RID: 1929
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06005414 RID: 21524 RVA: 0x00129536 File Offset: 0x00127736
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0012953E File Offset: 0x0012773E
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0012954E File Offset: 0x0012774E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x00129576 File Offset: 0x00127776
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00129596 File Offset: 0x00127796
		public void Dump()
		{
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00129598 File Offset: 0x00127798
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025ED RID: 9709
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x040025EE RID: 9710
		internal object value;
	}
}

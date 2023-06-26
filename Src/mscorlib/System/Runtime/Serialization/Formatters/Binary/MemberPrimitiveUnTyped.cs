using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078D RID: 1933
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x0600542B RID: 21547 RVA: 0x00129D83 File Offset: 0x00127F83
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00129D8B File Offset: 0x00127F8B
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00129D9B File Offset: 0x00127F9B
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x00129DA4 File Offset: 0x00127FA4
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x00129DB8 File Offset: 0x00127FB8
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00129DCC File Offset: 0x00127FCC
		public void Dump()
		{
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00129DD0 File Offset: 0x00127FD0
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				string text = Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x04002607 RID: 9735
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002608 RID: 9736
		internal object value;
	}
}

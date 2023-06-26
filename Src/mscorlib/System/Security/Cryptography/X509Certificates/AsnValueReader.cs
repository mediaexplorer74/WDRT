using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B5 RID: 693
	internal struct AsnValueReader
	{
		// Token: 0x060024EC RID: 9452 RVA: 0x00086CDB File Offset: 0x00084EDB
		internal AsnValueReader(ReadOnlySpan<byte> span, AsnEncodingRules ruleSet)
		{
			this._span = span;
			this._ruleSet = ruleSet;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00086CEB File Offset: 0x00084EEB
		internal bool HasData
		{
			get
			{
				return !this._span.IsEmpty;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00086CFB File Offset: 0x00084EFB
		internal void ThrowIfNotEmpty()
		{
			if (!this._span.IsEmpty)
			{
				new AsnReader(AsnValueReader.s_singleByte, this._ruleSet).ThrowIfNotEmpty();
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00086D24 File Offset: 0x00084F24
		internal Asn1Tag PeekTag()
		{
			int num;
			return Asn1Tag.Decode(this._span, out num);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00086D40 File Offset: 0x00084F40
		internal ReadOnlySpan<byte> PeekContentBytes()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out num, out num2, out num3);
			return this._span.Slice(num, num2);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00086D74 File Offset: 0x00084F74
		internal ReadOnlySpan<byte> PeekEncodedValue()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out num, out num2, out num3);
			return this._span.Slice(0, num3);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00086DA8 File Offset: 0x00084FA8
		internal ReadOnlySpan<byte> ReadEncodedValue()
		{
			ReadOnlySpan<byte> readOnlySpan = this.PeekEncodedValue();
			this._span = this._span.Slice(readOnlySpan.Length);
			return readOnlySpan;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00086DD8 File Offset: 0x00084FD8
		internal bool TryReadInt32(out int value)
		{
			return this.TryReadInt32(out value, null);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00086DF8 File Offset: 0x00084FF8
		internal bool TryReadInt32(out int value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadInt32(this._span, this._ruleSet, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00086E30 File Offset: 0x00085030
		internal ReadOnlySpan<byte> ReadIntegerBytes()
		{
			return this.ReadIntegerBytes(null);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00086E4C File Offset: 0x0008504C
		internal ReadOnlySpan<byte> ReadIntegerBytes(Asn1Tag? expectedTag)
		{
			int num;
			ReadOnlySpan<byte> readOnlySpan = AsnDecoder.ReadIntegerBytes(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return readOnlySpan;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00086E84 File Offset: 0x00085084
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveBitString(out unusedBitCount, out value, null);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00086EA4 File Offset: 0x000850A4
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveBitString(this._span, this._ruleSet, out unusedBitCount, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00086EDC File Offset: 0x000850DC
		internal byte[] ReadBitString(out int unusedBitCount)
		{
			return this.ReadBitString(out unusedBitCount, null);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00086EFC File Offset: 0x000850FC
		internal byte[] ReadBitString(out int unusedBitCount, Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadBitString(this._span, this._ruleSet, out unusedBitCount, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00086F34 File Offset: 0x00085134
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveOctetString(out value, null);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00086F54 File Offset: 0x00085154
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveOctetString(this._span, this._ruleSet, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00086F8C File Offset: 0x0008518C
		internal byte[] ReadOctetString()
		{
			return this.ReadOctetString(null);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00086FA8 File Offset: 0x000851A8
		internal byte[] ReadOctetString(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadOctetString(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00086FE0 File Offset: 0x000851E0
		internal byte[] ReadObjectIdentifier()
		{
			return this.ReadObjectIdentifier(null);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00086FFC File Offset: 0x000851FC
		internal byte[] ReadObjectIdentifier(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadObjectIdentifier(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00087034 File Offset: 0x00085234
		internal AsnValueReader ReadSequence()
		{
			return this.ReadSequence(null);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00087050 File Offset: 0x00085250
		internal AsnValueReader ReadSequence(Asn1Tag? expectedTag)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSequence(this._span, this._ruleSet, out num, out num2, out num3, expectedTag);
			ReadOnlySpan<byte> readOnlySpan = this._span.Slice(num, num2);
			this._span = this._span.Slice(num3);
			return new AsnValueReader(readOnlySpan, this._ruleSet);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000870A4 File Offset: 0x000852A4
		internal AsnValueReader ReadSetOf()
		{
			return this.ReadSetOf(null, false);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000870C1 File Offset: 0x000852C1
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag)
		{
			return this.ReadSetOf(expectedTag, false);
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000870CC File Offset: 0x000852CC
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag, bool skipSortOrderValidation)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSetOf(this._span, this._ruleSet, out num, out num2, out num3, skipSortOrderValidation, expectedTag);
			ReadOnlySpan<byte> readOnlySpan = this._span.Slice(num, num2);
			this._span = this._span.Slice(num3);
			return new AsnValueReader(readOnlySpan, this._ruleSet);
		}

		// Token: 0x04000DD0 RID: 3536
		private static readonly byte[] s_singleByte = new byte[1];

		// Token: 0x04000DD1 RID: 3537
		private ReadOnlySpan<byte> _span;

		// Token: 0x04000DD2 RID: 3538
		private readonly AsnEncodingRules _ruleSet;
	}
}

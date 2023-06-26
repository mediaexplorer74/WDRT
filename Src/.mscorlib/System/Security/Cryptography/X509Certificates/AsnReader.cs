using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B2 RID: 690
	internal class AsnReader
	{
		// Token: 0x060024CE RID: 9422 RVA: 0x00086730 File Offset: 0x00084930
		public bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlyMemory<byte> value, Asn1Tag? expectedTag)
		{
			ReadOnlySpan<byte> readOnlySpan;
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveBitString(this._data.Span, this.RuleSet, out unusedBitCount, out readOnlySpan, out num, expectedTag);
			if (flag)
			{
				value = AsnDecoder.Slice(this._data, readOnlySpan);
				this._data = this._data.Slice(num);
			}
			else
			{
				value = default(ReadOnlyMemory<byte>);
			}
			return flag;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0008678C File Offset: 0x0008498C
		public bool TryReadBitString(Span<byte> destination, out int unusedBitCount, out int bytesWritten, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadBitString(this._data.Span, destination, this.RuleSet, out unusedBitCount, out num, out bytesWritten, expectedTag);
			if (flag)
			{
				this._data = this._data.Slice(num);
			}
			return flag;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000867D0 File Offset: 0x000849D0
		public byte[] ReadBitString(out int unusedBitCount, Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadBitString(this._data.Span, this.RuleSet, out unusedBitCount, out num, expectedTag);
			this._data = this._data.Slice(num);
			return array;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x0008680B File Offset: 0x00084A0B
		public AsnEncodingRules RuleSet
		{
			get
			{
				return this._ruleSet;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00086813 File Offset: 0x00084A13
		public bool HasData
		{
			get
			{
				return !this._data.IsEmpty;
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00086823 File Offset: 0x00084A23
		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet, AsnReaderOptions options)
		{
			AsnDecoder.CheckEncodingRules(ruleSet);
			this._data = data;
			this._ruleSet = ruleSet;
			this._options = options;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00086848 File Offset: 0x00084A48
		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet)
			: this(data, ruleSet, default(AsnReaderOptions))
		{
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00086866 File Offset: 0x00084A66
		public void ThrowIfNotEmpty()
		{
			if (this.HasData)
			{
				throw new InvalidOperationException("The last expected value has been read, but the reader still has pending data. This value may be from a newer schema, or is corrupt.");
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0008687C File Offset: 0x00084A7C
		public Asn1Tag PeekTag()
		{
			int num;
			return Asn1Tag.Decode(this._data.Span, out num);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0008689C File Offset: 0x00084A9C
		public ReadOnlyMemory<byte> PeekEncodedValue()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._data.Span, this.RuleSet, out num, out num2, out num3);
			return this._data.Slice(0, num3);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000868D4 File Offset: 0x00084AD4
		public ReadOnlyMemory<byte> PeekContentBytes()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._data.Span, this.RuleSet, out num, out num2, out num3);
			return this._data.Slice(num, num2);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0008690C File Offset: 0x00084B0C
		public ReadOnlyMemory<byte> ReadEncodedValue()
		{
			ReadOnlyMemory<byte> readOnlyMemory = this.PeekEncodedValue();
			this._data = this._data.Slice(readOnlyMemory.Length);
			return readOnlyMemory;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00086939 File Offset: 0x00084B39
		private AsnReader CloneAtSlice(int start, int length)
		{
			return new AsnReader(this._data.Slice(start, length), this.RuleSet, this._options);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0008695C File Offset: 0x00084B5C
		public ReadOnlyMemory<byte> ReadIntegerBytes(Asn1Tag? expectedTag)
		{
			int num;
			ReadOnlySpan<byte> readOnlySpan = AsnDecoder.ReadIntegerBytes(this._data.Span, this.RuleSet, out num, expectedTag);
			ReadOnlyMemory<byte> readOnlyMemory = AsnDecoder.Slice(this._data, readOnlySpan);
			this._data = this._data.Slice(num);
			return readOnlyMemory;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000869A4 File Offset: 0x00084BA4
		public bool TryReadInt32(out int value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadInt32(this._data.Span, this.RuleSet, out value, out num, expectedTag);
			this._data = this._data.Slice(num);
			return flag;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000869E0 File Offset: 0x00084BE0
		public bool TryReadUInt32(out uint value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadUInt32(this._data.Span, this.RuleSet, out value, out num, expectedTag);
			this._data = this._data.Slice(num);
			return flag;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00086A1C File Offset: 0x00084C1C
		public bool TryReadInt64(out long value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadInt64(this._data.Span, this.RuleSet, out value, out num, expectedTag);
			this._data = this._data.Slice(num);
			return flag;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00086A58 File Offset: 0x00084C58
		public bool TryReadUInt64(out ulong value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadUInt64(this._data.Span, this.RuleSet, out value, out num, expectedTag);
			this._data = this._data.Slice(num);
			return flag;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00086A94 File Offset: 0x00084C94
		public void ReadNull(Asn1Tag? expectedTag)
		{
			int num;
			AsnDecoder.ReadNull(this._data.Span, this.RuleSet, out num, expectedTag);
			this._data = this._data.Slice(num);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00086ACC File Offset: 0x00084CCC
		public bool TryReadOctetString(Span<byte> destination, out int bytesWritten, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadOctetString(this._data.Span, destination, this.RuleSet, out num, out bytesWritten, expectedTag);
			if (flag)
			{
				this._data = this._data.Slice(num);
			}
			return flag;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00086B0C File Offset: 0x00084D0C
		public byte[] ReadOctetString(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadOctetString(this._data.Span, this.RuleSet, out num, expectedTag);
			this._data = this._data.Slice(num);
			return array;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00086B48 File Offset: 0x00084D48
		public bool TryReadPrimitiveOctetString(out ReadOnlyMemory<byte> contents, Asn1Tag? expectedTag)
		{
			ReadOnlySpan<byte> readOnlySpan;
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveOctetString(this._data.Span, this.RuleSet, out readOnlySpan, out num, expectedTag);
			if (flag)
			{
				contents = AsnDecoder.Slice(this._data, readOnlySpan);
				this._data = this._data.Slice(num);
			}
			else
			{
				contents = default(ReadOnlyMemory<byte>);
			}
			return flag;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00086BA4 File Offset: 0x00084DA4
		public byte[] ReadObjectIdentifier(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadObjectIdentifier(this._data.Span, this.RuleSet, out num, expectedTag);
			this._data = this._data.Slice(num);
			return array;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00086BE0 File Offset: 0x00084DE0
		public AsnReader ReadSequence(Asn1Tag? expectedTag)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSequence(this._data.Span, this.RuleSet, out num, out num2, out num3, expectedTag);
			AsnReader asnReader = this.CloneAtSlice(num, num2);
			this._data = this._data.Slice(num3);
			return asnReader;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00086C28 File Offset: 0x00084E28
		public AsnReader ReadSetOf(Asn1Tag? expectedTag)
		{
			return this.ReadSetOf(this._options.SkipSetSortOrderVerification, expectedTag);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00086C4C File Offset: 0x00084E4C
		public AsnReader ReadSetOf(bool skipSortOrderValidation, Asn1Tag? expectedTag)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSetOf(this._data.Span, this.RuleSet, out num, out num2, out num3, skipSortOrderValidation, expectedTag);
			AsnReader asnReader = this.CloneAtSlice(num, num2);
			this._data = this._data.Slice(num3);
			return asnReader;
		}

		// Token: 0x04000DC5 RID: 3525
		internal const int MaxCERSegmentSize = 1000;

		// Token: 0x04000DC6 RID: 3526
		private ReadOnlyMemory<byte> _data;

		// Token: 0x04000DC7 RID: 3527
		private readonly AsnReaderOptions _options;

		// Token: 0x04000DC8 RID: 3528
		private AsnEncodingRules _ruleSet;
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B0 RID: 688
	internal struct Asn1Tag : IEquatable<Asn1Tag>
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x00084C12 File Offset: 0x00082E12
		public TagClass TagClass
		{
			get
			{
				return (TagClass)(this._controlFlags & 192);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00084C20 File Offset: 0x00082E20
		public bool IsConstructed
		{
			get
			{
				return (this._controlFlags & 32) > 0;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00084C2E File Offset: 0x00082E2E
		public int TagValue
		{
			get
			{
				return this._tagValue;
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00084C36 File Offset: 0x00082E36
		private Asn1Tag(byte controlFlags, int tagValue)
		{
			this._controlFlags = controlFlags & 224;
			this._tagValue = tagValue;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00084C4D File Offset: 0x00082E4D
		public Asn1Tag(UniversalTagNumber universalTagNumber, bool isConstructed)
		{
			this = new Asn1Tag(isConstructed ? 32 : 0, (int)universalTagNumber);
			if (universalTagNumber < UniversalTagNumber.EndOfContents || universalTagNumber > UniversalTagNumber.RelativeObjectIdentifierIRI || universalTagNumber == (UniversalTagNumber)15)
			{
				throw new ArgumentOutOfRangeException("universalTagNumber");
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00084C78 File Offset: 0x00082E78
		public Asn1Tag(TagClass tagClass, int tagValue, bool isConstructed)
		{
			this = new Asn1Tag((byte)tagClass | (isConstructed ? 32 : 0), tagValue);
			if (tagClass <= TagClass.Application)
			{
				if (tagClass == TagClass.Universal || tagClass == TagClass.Application)
				{
					goto IL_3D;
				}
			}
			else if (tagClass == TagClass.ContextSpecific || tagClass == TagClass.Private)
			{
				goto IL_3D;
			}
			throw new ArgumentOutOfRangeException("tagClass");
			IL_3D:
			if (tagValue < 0)
			{
				throw new ArgumentOutOfRangeException("tagValue");
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00084CD1 File Offset: 0x00082ED1
		public Asn1Tag(TagClass tagClass, int tagValue)
		{
			this = new Asn1Tag(tagClass, tagValue, false);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00084CDC File Offset: 0x00082EDC
		public Asn1Tag AsConstructed()
		{
			return new Asn1Tag(this._controlFlags | 32, this.TagValue);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00084CF4 File Offset: 0x00082EF4
		public static bool TryDecode(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesConsumed)
		{
			tag = default(Asn1Tag);
			bytesConsumed = 0;
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = source[bytesConsumed];
			bytesConsumed++;
			uint num = (uint)(b & 31);
			if (num == 31U)
			{
				num = 0U;
				while (source.Length > bytesConsumed)
				{
					byte b2 = source[bytesConsumed];
					byte b3 = b2 & 127;
					bytesConsumed++;
					if (num >= 33554432U)
					{
						bytesConsumed = 0;
						return false;
					}
					num <<= 7;
					num |= (uint)b3;
					if (num == 0U)
					{
						bytesConsumed = 0;
						return false;
					}
					if ((b2 & 128) != 128)
					{
						if (num <= 30U)
						{
							bytesConsumed = 0;
							return false;
						}
						if (num > 2147483647U)
						{
							bytesConsumed = 0;
							return false;
						}
						goto IL_99;
					}
				}
				bytesConsumed = 0;
				return false;
			}
			IL_99:
			tag = new Asn1Tag(b, (int)num);
			return true;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00084DA8 File Offset: 0x00082FA8
		public static Asn1Tag Decode(ReadOnlySpan<byte> source, out int bytesConsumed)
		{
			Asn1Tag asn1Tag;
			if (Asn1Tag.TryDecode(source, out asn1Tag, out bytesConsumed))
			{
				return asn1Tag;
			}
			throw new InvalidOperationException("The provided data does not represent a valid tag.");
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00084DCC File Offset: 0x00082FCC
		public bool Equals(Asn1Tag other)
		{
			return this._controlFlags == other._controlFlags && this.TagValue == other.TagValue;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00084DED File Offset: 0x00082FED
		public override bool Equals(object obj)
		{
			return obj is Asn1Tag && this.Equals((Asn1Tag)obj);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00084E05 File Offset: 0x00083005
		public override int GetHashCode()
		{
			return ((int)this._controlFlags << 24) ^ this.TagValue;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00084E17 File Offset: 0x00083017
		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00084E21 File Offset: 0x00083021
		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00084E2E File Offset: 0x0008302E
		public bool HasSameClassAndValue(Asn1Tag other)
		{
			return this.TagValue == other.TagValue && this.TagClass == other.TagClass;
		}

		// Token: 0x04000DB3 RID: 3507
		internal static readonly Asn1Tag EndOfContents = new Asn1Tag(0, 0);

		// Token: 0x04000DB4 RID: 3508
		public static readonly Asn1Tag Integer = new Asn1Tag(0, 2);

		// Token: 0x04000DB5 RID: 3509
		public static readonly Asn1Tag PrimitiveBitString = new Asn1Tag(0, 3);

		// Token: 0x04000DB6 RID: 3510
		public static readonly Asn1Tag ConstructedBitString = new Asn1Tag(32, 3);

		// Token: 0x04000DB7 RID: 3511
		public static readonly Asn1Tag PrimitiveOctetString = new Asn1Tag(0, 4);

		// Token: 0x04000DB8 RID: 3512
		public static readonly Asn1Tag ConstructedOctetString = new Asn1Tag(32, 4);

		// Token: 0x04000DB9 RID: 3513
		public static readonly Asn1Tag Null = new Asn1Tag(0, 5);

		// Token: 0x04000DBA RID: 3514
		public static readonly Asn1Tag ObjectIdentifier = new Asn1Tag(0, 6);

		// Token: 0x04000DBB RID: 3515
		public static readonly Asn1Tag Sequence = new Asn1Tag(32, 16);

		// Token: 0x04000DBC RID: 3516
		public static readonly Asn1Tag SetOf = new Asn1Tag(32, 17);

		// Token: 0x04000DBD RID: 3517
		private const byte ClassMask = 192;

		// Token: 0x04000DBE RID: 3518
		private const byte ConstructedMask = 32;

		// Token: 0x04000DBF RID: 3519
		private const byte ControlMask = 224;

		// Token: 0x04000DC0 RID: 3520
		private const byte TagNumberMask = 31;

		// Token: 0x04000DC1 RID: 3521
		private readonly byte _controlFlags;

		// Token: 0x04000DC2 RID: 3522
		private int _tagValue;
	}
}

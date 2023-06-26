using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005FA RID: 1530
	[Serializable]
	internal struct MetadataToken
	{
		// Token: 0x06004694 RID: 18068 RVA: 0x0010415E File Offset: 0x0010235E
		public static implicit operator int(MetadataToken token)
		{
			return token.Value;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00104166 File Offset: 0x00102366
		public static implicit operator MetadataToken(int token)
		{
			return new MetadataToken(token);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00104170 File Offset: 0x00102370
		public static bool IsTokenOfType(int token, params MetadataTokenType[] types)
		{
			for (int i = 0; i < types.Length; i++)
			{
				if ((MetadataTokenType)((long)token & (long)((ulong)(-16777216))) == types[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x0010419D File Offset: 0x0010239D
		public static bool IsNullToken(int token)
		{
			return (token & 16777215) == 0;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x001041A9 File Offset: 0x001023A9
		public MetadataToken(int token)
		{
			this.Value = token;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x001041B2 File Offset: 0x001023B2
		public bool IsGlobalTypeDefToken
		{
			get
			{
				return this.Value == 33554433;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x001041C1 File Offset: 0x001023C1
		public MetadataTokenType TokenType
		{
			get
			{
				return (MetadataTokenType)((long)this.Value & (long)((ulong)(-16777216)));
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x001041D2 File Offset: 0x001023D2
		public bool IsTypeRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeRef;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x001041E1 File Offset: 0x001023E1
		public bool IsTypeDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeDef;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x001041F0 File Offset: 0x001023F0
		public bool IsFieldDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.FieldDef;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x001041FF File Offset: 0x001023FF
		public bool IsMethodDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodDef;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x0010420E File Offset: 0x0010240E
		public bool IsMemberRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MemberRef;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x0010421D File Offset: 0x0010241D
		public bool IsEvent
		{
			get
			{
				return this.TokenType == MetadataTokenType.Event;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x0010422C File Offset: 0x0010242C
		public bool IsProperty
		{
			get
			{
				return this.TokenType == MetadataTokenType.Property;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x0010423B File Offset: 0x0010243B
		public bool IsParamDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.ParamDef;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x0010424A File Offset: 0x0010244A
		public bool IsTypeSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeSpec;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00104259 File Offset: 0x00102459
		public bool IsMethodSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodSpec;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x00104268 File Offset: 0x00102468
		public bool IsString
		{
			get
			{
				return this.TokenType == MetadataTokenType.String;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00104277 File Offset: 0x00102477
		public bool IsSignature
		{
			get
			{
				return this.TokenType == MetadataTokenType.Signature;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060046A7 RID: 18087 RVA: 0x00104286 File Offset: 0x00102486
		public bool IsModule
		{
			get
			{
				return this.TokenType == MetadataTokenType.Module;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x00104291 File Offset: 0x00102491
		public bool IsAssembly
		{
			get
			{
				return this.TokenType == MetadataTokenType.Assembly;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060046A9 RID: 18089 RVA: 0x001042A0 File Offset: 0x001024A0
		public bool IsGenericPar
		{
			get
			{
				return this.TokenType == MetadataTokenType.GenericPar;
			}
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001042AF File Offset: 0x001024AF
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x8}", this.Value);
		}

		// Token: 0x04001D57 RID: 7511
		public int Value;
	}
}

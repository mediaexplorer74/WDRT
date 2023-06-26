using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001AE RID: 430
	internal class CsdlSemanticsStringTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x0001A095 File Offset: 0x00018295
		public CsdlSemanticsStringTypeReference(CsdlSemanticsSchema schema, CsdlStringTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001A09F File Offset: 0x0001829F
		public bool? IsFixedLength
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsFixedLength;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001A0B1 File Offset: 0x000182B1
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001A0C3 File Offset: 0x000182C3
		public int? MaxLength
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).MaxLength;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001A0D5 File Offset: 0x000182D5
		public bool? IsUnicode
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnicode;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0001A0E7 File Offset: 0x000182E7
		public string Collation
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).Collation;
			}
		}
	}
}

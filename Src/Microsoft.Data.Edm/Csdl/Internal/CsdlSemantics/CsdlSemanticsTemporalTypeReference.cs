﻿using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001AF RID: 431
	internal class CsdlSemanticsTemporalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0001A0F9 File Offset: 0x000182F9
		public CsdlSemanticsTemporalTypeReference(CsdlSemanticsSchema schema, CsdlTemporalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001A103 File Offset: 0x00018303
		public int? Precision
		{
			get
			{
				return ((CsdlTemporalTypeReference)this.Reference).Precision;
			}
		}
	}
}

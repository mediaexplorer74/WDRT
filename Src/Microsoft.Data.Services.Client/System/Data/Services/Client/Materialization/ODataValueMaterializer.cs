using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000073 RID: 115
	internal sealed class ODataValueMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0001055F File Offset: 0x0000E75F
		public ODataValueMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001056C File Offset: 0x0000E76C
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00010574 File Offset: 0x0000E774
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			object obj = this.messageReader.ReadValue(expectedReaderType);
			this.currentValue = base.PrimitiveValueMaterializier.MaterializePrimitiveDataValue(base.ExpectedType, null, obj);
		}

		// Token: 0x040002B6 RID: 694
		private object currentValue;
	}
}

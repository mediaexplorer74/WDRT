using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000072 RID: 114
	internal sealed class ODataPropertyMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00010434 File Offset: 0x0000E634
		public ODataPropertyMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00010441 File Offset: 0x0000E641
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001044C File Offset: 0x0000E64C
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			ODataProperty odataProperty = this.messageReader.ReadProperty(expectedReaderType);
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			object value = odataProperty.Value;
			if (expectedClientType.IsCollection())
			{
				Type type2 = type;
				Type type3 = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
				object obj;
				if (type3 != null)
				{
					type2 = type3.GetGenericArguments()[0];
					obj = base.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(odataProperty, type);
				}
				else
				{
					type3 = typeof(ICollection<>).MakeGenericType(new Type[] { type2 });
					obj = base.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(odataProperty, type3);
				}
				base.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(odataProperty, obj, type2, ClientTypeUtil.GetAddToCollectionDelegate(type3));
				this.currentValue = obj;
				return;
			}
			if (expectedClientType.IsComplex())
			{
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				base.ComplexValueMaterializationPolicy.MaterializeComplexTypeProperty(type, odataComplexValue);
				this.currentValue = odataComplexValue.GetMaterializedValue();
				return;
			}
			this.currentValue = base.PrimitivePropertyConverter.ConvertPrimitiveValue(odataProperty.Value, base.ExpectedType);
		}

		// Token: 0x040002B5 RID: 693
		private object currentValue;
	}
}

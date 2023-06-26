using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace System.Data.Services.Client
{
	// Token: 0x02000032 RID: 50
	internal sealed class ClientEdmStructuredValue : IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00008D5C File Offset: 0x00006F5C
		public ClientEdmStructuredValue(object structuredValue, ClientEdmModel model, ClientTypeAnnotation clientTypeAnnotation)
		{
			if (clientTypeAnnotation.EdmType.TypeKind == EdmTypeKind.Complex)
			{
				this.Type = new EdmComplexTypeReference((IEdmComplexType)clientTypeAnnotation.EdmType, true);
			}
			else
			{
				this.Type = new EdmEntityTypeReference((IEdmEntityType)clientTypeAnnotation.EdmType, true);
			}
			this.structuredValue = structuredValue;
			this.typeAnnotation = clientTypeAnnotation;
			this.model = model;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008DC2 File Offset: 0x00006FC2
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00008DCA File Offset: 0x00006FCA
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008DD3 File Offset: 0x00006FD3
		public EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008DD7 File Offset: 0x00006FD7
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				return this.typeAnnotation.Properties().Select(new Func<ClientPropertyAnnotation, IEdmPropertyValue>(this.BuildEdmPropertyValue));
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			ClientPropertyAnnotation property = this.typeAnnotation.GetProperty(propertyName, true);
			if (property == null)
			{
				return null;
			}
			return this.BuildEdmPropertyValue(property);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008E20 File Offset: 0x00007020
		private IEdmPropertyValue BuildEdmPropertyValue(ClientPropertyAnnotation propertyAnnotation)
		{
			object value = propertyAnnotation.GetValue(this.structuredValue);
			IEdmValue edmValue = this.ConvertToEdmValue(value, propertyAnnotation.EdmProperty.Type);
			return new EdmPropertyValue(propertyAnnotation.EdmProperty.Name, edmValue);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008E80 File Offset: 0x00007080
		private IEdmValue ConvertToEdmValue(object propertyValue, IEdmTypeReference edmPropertyType)
		{
			if (propertyValue == null)
			{
				return EdmNullExpression.Instance;
			}
			if (edmPropertyType.IsStructured())
			{
				return new ClientEdmStructuredValue(propertyValue, this.model, this.model.GetClientTypeAnnotation(edmPropertyType.Definition));
			}
			if (edmPropertyType.IsCollection())
			{
				IEdmCollectionTypeReference collectionType = edmPropertyType as IEdmCollectionTypeReference;
				IEnumerable<IEdmValue> enumerable = from object v in (IEnumerable)propertyValue
					select this.ConvertToEdmValue(v, collectionType.ElementType());
				return new ClientEdmCollectionValue(collectionType, enumerable);
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = edmPropertyType as IEdmPrimitiveTypeReference;
			return EdmValueUtils.ConvertPrimitiveValue(propertyValue, edmPrimitiveTypeReference).Value;
		}

		// Token: 0x040001F1 RID: 497
		private readonly object structuredValue;

		// Token: 0x040001F2 RID: 498
		private readonly ClientTypeAnnotation typeAnnotation;

		// Token: 0x040001F3 RID: 499
		private readonly ClientEdmModel model;
	}
}

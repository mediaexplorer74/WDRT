using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Values;

namespace System.Data.Services.Common
{
	// Token: 0x02000129 RID: 297
	public sealed class DataServiceUrlConventions
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x0002863F File Offset: 0x0002683F
		private DataServiceUrlConventions(UrlConvention urlConvention)
		{
			this.urlConvention = urlConvention;
			this.keySerializer = KeySerializer.Create(urlConvention);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002865A File Offset: 0x0002685A
		public static DataServiceUrlConventions Default
		{
			get
			{
				return DataServiceUrlConventions.DefaultInstance;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00028661 File Offset: 0x00026861
		public static DataServiceUrlConventions KeyAsSegment
		{
			get
			{
				return DataServiceUrlConventions.KeyAsSegmentInstance;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002869C File Offset: 0x0002689C
		internal void AppendKeyExpression(IEdmStructuredValue entity, StringBuilder builder)
		{
			IEdmEntityTypeReference edmEntityTypeReference = entity.Type as IEdmEntityTypeReference;
			if (edmEntityTypeReference == null || !edmEntityTypeReference.Key().Any<IEdmStructuralProperty>())
			{
				throw Error.Argument(Strings.Content_EntityWithoutKey, "entity");
			}
			this.AppendKeyExpression<IEdmStructuralProperty>(edmEntityTypeReference.Key().ToList<IEdmStructuralProperty>(), (IEdmStructuralProperty p) => p.Name, (IEdmStructuralProperty p) => DataServiceUrlConventions.GetPropertyValue(entity.FindPropertyValue(p.Name), entity.Type), builder);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028764 File Offset: 0x00026964
		internal void AppendKeyExpression<T>(ICollection<T> keyProperties, Func<T, string> getPropertyName, Func<T, object> getValueForProperty, StringBuilder builder)
		{
			Func<T, object> func = delegate(T p)
			{
				object obj = getValueForProperty(p);
				if (obj == null)
				{
					throw Error.InvalidOperation(Strings.Context_NullKeysAreNotSupported(getPropertyName(p)));
				}
				return obj;
			};
			this.keySerializer.AppendKeyExpression<T>(builder, keyProperties, getPropertyName, func);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000287A7 File Offset: 0x000269A7
		internal void AddRequiredHeaders(HeaderCollection requestHeaders)
		{
			this.urlConvention.AddRequiredHeaders(requestHeaders);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000287B8 File Offset: 0x000269B8
		private static object GetPropertyValue(IEdmPropertyValue property, IEdmTypeReference type)
		{
			IEdmValue value = property.Value;
			if (value.ValueKind == EdmValueKind.Null)
			{
				throw Error.InvalidOperation(Strings.Context_NullKeysAreNotSupported(property.Name));
			}
			IEdmPrimitiveValue edmPrimitiveValue = value as IEdmPrimitiveValue;
			if (edmPrimitiveValue == null)
			{
				throw Error.InvalidOperation(Strings.ClientType_KeysMustBeSimpleTypes(type.FullName()));
			}
			return edmPrimitiveValue.ToClrValue();
		}

		// Token: 0x040005AC RID: 1452
		private static readonly DataServiceUrlConventions DefaultInstance = new DataServiceUrlConventions(UrlConvention.CreateWithExplicitValue(false));

		// Token: 0x040005AD RID: 1453
		private static readonly DataServiceUrlConventions KeyAsSegmentInstance = new DataServiceUrlConventions(UrlConvention.CreateWithExplicitValue(true));

		// Token: 0x040005AE RID: 1454
		private readonly KeySerializer keySerializer;

		// Token: 0x040005AF RID: 1455
		private readonly UrlConvention urlConvention;
	}
}

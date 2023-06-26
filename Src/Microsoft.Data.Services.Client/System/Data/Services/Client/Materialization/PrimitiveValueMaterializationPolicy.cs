using System;
using System.Data.Services.Client.Metadata;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000049 RID: 73
	internal class PrimitiveValueMaterializationPolicy
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		internal PrimitiveValueMaterializationPolicy(IODataMaterializerContext context, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter)
		{
			this.context = context;
			this.lazyPrimitivePropertyConverter = lazyPrimitivePropertyConverter;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		private PrimitivePropertyConverter PrimitivePropertyConverter
		{
			get
			{
				return this.lazyPrimitivePropertyConverter.Value;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C814 File Offset: 0x0000AA14
		public object MaterializePrimitiveDataValue(Type collectionItemType, string wireTypeName, object item)
		{
			object obj = null;
			this.MaterializePrimitiveDataValue(collectionItemType, wireTypeName, item, () => "TODO: Is this reachable?", out obj);
			return obj;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C854 File Offset: 0x0000AA54
		public object MaterializePrimitiveDataValueCollectionElement(Type collectionItemType, string wireTypeName, object item)
		{
			object obj = null;
			this.MaterializePrimitiveDataValue(collectionItemType, wireTypeName, item, () => Strings.Collection_NullCollectionItemsNotSupported, out obj);
			return obj;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C890 File Offset: 0x0000AA90
		private bool MaterializePrimitiveDataValue(Type type, string wireTypeName, object value, Func<string> throwOnNullMessage, out object materializedValue)
		{
			Type type2 = Nullable.GetUnderlyingType(type) ?? type;
			PrimitiveType primitiveType;
			bool flag = PrimitiveType.TryGetPrimitiveType(type2, out primitiveType);
			if (!flag)
			{
				ClientTypeAnnotation clientTypeAnnotation = this.context.ResolveTypeForMaterialization(type, wireTypeName);
				flag = PrimitiveType.TryGetPrimitiveType(clientTypeAnnotation.ElementType, out primitiveType);
			}
			if (flag)
			{
				if (value == null)
				{
					if (!ClientTypeUtil.CanAssignNull(type))
					{
						throw new InvalidOperationException(throwOnNullMessage());
					}
					materializedValue = null;
				}
				else
				{
					materializedValue = this.PrimitivePropertyConverter.ConvertPrimitiveValue(value, type2);
				}
				return true;
			}
			materializedValue = null;
			return false;
		}

		// Token: 0x0400023B RID: 571
		private readonly IODataMaterializerContext context;

		// Token: 0x0400023C RID: 572
		private readonly SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter;
	}
}

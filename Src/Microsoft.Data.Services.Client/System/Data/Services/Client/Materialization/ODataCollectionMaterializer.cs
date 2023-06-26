using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200006B RID: 107
	internal sealed class ODataCollectionMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000FF10 File Offset: 0x0000E110
		public ODataCollectionMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000FF1D File Offset: 0x0000E11D
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000FF28 File Offset: 0x0000E128
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			if (!expectedClientType.IsCollection())
			{
				throw new DataServiceClientException(Strings.AtomMaterializer_TypeShouldBeCollectionError(expectedClientType.FullName()));
			}
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			WebUtil.IsCLRTypeCollection(type, base.MaterializerContext.Model);
			Type type2 = type;
			Type type3 = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
			if (type3 != null)
			{
				type2 = type3.GetGenericArguments()[0];
			}
			else
			{
				type3 = typeof(ICollection<>).MakeGenericType(new Type[] { type2 });
			}
			Type backingTypeForCollectionProperty = WebUtil.GetBackingTypeForCollectionProperty(type3, type2);
			object obj = base.CollectionValueMaterializationPolicy.CreateCollectionInstance((IEdmCollectionTypeReference)expectedClientType, backingTypeForCollectionProperty);
			ODataCollectionReader odataCollectionReader = this.messageReader.CreateODataCollectionReader();
			ODataCollectionMaterializer.NonEntityItemsEnumerable nonEntityItemsEnumerable = new ODataCollectionMaterializer.NonEntityItemsEnumerable(odataCollectionReader);
			base.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(nonEntityItemsEnumerable, null, obj, type2, ClientTypeUtil.GetAddToCollectionDelegate(type3));
			this.currentValue = obj;
		}

		// Token: 0x040002AE RID: 686
		private object currentValue;

		// Token: 0x0200006C RID: 108
		private class NonEntityItemsEnumerable : IEnumerable, IEnumerator
		{
			// Token: 0x060003A6 RID: 934 RVA: 0x0001000D File Offset: 0x0000E20D
			internal NonEntityItemsEnumerable(ODataCollectionReader collectionReader)
			{
				this.collectionReader = collectionReader;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001001C File Offset: 0x0000E21C
			public object Current
			{
				get
				{
					return this.collectionReader.Item;
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00010029 File Offset: 0x0000E229
			public IEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x0001002C File Offset: 0x0000E22C
			public bool MoveNext()
			{
				bool flag;
				for (flag = this.collectionReader.Read(); flag && this.collectionReader.State != ODataCollectionReaderState.Value; flag = this.collectionReader.Read())
				{
				}
				return flag;
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00010065 File Offset: 0x0000E265
			public void Reset()
			{
				throw new InvalidOperationException(Strings.AtomMaterializer_ResetAfterEnumeratorCreationError);
			}

			// Token: 0x040002AF RID: 687
			private readonly ODataCollectionReader collectionReader;
		}
	}
}

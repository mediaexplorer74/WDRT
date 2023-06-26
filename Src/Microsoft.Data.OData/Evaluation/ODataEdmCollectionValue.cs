using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200017D RID: 381
	internal sealed class ODataEdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00023DFB File Offset: 0x00021FFB
		internal ODataEdmCollectionValue(ODataCollectionValue collectionValue)
			: base(collectionValue.GetEdmType())
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00024004 File Offset: 0x00022204
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				if (this.collectionValue != null)
				{
					IEdmTypeReference itemType = ((base.Type == null) ? null : (base.Type.Definition as IEdmCollectionType).ElementType);
					foreach (object collectionItem in this.collectionValue.Items)
					{
						yield return ODataEdmValueUtils.ConvertValue(collectionItem, itemType);
					}
				}
				yield break;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00024021 File Offset: 0x00022221
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x040003FE RID: 1022
		private readonly ODataCollectionValue collectionValue;
	}
}

using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BF RID: 447
	internal sealed class FeedWithoutExpectedTypeValidator
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003095C File Offset: 0x0002EB5C
		internal FeedWithoutExpectedTypeValidator()
		{
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00030964 File Offset: 0x0002EB64
		internal void ValidateEntry(IEdmEntityType entityType)
		{
			if (this.itemType == null)
			{
				this.itemType = entityType;
			}
			if (this.itemType.IsEquivalentTo(entityType))
			{
				return;
			}
			IEdmType commonBaseType = this.itemType.GetCommonBaseType(entityType);
			if (commonBaseType == null)
			{
				throw new ODataException(Strings.FeedWithoutExpectedTypeValidator_IncompatibleTypes(entityType.ODataFullName(), this.itemType.ODataFullName()));
			}
			this.itemType = (IEdmEntityType)commonBaseType;
		}

		// Token: 0x040004AD RID: 1197
		private IEdmEntityType itemType;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000071 RID: 113
	public class ODataSelectPath : ODataPath
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000A656 File Offset: 0x00008856
		public ODataSelectPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A665 File Offset: 0x00008865
		public ODataSelectPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A674 File Offset: 0x00008874
		private void ValidatePath()
		{
			int num = 0;
			foreach (ODataPathSegment odataPathSegment in this)
			{
				if (odataPathSegment is NavigationPropertySegment)
				{
					if (num != base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment);
					}
				}
				else if (odataPathSegment is OperationSegment)
				{
					if (num != base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_OperationSegmentCanOnlyBeLastSegment);
					}
				}
				else if (odataPathSegment is TypeSegment)
				{
					if (num == base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_CannotEndInTypeSegment);
					}
				}
				else
				{
					if (!(odataPathSegment is OpenPropertySegment) && !(odataPathSegment is PropertySegment))
					{
						throw new ODataException(Strings.ODataSelectPath_InvalidSelectPathSegmentType(odataPathSegment.GetType().Name));
					}
					continue;
				}
				num++;
			}
		}
	}
}

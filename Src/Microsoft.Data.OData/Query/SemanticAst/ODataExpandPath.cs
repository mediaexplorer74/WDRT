using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200004B RID: 75
	public class ODataExpandPath : ODataPath
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00007C69 File Offset: 0x00005E69
		public ODataExpandPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007C78 File Offset: 0x00005E78
		public ODataExpandPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007C87 File Offset: 0x00005E87
		internal IEdmNavigationProperty GetNavigationProperty()
		{
			return ((NavigationPropertySegment)base.LastSegment).NavigationProperty;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007C9C File Offset: 0x00005E9C
		private void ValidatePath()
		{
			int num = 0;
			bool flag = false;
			foreach (ODataPathSegment odataPathSegment in this)
			{
				if (odataPathSegment is TypeSegment)
				{
					if (num == base.Count - 1)
					{
						throw new ODataException(Strings.ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty);
					}
				}
				else
				{
					if (!(odataPathSegment is NavigationPropertySegment))
					{
						throw new ODataException(Strings.ODataExpandPath_InvalidExpandPathSegment(odataPathSegment.GetType().Name));
					}
					if (num < base.Count - 1 || flag)
					{
						throw new ODataException(Strings.ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty);
					}
					flag = true;
				}
				num++;
			}
		}
	}
}

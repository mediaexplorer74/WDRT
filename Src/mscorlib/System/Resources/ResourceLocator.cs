using System;

namespace System.Resources
{
	// Token: 0x02000396 RID: 918
	internal struct ResourceLocator
	{
		// Token: 0x06002D5B RID: 11611 RVA: 0x000AD2CF File Offset: 0x000AB4CF
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000AD2DF File Offset: 0x000AB4DF
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000AD2E7 File Offset: 0x000AB4E7
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x000AD2EF File Offset: 0x000AB4EF
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000AD2F8 File Offset: 0x000AB4F8
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x0400125F RID: 4703
		internal object _value;

		// Token: 0x04001260 RID: 4704
		internal int _dataPos;
	}
}

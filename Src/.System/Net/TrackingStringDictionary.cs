using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000228 RID: 552
	internal class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x0006B86C File Offset: 0x00069A6C
		internal TrackingStringDictionary()
			: this(false)
		{
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0006B875 File Offset: 0x00069A75
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0006B884 File Offset: 0x00069A84
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x0006B88C File Offset: 0x00069A8C
		internal bool IsChanged
		{
			get
			{
				return this.isChanged;
			}
			set
			{
				this.isChanged = value;
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0006B895 File Offset: 0x00069A95
		public override void Add(string key, string value)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Add(key, value);
			this.isChanged = true;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0006B8BE File Offset: 0x00069ABE
		public override void Clear()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Clear();
			this.isChanged = true;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0006B8E5 File Offset: 0x00069AE5
		public override void Remove(string key)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Remove(key);
			this.isChanged = true;
		}

		// Token: 0x1700043D RID: 1085
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
				}
				base[key] = value;
				this.isChanged = true;
			}
		}

		// Token: 0x04001616 RID: 5654
		private bool isChanged;

		// Token: 0x04001617 RID: 5655
		private bool isReadOnly;
	}
}

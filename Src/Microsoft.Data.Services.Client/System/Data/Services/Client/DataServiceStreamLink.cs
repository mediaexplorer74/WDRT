using System;
using System.ComponentModel;

namespace System.Data.Services.Client
{
	// Token: 0x0200005E RID: 94
	public sealed class DataServiceStreamLink : INotifyPropertyChanged
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000E09D File Offset: 0x0000C29D
		internal DataServiceStreamLink(string name)
		{
			this.name = name;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600031B RID: 795 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		// (remove) Token: 0x0600031C RID: 796 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E119 File Offset: 0x0000C319
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000E121 File Offset: 0x0000C321
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000E129 File Offset: 0x0000C329
		public Uri SelfLink
		{
			get
			{
				return this.selfLink;
			}
			internal set
			{
				this.selfLink = value;
				this.OnPropertyChanged("SelfLink");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000E13D File Offset: 0x0000C33D
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000E145 File Offset: 0x0000C345
		public Uri EditLink
		{
			get
			{
				return this.editLink;
			}
			internal set
			{
				this.editLink = value;
				this.OnPropertyChanged("EditLink");
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E159 File Offset: 0x0000C359
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000E161 File Offset: 0x0000C361
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			internal set
			{
				this.contentType = value;
				this.OnPropertyChanged("ContentType");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000E175 File Offset: 0x0000C375
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000E17D File Offset: 0x0000C37D
		public string ETag
		{
			get
			{
				return this.etag;
			}
			internal set
			{
				this.etag = value;
				this.OnPropertyChanged("ETag");
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E191 File Offset: 0x0000C391
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000280 RID: 640
		private readonly string name;

		// Token: 0x04000281 RID: 641
		private Uri selfLink;

		// Token: 0x04000282 RID: 642
		private Uri editLink;

		// Token: 0x04000283 RID: 643
		private string contentType;

		// Token: 0x04000284 RID: 644
		private string etag;
	}
}

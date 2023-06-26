using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000044 RID: 68
	internal sealed class ReferenceAppId
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00007046 File Offset: 0x00005246
		internal ReferenceAppId(IReferenceAppId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			this._id = id;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000705E File Offset: 0x0000525E
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000706B File Offset: 0x0000526B
		public string SubscriptionId
		{
			get
			{
				return this._id.get_SubscriptionId();
			}
			set
			{
				this._id.put_SubscriptionId(value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007079 File Offset: 0x00005279
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00007086 File Offset: 0x00005286
		public string Codebase
		{
			get
			{
				return this._id.get_Codebase();
			}
			set
			{
				this._id.put_Codebase(value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007094 File Offset: 0x00005294
		public EnumReferenceIdentity AppPath
		{
			get
			{
				return new EnumReferenceIdentity(this._id.EnumAppPath());
			}
		}

		// Token: 0x04000137 RID: 311
		internal IReferenceAppId _id;
	}
}

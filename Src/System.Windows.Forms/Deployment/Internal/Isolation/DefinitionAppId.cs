using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000042 RID: 66
	internal sealed class DefinitionAppId
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00006FD5 File Offset: 0x000051D5
		internal DefinitionAppId(IDefinitionAppId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			this._id = id;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006FED File Offset: 0x000051ED
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006FFA File Offset: 0x000051FA
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007008 File Offset: 0x00005208
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00007015 File Offset: 0x00005215
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007023 File Offset: 0x00005223
		public EnumDefinitionIdentity AppPath
		{
			get
			{
				return new EnumDefinitionIdentity(this._id.EnumAppPath());
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007035 File Offset: 0x00005235
		private void SetAppPath(IDefinitionIdentity[] Ids)
		{
			this._id.SetAppPath((uint)Ids.Length, Ids);
		}

		// Token: 0x04000136 RID: 310
		internal IDefinitionAppId _id;
	}
}

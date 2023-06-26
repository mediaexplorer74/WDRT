using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088D RID: 2189
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x00147093 File Offset: 0x00145293
		// (set) Token: 0x06005CF4 RID: 23796 RVA: 0x0014709B File Offset: 0x0014529B
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x001470A4 File Offset: 0x001452A4
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x001470B0 File Offset: 0x001452B0
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x040029EA RID: 10730
		private IPrincipal _principal;
	}
}

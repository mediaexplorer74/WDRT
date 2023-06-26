using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AF RID: 687
	public sealed class ODataProperty : ODataAnnotatable
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x00053ECE File Offset: 0x000520CE
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x00053ED6 File Offset: 0x000520D6
		public string Name { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00053EE0 File Offset: 0x000520E0
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00053F18 File Offset: 0x00052118
		public object Value
		{
			get
			{
				if (this.odataOrUntypedValue == null)
				{
					return null;
				}
				ODataUntypedValue odataUntypedValue = this.odataOrUntypedValue as ODataUntypedValue;
				if (odataUntypedValue != null)
				{
					return odataUntypedValue;
				}
				return ((ODataValue)this.odataOrUntypedValue).FromODataValue();
			}
			set
			{
				ODataUntypedValue odataUntypedValue = value as ODataUntypedValue;
				if (odataUntypedValue != null)
				{
					this.odataOrUntypedValue = odataUntypedValue;
					return;
				}
				this.odataOrUntypedValue = value.ToODataValue();
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00053F43 File Offset: 0x00052143
		internal ODataValue ODataValue
		{
			get
			{
				return (ODataValue)this.odataOrUntypedValue;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00053F50 File Offset: 0x00052150
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00053F58 File Offset: 0x00052158
		internal ODataPropertySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = value;
			}
		}

		// Token: 0x04000999 RID: 2457
		private ODataAnnotatable odataOrUntypedValue;

		// Token: 0x0400099A RID: 2458
		private ODataPropertySerializationInfo serializationInfo;
	}
}

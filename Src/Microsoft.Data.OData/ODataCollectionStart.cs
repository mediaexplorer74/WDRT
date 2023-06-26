using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023B RID: 571
	public sealed class ODataCollectionStart : ODataAnnotatable
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00044FC5 File Offset: 0x000431C5
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00044FCD File Offset: 0x000431CD
		public string Name { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00044FD6 File Offset: 0x000431D6
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00044FDE File Offset: 0x000431DE
		internal ODataCollectionStartSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataCollectionStartSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000699 RID: 1689
		private ODataCollectionStartSerializationInfo serializationInfo;
	}
}

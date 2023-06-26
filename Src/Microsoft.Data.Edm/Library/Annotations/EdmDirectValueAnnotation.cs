using System;
using Microsoft.Data.Edm.Annotations;

namespace Microsoft.Data.Edm.Library.Annotations
{
	// Token: 0x020001DE RID: 478
	public class EdmDirectValueAnnotation : EdmNamedElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x00021064 File Offset: 0x0001F264
		public EdmDirectValueAnnotation(string namespaceUri, string name, object value)
			: this(namespaceUri, name)
		{
			EdmUtil.CheckArgumentNull<object>(value, "value");
			this.value = value;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00021081 File Offset: 0x0001F281
		internal EdmDirectValueAnnotation(string namespaceUri, string name)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002109D File Offset: 0x0001F29D
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x000210A5 File Offset: 0x0001F2A5
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000552 RID: 1362
		private readonly object value;

		// Token: 0x04000553 RID: 1363
		private readonly string namespaceUri;
	}
}

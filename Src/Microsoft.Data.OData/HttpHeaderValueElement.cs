using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x02000121 RID: 289
	internal sealed class HttpHeaderValueElement
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x00019DB7 File Offset: 0x00017FB7
		public HttpHeaderValueElement(string name, string value, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyValuePair<string, string>>>(parameters, "parameters");
			this.Name = name;
			this.Value = value;
			this.Parameters = parameters;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00019DEA File Offset: 0x00017FEA
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x00019DF2 File Offset: 0x00017FF2
		public string Name { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00019DFB File Offset: 0x00017FFB
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00019E03 File Offset: 0x00018003
		public string Value { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00019E0C File Offset: 0x0001800C
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00019E14 File Offset: 0x00018014
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; private set; }

		// Token: 0x060007C3 RID: 1987 RVA: 0x00019E20 File Offset: 0x00018020
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			HttpHeaderValueElement.AppendNameValuePair(stringBuilder, this.Name, this.Value);
			foreach (KeyValuePair<string, string> keyValuePair in this.Parameters)
			{
				stringBuilder.Append(";");
				HttpHeaderValueElement.AppendNameValuePair(stringBuilder, keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019EA4 File Offset: 0x000180A4
		private static void AppendNameValuePair(StringBuilder stringBuilder, string name, string value)
		{
			stringBuilder.Append(name);
			if (value != null)
			{
				stringBuilder.Append("=");
				stringBuilder.Append(value);
			}
		}
	}
}

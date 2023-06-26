using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000136 RID: 310
	public sealed class ODataPreferenceHeader
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0001AA63 File Offset: 0x00018C63
		internal ODataPreferenceHeader(IODataRequestMessage requestMessage)
		{
			this.message = new ODataRequestMessage(requestMessage, true, false, -1L);
			this.preferenceHeaderName = "Prefer";
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001AA86 File Offset: 0x00018C86
		internal ODataPreferenceHeader(IODataResponseMessage responseMessage)
		{
			this.message = new ODataResponseMessage(responseMessage, true, false, -1L);
			this.preferenceHeaderName = "Preference-Applied";
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001AAAC File Offset: 0x00018CAC
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001AAEC File Offset: 0x00018CEC
		public bool? ReturnContent
		{
			get
			{
				if (this.PreferenceExists("return-content"))
				{
					return new bool?(true);
				}
				if (this.PreferenceExists("return-no-content"))
				{
					return new bool?(false);
				}
				return null;
			}
			set
			{
				this.Clear("return-content");
				this.Clear("return-no-content");
				if (value == true)
				{
					this.Set(ODataPreferenceHeader.ReturnContentPreference);
				}
				if (value == false)
				{
					this.Set(ODataPreferenceHeader.ReturnNoContentPreference);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001AB54 File Offset: 0x00018D54
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001AB8A File Offset: 0x00018D8A
		public string AnnotationFilter
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.include-annotations");
				if (httpHeaderValueElement != null)
				{
					return httpHeaderValueElement.Value.Trim(new char[] { '"' });
				}
				return null;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "AnnotationFilter");
				if (value == null)
				{
					this.Clear("odata.include-annotations");
					return;
				}
				this.Set(new HttpHeaderValueElement("odata.include-annotations", ODataPreferenceHeader.AddQuotes(value), ODataPreferenceHeader.EmptyParameters));
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		private HttpHeaderValue Preferences
		{
			get
			{
				HttpHeaderValue httpHeaderValue;
				if ((httpHeaderValue = this.preferences) == null)
				{
					httpHeaderValue = (this.preferences = this.ParsePreferences());
				}
				return httpHeaderValue;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001ABEA File Offset: 0x00018DEA
		private static string AddQuotes(string text)
		{
			return "\"" + text + "\"";
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001ABFC File Offset: 0x00018DFC
		private bool PreferenceExists(string preference)
		{
			return this.Get(preference) != null;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001AC0B File Offset: 0x00018E0B
		private void Clear(string preference)
		{
			if (this.Preferences.Remove(preference))
			{
				this.SetPreferencesToMessageHeader();
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001AC21 File Offset: 0x00018E21
		private void Set(HttpHeaderValueElement preference)
		{
			this.Preferences[preference.Name] = preference;
			this.SetPreferencesToMessageHeader();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001AC3C File Offset: 0x00018E3C
		private HttpHeaderValueElement Get(string preferenceName)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			if (!this.Preferences.TryGetValue(preferenceName, out httpHeaderValueElement))
			{
				return null;
			}
			return httpHeaderValueElement;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AC5C File Offset: 0x00018E5C
		private HttpHeaderValue ParsePreferences()
		{
			string header = this.message.GetHeader(this.preferenceHeaderName);
			HttpHeaderValueLexer httpHeaderValueLexer = HttpHeaderValueLexer.Create(this.preferenceHeaderName, header);
			return httpHeaderValueLexer.ToHttpHeaderValue();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001AC8E File Offset: 0x00018E8E
		private void SetPreferencesToMessageHeader()
		{
			this.message.SetHeader(this.preferenceHeaderName, this.Preferences.ToString());
		}

		// Token: 0x04000316 RID: 790
		private const string ReturnNoContentPreferenceToken = "return-no-content";

		// Token: 0x04000317 RID: 791
		private const string ReturnContentPreferenceToken = "return-content";

		// Token: 0x04000318 RID: 792
		private const string ODataAnnotationPreferenceToken = "odata.include-annotations";

		// Token: 0x04000319 RID: 793
		private const string PreferHeaderName = "Prefer";

		// Token: 0x0400031A RID: 794
		private const string PreferenceAppliedHeaderName = "Preference-Applied";

		// Token: 0x0400031B RID: 795
		private static readonly KeyValuePair<string, string>[] EmptyParameters = new KeyValuePair<string, string>[0];

		// Token: 0x0400031C RID: 796
		private static readonly HttpHeaderValueElement ReturnNoContentPreference = new HttpHeaderValueElement("return-no-content", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400031D RID: 797
		private static readonly HttpHeaderValueElement ReturnContentPreference = new HttpHeaderValueElement("return-content", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400031E RID: 798
		private readonly ODataMessage message;

		// Token: 0x0400031F RID: 799
		private readonly string preferenceHeaderName;

		// Token: 0x04000320 RID: 800
		private HttpHeaderValue preferences;
	}
}

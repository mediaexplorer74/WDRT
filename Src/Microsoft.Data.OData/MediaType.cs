using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x02000273 RID: 627
	[DebuggerDisplay("MediaType [{ToText()}]")]
	internal sealed class MediaType
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x0004DC74 File Offset: 0x0004BE74
		internal MediaType(string type, string subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0004DC7F File Offset: 0x0004BE7F
		internal MediaType(string type, string subType, params KeyValuePair<string, string>[] parameters)
			: this(type, subType, (IList<KeyValuePair<string, string>>)parameters)
		{
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004DC8F File Offset: 0x0004BE8F
		internal MediaType(string type, string subType, IList<KeyValuePair<string, string>> parameters)
		{
			this.type = type;
			this.subType = subType;
			this.parameters = parameters;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0004DCAC File Offset: 0x0004BEAC
		internal static Encoding FallbackEncoding
		{
			get
			{
				return MediaTypeUtils.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0004DCB3 File Offset: 0x0004BEB3
		internal static Encoding MissingEncoding
		{
			get
			{
				return Encoding.GetEncoding("ISO-8859-1", new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0004DCC9 File Offset: 0x0004BEC9
		internal string FullTypeName
		{
			get
			{
				return this.type + "/" + this.subType;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0004DCE1 File Offset: 0x0004BEE1
		internal string SubTypeName
		{
			get
			{
				return this.subType;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0004DCE9 File Offset: 0x0004BEE9
		internal string TypeName
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0004DCF1 File Offset: 0x0004BEF1
		internal IList<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004DD28 File Offset: 0x0004BF28
		internal Encoding SelectEncoding()
		{
			if (this.parameters != null)
			{
				using (IEnumerator<string> enumerator = (from parameter in this.parameters
					where HttpUtils.CompareMediaTypeParameterNames("charset", parameter.Key)
					select parameter.Value.Trim() into encodingName
					where encodingName.Length > 0
					select encodingName).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						return MediaType.EncodingFromName(text);
					}
				}
			}
			if (HttpUtils.CompareMediaTypeNames("text", this.type))
			{
				if (!HttpUtils.CompareMediaTypeNames("xml", this.subType))
				{
					return MediaType.MissingEncoding;
				}
				return null;
			}
			else
			{
				if (HttpUtils.CompareMediaTypeNames("application", this.type) && HttpUtils.CompareMediaTypeNames("json", this.subType))
				{
					return MediaType.FallbackEncoding;
				}
				return null;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004DE44 File Offset: 0x0004C044
		internal string ToText()
		{
			return this.ToText(null);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004DE50 File Offset: 0x0004C050
		internal string ToText(Encoding encoding)
		{
			if (this.parameters == null || this.parameters.Count == 0)
			{
				string text = this.FullTypeName;
				if (encoding != null)
				{
					text = string.Concat(new string[] { text, ";", "charset", "=", encoding.WebName });
				}
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(this.FullTypeName);
			foreach (KeyValuePair<string, string> keyValuePair in this.parameters)
			{
				if (!HttpUtils.CompareMediaTypeParameterNames("charset", keyValuePair.Key))
				{
					stringBuilder.Append(";");
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append("=");
					stringBuilder.Append(keyValuePair.Value);
				}
			}
			if (encoding != null)
			{
				stringBuilder.Append(";");
				stringBuilder.Append("charset");
				stringBuilder.Append("=");
				stringBuilder.Append(encoding.WebName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004DF7C File Offset: 0x0004C17C
		private static Encoding EncodingFromName(string name)
		{
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(name);
			if (encodingFromCharsetName == null)
			{
				throw new ODataException(Strings.MediaType_EncodingNotSupported(name));
			}
			return encodingFromCharsetName;
		}

		// Token: 0x04000756 RID: 1878
		private readonly IList<KeyValuePair<string, string>> parameters;

		// Token: 0x04000757 RID: 1879
		private readonly string subType;

		// Token: 0x04000758 RID: 1880
		private readonly string type;
	}
}

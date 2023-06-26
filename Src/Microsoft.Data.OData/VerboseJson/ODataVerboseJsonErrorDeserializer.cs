using System;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000237 RID: 567
	internal sealed class ODataVerboseJsonErrorDeserializer : ODataVerboseJsonDeserializer
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x000445C5 File Offset: 0x000427C5
		internal ODataVerboseJsonErrorDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000445D0 File Offset: 0x000427D0
		internal ODataError ReadTopLevelError()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			ODataError odataError = new ODataError();
			try
			{
				base.ReadPayloadStart(false, false);
				base.JsonReader.ReadStartObject();
				ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("error", text) != 0)
					{
						throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(text));
					}
					ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Error, "error");
					base.JsonReader.ReadStartObject();
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						text = base.JsonReader.ReadPropertyName();
						string text2;
						if ((text2 = text) != null)
						{
							if (text2 == "code")
							{
								ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Code, "code");
								odataError.ErrorCode = base.JsonReader.ReadStringValue("code");
								continue;
							}
							if (text2 == "message")
							{
								ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Message, "message");
								base.JsonReader.ReadStartObject();
								while (base.JsonReader.NodeType == JsonNodeType.Property)
								{
									text = base.JsonReader.ReadPropertyName();
									string text3;
									if ((text3 = text) != null)
									{
										if (text3 == "lang")
										{
											ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageLanguage, "lang");
											odataError.MessageLanguage = base.JsonReader.ReadStringValue("lang");
											continue;
										}
										if (text3 == "value")
										{
											ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue, "value");
											odataError.Message = base.JsonReader.ReadStringValue("value");
											continue;
										}
									}
									throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(text));
								}
								base.JsonReader.ReadEndObject();
								continue;
							}
							if (text2 == "innererror")
							{
								ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError, "innererror");
								odataError.InnerError = this.ReadInnerError(0);
								continue;
							}
						}
						throw new ODataException(Strings.ODataVerboseJsonErrorDeserializer_TopLevelErrorValueWithInvalidProperty(text));
					}
					base.JsonReader.ReadEndObject();
				}
				base.JsonReader.ReadEndObject();
				base.ReadPayloadEnd(false, false);
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return odataError;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00044810 File Offset: 0x00042A10
		private ODataInnerError ReadInnerError(int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			base.JsonReader.ReadStartObject();
			ODataInnerError odataInnerError = new ODataInnerError();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "message")
					{
						ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue, "message");
						odataInnerError.Message = base.JsonReader.ReadStringValue("message");
						continue;
					}
					if (text2 == "type")
					{
						ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.TypeName, "type");
						odataInnerError.TypeName = base.JsonReader.ReadStringValue("type");
						continue;
					}
					if (text2 == "stacktrace")
					{
						ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.StackTrace, "stacktrace");
						odataInnerError.StackTrace = base.JsonReader.ReadStringValue("stacktrace");
						continue;
					}
					if (text2 == "internalexception")
					{
						ODataVerboseJsonReaderUtils.VerifyErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError, "internalexception");
						odataInnerError.InnerError = this.ReadInnerError(recursionDepth);
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.ReadEndObject();
			return odataInnerError;
		}
	}
}

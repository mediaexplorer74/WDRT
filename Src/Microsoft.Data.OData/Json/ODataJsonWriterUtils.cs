using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200028A RID: 650
	internal static class ODataJsonWriterUtils
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x0004F070 File Offset: 0x0004D270
		internal static void WriteError(IJsonWriter jsonWriter, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth, bool writingJsonLight)
		{
			string text;
			string text2;
			string text3;
			ErrorUtils.GetErrorDetails(error, out text, out text2, out text3);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			IEnumerable<ODataInstanceAnnotation> instanceAnnotationsForWriting = error.GetInstanceAnnotationsForWriting();
			ODataJsonWriterUtils.WriteError(jsonWriter, text, text2, text3, odataInnerError, instanceAnnotationsForWriting, writeInstanceAnnotationsDelegate, maxInnerErrorDepth, writingJsonLight);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004F0AF File Offset: 0x0004D2AF
		internal static void WriteMetadataWithTypeName(IJsonWriter jsonWriter, string typeName)
		{
			jsonWriter.WriteName("__metadata");
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("type");
			jsonWriter.WriteValue(typeName);
			jsonWriter.EndObjectScope();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0004F0DA File Offset: 0x0004D2DA
		internal static void StartJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.WritePaddingFunctionName(settings.JsonPCallback);
				jsonWriter.StartPaddingFunctionScope();
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0004F0F6 File Offset: 0x0004D2F6
		internal static void EndJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.EndPaddingFunctionScope();
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0004F108 File Offset: 0x0004D308
		internal static string UriToUriString(ODataOutputContext outputContext, Uri uri, bool makeAbsolute)
		{
			Uri uri2;
			if (outputContext.UrlResolver != null)
			{
				uri2 = outputContext.UrlResolver.ResolveUrl(outputContext.MessageWriterSettings.BaseUri, uri);
				if (uri2 != null)
				{
					return UriUtilsCommon.UriToString(uri2);
				}
			}
			uri2 = uri;
			if (!uri2.IsAbsoluteUri)
			{
				if (makeAbsolute)
				{
					if (outputContext.MessageWriterSettings.BaseUri == null)
					{
						throw new ODataException(Strings.ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)));
					}
					uri2 = UriUtils.UriToAbsoluteUri(outputContext.MessageWriterSettings.BaseUri, uri);
				}
				else
				{
					uri2 = UriUtils.EnsureEscapedRelativeUri(uri2);
				}
			}
			return UriUtilsCommon.UriToString(uri2);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0004F198 File Offset: 0x0004D398
		private static void WriteError(IJsonWriter jsonWriter, string code, string message, string messageLanguage, ODataInnerError innerError, IEnumerable<ODataInstanceAnnotation> instanceAnnotations, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, int maxInnerErrorDepth, bool writingJsonLight)
		{
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName(writingJsonLight ? "odata.error" : "error");
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("code");
			jsonWriter.WriteValue(code);
			jsonWriter.WriteName("message");
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("lang");
			jsonWriter.WriteValue(messageLanguage);
			jsonWriter.WriteName("value");
			jsonWriter.WriteValue(message);
			jsonWriter.EndObjectScope();
			if (innerError != null)
			{
				ODataJsonWriterUtils.WriteInnerError(jsonWriter, innerError, "innererror", 0, maxInnerErrorDepth);
			}
			if (writingJsonLight)
			{
				writeInstanceAnnotationsDelegate(instanceAnnotations);
			}
			jsonWriter.EndObjectScope();
			jsonWriter.EndObjectScope();
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0004F244 File Offset: 0x0004D444
		private static void WriteInnerError(IJsonWriter jsonWriter, ODataInnerError innerError, string innerErrorPropertyName, int recursionDepth, int maxInnerErrorDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, maxInnerErrorDepth);
			jsonWriter.WriteName(innerErrorPropertyName);
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("message");
			jsonWriter.WriteValue(innerError.Message ?? string.Empty);
			jsonWriter.WriteName("type");
			jsonWriter.WriteValue(innerError.TypeName ?? string.Empty);
			jsonWriter.WriteName("stacktrace");
			jsonWriter.WriteValue(innerError.StackTrace ?? string.Empty);
			if (innerError.InnerError != null)
			{
				ODataJsonWriterUtils.WriteInnerError(jsonWriter, innerError.InnerError, "internalexception", recursionDepth, maxInnerErrorDepth);
			}
			jsonWriter.EndObjectScope();
		}
	}
}

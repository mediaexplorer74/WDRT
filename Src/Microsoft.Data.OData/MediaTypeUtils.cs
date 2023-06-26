using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025A RID: 602
	internal static class MediaTypeUtils
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0004AA4D File Offset: 0x00048C4D
		internal static UTF8Encoding EncodingUtf8NoPreamble
		{
			get
			{
				return MediaTypeUtils.encodingUtf8NoPreamble;
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004AA74 File Offset: 0x00048C74
		internal static ODataFormat GetContentTypeFromSettings(ODataMessageWriterSettings settings, ODataPayloadKind payloadKind, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding)
		{
			IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(payloadKind);
			if (mediaTypesForPayloadKind == null || mediaTypesForPayloadKind.Count == 0)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(null, settings.AcceptableMediaTypes));
			}
			ODataFormat format;
			if (settings.UseFormat == true)
			{
				mediaType = MediaTypeUtils.GetDefaultMediaType(mediaTypesForPayloadKind, settings.Format, out format);
				encoding = mediaType.SelectEncoding();
			}
			else
			{
				IList<KeyValuePair<MediaType, string>> list = HttpUtils.MediaTypesFromString(settings.AcceptableMediaTypes);
				if (settings.Version >= ODataVersion.V3)
				{
					MediaTypeUtils.ConvertApplicationJsonInAcceptableMediaTypes(list);
				}
				string text = null;
				MediaTypeWithFormat mediaTypeWithFormat;
				if (list == null || list.Count == 0)
				{
					mediaTypeWithFormat = mediaTypesForPayloadKind[0];
				}
				else
				{
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(list.Select((KeyValuePair<MediaType, string> kvp) => kvp.Key), mediaTypesForPayloadKind.Select((MediaTypeWithFormat smt) => smt.MediaType).ToArray<MediaType>());
					if (mediaTypeMatchInfo == null)
					{
						string text2 = string.Join(", ", mediaTypesForPayloadKind.Select((MediaTypeWithFormat mt) => mt.MediaType.ToText()).ToArray<string>());
						throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(text2, settings.AcceptableMediaTypes));
					}
					mediaTypeWithFormat = mediaTypesForPayloadKind[mediaTypeMatchInfo.TargetTypeIndex];
					text = list[mediaTypeMatchInfo.SourceTypeIndex].Value;
				}
				format = mediaTypeWithFormat.Format;
				mediaType = mediaTypeWithFormat.MediaType;
				string text3 = settings.AcceptableCharsets;
				if (text != null)
				{
					text3 = ((text3 == null) ? text : (text + "," + text3));
				}
				encoding = MediaTypeUtils.GetEncoding(text3, payloadKind, mediaType, true);
			}
			return format;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004AC48 File Offset: 0x00048E48
		internal static ODataFormat GetFormatFromContentType(string contentTypeHeader, ODataPayloadKind[] supportedPayloadKinds, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind, out string batchBoundary)
		{
			ODataFormat formatFromContentType = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, supportedPayloadKinds, mediaTypeResolver, out mediaType, out encoding, out selectedPayloadKind);
			if (selectedPayloadKind == ODataPayloadKind.Batch)
			{
				KeyValuePair<string, string> keyValuePair = default(KeyValuePair<string, string>);
				IEnumerable<KeyValuePair<string, string>> parameters = mediaType.Parameters;
				if (parameters != null)
				{
					bool flag = false;
					foreach (KeyValuePair<string, string> keyValuePair2 in parameters.Where((KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames("boundary", p.Key)))
					{
						if (flag)
						{
							throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(contentTypeHeader, "boundary"));
						}
						keyValuePair = keyValuePair2;
						flag = true;
					}
				}
				if (keyValuePair.Key == null)
				{
					throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(contentTypeHeader, "boundary"));
				}
				batchBoundary = keyValuePair.Value;
				ValidationUtils.ValidateBoundaryString(batchBoundary);
			}
			else
			{
				batchBoundary = null;
			}
			return formatFromContentType;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004AD34 File Offset: 0x00048F34
		internal static IList<ODataPayloadKindDetectionResult> GetPayloadKindsForContentType(string contentTypeHeader, MediaTypeResolver mediaTypeResolver, out MediaType contentType, out Encoding encoding)
		{
			encoding = null;
			string text;
			contentType = MediaTypeUtils.ParseContentType(contentTypeHeader, out text);
			MediaType[] array = new MediaType[] { contentType };
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			for (int i = 0; i < MediaTypeUtils.allSupportedPayloadKinds.Length; i++)
			{
				ODataPayloadKind odataPayloadKind = MediaTypeUtils.allSupportedPayloadKinds[i];
				IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(odataPayloadKind);
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(mediaTypesForPayloadKind.Select((MediaTypeWithFormat smt) => smt.MediaType), array);
				if (mediaTypeMatchInfo != null)
				{
					list.Add(new ODataPayloadKindDetectionResult(odataPayloadKind, mediaTypesForPayloadKind[mediaTypeMatchInfo.SourceTypeIndex].Format));
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				encoding = HttpUtils.GetEncodingFromCharsetName(text);
			}
			return list;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004ADEF File Offset: 0x00048FEF
		internal static bool MediaTypeAndSubtypeAreEqual(string firstTypeAndSubtype, string secondTypeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(firstTypeAndSubtype, "firstTypeAndSubtype");
			ExceptionUtils.CheckArgumentNotNull<string>(secondTypeAndSubtype, "secondTypeAndSubtype");
			return HttpUtils.CompareMediaTypeNames(firstTypeAndSubtype, secondTypeAndSubtype);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004AE0E File Offset: 0x0004900E
		internal static bool MediaTypeStartsWithTypeAndSubtype(string mediaType, string typeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(mediaType, "mediaType");
			ExceptionUtils.CheckArgumentNotNull<string>(typeAndSubtype, "typeAndSubtype");
			return mediaType.StartsWith(typeAndSubtype, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004AE64 File Offset: 0x00049064
		internal static bool MediaTypeHasParameterWithValue(this MediaType mediaType, string parameterName, string parameterValue)
		{
			return mediaType.Parameters != null && mediaType.Parameters.Any((KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, parameterName) && string.Compare(p.Value, parameterValue, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004AEA6 File Offset: 0x000490A6
		internal static bool HasStreamingSetToTrue(this MediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("streaming", "true");
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004AEB8 File Offset: 0x000490B8
		internal static void CheckMediaTypeForWildCards(MediaType mediaType)
		{
			if (HttpUtils.CompareMediaTypeNames("*", mediaType.TypeName) || HttpUtils.CompareMediaTypeNames("*", mediaType.SubTypeName))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_WildcardInContentType(mediaType.FullTypeName));
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004AEF0 File Offset: 0x000490F0
		internal static string AlterContentTypeForJsonPadding(string contentType)
		{
			if (contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
			{
				return contentType.Remove(0, "application/json".Length).Insert(0, "text/javascript");
			}
			if (contentType.StartsWith("text/plain", StringComparison.OrdinalIgnoreCase))
			{
				return contentType.Remove(0, "text/plain".Length).Insert(0, "text/javascript");
			}
			throw new ODataException(Strings.ODataMessageWriter_JsonPaddingOnInvalidContentType(contentType));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004AFAC File Offset: 0x000491AC
		private static ODataFormat GetFormatFromContentType(string contentTypeName, ODataPayloadKind[] supportedPayloadKinds, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind)
		{
			string text;
			mediaType = MediaTypeUtils.ParseContentType(contentTypeName, out text);
			if (!mediaTypeResolver.IsIllegalMediaType(mediaType))
			{
				foreach (ODataPayloadKind odataPayloadKind in supportedPayloadKinds)
				{
					IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(odataPayloadKind);
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(mediaTypesForPayloadKind.Select((MediaTypeWithFormat smt) => smt.MediaType), new MediaType[] { mediaType });
					if (mediaTypeMatchInfo != null)
					{
						selectedPayloadKind = odataPayloadKind;
						encoding = MediaTypeUtils.GetEncoding(text, selectedPayloadKind, mediaType, false);
						return mediaTypesForPayloadKind[mediaTypeMatchInfo.SourceTypeIndex].Format;
					}
				}
			}
			string text2 = string.Join(", ", supportedPayloadKinds.SelectMany((ODataPayloadKind pk) => from mt in mediaTypeResolver.GetMediaTypesForPayloadKind(pk)
				select mt.MediaType.ToText()).ToArray<string>());
			throw new ODataContentTypeException(Strings.MediaTypeUtils_CannotDetermineFormatFromContentType(text2, contentTypeName));
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0004B09C File Offset: 0x0004929C
		private static MediaType ParseContentType(string contentTypeHeader, out string charset)
		{
			IList<KeyValuePair<MediaType, string>> list = HttpUtils.MediaTypesFromString(contentTypeHeader);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(contentTypeHeader));
			}
			MediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			charset = list[0].Value;
			return key;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004B0F0 File Offset: 0x000492F0
		private static MediaType GetDefaultMediaType(IList<MediaTypeWithFormat> supportedMediaTypes, ODataFormat specifiedFormat, out ODataFormat actualFormat)
		{
			for (int i = 0; i < supportedMediaTypes.Count; i++)
			{
				MediaTypeWithFormat mediaTypeWithFormat = supportedMediaTypes[i];
				if (specifiedFormat == null || mediaTypeWithFormat.Format == specifiedFormat)
				{
					actualFormat = mediaTypeWithFormat.Format;
					return mediaTypeWithFormat.MediaType;
				}
			}
			throw new ODataException(Strings.ODataUtils_DidNotFindDefaultMediaType(specifiedFormat));
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004B13C File Offset: 0x0004933C
		private static Encoding GetEncoding(string acceptCharsetHeader, ODataPayloadKind payloadKind, MediaType mediaType, bool useDefaultEncoding)
		{
			if (payloadKind == ODataPayloadKind.BinaryValue)
			{
				return null;
			}
			return HttpUtils.EncodingFromAcceptableCharsets(acceptCharsetHeader, mediaType, MediaTypeUtils.encodingUtf8NoPreamble, useDefaultEncoding ? MediaTypeUtils.encodingUtf8NoPreamble : null);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004B15C File Offset: 0x0004935C
		private static MediaTypeUtils.MediaTypeMatchInfo MatchMediaTypes(IEnumerable<MediaType> sourceTypes, MediaType[] targetTypes)
		{
			MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = null;
			int num = 0;
			if (sourceTypes != null)
			{
				foreach (MediaType mediaType in sourceTypes)
				{
					int num2 = 0;
					foreach (MediaType mediaType2 in targetTypes)
					{
						MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo2 = new MediaTypeUtils.MediaTypeMatchInfo(mediaType, mediaType2, num, num2);
						if (!mediaTypeMatchInfo2.IsMatch)
						{
							num2++;
						}
						else
						{
							if (mediaTypeMatchInfo == null)
							{
								mediaTypeMatchInfo = mediaTypeMatchInfo2;
							}
							else
							{
								int num3 = mediaTypeMatchInfo.CompareTo(mediaTypeMatchInfo2);
								if (num3 < 0)
								{
									mediaTypeMatchInfo = mediaTypeMatchInfo2;
								}
							}
							num2++;
						}
					}
					num++;
				}
			}
			if (mediaTypeMatchInfo == null)
			{
				return null;
			}
			return mediaTypeMatchInfo;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004B224 File Offset: 0x00049424
		private static void ConvertApplicationJsonInAcceptableMediaTypes(IList<KeyValuePair<MediaType, string>> specifiedTypes)
		{
			if (specifiedTypes == null)
			{
				return;
			}
			for (int i = 0; i < specifiedTypes.Count; i++)
			{
				MediaType key = specifiedTypes[i].Key;
				if (HttpUtils.CompareMediaTypeNames(key.SubTypeName, "json") && HttpUtils.CompareMediaTypeNames(key.TypeName, "application"))
				{
					if (key.Parameters != null)
					{
						if (key.Parameters.Any((KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, "odata")))
						{
							goto IL_E6;
						}
					}
					IList<KeyValuePair<string, string>> parameters = key.Parameters;
					int num = ((parameters == null) ? 1 : (parameters.Count + 1));
					List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(num);
					list.Add(new KeyValuePair<string, string>("odata", "minimalmetadata"));
					if (parameters != null)
					{
						list.AddRange(parameters);
					}
					specifiedTypes[i] = new KeyValuePair<MediaType, string>(new MediaType(key.TypeName, key.SubTypeName, list), specifiedTypes[i].Value);
				}
				IL_E6:;
			}
		}

		// Token: 0x0400070B RID: 1803
		private static readonly ODataPayloadKind[] allSupportedPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.Feed,
			ODataPayloadKind.Entry,
			ODataPayloadKind.Property,
			ODataPayloadKind.MetadataDocument,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Value,
			ODataPayloadKind.BinaryValue,
			ODataPayloadKind.Collection,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.Batch,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter
		};

		// Token: 0x0400070C RID: 1804
		private static readonly UTF8Encoding encodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x0200025B RID: 603
		private sealed class MediaTypeMatchInfo : IComparable<MediaTypeUtils.MediaTypeMatchInfo>
		{
			// Token: 0x060013EB RID: 5099 RVA: 0x0004B387 File Offset: 0x00049587
			public MediaTypeMatchInfo(MediaType sourceType, MediaType targetType, int sourceIndex, int targetIndex)
			{
				this.sourceIndex = sourceIndex;
				this.targetIndex = targetIndex;
				this.MatchTypes(sourceType, targetType);
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x060013EC RID: 5100 RVA: 0x0004B3A6 File Offset: 0x000495A6
			public int SourceTypeIndex
			{
				get
				{
					return this.sourceIndex;
				}
			}

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x060013ED RID: 5101 RVA: 0x0004B3AE File Offset: 0x000495AE
			public int TargetTypeIndex
			{
				get
				{
					return this.targetIndex;
				}
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x060013EE RID: 5102 RVA: 0x0004B3B6 File Offset: 0x000495B6
			// (set) Token: 0x060013EF RID: 5103 RVA: 0x0004B3BE File Offset: 0x000495BE
			public int MatchingTypeNamePartCount { get; private set; }

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0004B3C7 File Offset: 0x000495C7
			// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0004B3CF File Offset: 0x000495CF
			public int MatchingParameterCount { get; private set; }

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0004B3D8 File Offset: 0x000495D8
			// (set) Token: 0x060013F3 RID: 5107 RVA: 0x0004B3E0 File Offset: 0x000495E0
			public int QualityValue { get; private set; }

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0004B3E9 File Offset: 0x000495E9
			// (set) Token: 0x060013F5 RID: 5109 RVA: 0x0004B3F1 File Offset: 0x000495F1
			public int SourceTypeParameterCountForMatching { get; private set; }

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0004B3FA File Offset: 0x000495FA
			public bool IsMatch
			{
				get
				{
					return this.QualityValue != 0 && this.MatchingTypeNamePartCount >= 0 && (this.MatchingTypeNamePartCount <= 1 || this.MatchingParameterCount == -1 || this.MatchingParameterCount >= this.SourceTypeParameterCountForMatching);
				}
			}

			// Token: 0x060013F7 RID: 5111 RVA: 0x0004B434 File Offset: 0x00049634
			public int CompareTo(MediaTypeUtils.MediaTypeMatchInfo other)
			{
				ExceptionUtils.CheckArgumentNotNull<MediaTypeUtils.MediaTypeMatchInfo>(other, "other");
				if (this.MatchingTypeNamePartCount > other.MatchingTypeNamePartCount)
				{
					return 1;
				}
				if (this.MatchingTypeNamePartCount == other.MatchingTypeNamePartCount)
				{
					if (this.MatchingParameterCount > other.MatchingParameterCount)
					{
						return 1;
					}
					if (this.MatchingParameterCount == other.MatchingParameterCount)
					{
						int num = this.QualityValue.CompareTo(other.QualityValue);
						if (num != 0)
						{
							return num;
						}
						if (other.TargetTypeIndex >= this.TargetTypeIndex)
						{
							return 1;
						}
						return -1;
					}
				}
				return -1;
			}

			// Token: 0x060013F8 RID: 5112 RVA: 0x0004B4B8 File Offset: 0x000496B8
			private static int ParseQualityValue(string qualityValueText)
			{
				int num = 1000;
				if (qualityValueText.Length > 0)
				{
					int num2 = 0;
					HttpUtils.ReadQualityValue(qualityValueText, ref num2, out num);
				}
				return num;
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x0004B4E4 File Offset: 0x000496E4
			private static bool TryFindMediaTypeParameter(IList<KeyValuePair<string, string>> parameters, string parameterName, out string parameterValue)
			{
				parameterValue = null;
				if (parameters != null)
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						string key = parameters[i].Key;
						if (HttpUtils.CompareMediaTypeParameterNames(parameterName, key))
						{
							parameterValue = parameters[i].Value;
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060013FA RID: 5114 RVA: 0x0004B535 File Offset: 0x00049735
			private static bool IsQualityValueParameter(string parameterName)
			{
				return HttpUtils.CompareMediaTypeParameterNames("q", parameterName);
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x0004B544 File Offset: 0x00049744
			private void MatchTypes(MediaType sourceType, MediaType targetType)
			{
				this.MatchingTypeNamePartCount = -1;
				if (sourceType.TypeName == "*")
				{
					this.MatchingTypeNamePartCount = 0;
				}
				else if (HttpUtils.CompareMediaTypeNames(sourceType.TypeName, targetType.TypeName))
				{
					if (sourceType.SubTypeName == "*")
					{
						this.MatchingTypeNamePartCount = 1;
					}
					else if (HttpUtils.CompareMediaTypeNames(sourceType.SubTypeName, targetType.SubTypeName))
					{
						this.MatchingTypeNamePartCount = 2;
					}
				}
				this.QualityValue = 1000;
				this.SourceTypeParameterCountForMatching = 0;
				this.MatchingParameterCount = 0;
				IList<KeyValuePair<string, string>> parameters = sourceType.Parameters;
				IList<KeyValuePair<string, string>> parameters2 = targetType.Parameters;
				bool flag = parameters2 != null && parameters2.Count > 0;
				bool flag2 = parameters != null && parameters.Count > 0;
				if (flag2)
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						string key = parameters[i].Key;
						if (MediaTypeUtils.MediaTypeMatchInfo.IsQualityValueParameter(key))
						{
							this.QualityValue = MediaTypeUtils.MediaTypeMatchInfo.ParseQualityValue(parameters[i].Value.Trim());
							break;
						}
						this.SourceTypeParameterCountForMatching = i + 1;
						string text;
						if (flag && MediaTypeUtils.MediaTypeMatchInfo.TryFindMediaTypeParameter(parameters2, key, out text) && string.Compare(parameters[i].Value.Trim(), text.Trim(), StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.MatchingParameterCount++;
						}
					}
				}
				if (!flag2 || this.SourceTypeParameterCountForMatching == 0 || this.MatchingParameterCount == this.SourceTypeParameterCountForMatching)
				{
					this.MatchingParameterCount = -1;
				}
			}

			// Token: 0x04000714 RID: 1812
			private const int DefaultQualityValue = 1000;

			// Token: 0x04000715 RID: 1813
			private readonly int sourceIndex;

			// Token: 0x04000716 RID: 1814
			private readonly int targetIndex;
		}
	}
}

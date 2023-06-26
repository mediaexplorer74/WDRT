using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000167 RID: 359
	internal sealed class ODataJsonLightMetadataUriParser
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x0002060A File Offset: 0x0001E80A
		private ODataJsonLightMetadataUriParser(IEdmModel model, Uri metadataUriFromPayload)
		{
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_NoModel);
			}
			this.model = model;
			this.parseResult = new ODataJsonLightMetadataUriParseResult(metadataUriFromPayload);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00020638 File Offset: 0x0001E838
		internal static ODataJsonLightMetadataUriParseResult Parse(IEdmModel model, string metadataUriFromPayload, ODataPayloadKind payloadKind, ODataVersion version, ODataReaderBehavior readerBehavior)
		{
			if (metadataUriFromPayload == null)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_NullMetadataDocumentUri);
			}
			Uri uri = new Uri(metadataUriFromPayload, UriKind.Absolute);
			ODataJsonLightMetadataUriParser odataJsonLightMetadataUriParser = new ODataJsonLightMetadataUriParser(model, uri);
			odataJsonLightMetadataUriParser.TokenizeMetadataUri();
			odataJsonLightMetadataUriParser.ParseMetadataUri(payloadKind, readerBehavior, version);
			return odataJsonLightMetadataUriParser.parseResult;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002067C File Offset: 0x0001E87C
		private static string ExtractSelectQueryOption(string fragment)
		{
			int num = fragment.IndexOf(ODataJsonLightMetadataUriParser.SelectQueryOptionStart, StringComparison.Ordinal);
			if (num < 0)
			{
				return null;
			}
			int num2 = num + ODataJsonLightMetadataUriParser.SelectQueryOptionStart.Length;
			int num3 = fragment.IndexOf('&', num2);
			string text;
			if (num3 < 0)
			{
				text = fragment.Substring(num2);
			}
			else
			{
				text = fragment.Substring(num2, num3 - num2);
			}
			return text.Trim();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000206D4 File Offset: 0x0001E8D4
		private void TokenizeMetadataUri()
		{
			Uri metadataUri = this.parseResult.MetadataUri;
			UriBuilder uriBuilder = new UriBuilder(metadataUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = metadataUri.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00020724 File Offset: 0x0001E924
		private void ParseMetadataUri(ODataPayloadKind expectedPayloadKind, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			ODataPayloadKind odataPayloadKind = this.ParseMetadataUriFragment(this.parseResult.Fragment, readerBehavior, version);
			bool flag = odataPayloadKind == expectedPayloadKind || expectedPayloadKind == ODataPayloadKind.Unsupported;
			if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Collection,
					ODataPayloadKind.Property
				};
				if (expectedPayloadKind == ODataPayloadKind.Property)
				{
					flag = true;
				}
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (!flag)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_MetadataUriDoesNotMatchExpectedPayloadKind(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), expectedPayloadKind.ToString()));
			}
			string selectQueryOption = this.parseResult.SelectQueryOption;
			if (selectQueryOption != null && odataPayloadKind != ODataPayloadKind.Feed && odataPayloadKind != ODataPayloadKind.Entry)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidPayloadKindWithSelectQueryOption(expectedPayloadKind.ToString()));
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000208C8 File Offset: 0x0001EAC8
		private ODataPayloadKind ParseMetadataUriFragment(string fragment, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			int num = fragment.IndexOf('&');
			if (num > 0)
			{
				string text = fragment.Substring(num);
				this.parseResult.SelectQueryOption = ODataJsonLightMetadataUriParser.ExtractSelectQueryOption(text);
				fragment = fragment.Substring(0, num);
			}
			string[] parts = fragment.Split(new char[] { '/' });
			int num2 = parts.Length;
			EdmTypeResolver edmTypeResolver = new EdmTypeReaderResolver(this.model, readerBehavior, version);
			int num3 = fragment.IndexOf("$links", StringComparison.Ordinal);
			ODataPayloadKind odataPayloadKind;
			if (num3 > -1)
			{
				odataPayloadKind = this.ParseAssociationLinks(edmTypeResolver, num2, parts, readerBehavior, version);
			}
			else
			{
				switch (num2)
				{
				case 1:
					if (fragment.Length == 0)
					{
						odataPayloadKind = ODataPayloadKind.ServiceDocument;
					}
					else if (parts[0].Equals("Edm.Null", StringComparison.OrdinalIgnoreCase))
					{
						odataPayloadKind = ODataPayloadKind.Property;
						this.parseResult.IsNullProperty = true;
					}
					else
					{
						IEdmEntitySet edmEntitySet = this.model.ResolveEntitySet(parts[0]);
						if (edmEntitySet != null)
						{
							this.parseResult.EntitySet = edmEntitySet;
							this.parseResult.EdmType = edmTypeResolver.GetElementType(edmEntitySet);
							odataPayloadKind = ODataPayloadKind.Feed;
						}
						else
						{
							this.parseResult.EdmType = this.ResolveType(parts[0], readerBehavior, version);
							odataPayloadKind = ((this.parseResult.EdmType is IEdmCollectionType) ? ODataPayloadKind.Collection : ODataPayloadKind.Property);
						}
					}
					break;
				case 2:
					odataPayloadKind = this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
					{
						IEdmEntityType elementType = edmTypeResolver.GetElementType(resolvedEntitySet);
						if (string.CompareOrdinal("@Element", parts[1]) == 0)
						{
							this.parseResult.EdmType = elementType;
							return ODataPayloadKind.Entry;
						}
						this.parseResult.EdmType = this.ResolveTypeCast(resolvedEntitySet, parts[1], readerBehavior, version, elementType);
						return ODataPayloadKind.Feed;
					});
					break;
				case 3:
					odataPayloadKind = this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
					{
						IEdmEntityType elementType2 = edmTypeResolver.GetElementType(resolvedEntitySet);
						this.parseResult.EdmType = this.ResolveTypeCast(resolvedEntitySet, parts[1], readerBehavior, version, elementType2);
						this.ValidateMetadataUriFragmentItemSelector(parts[2]);
						return ODataPayloadKind.Entry;
					});
					break;
				default:
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_FragmentWithInvalidNumberOfParts(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), num2, 3));
				}
			}
			return odataPayloadKind;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00020D40 File Offset: 0x0001EF40
		private ODataPayloadKind ParseAssociationLinks(EdmTypeResolver edmTypeResolver, int partCount, string[] parts, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			return this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
			{
				ODataPayloadKind odataPayloadKind;
				switch (partCount)
				{
				case 3:
				{
					if (string.CompareOrdinal("$links", parts[1]) != 0)
					{
						throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
					}
					IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, null, parts[2], readerBehavior, version);
					odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, null);
					break;
				}
				case 4:
					if (string.CompareOrdinal("$links", parts[1]) == 0)
					{
						IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, null, parts[2], readerBehavior, version);
						this.ValidateLinkMetadataUriFragmentItemSelector(parts[3]);
						odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, parts[3]);
					}
					else
					{
						if (string.CompareOrdinal("$links", parts[2]) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
						}
						IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, parts[1], parts[3], readerBehavior, version);
						odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, null);
					}
					break;
				case 5:
				{
					if (string.CompareOrdinal("$links", parts[2]) != 0)
					{
						throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
					}
					IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, parts[1], parts[3], readerBehavior, version);
					this.ValidateLinkMetadataUriFragmentItemSelector(parts[2]);
					odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, parts[4]);
					break;
				}
				default:
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
				}
				return odataPayloadKind;
			});
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00020D9C File Offset: 0x0001EF9C
		private ODataPayloadKind SetEntityLinkParseResults(IEdmNavigationProperty navigationProperty, string singleElement)
		{
			this.parseResult.NavigationProperty = navigationProperty;
			ODataPayloadKind odataPayloadKind = (navigationProperty.Type.IsCollection() ? ODataPayloadKind.EntityReferenceLinks : ODataPayloadKind.EntityReferenceLink);
			if (singleElement != null && string.CompareOrdinal("@Element", singleElement) == 0)
			{
				if (!navigationProperty.Type.IsCollection())
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidSingletonNavPropertyForEntityReferenceLinkUri(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), navigationProperty.Name, singleElement));
				}
				odataPayloadKind = ODataPayloadKind.EntityReferenceLink;
			}
			return odataPayloadKind;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00020E14 File Offset: 0x0001F014
		private IEdmNavigationProperty ResolveEntityReferenceLinkMetadataFragment(EdmTypeResolver edmTypeResolver, IEdmEntitySet entitySet, string typeName, string propertyName, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			IEdmEntityType edmEntityType = edmTypeResolver.GetElementType(entitySet);
			if (typeName != null)
			{
				edmEntityType = this.ResolveTypeCast(entitySet, typeName, readerBehavior, version, edmEntityType);
			}
			return this.ResolveNavigationProperty(edmEntityType, propertyName);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00020E45 File Offset: 0x0001F045
		private void ValidateLinkMetadataUriFragmentItemSelector(string elementSelector)
		{
			if (string.CompareOrdinal("@Element", elementSelector) != 0)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityReferenceLinkUriSuffix(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), elementSelector, "@Element"));
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00020E75 File Offset: 0x0001F075
		private void ValidateMetadataUriFragmentItemSelector(string elementSelector)
		{
			if (string.CompareOrdinal("@Element", elementSelector) != 0)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityWithTypeCastUriSuffix(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), elementSelector, "@Element"));
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		private IEdmNavigationProperty ResolveNavigationProperty(IEdmEntityType entityType, string navigationPropertyName)
		{
			IEdmProperty edmProperty = entityType.FindProperty(navigationPropertyName);
			IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidPropertyForEntityReferenceLinkUri(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), navigationPropertyName));
			}
			return edmNavigationProperty;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		private ODataPayloadKind ResolveEntitySet(string entitySetPart, Func<IEdmEntitySet, ODataPayloadKind> resolvedEntitySet)
		{
			IEdmEntitySet edmEntitySet = this.model.ResolveEntitySet(entitySetPart);
			if (edmEntitySet != null)
			{
				this.parseResult.EntitySet = edmEntitySet;
				return resolvedEntitySet(edmEntitySet);
			}
			throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntitySetName(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), entitySetPart));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00020F34 File Offset: 0x0001F134
		private IEdmEntityType ResolveTypeCast(IEdmEntitySet entitySet, string typeCast, ODataReaderBehavior readerBehavior, ODataVersion version, IEdmEntityType entitySetElementType)
		{
			IEdmEntityType edmEntityType = entitySetElementType;
			if (!string.IsNullOrEmpty(typeCast))
			{
				EdmTypeKind edmTypeKind;
				edmEntityType = MetadataUtils.ResolveTypeNameForRead(this.model, null, typeCast, readerBehavior, version, out edmTypeKind) as IEdmEntityType;
				if (edmEntityType == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityTypeInTypeCast(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeCast));
				}
				if (!entitySetElementType.IsAssignableFrom(edmEntityType))
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_IncompatibleEntityTypeInTypeCast(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeCast, entitySetElementType.FullName(), entitySet.FullName()));
				}
			}
			return edmEntityType;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00020FB8 File Offset: 0x0001F1B8
		private IEdmType ResolveType(string typeName, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			string text = EdmLibraryExtensions.GetCollectionItemTypeName(typeName) ?? typeName;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(this.model, null, text, readerBehavior, version, out edmTypeKind);
			if (edmType == null || (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Complex))
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntitySetNameOrTypeName(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeName));
			}
			return (text == typeName) ? edmType : EdmLibraryExtensions.GetCollectionType(edmType.ToTypeReference(true));
		}

		// Token: 0x040003A6 RID: 934
		private static readonly string SelectQueryOptionStart = "$select" + '=';

		// Token: 0x040003A7 RID: 935
		private readonly IEdmModel model;

		// Token: 0x040003A8 RID: 936
		private readonly ODataJsonLightMetadataUriParseResult parseResult;
	}
}

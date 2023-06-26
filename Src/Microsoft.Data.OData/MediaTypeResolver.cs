using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B3 RID: 435
	internal sealed class MediaTypeResolver
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		private MediaTypeResolver(ODataVersion version)
		{
			this.version = version;
			this.mediaTypesForPayloadKind = MediaTypeResolver.CloneDefaultMediaTypes();
			if (this.version < ODataVersion.V3)
			{
				MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonMediaType
				};
				this.AddForJsonPayloadKinds(mediaTypeWithFormat);
				return;
			}
			this.AddJsonLightMediaTypes();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002E8FA File Offset: 0x0002CAFA
		private MediaTypeResolver(ODataVersion version, ODataBehaviorKind formatBehaviorKind)
			: this(version)
		{
			if (this.version < ODataVersion.V3 && formatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				this.AddV2ClientMediaTypes();
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002E916 File Offset: 0x0002CB16
		public static MediaTypeResolver DefaultMediaTypeResolver
		{
			get
			{
				return MediaTypeResolver.MediaTypeResolverCache[ODataVersion.V1];
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002E923 File Offset: 0x0002CB23
		internal static MediaTypeResolver GetWriterMediaTypeResolver(ODataVersion version)
		{
			return MediaTypeResolver.MediaTypeResolverCache[version];
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002E930 File Offset: 0x0002CB30
		internal static MediaTypeResolver CreateReaderMediaTypeResolver(ODataVersion version, ODataBehaviorKind formatBehaviorKind)
		{
			return new MediaTypeResolver(version, formatBehaviorKind);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002E939 File Offset: 0x0002CB39
		internal IList<MediaTypeWithFormat> GetMediaTypesForPayloadKind(ODataPayloadKind payloadKind)
		{
			return this.mediaTypesForPayloadKind[(int)payloadKind];
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002E944 File Offset: 0x0002CB44
		internal bool IsIllegalMediaType(MediaType mediaType)
		{
			return this.version < ODataVersion.V3 && HttpUtils.CompareMediaTypeNames(mediaType.SubTypeName, "json") && HttpUtils.CompareMediaTypeNames(mediaType.TypeName, "application") && (mediaType.MediaTypeHasParameterWithValue("odata", "minimalmetadata") || mediaType.MediaTypeHasParameterWithValue("odata", "fullmetadata") || mediaType.MediaTypeHasParameterWithValue("odata", "nometadata"));
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002E9B8 File Offset: 0x0002CBB8
		private static IList<MediaTypeWithFormat>[] CloneDefaultMediaTypes()
		{
			IList<MediaTypeWithFormat>[] array = new IList<MediaTypeWithFormat>[MediaTypeResolver.defaultMediaTypes.Length];
			for (int i = 0; i < MediaTypeResolver.defaultMediaTypes.Length; i++)
			{
				array[i] = new List<MediaTypeWithFormat>(MediaTypeResolver.defaultMediaTypes[i]);
			}
			return array;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
		private static void AddMediaTypeEntryOrdered(IList<MediaTypeWithFormat> mediaTypeList, MediaTypeWithFormat mediaTypeToInsert, ODataFormat beforeFormat)
		{
			for (int i = 0; i < mediaTypeList.Count; i++)
			{
				if (mediaTypeList[i].Format == beforeFormat)
				{
					mediaTypeList.Insert(i, mediaTypeToInsert);
					return;
				}
			}
			mediaTypeList.Add(mediaTypeToInsert);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002EB4C File Offset: 0x0002CD4C
		private void AddJsonLightMediaTypes()
		{
			var array = new <>f__AnonymousType0<string, string[]>[]
			{
				new
				{
					ParameterName = "odata",
					Values = new string[] { "minimalmetadata", "fullmetadata", "nometadata" }
				},
				new
				{
					ParameterName = "streaming",
					Values = new string[] { "true", "false" }
				}
			};
			LinkedList<MediaType> linkedList = new LinkedList<MediaType>();
			linkedList.AddFirst(MediaTypeResolver.ApplicationJsonMediaType);
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				for (LinkedListNode<MediaType> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					MediaType value = linkedListNode.Value;
					foreach (string text in <>f__AnonymousType.Values)
					{
						List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(value.Parameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
						{
							new KeyValuePair<string, string>(<>f__AnonymousType.ParameterName, text)
						};
						MediaType mediaType = new MediaType(value.TypeName, value.SubTypeName, list);
						linkedList.AddBefore(linkedListNode, mediaType);
					}
				}
			}
			foreach (MediaType mediaType2 in linkedList)
			{
				MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
				{
					Format = ODataFormat.Json,
					MediaType = mediaType2
				};
				if (mediaType2 == MediaTypeResolver.ApplicationJsonMediaType)
				{
					this.AddForJsonPayloadKinds(mediaTypeWithFormat);
				}
				else
				{
					this.InsertForJsonPayloadKinds(mediaTypeWithFormat, ODataFormat.VerboseJson);
				}
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002ED04 File Offset: 0x0002CF04
		private void AddForJsonPayloadKinds(MediaTypeWithFormat mediaTypeWithFormat)
		{
			foreach (ODataPayloadKind odataPayloadKind in MediaTypeResolver.JsonPayloadKinds)
			{
				this.mediaTypesForPayloadKind[(int)odataPayloadKind].Add(mediaTypeWithFormat);
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002ED38 File Offset: 0x0002CF38
		private void InsertForJsonPayloadKinds(MediaTypeWithFormat mediaTypeWithFormat, ODataFormat beforeFormat)
		{
			foreach (ODataPayloadKind odataPayloadKind in MediaTypeResolver.JsonPayloadKinds)
			{
				MediaTypeResolver.AddMediaTypeEntryOrdered(this.mediaTypesForPayloadKind[(int)odataPayloadKind], mediaTypeWithFormat, beforeFormat);
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002ED6C File Offset: 0x0002CF6C
		private void AddV2ClientMediaTypes()
		{
			MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
			};
			MediaTypeWithFormat mediaTypeWithFormat2 = new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = MediaTypeResolver.ApplicationXmlMediaType
			};
			IList<MediaTypeWithFormat> list = this.mediaTypesForPayloadKind[0];
			list.Add(new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = new MediaType("application", "xml", new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("type", "feed")
				})
			});
			list.Add(mediaTypeWithFormat2);
			IList<MediaTypeWithFormat> list2 = this.mediaTypesForPayloadKind[1];
			list2.Add(new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = new MediaType("application", "xml", new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("type", "entry")
				})
			});
			list2.Add(mediaTypeWithFormat2);
			this.mediaTypesForPayloadKind[2].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[3].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[4].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[7].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[8].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[9].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[10].Add(mediaTypeWithFormat);
		}

		// Token: 0x04000482 RID: 1154
		private static readonly MediaType ApplicationAtomXmlMediaType = new MediaType("application", "atom+xml");

		// Token: 0x04000483 RID: 1155
		private static readonly MediaType ApplicationXmlMediaType = new MediaType("application", "xml");

		// Token: 0x04000484 RID: 1156
		private static readonly MediaType TextXmlMediaType = new MediaType("text", "xml");

		// Token: 0x04000485 RID: 1157
		private static readonly MediaType ApplicationJsonMediaType = new MediaType("application", "json");

		// Token: 0x04000486 RID: 1158
		private static readonly MediaType ApplicationJsonVerboseMediaType = new MediaType("application", "json", new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("odata", "verbose")
		});

		// Token: 0x04000487 RID: 1159
		private static readonly MediaTypeWithFormat[][] defaultMediaTypes = new MediaTypeWithFormat[][]
		{
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atom+xml", new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("type", "feed")
					})
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atom+xml", new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("type", "entry")
					})
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.RawValue,
					MediaType = new MediaType("text", "plain")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.RawValue,
					MediaType = new MediaType("application", "octet-stream")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atomsvc+xml")
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Metadata,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Batch,
					MediaType = new MediaType("multipart", "mixed")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			}
		};

		// Token: 0x04000488 RID: 1160
		private static readonly ODataVersionCache<MediaTypeResolver> MediaTypeResolverCache = new ODataVersionCache<MediaTypeResolver>((ODataVersion version) => new MediaTypeResolver(version));

		// Token: 0x04000489 RID: 1161
		private readonly ODataVersion version;

		// Token: 0x0400048A RID: 1162
		private readonly IList<MediaTypeWithFormat>[] mediaTypesForPayloadKind;

		// Token: 0x0400048B RID: 1163
		private static readonly ODataPayloadKind[] JsonPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.Feed,
			ODataPayloadKind.Entry,
			ODataPayloadKind.Property,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.Collection,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter
		};
	}
}

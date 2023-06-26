using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001BA RID: 442
	internal sealed class ODataVerboseJsonPayloadKindDetectionDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x000302B7 File Offset: 0x0002E4B7
		internal ODataVerboseJsonPayloadKindDetectionDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000302D0 File Offset: 0x0002E4D0
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind()
		{
			this.detectedPayloadKinds.Clear();
			base.JsonReader.DisableInStreamErrorDetection = true;
			IEnumerable<ODataPayloadKind> enumerable;
			try
			{
				base.ReadPayloadStart(false);
				JsonNodeType nodeType = base.JsonReader.NodeType;
				if (nodeType == JsonNodeType.StartObject)
				{
					base.JsonReader.ReadStartObject();
					int num = 0;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						num++;
						if (string.CompareOrdinal("__metadata", text) == 0)
						{
							this.ProcessMetadataPropertyValue();
							break;
						}
						if (num == 1)
						{
							this.AddPayloadKinds(new ODataPayloadKind[]
							{
								ODataPayloadKind.Property,
								ODataPayloadKind.Entry,
								ODataPayloadKind.Parameter
							});
							ODataError odataError;
							if (string.CompareOrdinal("uri", text) == 0 && base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
							{
								this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
							}
							else if (string.CompareOrdinal("error", text) == 0 && base.JsonReader.StartBufferingAndTryToReadInStreamErrorPropertyValue(out odataError))
							{
								this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Error });
							}
						}
						else if (num == 2)
						{
							this.RemovePayloadKinds(new ODataPayloadKind[]
							{
								ODataPayloadKind.Property,
								ODataPayloadKind.EntityReferenceLink,
								ODataPayloadKind.Error
							});
						}
						if (string.CompareOrdinal("results", text) == 0 && base.JsonReader.NodeType == JsonNodeType.StartArray)
						{
							this.DetectStartArrayPayloadKind(false);
						}
						else if (base.ReadingResponse && string.CompareOrdinal("EntitySets", text) == 0 && base.JsonReader.NodeType == JsonNodeType.StartArray)
						{
							this.ProcessEntitySetsArray();
						}
						base.JsonReader.SkipValue();
					}
					if (num == 0)
					{
						this.AddPayloadKinds(new ODataPayloadKind[]
						{
							ODataPayloadKind.Entry,
							ODataPayloadKind.Parameter
						});
					}
				}
				else if (nodeType == JsonNodeType.StartArray)
				{
					this.DetectStartArrayPayloadKind(true);
				}
				enumerable = this.detectedPayloadKinds;
			}
			catch (ODataException)
			{
				enumerable = Enumerable.Empty<ODataPayloadKind>();
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return enumerable;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000304E4 File Offset: 0x0002E6E4
		private void DetectStartArrayPayloadKind(bool isTopLevel)
		{
			if (!isTopLevel)
			{
				this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Property });
			}
			base.JsonReader.StartBuffering();
			try
			{
				base.JsonReader.ReadStartArray();
				JsonNodeType nodeType = base.JsonReader.NodeType;
				if (nodeType != JsonNodeType.StartObject)
				{
					switch (nodeType)
					{
					case JsonNodeType.EndArray:
						this.AddPayloadKinds(new ODataPayloadKind[]
						{
							ODataPayloadKind.Feed,
							ODataPayloadKind.Collection,
							ODataPayloadKind.EntityReferenceLinks
						});
						break;
					case JsonNodeType.PrimitiveValue:
						this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Collection });
						break;
					}
				}
				else
				{
					base.JsonReader.ReadStartObject();
					bool flag = false;
					int num = 0;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						num++;
						if (num > 1)
						{
							break;
						}
						if (string.CompareOrdinal("uri", text) == 0 && base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
						{
							flag = true;
						}
						base.JsonReader.SkipValue();
					}
					this.AddPayloadKinds(new ODataPayloadKind[]
					{
						ODataPayloadKind.Feed,
						ODataPayloadKind.Collection
					});
					if (flag && num == 1)
					{
						this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
					}
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003063C File Offset: 0x0002E83C
		private void ProcessMetadataPropertyValue()
		{
			this.detectedPayloadKinds.Clear();
			string text = base.ReadTypeNameFromMetadataPropertyValue();
			EdmTypeKind edmTypeKind = EdmTypeKind.None;
			if (text != null)
			{
				MetadataUtils.ResolveTypeNameForRead(EdmCoreModel.Instance, null, text, base.MessageReaderSettings.ReaderBehavior, base.Version, out edmTypeKind);
			}
			if (edmTypeKind == EdmTypeKind.Primitive || edmTypeKind == EdmTypeKind.Collection)
			{
				return;
			}
			this.detectedPayloadKinds.Add(ODataPayloadKind.Entry);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00030698 File Offset: 0x0002E898
		private void ProcessEntitySetsArray()
		{
			base.JsonReader.StartBuffering();
			try
			{
				base.JsonReader.ReadStartArray();
				if (base.JsonReader.NodeType == JsonNodeType.EndArray || base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
				{
					this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00030708 File Offset: 0x0002E908
		private void AddPayloadKinds(params ODataPayloadKind[] payloadKinds)
		{
			this.AddOrRemovePayloadKinds(new Func<ODataPayloadKind, bool>(this.detectedPayloadKinds.Add), payloadKinds);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00030722 File Offset: 0x0002E922
		private void RemovePayloadKinds(params ODataPayloadKind[] payloadKinds)
		{
			this.AddOrRemovePayloadKinds(new Func<ODataPayloadKind, bool>(this.detectedPayloadKinds.Remove), payloadKinds);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003073C File Offset: 0x0002E93C
		private void AddOrRemovePayloadKinds(Func<ODataPayloadKind, bool> addOrRemoveAction, params ODataPayloadKind[] payloadKinds)
		{
			foreach (ODataPayloadKind odataPayloadKind in payloadKinds)
			{
				if (ODataUtilsInternal.IsPayloadKindSupported(odataPayloadKind, !base.ReadingResponse))
				{
					addOrRemoveAction(odataPayloadKind);
				}
			}
		}

		// Token: 0x04000496 RID: 1174
		private readonly HashSet<ODataPayloadKind> detectedPayloadKinds = new HashSet<ODataPayloadKind>(EqualityComparer<ODataPayloadKind>.Default);
	}
}

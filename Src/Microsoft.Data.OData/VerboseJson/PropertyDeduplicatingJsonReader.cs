using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000179 RID: 377
	internal sealed class PropertyDeduplicatingJsonReader : BufferingJsonReader
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00023AF7 File Offset: 0x00021CF7
		internal PropertyDeduplicatingJsonReader(TextReader reader, int maxInnerErrorDepth)
			: base(reader, "error", maxInnerErrorDepth, ODataFormat.VerboseJson)
		{
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00023B0C File Offset: 0x00021D0C
		protected override void ProcessObjectValue()
		{
			Stack<PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord> stack = new Stack<PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord>();
			for (;;)
			{
				if (this.currentBufferedNode.NodeType == JsonNodeType.StartObject)
				{
					stack.Push(new PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord());
					BufferingJsonReader.BufferedNode currentBufferedNode = this.currentBufferedNode;
					base.ProcessObjectValue();
					this.currentBufferedNode = currentBufferedNode;
				}
				else if (this.currentBufferedNode.NodeType == JsonNodeType.EndObject)
				{
					PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord objectRecordPropertyDeduplicationRecord = stack.Pop();
					if (objectRecordPropertyDeduplicationRecord.CurrentPropertyRecord != null)
					{
						objectRecordPropertyDeduplicationRecord.CurrentPropertyRecord.LastPropertyValueNode = this.currentBufferedNode.Previous;
					}
					foreach (List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord> list in objectRecordPropertyDeduplicationRecord.Values)
					{
						if (list.Count > 1)
						{
							PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord propertyDeduplicationRecord = list[0];
							for (int i = 1; i < list.Count; i++)
							{
								PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord propertyDeduplicationRecord2 = list[i];
								propertyDeduplicationRecord2.PropertyNode.Previous.Next = propertyDeduplicationRecord2.LastPropertyValueNode.Next;
								propertyDeduplicationRecord2.LastPropertyValueNode.Next.Previous = propertyDeduplicationRecord2.PropertyNode.Previous;
								propertyDeduplicationRecord.PropertyNode.Previous.Next = propertyDeduplicationRecord2.PropertyNode;
								propertyDeduplicationRecord2.PropertyNode.Previous = propertyDeduplicationRecord.PropertyNode.Previous;
								propertyDeduplicationRecord.LastPropertyValueNode.Next.Previous = propertyDeduplicationRecord2.LastPropertyValueNode;
								propertyDeduplicationRecord2.LastPropertyValueNode.Next = propertyDeduplicationRecord.LastPropertyValueNode.Next;
								propertyDeduplicationRecord = propertyDeduplicationRecord2;
							}
						}
					}
					if (stack.Count == 0)
					{
						break;
					}
				}
				else if (this.currentBufferedNode.NodeType == JsonNodeType.Property)
				{
					PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord objectRecordPropertyDeduplicationRecord2 = stack.Peek();
					if (objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord != null)
					{
						objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord.LastPropertyValueNode = this.currentBufferedNode.Previous;
					}
					objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord = new PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord(this.currentBufferedNode);
					string text = (string)this.currentBufferedNode.Value;
					List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord> list2;
					if (!objectRecordPropertyDeduplicationRecord2.TryGetValue(text, out list2))
					{
						list2 = new List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord>();
						objectRecordPropertyDeduplicationRecord2.Add(text, list2);
					}
					list2.Add(objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord);
				}
				if (!base.ReadInternal())
				{
					return;
				}
			}
		}

		// Token: 0x0200017A RID: 378
		private sealed class ObjectRecordPropertyDeduplicationRecord : Dictionary<string, List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord>>
		{
			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00023D44 File Offset: 0x00021F44
			// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00023D4C File Offset: 0x00021F4C
			internal PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord CurrentPropertyRecord { get; set; }
		}

		// Token: 0x0200017B RID: 379
		private sealed class PropertyDeduplicationRecord
		{
			// Token: 0x06000AB9 RID: 2745 RVA: 0x00023D5D File Offset: 0x00021F5D
			internal PropertyDeduplicationRecord(BufferingJsonReader.BufferedNode propertyNode)
			{
				this.propertyNode = propertyNode;
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00023D6C File Offset: 0x00021F6C
			internal BufferingJsonReader.BufferedNode PropertyNode
			{
				get
				{
					return this.propertyNode;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00023D74 File Offset: 0x00021F74
			// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00023D7C File Offset: 0x00021F7C
			internal BufferingJsonReader.BufferedNode LastPropertyValueNode
			{
				get
				{
					return this.lastPropertyValueNode;
				}
				set
				{
					this.lastPropertyValueNode = value;
				}
			}

			// Token: 0x040003FA RID: 1018
			private readonly BufferingJsonReader.BufferedNode propertyNode;

			// Token: 0x040003FB RID: 1019
			private BufferingJsonReader.BufferedNode lastPropertyValueNode;
		}
	}
}

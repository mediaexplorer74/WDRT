using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000173 RID: 371
	internal sealed class ReorderingJsonReader : BufferingJsonReader
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00022B84 File Offset: 0x00020D84
		internal ReorderingJsonReader(TextReader reader, int maxInnerErrorDepth)
			: base(reader, "odata.error", maxInnerErrorDepth, ODataFormat.Json)
		{
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00022B98 File Offset: 0x00020D98
		protected override void ProcessObjectValue()
		{
			Stack<ReorderingJsonReader.BufferedObject> stack = new Stack<ReorderingJsonReader.BufferedObject>();
			for (;;)
			{
				switch (this.currentBufferedNode.NodeType)
				{
				case JsonNodeType.StartObject:
				{
					ReorderingJsonReader.BufferedObject bufferedObject = new ReorderingJsonReader.BufferedObject
					{
						ObjectStart = this.currentBufferedNode
					};
					stack.Push(bufferedObject);
					base.ProcessObjectValue();
					this.currentBufferedNode = bufferedObject.ObjectStart;
					base.ReadInternal();
					continue;
				}
				case JsonNodeType.EndObject:
				{
					ReorderingJsonReader.BufferedObject bufferedObject2 = stack.Pop();
					if (bufferedObject2.CurrentProperty != null)
					{
						bufferedObject2.CurrentProperty.EndOfPropertyValueNode = this.currentBufferedNode.Previous;
					}
					bufferedObject2.Reorder();
					if (stack.Count == 0)
					{
						return;
					}
					base.ReadInternal();
					continue;
				}
				case JsonNodeType.Property:
				{
					ReorderingJsonReader.BufferedObject bufferedObject3 = stack.Peek();
					if (bufferedObject3.CurrentProperty != null)
					{
						bufferedObject3.CurrentProperty.EndOfPropertyValueNode = this.currentBufferedNode.Previous;
					}
					ReorderingJsonReader.BufferedProperty bufferedProperty = new ReorderingJsonReader.BufferedProperty();
					bufferedProperty.PropertyNameNode = this.currentBufferedNode;
					string text;
					string text2;
					this.ReadPropertyName(out text, out text2);
					bufferedProperty.PropertyAnnotationName = text2;
					bufferedObject3.AddBufferedProperty(text, text2, bufferedProperty);
					if (text2 != null)
					{
						this.BufferValue();
						continue;
					}
					continue;
				}
				}
				base.ReadInternal();
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00022CCC File Offset: 0x00020ECC
		private void ReadPropertyName(out string propertyName, out string annotationName)
		{
			string propertyName2 = this.GetPropertyName();
			base.ReadInternal();
			int num = propertyName2.IndexOf('@');
			if (num >= 0)
			{
				propertyName = propertyName2.Substring(0, num);
				annotationName = propertyName2.Substring(num + 1);
				return;
			}
			int num2 = propertyName2.IndexOf('.');
			if (num2 < 0)
			{
				propertyName = propertyName2;
				annotationName = null;
				return;
			}
			propertyName = null;
			annotationName = propertyName2;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00022D24 File Offset: 0x00020F24
		private void BufferValue()
		{
			int num = 0;
			do
			{
				switch (this.NodeType)
				{
				case JsonNodeType.StartObject:
				case JsonNodeType.StartArray:
					num++;
					break;
				case JsonNodeType.EndObject:
				case JsonNodeType.EndArray:
					num--;
					break;
				}
				base.ReadInternal();
			}
			while (num > 0);
		}

		// Token: 0x02000174 RID: 372
		private sealed class BufferedObject
		{
			// Token: 0x06000A8B RID: 2699 RVA: 0x00022D71 File Offset: 0x00020F71
			internal BufferedObject()
			{
				this.propertyNamesWithAnnotations = new List<KeyValuePair<string, string>>();
				this.dataProperties = new HashSet<string>(StringComparer.Ordinal);
				this.propertyCache = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00022DA4 File Offset: 0x00020FA4
			// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00022DAC File Offset: 0x00020FAC
			internal BufferingJsonReader.BufferedNode ObjectStart { get; set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00022DB5 File Offset: 0x00020FB5
			// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00022DBD File Offset: 0x00020FBD
			internal ReorderingJsonReader.BufferedProperty CurrentProperty { get; private set; }

			// Token: 0x06000A90 RID: 2704 RVA: 0x00022DC8 File Offset: 0x00020FC8
			internal void AddBufferedProperty(string propertyName, string annotationName, ReorderingJsonReader.BufferedProperty bufferedProperty)
			{
				this.CurrentProperty = bufferedProperty;
				string text = propertyName ?? annotationName;
				if (propertyName == null)
				{
					this.propertyNamesWithAnnotations.Add(new KeyValuePair<string, string>(annotationName, null));
				}
				else if (!this.dataProperties.Contains(propertyName))
				{
					if (annotationName == null)
					{
						this.dataProperties.Add(propertyName);
					}
					this.propertyNamesWithAnnotations.Add(new KeyValuePair<string, string>(propertyName, annotationName));
				}
				object obj;
				if (this.propertyCache.TryGetValue(text, out obj))
				{
					ReorderingJsonReader.BufferedProperty bufferedProperty2 = obj as ReorderingJsonReader.BufferedProperty;
					List<ReorderingJsonReader.BufferedProperty> list;
					if (bufferedProperty2 != null)
					{
						list = new List<ReorderingJsonReader.BufferedProperty>(4);
						list.Add(bufferedProperty2);
						this.propertyCache[text] = list;
					}
					else
					{
						list = (List<ReorderingJsonReader.BufferedProperty>)obj;
					}
					list.Add(bufferedProperty);
					return;
				}
				this.propertyCache.Add(text, bufferedProperty);
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00022E80 File Offset: 0x00021080
			internal void Reorder()
			{
				BufferingJsonReader.BufferedNode bufferedNode = this.ObjectStart;
				IEnumerable<string> enumerable = this.SortPropertyNames();
				foreach (string text in enumerable)
				{
					object obj = this.propertyCache[text];
					ReorderingJsonReader.BufferedProperty bufferedProperty = obj as ReorderingJsonReader.BufferedProperty;
					if (bufferedProperty != null)
					{
						bufferedProperty.InsertAfter(bufferedNode);
						bufferedNode = bufferedProperty.EndOfPropertyValueNode;
					}
					else
					{
						IEnumerable<ReorderingJsonReader.BufferedProperty> enumerable2 = ReorderingJsonReader.BufferedObject.SortBufferedProperties((IList<ReorderingJsonReader.BufferedProperty>)obj);
						foreach (ReorderingJsonReader.BufferedProperty bufferedProperty2 in enumerable2)
						{
							bufferedProperty2.InsertAfter(bufferedNode);
							bufferedNode = bufferedProperty2.EndOfPropertyValueNode;
						}
					}
				}
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x00023124 File Offset: 0x00021324
			private static IEnumerable<ReorderingJsonReader.BufferedProperty> SortBufferedProperties(IList<ReorderingJsonReader.BufferedProperty> bufferedProperties)
			{
				List<ReorderingJsonReader.BufferedProperty> delayedProperties = null;
				for (int i = 0; i < bufferedProperties.Count; i++)
				{
					ReorderingJsonReader.BufferedProperty bufferedProperty = bufferedProperties[i];
					string annotationName = bufferedProperty.PropertyAnnotationName;
					if (annotationName == null || !ReorderingJsonReader.BufferedObject.IsODataInstanceAnnotation(annotationName))
					{
						if (delayedProperties == null)
						{
							delayedProperties = new List<ReorderingJsonReader.BufferedProperty>();
						}
						delayedProperties.Add(bufferedProperty);
					}
					else
					{
						yield return bufferedProperty;
					}
				}
				if (delayedProperties != null)
				{
					for (int j = 0; j < delayedProperties.Count; j++)
					{
						yield return delayedProperties[j];
					}
				}
				yield break;
			}

			// Token: 0x06000A93 RID: 2707 RVA: 0x00023141 File Offset: 0x00021341
			private static bool IsODataInstanceAnnotation(string annotationName)
			{
				return annotationName.StartsWith("odata.", StringComparison.Ordinal);
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x0002314F File Offset: 0x0002134F
			private static bool IsODataMetadataAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.metadata", annotationName) == 0;
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x0002315F File Offset: 0x0002135F
			private static bool IsODataAnnotationGroupReferenceAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.annotationGroupReference", annotationName) == 0;
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x0002316F File Offset: 0x0002136F
			private static bool IsODataAnnotationGroupAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.annotationGroup", annotationName) == 0;
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x0002317F File Offset: 0x0002137F
			private static bool IsODataTypeAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.type", annotationName) == 0;
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x0002318F File Offset: 0x0002138F
			private static bool IsODataIdAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.id", annotationName) == 0;
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x0002319F File Offset: 0x0002139F
			private static bool IsODataETagAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.etag", annotationName) == 0;
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x00023698 File Offset: 0x00021898
			private IEnumerable<string> SortPropertyNames()
			{
				string metadataAnnotationName = null;
				string typeAnnotationName = null;
				string idAnnotationName = null;
				string etagAnnotationName = null;
				string annotationGroupDeclarationName = null;
				string annotationGroupReferenceName = null;
				List<string> odataAnnotationNames = null;
				List<string> otherNames = null;
				foreach (KeyValuePair<string, string> keyValuePair in this.propertyNamesWithAnnotations)
				{
					string key = keyValuePair.Key;
					if (keyValuePair.Value == null || !this.dataProperties.Contains(key))
					{
						this.dataProperties.Add(key);
						if (ReorderingJsonReader.BufferedObject.IsODataMetadataAnnotation(key))
						{
							metadataAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataAnnotationGroupAnnotation(key))
						{
							annotationGroupDeclarationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataAnnotationGroupReferenceAnnotation(key))
						{
							annotationGroupReferenceName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataTypeAnnotation(key))
						{
							typeAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataIdAnnotation(key))
						{
							idAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataETagAnnotation(key))
						{
							etagAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataInstanceAnnotation(key))
						{
							if (odataAnnotationNames == null)
							{
								odataAnnotationNames = new List<string>();
							}
							odataAnnotationNames.Add(key);
						}
						else
						{
							if (otherNames == null)
							{
								otherNames = new List<string>();
							}
							otherNames.Add(key);
						}
					}
				}
				if (metadataAnnotationName != null)
				{
					yield return metadataAnnotationName;
				}
				if (annotationGroupDeclarationName != null)
				{
					yield return annotationGroupDeclarationName;
				}
				if (annotationGroupReferenceName != null)
				{
					yield return annotationGroupReferenceName;
				}
				if (typeAnnotationName != null)
				{
					yield return typeAnnotationName;
				}
				if (idAnnotationName != null)
				{
					yield return idAnnotationName;
				}
				if (etagAnnotationName != null)
				{
					yield return etagAnnotationName;
				}
				if (odataAnnotationNames != null)
				{
					foreach (string propertyName in odataAnnotationNames)
					{
						yield return propertyName;
					}
				}
				if (otherNames != null)
				{
					foreach (string propertyName2 in otherNames)
					{
						yield return propertyName2;
					}
				}
				yield break;
			}

			// Token: 0x040003EC RID: 1004
			private readonly Dictionary<string, object> propertyCache;

			// Token: 0x040003ED RID: 1005
			private readonly HashSet<string> dataProperties;

			// Token: 0x040003EE RID: 1006
			private readonly List<KeyValuePair<string, string>> propertyNamesWithAnnotations;
		}

		// Token: 0x02000175 RID: 373
		private sealed class BufferedProperty
		{
			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000A9B RID: 2715 RVA: 0x000236B5 File Offset: 0x000218B5
			// (set) Token: 0x06000A9C RID: 2716 RVA: 0x000236BD File Offset: 0x000218BD
			internal string PropertyAnnotationName { get; set; }

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000236C6 File Offset: 0x000218C6
			// (set) Token: 0x06000A9E RID: 2718 RVA: 0x000236CE File Offset: 0x000218CE
			internal BufferingJsonReader.BufferedNode PropertyNameNode { get; set; }

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000236D7 File Offset: 0x000218D7
			// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000236DF File Offset: 0x000218DF
			internal BufferingJsonReader.BufferedNode EndOfPropertyValueNode { get; set; }

			// Token: 0x06000AA1 RID: 2721 RVA: 0x000236E8 File Offset: 0x000218E8
			internal void InsertAfter(BufferingJsonReader.BufferedNode node)
			{
				BufferingJsonReader.BufferedNode previous = this.PropertyNameNode.Previous;
				BufferingJsonReader.BufferedNode bufferedNode = this.EndOfPropertyValueNode.Next;
				previous.Next = bufferedNode;
				bufferedNode.Previous = previous;
				bufferedNode = node.Next;
				node.Next = this.PropertyNameNode;
				this.PropertyNameNode.Previous = node;
				this.EndOfPropertyValueNode.Next = bufferedNode;
				if (bufferedNode != null)
				{
					bufferedNode.Previous = this.EndOfPropertyValueNode;
				}
			}
		}
	}
}

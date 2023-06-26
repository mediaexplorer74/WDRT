using System;
using System.IO;
using Microsoft.Data.OData.VerboseJson;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000171 RID: 369
	internal class BufferingJsonReader : JsonReader
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x00022340 File Offset: 0x00020540
		internal BufferingJsonReader(TextReader reader, string inStreamErrorPropertyName, int maxInnerErrorDepth, ODataFormat jsonFormat)
			: base(reader, jsonFormat)
		{
			this.inStreamErrorPropertyName = inStreamErrorPropertyName;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			this.bufferedNodesHead = null;
			this.currentBufferedNode = null;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00022367 File Offset: 0x00020567
		public override JsonNodeType NodeType
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.NodeType;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.NodeType;
				}
				return this.bufferedNodesHead.NodeType;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00022397 File Offset: 0x00020597
		public override object Value
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.Value;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.Value;
				}
				return this.bufferedNodesHead.Value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x000223C7 File Offset: 0x000205C7
		public override string RawValue
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.RawValue;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.RawValue;
				}
				return this.bufferedNodesHead.RawValue;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000223F7 File Offset: 0x000205F7
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000223FF File Offset: 0x000205FF
		internal bool DisableInStreamErrorDetection
		{
			get
			{
				return this.disableInStreamErrorDetection;
			}
			set
			{
				this.disableInStreamErrorDetection = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00022408 File Offset: 0x00020608
		internal bool IsBuffering
		{
			get
			{
				return this.isBuffering;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00022410 File Offset: 0x00020610
		public override bool Read()
		{
			return this.ReadInternal();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00022418 File Offset: 0x00020618
		internal void StartBuffering()
		{
			if (this.bufferedNodesHead == null)
			{
				this.bufferedNodesHead = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value, base.RawValue);
			}
			else
			{
				this.removeOnNextRead = false;
			}
			if (this.currentBufferedNode == null)
			{
				this.currentBufferedNode = this.bufferedNodesHead;
			}
			this.isBuffering = true;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002246E File Offset: 0x0002066E
		internal object BookmarkCurrentPosition()
		{
			return this.currentBufferedNode;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00022478 File Offset: 0x00020678
		internal void MoveToBookmark(object bookmark)
		{
			BufferingJsonReader.BufferedNode bufferedNode = bookmark as BufferingJsonReader.BufferedNode;
			this.currentBufferedNode = bufferedNode;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00022493 File Offset: 0x00020693
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000224AC File Offset: 0x000206AC
		internal bool StartBufferingAndTryToReadInStreamErrorPropertyValue(out ODataError error)
		{
			error = null;
			this.StartBuffering();
			this.parsingInStreamError = true;
			bool flag;
			try
			{
				flag = this.TryReadInStreamErrorPropertyValue(out error);
			}
			finally
			{
				this.StopBuffering();
				this.parsingInStreamError = false;
			}
			return flag;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000224F4 File Offset: 0x000206F4
		protected bool ReadInternal()
		{
			if (this.removeOnNextRead)
			{
				this.RemoveFirstNodeInBuffer();
				this.removeOnNextRead = false;
			}
			bool flag;
			if (this.isBuffering)
			{
				if (this.currentBufferedNode.Next != this.bufferedNodesHead)
				{
					this.currentBufferedNode = this.currentBufferedNode.Next;
					flag = true;
				}
				else if (this.parsingInStreamError)
				{
					flag = base.Read();
					BufferingJsonReader.BufferedNode bufferedNode = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value, base.RawValue);
					bufferedNode.Previous = this.bufferedNodesHead.Previous;
					bufferedNode.Next = this.bufferedNodesHead;
					this.bufferedNodesHead.Previous.Next = bufferedNode;
					this.bufferedNodesHead.Previous = bufferedNode;
					this.currentBufferedNode = bufferedNode;
				}
				else
				{
					flag = this.ReadNextAndCheckForInStreamError();
				}
			}
			else if (this.bufferedNodesHead == null)
			{
				flag = (this.parsingInStreamError ? base.Read() : this.ReadNextAndCheckForInStreamError());
			}
			else
			{
				flag = this.bufferedNodesHead.NodeType != JsonNodeType.EndOfInput;
				this.removeOnNextRead = true;
			}
			return flag;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000225FC File Offset: 0x000207FC
		protected virtual void ProcessObjectValue()
		{
			ODataError odataError = null;
			if (!this.DisableInStreamErrorDetection)
			{
				this.ReadInternal();
				bool flag = false;
				while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
				{
					string text = (string)this.currentBufferedNode.Value;
					if (string.CompareOrdinal(this.inStreamErrorPropertyName, text) != 0 || flag)
					{
						return;
					}
					flag = true;
					this.ReadInternal();
					if (!this.TryReadInStreamErrorPropertyValue(out odataError))
					{
						return;
					}
				}
				if (flag)
				{
					throw new ODataErrorException(odataError);
				}
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00022670 File Offset: 0x00020870
		private bool ReadNextAndCheckForInStreamError()
		{
			this.parsingInStreamError = true;
			bool flag3;
			try
			{
				bool flag = this.ReadInternal();
				if (base.NodeType == JsonNodeType.StartObject)
				{
					bool flag2 = this.isBuffering;
					BufferingJsonReader.BufferedNode bufferedNode = null;
					if (this.isBuffering)
					{
						bufferedNode = this.currentBufferedNode;
					}
					else
					{
						this.StartBuffering();
					}
					this.ProcessObjectValue();
					if (flag2)
					{
						this.currentBufferedNode = bufferedNode;
					}
					else
					{
						this.StopBuffering();
					}
				}
				flag3 = flag;
			}
			finally
			{
				this.parsingInStreamError = false;
			}
			return flag3;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000226EC File Offset: 0x000208EC
		private bool TryReadInStreamErrorPropertyValue(out ODataError error)
		{
			error = null;
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			this.ReadInternal();
			error = new ODataError();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "code"))
					{
						if (!(text2 == "message"))
						{
							if (!(text2 == "innererror"))
							{
								return false;
							}
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError))
							{
								return false;
							}
							ODataInnerError odataInnerError;
							if (!this.TryReadInnerErrorPropertyValue(out odataInnerError, 0))
							{
								return false;
							}
							error.InnerError = odataInnerError;
						}
						else
						{
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Message))
							{
								return false;
							}
							if (!this.TryReadMessagePropertyValue(error))
							{
								return false;
							}
						}
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Code))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						error.ErrorCode = text3;
					}
					this.ReadInternal();
					continue;
				}
				return false;
			}
			this.ReadInternal();
			return errorPropertyBitMask != ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000227F0 File Offset: 0x000209F0
		private bool TryReadMessagePropertyValue(ODataError error)
		{
			this.ReadInternal();
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			this.ReadInternal();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "lang"))
					{
						if (!(text2 == "value"))
						{
							return false;
						}
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						error.Message = text3;
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageLanguage))
						{
							return false;
						}
						string text4;
						if (!this.TryReadErrorStringPropertyValue(out text4))
						{
							return false;
						}
						error.MessageLanguage = text4;
					}
					this.ReadInternal();
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000228B8 File Offset: 0x00020AB8
		private bool TryReadInnerErrorPropertyValue(out ODataInnerError innerError, int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, this.maxInnerErrorDepth);
			this.ReadInternal();
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				innerError = null;
				return false;
			}
			this.ReadInternal();
			innerError = new ODataInnerError();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) == null)
				{
					goto IL_125;
				}
				if (!(text2 == "message"))
				{
					if (!(text2 == "type"))
					{
						if (!(text2 == "stacktrace"))
						{
							if (!(text2 == "internalexception"))
							{
								goto IL_125;
							}
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError))
							{
								return false;
							}
							ODataInnerError odataInnerError;
							if (!this.TryReadInnerErrorPropertyValue(out odataInnerError, recursionDepth))
							{
								return false;
							}
							innerError.InnerError = odataInnerError;
						}
						else
						{
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.StackTrace))
							{
								return false;
							}
							string text3;
							if (!this.TryReadErrorStringPropertyValue(out text3))
							{
								return false;
							}
							innerError.StackTrace = text3;
						}
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.TypeName))
						{
							return false;
						}
						string text4;
						if (!this.TryReadErrorStringPropertyValue(out text4))
						{
							return false;
						}
						innerError.TypeName = text4;
					}
				}
				else
				{
					if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue))
					{
						return false;
					}
					string text5;
					if (!this.TryReadErrorStringPropertyValue(out text5))
					{
						return false;
					}
					innerError.Message = text5;
				}
				IL_12B:
				this.ReadInternal();
				continue;
				IL_125:
				this.SkipValueInternal();
				goto IL_12B;
			}
			return true;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00022A0C File Offset: 0x00020C0C
		private bool TryReadErrorStringPropertyValue(out string stringValue)
		{
			this.ReadInternal();
			stringValue = this.currentBufferedNode.Value as string;
			return this.currentBufferedNode.NodeType == JsonNodeType.PrimitiveValue && (this.currentBufferedNode.Value == null || stringValue != null);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00022A5C File Offset: 0x00020C5C
		private void SkipValueInternal()
		{
			int num = 0;
			do
			{
				switch (this.currentBufferedNode.NodeType)
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
				this.ReadInternal();
			}
			while (num > 0);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00022AB0 File Offset: 0x00020CB0
		private void RemoveFirstNodeInBuffer()
		{
			if (this.bufferedNodesHead.Next == this.bufferedNodesHead)
			{
				this.bufferedNodesHead = null;
				return;
			}
			this.bufferedNodesHead.Previous.Next = this.bufferedNodesHead.Next;
			this.bufferedNodesHead.Next.Previous = this.bufferedNodesHead.Previous;
			this.bufferedNodesHead = this.bufferedNodesHead.Next;
		}

		// Token: 0x040003DF RID: 991
		protected BufferingJsonReader.BufferedNode bufferedNodesHead;

		// Token: 0x040003E0 RID: 992
		protected BufferingJsonReader.BufferedNode currentBufferedNode;

		// Token: 0x040003E1 RID: 993
		private readonly int maxInnerErrorDepth;

		// Token: 0x040003E2 RID: 994
		private readonly string inStreamErrorPropertyName;

		// Token: 0x040003E3 RID: 995
		private bool isBuffering;

		// Token: 0x040003E4 RID: 996
		private bool removeOnNextRead;

		// Token: 0x040003E5 RID: 997
		private bool parsingInStreamError;

		// Token: 0x040003E6 RID: 998
		private bool disableInStreamErrorDetection;

		// Token: 0x02000172 RID: 370
		protected internal sealed class BufferedNode
		{
			// Token: 0x06000A7F RID: 2687 RVA: 0x00022B1F File Offset: 0x00020D1F
			internal BufferedNode(JsonNodeType nodeType, object value, string rawValue)
			{
				this.nodeType = nodeType;
				this.nodeValue = value;
				this.nodeRawValue = rawValue;
				this.Previous = this;
				this.Next = this;
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00022B4A File Offset: 0x00020D4A
			internal JsonNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00022B52 File Offset: 0x00020D52
			internal object Value
			{
				get
				{
					return this.nodeValue;
				}
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00022B5A File Offset: 0x00020D5A
			internal string RawValue
			{
				get
				{
					return this.nodeRawValue;
				}
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00022B62 File Offset: 0x00020D62
			// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00022B6A File Offset: 0x00020D6A
			internal BufferingJsonReader.BufferedNode Previous { get; set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00022B73 File Offset: 0x00020D73
			// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00022B7B File Offset: 0x00020D7B
			internal BufferingJsonReader.BufferedNode Next { get; set; }

			// Token: 0x040003E7 RID: 999
			private readonly JsonNodeType nodeType;

			// Token: 0x040003E8 RID: 1000
			private readonly object nodeValue;

			// Token: 0x040003E9 RID: 1001
			private readonly string nodeRawValue;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000223 RID: 547
	internal sealed class BufferingXmlReader : XmlReader
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		internal BufferingXmlReader(XmlReader reader, Uri parentXmlBaseUri, Uri documentBaseUri, bool disableXmlBase, int maxInnerErrorDepth, string odataNamespace)
		{
			this.reader = reader;
			this.documentBaseUri = documentBaseUri;
			this.disableXmlBase = disableXmlBase;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			XmlNameTable nameTable = this.reader.NameTable;
			this.XmlNamespace = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.XmlBaseAttributeName = nameTable.Add("base");
			this.XmlLangAttributeName = nameTable.Add("lang");
			this.ODataMetadataNamespace = nameTable.Add("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			this.ODataNamespace = nameTable.Add(odataNamespace);
			this.ODataErrorElementName = nameTable.Add("error");
			this.bufferedNodes = new LinkedList<BufferingXmlReader.BufferedNode>();
			this.currentBufferedNode = null;
			this.endOfInputBufferedNode = BufferingXmlReader.BufferedNode.CreateEndOfInput(this.reader.NameTable);
			this.xmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
			if (parentXmlBaseUri != null)
			{
				this.xmlBaseStack.Push(new BufferingXmlReader.XmlBaseDefinition(parentXmlBaseUri, 0));
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003FDC2 File Offset: 0x0003DFC2
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.NodeType;
				}
				return this.currentBufferedNodeToReport.Value.NodeType;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		public override bool IsEmptyElement
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.IsEmptyElement;
				}
				return this.currentBufferedNodeToReport.Value.IsEmptyElement;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003FE0E File Offset: 0x0003E00E
		public override string LocalName
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.LocalName;
				}
				return this.currentBufferedNodeToReport.Value.LocalName;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0003FE34 File Offset: 0x0003E034
		public override string Prefix
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Prefix;
				}
				return this.currentBufferedNodeToReport.Value.Prefix;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0003FE5A File Offset: 0x0003E05A
		public override string NamespaceURI
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.NamespaceURI;
				}
				return this.currentBufferedNodeToReport.Value.NamespaceUri;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0003FE80 File Offset: 0x0003E080
		public override string Value
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Value;
				}
				return this.currentBufferedNodeToReport.Value.Value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0003FEA6 File Offset: 0x0003E0A6
		public override int Depth
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Depth;
				}
				return this.currentBufferedNodeToReport.Value.Depth;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0003FECC File Offset: 0x0003E0CC
		public override bool EOF
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.EOF;
				}
				return this.IsEndOfInputNode(this.currentBufferedNodeToReport.Value);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0003FEF3 File Offset: 0x0003E0F3
		public override ReadState ReadState
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.ReadState;
				}
				if (this.IsEndOfInputNode(this.currentBufferedNodeToReport.Value))
				{
					return ReadState.EndOfFile;
				}
				if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.None)
				{
					return ReadState.Interactive;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0003FF33 File Offset: 0x0003E133
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0003FF40 File Offset: 0x0003E140
		public override int AttributeCount
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.AttributeCount;
				}
				if (this.currentBufferedNodeToReport.Value.AttributeNodes == null)
				{
					return 0;
				}
				return this.currentBufferedNodeToReport.Value.AttributeNodes.Count;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0003FF7F File Offset: 0x0003E17F
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0003FF84 File Offset: 0x0003E184
		public override bool HasValue
		{
			get
			{
				if (this.currentBufferedNodeToReport != null)
				{
					switch (this.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
					case XmlNodeType.XmlDeclaration:
						return true;
					}
					return false;
				}
				return this.reader.HasValue;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0003FFF9 File Offset: 0x0003E1F9
		internal Uri XmlBaseUri
		{
			get
			{
				if (this.xmlBaseStack.Count <= 0)
				{
					return null;
				}
				return this.xmlBaseStack.Peek().BaseUri;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004001C File Offset: 0x0003E21C
		internal Uri ParentXmlBaseUri
		{
			get
			{
				if (this.xmlBaseStack.Count == 0)
				{
					return null;
				}
				BufferingXmlReader.XmlBaseDefinition xmlBaseDefinition = this.xmlBaseStack.Peek();
				if (xmlBaseDefinition.Depth == this.Depth)
				{
					if (this.xmlBaseStack.Count == 1)
					{
						return null;
					}
					xmlBaseDefinition = this.xmlBaseStack.Skip(1).First<BufferingXmlReader.XmlBaseDefinition>();
				}
				return xmlBaseDefinition.BaseUri;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004007A File Offset: 0x0003E27A
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x00040082 File Offset: 0x0003E282
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

		// Token: 0x0600110E RID: 4366 RVA: 0x0004008C File Offset: 0x0003E28C
		public override bool Read()
		{
			if (!this.disableXmlBase && this.xmlBaseStack.Count > 0)
			{
				XmlNodeType xmlNodeType = this.NodeType;
				if (xmlNodeType == XmlNodeType.Attribute)
				{
					this.MoveToElement();
					xmlNodeType = XmlNodeType.Element;
				}
				if (this.xmlBaseStack.Peek().Depth == this.Depth && (xmlNodeType == XmlNodeType.EndElement || (xmlNodeType == XmlNodeType.Element && this.IsEmptyElement)))
				{
					this.xmlBaseStack.Pop();
				}
			}
			bool flag = this.ReadInternal(this.disableInStreamErrorDetection);
			if (flag && !this.disableXmlBase && this.NodeType == XmlNodeType.Element)
			{
				string attributeWithAtomizedName = this.GetAttributeWithAtomizedName(this.XmlBaseAttributeName, this.XmlNamespace);
				if (attributeWithAtomizedName != null)
				{
					Uri uri = new Uri(attributeWithAtomizedName, UriKind.RelativeOrAbsolute);
					if (!uri.IsAbsoluteUri)
					{
						if (this.xmlBaseStack.Count == 0)
						{
							if (this.documentBaseUri == null)
							{
								throw new ODataException(Strings.ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(attributeWithAtomizedName));
							}
							uri = UriUtils.UriToAbsoluteUri(this.documentBaseUri, uri);
						}
						else
						{
							uri = UriUtils.UriToAbsoluteUri(this.xmlBaseStack.Peek().BaseUri, uri);
						}
					}
					this.xmlBaseStack.Push(new BufferingXmlReader.XmlBaseDefinition(uri, this.Depth));
				}
			}
			return flag;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000401B0 File Offset: 0x0003E3B0
		public override bool MoveToElement()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToElement();
			}
			this.MoveFromAttributeValueNode();
			if (this.isBuffering)
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == XmlNodeType.Attribute)
				{
					this.currentBufferedNodeToReport = this.currentBufferedNode;
					return true;
				}
				return false;
			}
			else
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == XmlNodeType.Attribute)
				{
					this.currentBufferedNodeToReport = this.bufferedNodes.First;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00040230 File Offset: 0x0003E430
		public override bool MoveToFirstAttribute()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToFirstAttribute();
			}
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = currentElementNode.AttributeNodes.First;
				return true;
			}
			return false;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00040290 File Offset: 0x0003E490
		public override bool MoveToNextAttribute()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToNextAttribute();
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.currentAttributeNode;
			if (linkedListNode == null)
			{
				linkedListNode = this.currentBufferedNodeToReport;
			}
			if (linkedListNode.Value.NodeType == XmlNodeType.Attribute)
			{
				if (linkedListNode.Next != null)
				{
					this.currentAttributeNode = null;
					this.currentBufferedNodeToReport = linkedListNode.Next;
					return true;
				}
				return false;
			}
			else
			{
				if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				if (this.currentBufferedNodeToReport.Value.AttributeNodes.Count > 0)
				{
					this.currentBufferedNodeToReport = this.currentBufferedNodeToReport.Value.AttributeNodes.First;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00040344 File Offset: 0x0003E544
		public override bool ReadAttributeValue()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.currentAttributeNode != null)
			{
				return false;
			}
			BufferingXmlReader.BufferedNode bufferedNode = new BufferingXmlReader.BufferedNode(this.currentBufferedNodeToReport.Value.Value, this.currentBufferedNodeToReport.Value.Depth, this.NameTable);
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = new LinkedListNode<BufferingXmlReader.BufferedNode>(bufferedNode);
			this.currentAttributeNode = this.currentBufferedNodeToReport;
			this.currentBufferedNodeToReport = linkedListNode;
			return true;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000403D1 File Offset: 0x0003E5D1
		public override void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000403D8 File Offset: 0x0003E5D8
		public override string GetAttribute(int i)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(i);
			}
			if (i < 0 || i >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(i);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00040430 File Offset: 0x0003E630
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(name, namespaceURI);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name, namespaceURI);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00040474 File Offset: 0x0003E674
		public override string GetAttribute(string name)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(name);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000404B4 File Offset: 0x0003E6B4
		public override string LookupNamespace(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000404BC File Offset: 0x0003E6BC
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToAttribute(name, ns);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name, ns);
			if (linkedListNode != null)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = linkedListNode;
				return true;
			}
			return false;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00040504 File Offset: 0x0003E704
		public override bool MoveToAttribute(string name)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToAttribute(name);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name);
			if (linkedListNode != null)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = linkedListNode;
				return true;
			}
			return false;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00040548 File Offset: 0x0003E748
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Strings.ODataException_GeneralError);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00040554 File Offset: 0x0003E754
		internal void StartBuffering()
		{
			if (this.bufferedNodes.Count == 0)
			{
				this.bufferedNodes.AddFirst(this.BufferCurrentReaderNode());
			}
			else
			{
				this.removeOnNextRead = false;
			}
			this.currentBufferedNode = this.bufferedNodes.First;
			this.currentBufferedNodeToReport = this.currentBufferedNode;
			int count = this.xmlBaseStack.Count;
			switch (count)
			{
			case 0:
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
				break;
			case 1:
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
				this.bufferStartXmlBaseStack.Push(this.xmlBaseStack.Peek());
				break;
			default:
			{
				BufferingXmlReader.XmlBaseDefinition[] array = this.xmlBaseStack.ToArray();
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>(count);
				for (int i = count - 1; i >= 0; i--)
				{
					this.bufferStartXmlBaseStack.Push(array[i]);
				}
				break;
			}
			}
			this.isBuffering = true;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00040630 File Offset: 0x0003E830
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
			if (this.bufferedNodes.Count > 0)
			{
				this.currentBufferedNodeToReport = this.bufferedNodes.First;
			}
			this.xmlBaseStack = this.bufferStartXmlBaseStack;
			this.bufferStartXmlBaseStack = null;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00040684 File Offset: 0x0003E884
		private bool ReadInternal(bool ignoreInStreamErrors)
		{
			if (this.removeOnNextRead)
			{
				this.currentBufferedNodeToReport = this.currentBufferedNodeToReport.Next;
				this.bufferedNodes.RemoveFirst();
				this.removeOnNextRead = false;
			}
			bool flag;
			if (this.isBuffering)
			{
				this.MoveFromAttributeValueNode();
				if (this.currentBufferedNode.Next != null)
				{
					this.currentBufferedNode = this.currentBufferedNode.Next;
					this.currentBufferedNodeToReport = this.currentBufferedNode;
					flag = true;
				}
				else if (ignoreInStreamErrors)
				{
					flag = this.reader.Read();
					this.bufferedNodes.AddLast(this.BufferCurrentReaderNode());
					this.currentBufferedNode = this.bufferedNodes.Last;
					this.currentBufferedNodeToReport = this.currentBufferedNode;
				}
				else
				{
					flag = this.ReadNextAndCheckForInStreamError();
				}
			}
			else if (this.bufferedNodes.Count == 0)
			{
				flag = (ignoreInStreamErrors ? this.reader.Read() : this.ReadNextAndCheckForInStreamError());
			}
			else
			{
				this.currentBufferedNodeToReport = this.bufferedNodes.First;
				BufferingXmlReader.BufferedNode value = this.currentBufferedNodeToReport.Value;
				flag = !this.IsEndOfInputNode(value);
				this.removeOnNextRead = true;
			}
			return flag;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000407A0 File Offset: 0x0003E9A0
		private bool ReadNextAndCheckForInStreamError()
		{
			bool flag = this.ReadInternal(true);
			if (!this.disableInStreamErrorDetection && this.NodeType == XmlNodeType.Element && this.LocalNameEquals(this.ODataErrorElementName) && this.NamespaceEquals(this.ODataMetadataNamespace))
			{
				ODataError odataError = ODataAtomErrorDeserializer.ReadErrorElement(this, this.maxInnerErrorDepth);
				throw new ODataErrorException(odataError);
			}
			return flag;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000407F7 File Offset: 0x0003E9F7
		private bool IsEndOfInputNode(BufferingXmlReader.BufferedNode node)
		{
			return object.ReferenceEquals(node, this.endOfInputBufferedNode);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00040808 File Offset: 0x0003EA08
		private BufferingXmlReader.BufferedNode BufferCurrentReaderNode()
		{
			if (this.reader.EOF)
			{
				return this.endOfInputBufferedNode;
			}
			BufferingXmlReader.BufferedNode bufferedNode = new BufferingXmlReader.BufferedNode(this.reader);
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				while (this.reader.MoveToNextAttribute())
				{
					bufferedNode.AttributeNodes.AddLast(new BufferingXmlReader.BufferedNode(this.reader));
				}
				this.reader.MoveToElement();
			}
			return bufferedNode;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00040876 File Offset: 0x0003EA76
		private BufferingXmlReader.BufferedNode GetCurrentElementNode()
		{
			if (this.isBuffering)
			{
				return this.currentBufferedNode.Value;
			}
			return this.bufferedNodes.First.Value;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0004089C File Offset: 0x0003EA9C
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(int index)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First;
				int num = 0;
				while (linkedListNode != null)
				{
					if (num == index)
					{
						return linkedListNode;
					}
					num++;
					linkedListNode = linkedListNode.Next;
				}
			}
			return null;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000408EC File Offset: 0x0003EAEC
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string localName, string namespaceUri)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					if (string.CompareOrdinal(value.NamespaceUri, namespaceUri) == 0 && string.CompareOrdinal(value.LocalName, localName) == 0)
					{
						return linkedListNode;
					}
				}
			}
			return null;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00040958 File Offset: 0x0003EB58
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string qualifiedName)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					bool flag = !string.IsNullOrEmpty(value.Prefix);
					if ((!flag && string.CompareOrdinal(value.LocalName, qualifiedName) == 0) || (flag && string.CompareOrdinal(value.Prefix + ":" + value.LocalName, qualifiedName) == 0))
					{
						return linkedListNode;
					}
				}
			}
			return null;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000409E6 File Offset: 0x0003EBE6
		private void MoveFromAttributeValueNode()
		{
			if (this.currentAttributeNode != null)
			{
				this.currentBufferedNodeToReport = this.currentAttributeNode;
				this.currentAttributeNode = null;
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00040A04 File Offset: 0x0003EC04
		private string GetAttributeWithAtomizedName(string name, string namespaceURI)
		{
			if (this.bufferedNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.GetCurrentElementNode().AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					if (object.ReferenceEquals(namespaceURI, value.NamespaceUri) && object.ReferenceEquals(name, value.LocalName))
					{
						return linkedListNode.Value.Value;
					}
				}
				return null;
			}
			string text = null;
			while (this.reader.MoveToNextAttribute())
			{
				if (object.ReferenceEquals(name, this.reader.LocalName) && object.ReferenceEquals(namespaceURI, this.reader.NamespaceURI))
				{
					text = this.reader.Value;
					break;
				}
			}
			this.reader.MoveToElement();
			return text;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00040ABF File Offset: 0x0003ECBF
		[Conditional("DEBUG")]
		private void ValidateInternalState()
		{
		}

		// Token: 0x0400063D RID: 1597
		internal readonly string XmlNamespace;

		// Token: 0x0400063E RID: 1598
		internal readonly string XmlBaseAttributeName;

		// Token: 0x0400063F RID: 1599
		internal readonly string XmlLangAttributeName;

		// Token: 0x04000640 RID: 1600
		internal readonly string ODataMetadataNamespace;

		// Token: 0x04000641 RID: 1601
		internal readonly string ODataNamespace;

		// Token: 0x04000642 RID: 1602
		internal readonly string ODataErrorElementName;

		// Token: 0x04000643 RID: 1603
		private readonly XmlReader reader;

		// Token: 0x04000644 RID: 1604
		private readonly LinkedList<BufferingXmlReader.BufferedNode> bufferedNodes;

		// Token: 0x04000645 RID: 1605
		private readonly BufferingXmlReader.BufferedNode endOfInputBufferedNode;

		// Token: 0x04000646 RID: 1606
		private readonly bool disableXmlBase;

		// Token: 0x04000647 RID: 1607
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000648 RID: 1608
		private readonly Uri documentBaseUri;

		// Token: 0x04000649 RID: 1609
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNode;

		// Token: 0x0400064A RID: 1610
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentAttributeNode;

		// Token: 0x0400064B RID: 1611
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNodeToReport;

		// Token: 0x0400064C RID: 1612
		private bool isBuffering;

		// Token: 0x0400064D RID: 1613
		private bool removeOnNextRead;

		// Token: 0x0400064E RID: 1614
		private bool disableInStreamErrorDetection;

		// Token: 0x0400064F RID: 1615
		private Stack<BufferingXmlReader.XmlBaseDefinition> xmlBaseStack;

		// Token: 0x04000650 RID: 1616
		private Stack<BufferingXmlReader.XmlBaseDefinition> bufferStartXmlBaseStack;

		// Token: 0x02000224 RID: 548
		private sealed class BufferedNode
		{
			// Token: 0x06001128 RID: 4392 RVA: 0x00040AC4 File Offset: 0x0003ECC4
			internal BufferedNode(XmlReader reader)
			{
				this.NodeType = reader.NodeType;
				this.NamespaceUri = reader.NamespaceURI;
				this.LocalName = reader.LocalName;
				this.Prefix = reader.Prefix;
				this.Value = reader.Value;
				this.Depth = reader.Depth;
				this.IsEmptyElement = reader.IsEmptyElement;
			}

			// Token: 0x06001129 RID: 4393 RVA: 0x00040B2C File Offset: 0x0003ED2C
			internal BufferedNode(string value, int depth, XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				this.NodeType = XmlNodeType.Text;
				this.NamespaceUri = text;
				this.LocalName = text;
				this.Prefix = text;
				this.Value = value;
				this.Depth = depth + 1;
				this.IsEmptyElement = false;
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x00040B7E File Offset: 0x0003ED7E
			private BufferedNode(string emptyString)
			{
				this.NodeType = XmlNodeType.None;
				this.NamespaceUri = emptyString;
				this.LocalName = emptyString;
				this.Prefix = emptyString;
				this.Value = emptyString;
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x0600112B RID: 4395 RVA: 0x00040BA9 File Offset: 0x0003EDA9
			// (set) Token: 0x0600112C RID: 4396 RVA: 0x00040BB1 File Offset: 0x0003EDB1
			internal XmlNodeType NodeType { get; private set; }

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x0600112D RID: 4397 RVA: 0x00040BBA File Offset: 0x0003EDBA
			// (set) Token: 0x0600112E RID: 4398 RVA: 0x00040BC2 File Offset: 0x0003EDC2
			internal string NamespaceUri { get; private set; }

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x0600112F RID: 4399 RVA: 0x00040BCB File Offset: 0x0003EDCB
			// (set) Token: 0x06001130 RID: 4400 RVA: 0x00040BD3 File Offset: 0x0003EDD3
			internal string LocalName { get; private set; }

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06001131 RID: 4401 RVA: 0x00040BDC File Offset: 0x0003EDDC
			// (set) Token: 0x06001132 RID: 4402 RVA: 0x00040BE4 File Offset: 0x0003EDE4
			internal string Prefix { get; private set; }

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06001133 RID: 4403 RVA: 0x00040BED File Offset: 0x0003EDED
			// (set) Token: 0x06001134 RID: 4404 RVA: 0x00040BF5 File Offset: 0x0003EDF5
			internal string Value { get; private set; }

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06001135 RID: 4405 RVA: 0x00040BFE File Offset: 0x0003EDFE
			// (set) Token: 0x06001136 RID: 4406 RVA: 0x00040C06 File Offset: 0x0003EE06
			internal int Depth { get; private set; }

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06001137 RID: 4407 RVA: 0x00040C0F File Offset: 0x0003EE0F
			// (set) Token: 0x06001138 RID: 4408 RVA: 0x00040C17 File Offset: 0x0003EE17
			internal bool IsEmptyElement { get; private set; }

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06001139 RID: 4409 RVA: 0x00040C20 File Offset: 0x0003EE20
			internal LinkedList<BufferingXmlReader.BufferedNode> AttributeNodes
			{
				get
				{
					if (this.NodeType == XmlNodeType.Element && this.attributeNodes == null)
					{
						this.attributeNodes = new LinkedList<BufferingXmlReader.BufferedNode>();
					}
					return this.attributeNodes;
				}
			}

			// Token: 0x0600113A RID: 4410 RVA: 0x00040C44 File Offset: 0x0003EE44
			internal static BufferingXmlReader.BufferedNode CreateEndOfInput(XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				return new BufferingXmlReader.BufferedNode(text);
			}

			// Token: 0x04000651 RID: 1617
			private LinkedList<BufferingXmlReader.BufferedNode> attributeNodes;
		}

		// Token: 0x02000225 RID: 549
		private sealed class XmlBaseDefinition
		{
			// Token: 0x0600113B RID: 4411 RVA: 0x00040C63 File Offset: 0x0003EE63
			internal XmlBaseDefinition(Uri baseUri, int depth)
			{
				this.BaseUri = baseUri;
				this.Depth = depth;
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x0600113C RID: 4412 RVA: 0x00040C79 File Offset: 0x0003EE79
			// (set) Token: 0x0600113D RID: 4413 RVA: 0x00040C81 File Offset: 0x0003EE81
			internal Uri BaseUri { get; private set; }

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x0600113E RID: 4414 RVA: 0x00040C8A File Offset: 0x0003EE8A
			// (set) Token: 0x0600113F RID: 4415 RVA: 0x00040C92 File Offset: 0x0003EE92
			internal int Depth { get; private set; }
		}
	}
}

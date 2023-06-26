using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	/// <summary>Represents the XML object model for encoding security objects. This class cannot be inherited.</summary>
	// Token: 0x020001BD RID: 445
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement : ISecurityElementFactory
	{
		// Token: 0x06001BCF RID: 7119 RVA: 0x0005F884 File Offset: 0x0005DA84
		internal SecurityElement()
		{
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0005F88C File Offset: 0x0005DA8C
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0005F88F File Offset: 0x0005DA8F
		string ISecurityElementFactory.GetTag()
		{
			return this.Tag;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0005F897 File Offset: 0x0005DA97
		object ISecurityElementFactory.Copy()
		{
			return this.Copy();
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0005F89F File Offset: 0x0005DA9F
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.Attribute(attributeName);
		}

		/// <summary>Creates a security element from an XML-encoded string.</summary>
		/// <param name="xml">The XML-encoded string from which to create the security element.</param>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> created from the XML.</returns>
		/// <exception cref="T:System.Security.XmlSyntaxException">
		///   <paramref name="xml" /> contains one or more single quotation mark characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is <see langword="null" />.</exception>
		// Token: 0x06001BD4 RID: 7124 RVA: 0x0005F8A8 File Offset: 0x0005DAA8
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			return new Parser(xml).GetTopElement();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag.</summary>
		/// <param name="tag">The tag name of an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter is invalid in XML.</exception>
		// Token: 0x06001BD5 RID: 7125 RVA: 0x0005F8C4 File Offset: 0x0005DAC4
		public SecurityElement(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), tag));
			}
			this.m_strTag = tag;
			this.m_strText = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag and text.</summary>
		/// <param name="tag">The tag name of the XML element.</param>
		/// <param name="text">The text content within the element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter or <paramref name="text" /> parameter is invalid in XML.</exception>
		// Token: 0x06001BD6 RID: 7126 RVA: 0x0005F918 File Offset: 0x0005DB18
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), tag));
			}
			if (text != null && !SecurityElement.IsValidText(text))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementText"), text));
			}
			this.m_strTag = tag;
			this.m_strText = text;
		}

		/// <summary>Gets or sets the tag name of an XML element.</summary>
		/// <returns>The tag name of an XML element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The tag is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The tag is not valid in XML.</exception>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0005F990 File Offset: 0x0005DB90
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x0005F998 File Offset: 0x0005DB98
		public string Tag
		{
			get
			{
				return this.m_strTag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), value));
				}
				this.m_strTag = value;
			}
		}

		/// <summary>Gets or sets the attributes of an XML element as name/value pairs.</summary>
		/// <returns>The <see cref="T:System.Collections.Hashtable" /> object for the attribute values of the XML element.</returns>
		/// <exception cref="T:System.InvalidCastException">The name or value of the <see cref="T:System.Collections.Hashtable" /> object is invalid.</exception>
		/// <exception cref="T:System.ArgumentException">The name is not a valid XML attribute name.</exception>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0005F9D4 File Offset: 0x0005DBD4
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x0005FA44 File Offset: 0x0005DC44
		public Hashtable Attributes
		{
			get
			{
				if (this.m_lAttributes == null || this.m_lAttributes.Count == 0)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.m_lAttributes.Count / 2);
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					hashtable.Add(this.m_lAttributes[i], this.m_lAttributes[i + 1]);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.m_lAttributes = null;
					return;
				}
				ArrayList arrayList = new ArrayList(value.Count);
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = (string)enumerator.Value;
					if (!SecurityElement.IsValidAttributeName(text))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (string)enumerator.Current));
					}
					if (!SecurityElement.IsValidAttributeValue(text2))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (string)enumerator.Value));
					}
					arrayList.Add(text);
					arrayList.Add(text2);
				}
				this.m_lAttributes = arrayList;
			}
		}

		/// <summary>Gets or sets the text within an XML element.</summary>
		/// <returns>The value of the text within an XML element.</returns>
		/// <exception cref="T:System.ArgumentException">The text is not valid in XML.</exception>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0005FB10 File Offset: 0x0005DD10
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0005FB1D File Offset: 0x0005DD1D
		public string Text
		{
			get
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			set
			{
				if (value == null)
				{
					this.m_strText = null;
					return;
				}
				if (!SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), value));
				}
				this.m_strText = value;
			}
		}

		/// <summary>Gets or sets the array of child elements of the XML element.</summary>
		/// <returns>The ordered child elements of the XML element as security elements.</returns>
		/// <exception cref="T:System.ArgumentException">A child of the XML parent node is <see langword="null" />.</exception>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0005FB54 File Offset: 0x0005DD54
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0005FB64 File Offset: 0x0005DD64
		public ArrayList Children
		{
			get
			{
				this.ConvertSecurityElementFactories();
				return this.m_lChildren;
			}
			set
			{
				if (value != null)
				{
					IEnumerator enumerator = value.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Child"));
						}
					}
				}
				this.m_lChildren = value;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		internal void ConvertSecurityElementFactories()
		{
			if (this.m_lChildren == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				ISecurityElementFactory securityElementFactory = this.m_lChildren[i] as ISecurityElementFactory;
				if (securityElementFactory != null && !(this.m_lChildren[i] is SecurityElement))
				{
					this.m_lChildren[i] = securityElementFactory.CreateSecurityElement();
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0005FC0A File Offset: 0x0005DE0A
		internal ArrayList InternalChildren
		{
			get
			{
				return this.m_lChildren;
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0005FC14 File Offset: 0x0005DE14
		internal void AddAttributeSafe(string name, string value)
		{
			if (this.m_lAttributes == null)
			{
				this.m_lAttributes = new ArrayList(8);
			}
			else
			{
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string text = (string)this.m_lAttributes[i];
					if (string.Equals(text, name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_AttributeNamesMustBeUnique"));
					}
				}
			}
			this.m_lAttributes.Add(name);
			this.m_lAttributes.Add(value);
		}

		/// <summary>Adds a name/value attribute to an XML element.</summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is invalid in XML.  
		///  -or-  
		///  An attribute with the name specified by the <paramref name="name" /> parameter already exists.</exception>
		// Token: 0x06001BE2 RID: 7138 RVA: 0x0005FC94 File Offset: 0x0005DE94
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!SecurityElement.IsValidAttributeName(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), name));
			}
			if (!SecurityElement.IsValidAttributeValue(value))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), value));
			}
			this.AddAttributeSafe(name, value);
		}

		/// <summary>Adds a child element to the XML element.</summary>
		/// <param name="child">The child element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0005FD0B File Offset: 0x0005DF0B
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0005FD3C File Offset: 0x0005DF3C
		internal void AddChild(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0005FD70 File Offset: 0x0005DF70
		internal void AddChildNoDuplicates(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
				this.m_lChildren.Add(child);
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				if (this.m_lChildren[i] == child)
				{
					return;
				}
			}
			this.m_lChildren.Add(child);
		}

		/// <summary>Compares two XML element objects for equality.</summary>
		/// <param name="other">An XML element object to which to compare the current XML element object.</param>
		/// <returns>
		///   <see langword="true" /> if the tag, attribute names and values, child elements, and text fields in the current XML element are identical to their counterparts in the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x0005FDE0 File Offset: 0x0005DFE0
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this.m_strTag, other.m_strTag))
			{
				return false;
			}
			if (!string.Equals(this.m_strText, other.m_strText))
			{
				return false;
			}
			if (this.m_lAttributes == null || other.m_lAttributes == null)
			{
				if (this.m_lAttributes != other.m_lAttributes)
				{
					return false;
				}
			}
			else
			{
				int count = this.m_lAttributes.Count;
				if (count != other.m_lAttributes.Count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					string text = (string)this.m_lAttributes[i];
					string text2 = (string)other.m_lAttributes[i];
					if (!string.Equals(text, text2))
					{
						return false;
					}
				}
			}
			if (this.m_lChildren == null || other.m_lChildren == null)
			{
				if (this.m_lChildren != other.m_lChildren)
				{
					return false;
				}
			}
			else
			{
				if (this.m_lChildren.Count != other.m_lChildren.Count)
				{
					return false;
				}
				this.ConvertSecurityElementFactories();
				other.ConvertSecurityElementFactories();
				IEnumerator enumerator = this.m_lChildren.GetEnumerator();
				IEnumerator enumerator2 = other.m_lChildren.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator2.MoveNext();
					SecurityElement securityElement = (SecurityElement)enumerator.Current;
					SecurityElement securityElement2 = (SecurityElement)enumerator2.Current;
					if (securityElement == null || !securityElement.Equal(securityElement2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates and returns an identical copy of the current <see cref="T:System.Security.SecurityElement" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Security.SecurityElement" /> object.</returns>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x0005FF38 File Offset: 0x0005E138
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this.m_strTag, this.m_strText)
			{
				m_lChildren = ((this.m_lChildren == null) ? null : new ArrayList(this.m_lChildren)),
				m_lAttributes = ((this.m_lAttributes == null) ? null : new ArrayList(this.m_lAttributes))
			};
		}

		/// <summary>Determines whether a string is a valid tag.</summary>
		/// <param name="tag">The tag to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="tag" /> parameter is a valid XML tag; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BE8 RID: 7144 RVA: 0x0005FF90 File Offset: 0x0005E190
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
		}

		/// <summary>Determines whether a string is valid as text within an XML element.</summary>
		/// <param name="text">The text to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="text" /> parameter is a valid XML text element; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x0005FFA5 File Offset: 0x0005E1A5
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
		}

		/// <summary>Determines whether a string is a valid attribute name.</summary>
		/// <param name="name">The attribute name to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="name" /> parameter is a valid XML attribute name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BEA RID: 7146 RVA: 0x0005FFBA File Offset: 0x0005E1BA
		public static bool IsValidAttributeName(string name)
		{
			return SecurityElement.IsValidTag(name);
		}

		/// <summary>Determines whether a string is a valid attribute value.</summary>
		/// <param name="value">The attribute value to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid XML attribute value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BEB RID: 7147 RVA: 0x0005FFC2 File Offset: 0x0005E1C2
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0005FFD8 File Offset: 0x0005E1D8
		private static string GetEscapeSequence(char c)
		{
			int num = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num; i += 2)
			{
				string text = SecurityElement.s_escapeStringPairs[i];
				string text2 = SecurityElement.s_escapeStringPairs[i + 1];
				if (text[0] == c)
				{
					return text2;
				}
			}
			return c.ToString();
		}

		/// <summary>Replaces invalid XML characters in a string with their valid XML equivalent.</summary>
		/// <param name="str">The string within which to escape invalid characters.</param>
		/// <returns>The input string with invalid characters replaced.</returns>
		// Token: 0x06001BED RID: 7149 RVA: 0x00060020 File Offset: 0x0005E220
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOfAny(SecurityElement.s_escapeChars, num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetEscapeSequence(str[num2]));
				num = num2 + 1;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00060094 File Offset: 0x0005E294
		private static string GetUnescapeSequence(string str, int index, out int newIndex)
		{
			int num = str.Length - index;
			int num2 = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num2; i += 2)
			{
				string text = SecurityElement.s_escapeStringPairs[i];
				string text2 = SecurityElement.s_escapeStringPairs[i + 1];
				int length = text2.Length;
				if (length <= num && string.Compare(text2, 0, str, index, length, StringComparison.Ordinal) == 0)
				{
					newIndex = index + text2.Length;
					return text;
				}
			}
			newIndex = index + 1;
			return str[index].ToString();
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00060110 File Offset: 0x0005E310
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOf('&', num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, num2, out num));
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00060179 File Offset: 0x0005E379
		private static void ToStringHelperStringBuilder(object obj, string str)
		{
			((StringBuilder)obj).Append(str);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00060188 File Offset: 0x0005E388
		private static void ToStringHelperStreamWriter(object obj, string str)
		{
			((StreamWriter)obj).Write(str);
		}

		/// <summary>Produces a string representation of an XML element and its constituent attributes, child elements, and text.</summary>
		/// <returns>The XML element and its contents.</returns>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x00060198 File Offset: 0x0005E398
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString("", stringBuilder, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStringBuilder));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000601C9 File Offset: 0x0005E3C9
		internal void ToWriter(StreamWriter writer)
		{
			this.ToString("", writer, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStreamWriter));
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000601E4 File Offset: 0x0005E3E4
		private void ToString(string indent, object obj, SecurityElement.ToStringHelperFunc func)
		{
			func(obj, "<");
			SecurityElementType type = this.m_type;
			if (type != SecurityElementType.Format)
			{
				if (type == SecurityElementType.Comment)
				{
					func(obj, "!");
				}
			}
			else
			{
				func(obj, "?");
			}
			func(obj, this.m_strTag);
			if (this.m_lAttributes != null && this.m_lAttributes.Count > 0)
			{
				func(obj, " ");
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string text = (string)this.m_lAttributes[i];
					string text2 = (string)this.m_lAttributes[i + 1];
					func(obj, text);
					func(obj, "=\"");
					func(obj, text2);
					func(obj, "\"");
					if (i != this.m_lAttributes.Count - 2)
					{
						if (this.m_type == SecurityElementType.Regular)
						{
							func(obj, Environment.NewLine);
						}
						else
						{
							func(obj, " ");
						}
					}
				}
			}
			if (this.m_strText == null && (this.m_lChildren == null || this.m_lChildren.Count == 0))
			{
				SecurityElementType type2 = this.m_type;
				if (type2 != SecurityElementType.Format)
				{
					if (type2 == SecurityElementType.Comment)
					{
						func(obj, ">");
					}
					else
					{
						func(obj, "/>");
					}
				}
				else
				{
					func(obj, " ?>");
				}
				func(obj, Environment.NewLine);
				return;
			}
			func(obj, ">");
			func(obj, this.m_strText);
			if (this.m_lChildren != null)
			{
				this.ConvertSecurityElementFactories();
				func(obj, Environment.NewLine);
				for (int j = 0; j < this.m_lChildren.Count; j++)
				{
					((SecurityElement)this.m_lChildren[j]).ToString("", obj, func);
				}
			}
			func(obj, "</");
			func(obj, this.m_strTag);
			func(obj, ">");
			func(obj, Environment.NewLine);
		}

		/// <summary>Finds an attribute by name in an XML element.</summary>
		/// <param name="name">The name of the attribute for which to search.</param>
		/// <returns>The value associated with the named attribute, or <see langword="null" /> if no attribute with <paramref name="name" /> exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00060400 File Offset: 0x0005E600
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.m_lAttributes == null)
			{
				return null;
			}
			int count = this.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string text = (string)this.m_lAttributes[i];
				if (string.Equals(text, name))
				{
					string text2 = (string)this.m_lAttributes[i + 1];
					return SecurityElement.Unescape(text2);
				}
			}
			return null;
		}

		/// <summary>Finds a child by its tag name.</summary>
		/// <param name="tag">The tag for which to search in child elements.</param>
		/// <returns>The first child XML element with the specified tag value, or <see langword="null" /> if no child element with <paramref name="tag" /> exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001BF6 RID: 7158 RVA: 0x00060474 File Offset: 0x0005E674
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement != null && string.Equals(securityElement.Tag, tag))
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000604D0 File Offset: 0x0005E6D0
		internal IPermission ToPermission(bool ignoreTypeLoadFailures)
		{
			IPermission permission = XMLUtil.CreatePermission(this, PermissionState.None, ignoreTypeLoadFailures);
			if (permission == null)
			{
				return null;
			}
			permission.FromXml(this);
			PermissionToken token = PermissionToken.GetToken(permission);
			return permission;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000604FC File Offset: 0x0005E6FC
		[SecurityCritical]
		internal object ToSecurityObject()
		{
			string strTag = this.m_strTag;
			if (strTag == "PermissionSet")
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.FromXml(this);
				return permissionSet;
			}
			return this.ToPermission(false);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00060534 File Offset: 0x0005E734
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.m_strTag == null)
			{
				return null;
			}
			if (this.m_strTag.Equals(strLocalName) || this.m_strTag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		/// <summary>Finds a child by its tag name and returns the contained text.</summary>
		/// <param name="tag">The tag for which to search in child elements.</param>
		/// <returns>The text contents of the first child element with the specified tag value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tag" /> is <see langword="null" />.</exception>
		// Token: 0x06001BFA RID: 7162 RVA: 0x000605C4 File Offset: 0x0005E7C4
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (string.Equals(this.m_strTag, tag))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			IEnumerator enumerator = this.m_lChildren.GetEnumerator();
			this.ConvertSecurityElementFactories();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = ((SecurityElement)obj).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x040009A9 RID: 2473
		internal string m_strTag;

		// Token: 0x040009AA RID: 2474
		internal string m_strText;

		// Token: 0x040009AB RID: 2475
		private ArrayList m_lChildren;

		// Token: 0x040009AC RID: 2476
		internal ArrayList m_lAttributes;

		// Token: 0x040009AD RID: 2477
		internal SecurityElementType m_type;

		// Token: 0x040009AE RID: 2478
		private static readonly char[] s_tagIllegalCharacters = new char[] { ' ', '<', '>' };

		// Token: 0x040009AF RID: 2479
		private static readonly char[] s_textIllegalCharacters = new char[] { '<', '>' };

		// Token: 0x040009B0 RID: 2480
		private static readonly char[] s_valueIllegalCharacters = new char[] { '<', '>', '"' };

		// Token: 0x040009B1 RID: 2481
		private const string s_strIndent = "   ";

		// Token: 0x040009B2 RID: 2482
		private const int c_AttributesTypical = 8;

		// Token: 0x040009B3 RID: 2483
		private const int c_ChildrenTypical = 1;

		// Token: 0x040009B4 RID: 2484
		private static readonly string[] s_escapeStringPairs = new string[] { "<", "&lt;", ">", "&gt;", "\"", "&quot;", "'", "&apos;", "&", "&amp;" };

		// Token: 0x040009B5 RID: 2485
		private static readonly char[] s_escapeChars = new char[] { '<', '>', '"', '\'', '&' };

		// Token: 0x02000B29 RID: 2857
		// (Invoke) Token: 0x06006B75 RID: 27509
		private delegate void ToStringHelperFunc(object obj, string str);
	}
}

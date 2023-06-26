using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Represents an area within a <see cref="T:System.Windows.Forms.LinkLabel" /> control that represents a hyperlink within the control.</summary>
	// Token: 0x020002BF RID: 703
	[TypeConverter(typeof(LinkArea.LinkAreaConverter))]
	[Serializable]
	public struct LinkArea
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkArea" /> class.</summary>
		/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
		/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
		// Token: 0x06002B2B RID: 11051 RVA: 0x000C2111 File Offset: 0x000C0311
		public LinkArea(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		/// <summary>Gets or sets the starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000C2121 File Offset: 0x000C0321
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000C2129 File Offset: 0x000C0329
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		/// <summary>Gets or sets the number of characters in the link area.</summary>
		/// <returns>The number of characters, including spaces, in the link area.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000C2132 File Offset: 0x000C0332
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x000C213A File Offset: 0x000C033A
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.LinkArea" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the specified start and length return an empty link area; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000C2143 File Offset: 0x000C0343
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsEmpty
		{
			get
			{
				return this.length == this.start && this.start == 0;
			}
		}

		/// <summary>Determines whether this <see cref="T:System.Windows.Forms.LinkArea" /> is equal to the specified object.</summary>
		/// <param name="o">The object to compare to this <see cref="T:System.Windows.Forms.LinkArea" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current <see cref="T:System.Windows.Forms.LinkArea" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B31 RID: 11057 RVA: 0x000C2160 File Offset: 0x000C0360
		public override bool Equals(object o)
		{
			if (!(o is LinkArea))
			{
				return false;
			}
			LinkArea linkArea = (LinkArea)o;
			return this == linkArea;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		// Token: 0x06002B32 RID: 11058 RVA: 0x000C218C File Offset: 0x000C038C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Start=",
				this.Start.ToString(CultureInfo.CurrentCulture),
				", Length=",
				this.Length.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal.</summary>
		/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B33 RID: 11059 RVA: 0x000C21E8 File Offset: 0x000C03E8
		public static bool operator ==(LinkArea linkArea1, LinkArea linkArea2)
		{
			return linkArea1.start == linkArea2.start && linkArea1.length == linkArea2.length;
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal.</summary>
		/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B34 RID: 11060 RVA: 0x000C2208 File Offset: 0x000C0408
		public static bool operator !=(LinkArea linkArea1, LinkArea linkArea2)
		{
			return !(linkArea1 == linkArea2);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06002B35 RID: 11061 RVA: 0x000C2214 File Offset: 0x000C0414
		public override int GetHashCode()
		{
			return (this.start << 4) | this.length;
		}

		// Token: 0x04001221 RID: 4641
		private int start;

		// Token: 0x04001222 RID: 4642
		private int length;

		/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.LinkArea.LinkAreaConverter" /> objects to and from various other representations.</summary>
		// Token: 0x020006B8 RID: 1720
		public class LinkAreaConverter : TypeConverter
		{
			/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null.</param>
			/// <param name="sourceType">The type you wish to convert from.</param>
			/// <returns>True if this object can perform the conversion.</returns>
			// Token: 0x060068BA RID: 26810 RVA: 0x000C223C File Offset: 0x000C043C
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
			/// <returns>
			///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x060068BB RID: 26811 RVA: 0x0002792C File Offset: 0x00025B2C
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			/// <summary>Converts the given object to the converter's native type.</summary>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null.</param>
			/// <param name="culture">An optional culture info. If not supplied, the current culture is assumed.</param>
			/// <param name="value">The object to convert.</param>
			/// <returns>The converted object. This will throw an exception if the conversion could not be performed.</returns>
			// Token: 0x060068BC RID: 26812 RVA: 0x0018510C File Offset: 0x0018330C
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (!(value is string))
				{
					return base.ConvertFrom(context, culture, value);
				}
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return null;
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				char c = culture.TextInfo.ListSeparator[0];
				string[] array = text.Split(new char[] { c });
				int[] array2 = new int[array.Length];
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (int)converter.ConvertFromString(context, culture, array[i]);
				}
				if (array2.Length == 2)
				{
					return new LinkArea(array2[0], array2[1]);
				}
				throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[] { text, "start, length" }));
			}

			/// <summary>Converts the given object to another type. The most common types to convert are to and from a string object. The default implementation will make a call to <see cref="M:System.Windows.Forms.LinkArea.ToString" /> on the object if the object is valid and if the destination type is string. If this cannot convert to the destination type, this will throw a <see cref="T:System.NotSupportedException" />.</summary>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null.</param>
			/// <param name="culture">An optional culture info. If not supplied the current culture is assumed.</param>
			/// <param name="value">The object to convert.</param>
			/// <param name="destinationType">The type to convert the object to.</param>
			/// <returns>The converted object.</returns>
			// Token: 0x060068BD RID: 26813 RVA: 0x001851EC File Offset: 0x001833EC
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (destinationType == typeof(string) && value is LinkArea)
				{
					LinkArea linkArea = (LinkArea)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string text = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, linkArea.Start);
					array[num++] = converter.ConvertToString(context, culture, linkArea.Length);
					return string.Join(text, array);
				}
				if (destinationType == typeof(InstanceDescriptor) && value is LinkArea)
				{
					LinkArea linkArea2 = (LinkArea)value;
					ConstructorInfo constructor = typeof(LinkArea).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[] { linkArea2.Start, linkArea2.Length });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			/// <summary>Creates an instance of this type, given a set of property values for the object. This is useful for objects that are immutable, but still want to provide changeable properties.</summary>
			/// <param name="context">A type descriptor through which additional context may be provided.</param>
			/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" />.</param>
			/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
			// Token: 0x060068BE RID: 26814 RVA: 0x00185347 File Offset: 0x00183547
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				return new LinkArea((int)propertyValues["Start"], (int)propertyValues["Length"]);
			}

			/// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
			/// <param name="context">A type descriptor through which additional context may be provided.</param>
			/// <returns>Returns <see langword="true" /> if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> should be called when a change is made to one or more properties of this object.</returns>
			// Token: 0x060068BF RID: 26815 RVA: 0x00012E4E File Offset: 0x0001104E
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			/// <summary>Retrieves the set of properties for this type.</summary>
			/// <param name="context">A type descriptor through which additional context may be provided.</param>
			/// <param name="value">The value of the object to get the properties for.</param>
			/// <param name="attributes">The attributes of the object to get the properties for.</param>
			/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this might return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
			// Token: 0x060068C0 RID: 26816 RVA: 0x00185374 File Offset: 0x00183574
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(LinkArea), attributes);
				return properties.Sort(new string[] { "Start", "Length" });
			}

			/// <summary>Determines if this object supports properties. By default, this is <see langword="false" />.</summary>
			/// <param name="context">A type descriptor through which additional context may be provided.</param>
			/// <returns>Returns <see langword="true" /> if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object.</returns>
			// Token: 0x060068C1 RID: 26817 RVA: 0x00012E4E File Offset: 0x0001104E
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}

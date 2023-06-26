using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x0200034A RID: 842
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class.</summary>
		// Token: 0x06001E26 RID: 7718 RVA: 0x0008D7AC File Offset: 0x0008B9AC
		public WebRequestModuleElement()
		{
			this.properties.Add(this.prefix);
			this.properties.Add(this.type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix.</param>
		// Token: 0x06001E27 RID: 7719 RVA: 0x0008D82A File Offset: 0x0008BA2A
		public WebRequestModuleElement(string prefix, string type)
			: this()
		{
			this.Prefix = prefix;
			base[this.type] = new WebRequestModuleElement.TypeAndName(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix.</param>
		// Token: 0x06001E28 RID: 7720 RVA: 0x0008D84B File Offset: 0x0008BA4B
		public WebRequestModuleElement(string prefix, Type type)
			: this()
		{
			this.Prefix = prefix;
			this.Type = type;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0008D861 File Offset: 0x0008BA61
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0008D869 File Offset: 0x0008BA69
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0008D87C File Offset: 0x0008BA7C
		[ConfigurationProperty("prefix", IsRequired = true, IsKey = true)]
		public string Prefix
		{
			get
			{
				return (string)base[this.prefix];
			}
			set
			{
				base[this.prefix] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0008D88C File Offset: 0x0008BA8C
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0008D8B6 File Offset: 0x0008BAB6
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(WebRequestModuleElement.TypeTypeConverter))]
		public Type Type
		{
			get
			{
				WebRequestModuleElement.TypeAndName typeAndName = (WebRequestModuleElement.TypeAndName)base[this.type];
				if (typeAndName != null)
				{
					return typeAndName.type;
				}
				return null;
			}
			set
			{
				base[this.type] = new WebRequestModuleElement.TypeAndName(value);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0008D8CA File Offset: 0x0008BACA
		internal string Key
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x04001CA3 RID: 7331
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CA4 RID: 7332
		private readonly ConfigurationProperty prefix = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001CA5 RID: 7333
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(WebRequestModuleElement.TypeAndName), null, new WebRequestModuleElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x020007C7 RID: 1991
		private class TypeAndName
		{
			// Token: 0x06004370 RID: 17264 RVA: 0x0011C36B File Offset: 0x0011A56B
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x06004371 RID: 17265 RVA: 0x0011C388 File Offset: 0x0011A588
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x06004372 RID: 17266 RVA: 0x0011C397 File Offset: 0x0011A597
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x06004373 RID: 17267 RVA: 0x0011C3A4 File Offset: 0x0011A5A4
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((WebRequestModuleElement.TypeAndName)comparand).type);
			}

			// Token: 0x0400345F RID: 13407
			public readonly Type type;

			// Token: 0x04003460 RID: 13408
			public readonly string name;
		}

		// Token: 0x020007C8 RID: 1992
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x06004374 RID: 17268 RVA: 0x0011C3BC File Offset: 0x0011A5BC
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004375 RID: 17269 RVA: 0x0011C3DA File Offset: 0x0011A5DA
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new WebRequestModuleElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x06004376 RID: 17270 RVA: 0x0011C3FC File Offset: 0x0011A5FC
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				WebRequestModuleElement.TypeAndName typeAndName = (WebRequestModuleElement.TypeAndName)value;
				if (typeAndName.name != null)
				{
					return typeAndName.name;
				}
				return typeAndName.type.AssemblyQualifiedName;
			}
		}
	}
}

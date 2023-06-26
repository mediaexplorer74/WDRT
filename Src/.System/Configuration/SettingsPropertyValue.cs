using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Configuration
{
	/// <summary>Contains the value of a settings property that can be loaded and stored by an instance of <see cref="T:System.Configuration.SettingsBase" />.</summary>
	// Token: 0x020000AD RID: 173
	public class SettingsPropertyValue
	{
		/// <summary>Gets the name of the property from the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00023319 File Offset: 0x00021519
		public string Name
		{
			get
			{
				return this._Property.Name;
			}
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00023326 File Offset: 0x00021526
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0002332E File Offset: 0x0002152E
		public bool IsDirty
		{
			get
			{
				return this._IsDirty;
			}
			set
			{
				this._IsDirty = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object that describes the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00023337 File Offset: 0x00021537
		public SettingsProperty Property
		{
			get
			{
				return this._Property;
			}
		}

		/// <summary>Gets a Boolean value specifying whether the value of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object is the default value as defined by the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property value on the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="T:System.Configuration.SettingsProperty" /> object is the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0002333F File Offset: 0x0002153F
		public bool UsingDefaultValue
		{
			get
			{
				return this._UsingDefaultValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValue" /> class, based on supplied parameters.</summary>
		/// <param name="property">Specifies a <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005EB RID: 1515 RVA: 0x00023347 File Offset: 0x00021547
		public SettingsPropertyValue(SettingsProperty property)
		{
			this._Property = property;
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.SettingsProperty" /> object. When this value is set, the <see cref="P:System.Configuration.SettingsPropertyValue.IsDirty" /> property is set to <see langword="true" /> and <see cref="P:System.Configuration.SettingsPropertyValue.UsingDefaultValue" /> is set to <see langword="false" />.  
		///  When a value is first accessed from the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property, and if the value was initially stored into the <see cref="T:System.Configuration.SettingsPropertyValue" /> object as a serialized representation using the <see cref="P:System.Configuration.SettingsPropertyValue.SerializedValue" /> property, the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property will trigger deserialization of the underlying value.  As a side effect, the <see cref="P:System.Configuration.SettingsPropertyValue.Deserialized" /> property will be set to <see langword="true" />.  
		///  If this chain of events occurs in ASP.NET, and if an error occurs during the deserialization process, the error is logged using the health-monitoring feature of ASP.NET. By default, this means that deserialization errors will show up in the Application Event Log when running under ASP.NET. If this process occurs outside of ASP.NET, and if an error occurs during deserialization, the error is suppressed, and the remainder of the logic during deserialization occurs. If there is no serialized value to deserialize when the deserialization is attempted, then <see cref="T:System.Configuration.SettingsPropertyValue" /> object will instead attempt to return a default value if one was configured as defined on the associated <see cref="T:System.Configuration.SettingsProperty" /> instance. In this case, if the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property was set to either <see langword="null" />, or to the string "[null]", then the <see cref="T:System.Configuration.SettingsPropertyValue" /> object will initialize the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property to either <see langword="null" /> for reference types, or to the default value for the associated value type.  On the other hand, if <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property holds a valid object reference or string value (other than "[null]"), then the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property is returned instead.  
		///  If there is no serialized value to deserialize when the deserialization is attempted, and no default value was specified, then an empty string will be returned for string types. For all other types, a default instance will be returned by calling <see cref="M:System.Activator.CreateInstance(System.Type)" /> - for reference types this means an attempt will be made to create an object instance using the default constructor.  If this attempt fails, then <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentException">While attempting to use the default value from the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property, an error occurred.  Either the attempt to convert <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property to a valid type failed, or the resulting value was not compatible with the type defined by <see cref="P:System.Configuration.SettingsProperty.PropertyType" />.</exception>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00023360 File Offset: 0x00021560
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x000233D7 File Offset: 0x000215D7
		public object PropertyValue
		{
			get
			{
				if (!this._Deserialized)
				{
					this._Value = this.Deserialize();
					this._Deserialized = true;
				}
				if (this._Value != null && !this.Property.PropertyType.IsPrimitive && !(this._Value is string) && !(this._Value is DateTime))
				{
					this._UsingDefaultValue = false;
					this._ChangedSinceLastSerialized = true;
					this._IsDirty = true;
				}
				return this._Value;
			}
			set
			{
				this._Value = value;
				this._IsDirty = true;
				this._ChangedSinceLastSerialized = true;
				this._Deserialized = true;
				this._UsingDefaultValue = false;
			}
		}

		/// <summary>Gets or sets the serialized value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The serialized value of a <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The serialization options for the property indicated the use of a string type converter, but a type converter was not available.</exception>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000233FC File Offset: 0x000215FC
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0002341F File Offset: 0x0002161F
		public object SerializedValue
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
			get
			{
				if (this._ChangedSinceLastSerialized)
				{
					this._ChangedSinceLastSerialized = false;
					this._SerializedValue = this.SerializePropertyValue();
				}
				return this._SerializedValue;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
			set
			{
				this._UsingDefaultValue = false;
				this._SerializedValue = value;
			}
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0002342F File Offset: 0x0002162F
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00023437 File Offset: 0x00021637
		public bool Deserialized
		{
			get
			{
				return this._Deserialized;
			}
			set
			{
				this._Deserialized = value;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00023440 File Offset: 0x00021640
		private bool IsHostedInAspnet()
		{
			return AppDomain.CurrentDomain.GetData(".appDomain") != null;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00023454 File Offset: 0x00021654
		private object Deserialize()
		{
			object obj = null;
			if (this.SerializedValue != null)
			{
				try
				{
					if (this.SerializedValue is string)
					{
						obj = SettingsPropertyValue.GetObjectFromString(this.Property.PropertyType, this.Property.SerializeAs, (string)this.SerializedValue);
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream((byte[])this.SerializedValue);
						try
						{
							obj = new BinaryFormatter().Deserialize(memoryStream);
						}
						finally
						{
							memoryStream.Close();
						}
					}
				}
				catch (Exception ex)
				{
					try
					{
						if (this.IsHostedInAspnet())
						{
							object[] array = new object[] { this.Property, this, ex };
							Type type = Type.GetType("System.Web.Management.WebBaseEvent, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
							type.InvokeMember("RaisePropertyDeserializationWebErrorEvent", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, array, CultureInfo.InvariantCulture);
						}
					}
					catch
					{
					}
				}
				if (obj != null && !this.Property.PropertyType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
			}
			if (obj == null)
			{
				this._UsingDefaultValue = true;
				if (this.Property.DefaultValue == null || this.Property.DefaultValue.ToString() == "[null]")
				{
					if (this.Property.PropertyType.IsValueType)
					{
						return SecurityUtils.SecureCreateInstance(this.Property.PropertyType);
					}
					return null;
				}
				else
				{
					if (!(this.Property.DefaultValue is string))
					{
						obj = this.Property.DefaultValue;
					}
					else
					{
						try
						{
							obj = SettingsPropertyValue.GetObjectFromString(this.Property.PropertyType, this.Property.SerializeAs, (string)this.Property.DefaultValue);
						}
						catch (Exception ex2)
						{
							throw new ArgumentException(SR.GetString("Could_not_create_from_default_value", new object[]
							{
								this.Property.Name,
								ex2.Message
							}));
						}
					}
					if (obj != null && !this.Property.PropertyType.IsAssignableFrom(obj.GetType()))
					{
						throw new ArgumentException(SR.GetString("Could_not_create_from_default_value_2", new object[] { this.Property.Name }));
					}
				}
			}
			if (obj == null)
			{
				if (this.Property.PropertyType == typeof(string))
				{
					obj = "";
				}
				else
				{
					try
					{
						obj = SecurityUtils.SecureCreateInstance(this.Property.PropertyType);
					}
					catch
					{
					}
				}
			}
			return obj;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000236D0 File Offset: 0x000218D0
		private static object GetObjectFromString(Type type, SettingsSerializeAs serializeAs, string attValue)
		{
			if (type == typeof(string) && (attValue == null || attValue.Length < 1 || serializeAs == SettingsSerializeAs.String))
			{
				return attValue;
			}
			if (attValue == null || attValue.Length < 1)
			{
				return null;
			}
			switch (serializeAs)
			{
			case SettingsSerializeAs.String:
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
				{
					return converter.ConvertFromInvariantString(attValue);
				}
				throw new ArgumentException(SR.GetString("Unable_to_convert_type_from_string", new object[] { type.ToString() }), "type");
			}
			case SettingsSerializeAs.Xml:
				break;
			case SettingsSerializeAs.Binary:
			{
				byte[] array = Convert.FromBase64String(attValue);
				MemoryStream memoryStream = null;
				try
				{
					memoryStream = new MemoryStream(array);
					return new BinaryFormatter().Deserialize(memoryStream);
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
				}
				break;
			}
			default:
				return null;
			}
			StringReader stringReader = new StringReader(attValue);
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			return xmlSerializer.Deserialize(stringReader);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000237DC File Offset: 0x000219DC
		private object SerializePropertyValue()
		{
			if (this._Value == null)
			{
				return null;
			}
			if (this.Property.SerializeAs != SettingsSerializeAs.Binary)
			{
				return SettingsPropertyValue.ConvertObjectToString(this._Value, this.Property.PropertyType, this.Property.SerializeAs, this.Property.ThrowOnErrorSerializing);
			}
			MemoryStream memoryStream = new MemoryStream();
			object obj;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this._Value);
				obj = memoryStream.ToArray();
			}
			finally
			{
				memoryStream.Close();
			}
			return obj;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00023868 File Offset: 0x00021A68
		private static string ConvertObjectToString(object propValue, Type type, SettingsSerializeAs serializeAs, bool throwOnError)
		{
			if (serializeAs == SettingsSerializeAs.ProviderSpecific)
			{
				if (type == typeof(string) || type.IsPrimitive)
				{
					serializeAs = SettingsSerializeAs.String;
				}
				else
				{
					serializeAs = SettingsSerializeAs.Xml;
				}
			}
			try
			{
				switch (serializeAs)
				{
				case SettingsSerializeAs.String:
				{
					TypeConverter converter = TypeDescriptor.GetConverter(type);
					if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
					{
						return converter.ConvertToInvariantString(propValue);
					}
					throw new ArgumentException(SR.GetString("Unable_to_convert_type_to_string", new object[] { type.ToString() }), "type");
				}
				case SettingsSerializeAs.Xml:
					break;
				case SettingsSerializeAs.Binary:
				{
					MemoryStream memoryStream = new MemoryStream();
					try
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.Serialize(memoryStream, propValue);
						byte[] array = memoryStream.ToArray();
						return Convert.ToBase64String(array);
					}
					finally
					{
						memoryStream.Close();
					}
					break;
				}
				default:
					goto IL_FC;
				}
				XmlSerializer xmlSerializer = new XmlSerializer(type);
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				xmlSerializer.Serialize(stringWriter, propValue);
				return stringWriter.ToString();
			}
			catch (Exception)
			{
				if (throwOnError)
				{
					throw;
				}
			}
			IL_FC:
			return null;
		}

		// Token: 0x04000C4E RID: 3150
		private object _Value;

		// Token: 0x04000C4F RID: 3151
		private object _SerializedValue;

		// Token: 0x04000C50 RID: 3152
		private bool _Deserialized;

		// Token: 0x04000C51 RID: 3153
		private bool _IsDirty;

		// Token: 0x04000C52 RID: 3154
		private SettingsProperty _Property;

		// Token: 0x04000C53 RID: 3155
		private bool _ChangedSinceLastSerialized;

		// Token: 0x04000C54 RID: 3156
		private bool _UsingDefaultValue = true;
	}
}

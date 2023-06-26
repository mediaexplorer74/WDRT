using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	// Token: 0x020000A9 RID: 169
	public class SettingsProperty
	{
		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00022FA2 File Offset: 0x000211A2
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00022FAA File Offset: 0x000211AA
		public virtual string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00022FB3 File Offset: 0x000211B3
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00022FBB File Offset: 0x000211BB
		public virtual bool IsReadOnly
		{
			get
			{
				return this._IsReadOnly;
			}
			set
			{
				this._IsReadOnly = value;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00022FC4 File Offset: 0x000211C4
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00022FCC File Offset: 0x000211CC
		public virtual object DefaultValue
		{
			get
			{
				return this._DefaultValue;
			}
			set
			{
				this._DefaultValue = value;
			}
		}

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00022FD5 File Offset: 0x000211D5
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00022FDD File Offset: 0x000211DD
		public virtual Type PropertyType
		{
			get
			{
				return this._PropertyType;
			}
			set
			{
				this._PropertyType = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00022FE6 File Offset: 0x000211E6
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00022FEE File Offset: 0x000211EE
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this._SerializeAs;
			}
			set
			{
				this._SerializeAs = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00022FF7 File Offset: 0x000211F7
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00022FFF File Offset: 0x000211FF
		public virtual SettingsProvider Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00023008 File Offset: 0x00021208
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this._Attributes;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the error will be thrown when the property is unsuccessfully deserialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00023010 File Offset: 0x00021210
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00023018 File Offset: 0x00021218
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this._ThrowOnErrorDeserializing;
			}
			set
			{
				this._ThrowOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the error will be thrown when the property is unsuccessfully serialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00023021 File Offset: 0x00021221
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00023029 File Offset: 0x00021229
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this._ThrowOnErrorSerializing;
			}
			set
			{
				this._ThrowOnErrorSerializing = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005C8 RID: 1480 RVA: 0x00023032 File Offset: 0x00021232
		public SettingsProperty(string name)
		{
			this._Name = name;
			this._Attributes = new SettingsAttributeDictionary();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x060005C9 RID: 1481 RVA: 0x0002304C File Offset: 0x0002124C
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this._Name = name;
			this._PropertyType = propertyType;
			this._Provider = provider;
			this._IsReadOnly = isReadOnly;
			this._DefaultValue = defaultValue;
			this._SerializeAs = serializeAs;
			this._Attributes = attributes;
			this._ThrowOnErrorDeserializing = throwOnErrorDeserializing;
			this._ThrowOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005CA RID: 1482 RVA: 0x000230A4 File Offset: 0x000212A4
		public SettingsProperty(SettingsProperty propertyToCopy)
		{
			this._Name = propertyToCopy.Name;
			this._IsReadOnly = propertyToCopy.IsReadOnly;
			this._DefaultValue = propertyToCopy.DefaultValue;
			this._SerializeAs = propertyToCopy.SerializeAs;
			this._Provider = propertyToCopy.Provider;
			this._PropertyType = propertyToCopy.PropertyType;
			this._ThrowOnErrorDeserializing = propertyToCopy.ThrowOnErrorDeserializing;
			this._ThrowOnErrorSerializing = propertyToCopy.ThrowOnErrorSerializing;
			this._Attributes = new SettingsAttributeDictionary(propertyToCopy.Attributes);
		}

		// Token: 0x04000C43 RID: 3139
		private string _Name;

		// Token: 0x04000C44 RID: 3140
		private bool _IsReadOnly;

		// Token: 0x04000C45 RID: 3141
		private object _DefaultValue;

		// Token: 0x04000C46 RID: 3142
		private SettingsSerializeAs _SerializeAs;

		// Token: 0x04000C47 RID: 3143
		private SettingsProvider _Provider;

		// Token: 0x04000C48 RID: 3144
		private SettingsAttributeDictionary _Attributes;

		// Token: 0x04000C49 RID: 3145
		private Type _PropertyType;

		// Token: 0x04000C4A RID: 3146
		private bool _ThrowOnErrorDeserializing;

		// Token: 0x04000C4B RID: 3147
		private bool _ThrowOnErrorSerializing;
	}
}

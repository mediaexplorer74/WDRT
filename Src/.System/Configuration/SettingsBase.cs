using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides the base class used to support user property settings.</summary>
	// Token: 0x020000A7 RID: 167
	public abstract class SettingsBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsBase" /> class.</summary>
		// Token: 0x060005A7 RID: 1447 RVA: 0x00022A6B File Offset: 0x00020C6B
		protected SettingsBase()
		{
			this._PropertyValues = new SettingsPropertyValueCollection();
		}

		/// <summary>Gets or sets the value of the specified settings property.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <returns>If found, the value of the named settings property.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current object, or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		// Token: 0x170000DC RID: 220
		public virtual object this[string propertyName]
		{
			get
			{
				if (this.IsSynchronized)
				{
					lock (this)
					{
						return this.GetPropertyValueByName(propertyName);
					}
				}
				return this.GetPropertyValueByName(propertyName);
			}
			set
			{
				if (this.IsSynchronized)
				{
					lock (this)
					{
						this.SetPropertyValueByName(propertyName, value);
						return;
					}
				}
				this.SetPropertyValueByName(propertyName, value);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00022B20 File Offset: 0x00020D20
		private object GetPropertyValueByName(string propertyName)
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
			}
			SettingsPropertyValue settingsPropertyValue = this._PropertyValues[propertyName];
			if (settingsPropertyValue == null)
			{
				this.GetPropertiesFromProvider(settingsProperty.Provider);
				settingsPropertyValue = this._PropertyValues[propertyName];
				if (settingsPropertyValue == null)
				{
					throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
				}
			}
			return settingsPropertyValue.PropertyValue;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00022BDC File Offset: 0x00020DDC
		private void SetPropertyValueByName(string propertyName, object propertyValue)
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
			}
			if (settingsProperty.IsReadOnly)
			{
				throw new SettingsPropertyIsReadOnlyException(SR.GetString("SettingsPropertyReadOnly", new object[] { propertyName }));
			}
			if (propertyValue != null && !settingsProperty.PropertyType.IsInstanceOfType(propertyValue))
			{
				throw new SettingsPropertyWrongTypeException(SR.GetString("SettingsPropertyWrongType", new object[] { propertyName }));
			}
			SettingsPropertyValue settingsPropertyValue = this._PropertyValues[propertyName];
			if (settingsPropertyValue == null)
			{
				this.GetPropertiesFromProvider(settingsProperty.Provider);
				settingsPropertyValue = this._PropertyValues[propertyName];
				if (settingsPropertyValue == null)
				{
					throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[] { propertyName }));
				}
			}
			settingsPropertyValue.PropertyValue = propertyValue;
		}

		/// <summary>Initializes internal properties used by <see cref="T:System.Configuration.SettingsBase" /> object.</summary>
		/// <param name="context">The settings context related to the settings properties.</param>
		/// <param name="properties">The settings properties that will be accessible from the <see cref="T:System.Configuration.SettingsBase" /> instance.</param>
		/// <param name="providers">The initialized providers that should be used when loading and saving property values.</param>
		// Token: 0x060005AC RID: 1452 RVA: 0x00022CE4 File Offset: 0x00020EE4
		public void Initialize(SettingsContext context, SettingsPropertyCollection properties, SettingsProviderCollection providers)
		{
			this._Context = context;
			this._Properties = properties;
			this._Providers = providers;
		}

		/// <summary>Stores the current values of the settings properties.</summary>
		// Token: 0x060005AD RID: 1453 RVA: 0x00022CFC File Offset: 0x00020EFC
		public virtual void Save()
		{
			if (this.IsSynchronized)
			{
				lock (this)
				{
					this.SaveCore();
					return;
				}
			}
			this.SaveCore();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00022D48 File Offset: 0x00020F48
		private void SaveCore()
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				return;
			}
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
			foreach (object obj3 in this.PropertyValues)
			{
				SettingsPropertyValue settingsPropertyValue2 = (SettingsPropertyValue)obj3;
				settingsPropertyValue2.IsDirty = false;
			}
		}

		/// <summary>Gets the collection of settings properties.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> collection containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00022E84 File Offset: 0x00021084
		public virtual SettingsPropertyCollection Properties
		{
			get
			{
				return this._Properties;
			}
		}

		/// <summary>Gets a collection of settings providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing <see cref="T:System.Configuration.SettingsProvider" /> objects.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00022E8C File Offset: 0x0002108C
		public virtual SettingsProviderCollection Providers
		{
			get
			{
				return this._Providers;
			}
		}

		/// <summary>Gets a collection of settings property values.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects representing the actual data values for the properties managed by the <see cref="T:System.Configuration.SettingsBase" /> instance.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00022E94 File Offset: 0x00021094
		public virtual SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				return this._PropertyValues;
			}
		}

		/// <summary>Gets the associated settings context.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings instance.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00022E9C File Offset: 0x0002109C
		public virtual SettingsContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00022EA4 File Offset: 0x000210A4
		private void GetPropertiesFromProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection.Count > 0)
			{
				SettingsPropertyValueCollection propertyValues = provider.GetPropertyValues(this.Context, settingsPropertyCollection);
				foreach (object obj2 in propertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (this._PropertyValues[settingsPropertyValue.Name] == null)
					{
						this._PropertyValues.Add(settingsPropertyValue);
					}
				}
			}
		}

		/// <summary>Provides a <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized (thread safe).</summary>
		/// <param name="settingsBase">The class used to support user property settings.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized.</returns>
		// Token: 0x060005B4 RID: 1460 RVA: 0x00022F88 File Offset: 0x00021188
		public static SettingsBase Synchronized(SettingsBase settingsBase)
		{
			settingsBase._IsSynchronized = true;
			return settingsBase;
		}

		/// <summary>Gets a value indicating whether access to the object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsBase" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00022F92 File Offset: 0x00021192
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this._IsSynchronized;
			}
		}

		// Token: 0x04000C3E RID: 3134
		private SettingsPropertyCollection _Properties;

		// Token: 0x04000C3F RID: 3135
		private SettingsProviderCollection _Providers;

		// Token: 0x04000C40 RID: 3136
		private SettingsPropertyValueCollection _PropertyValues;

		// Token: 0x04000C41 RID: 3137
		private SettingsContext _Context;

		// Token: 0x04000C42 RID: 3138
		private bool _IsSynchronized;
	}
}

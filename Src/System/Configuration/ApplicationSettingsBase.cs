using System;
using System.Collections;
using System.ComponentModel;
using System.Deployment.Internal;
using System.Reflection;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving concrete wrapper classes to implement the application settings feature in Window Forms applications.</summary>
	// Token: 0x02000079 RID: 121
	public abstract class ApplicationSettingsBase : SettingsBase, INotifyPropertyChanged
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class to its default state.</summary>
		// Token: 0x060004C8 RID: 1224 RVA: 0x0001FF19 File Offset: 0x0001E119
		protected ApplicationSettingsBase()
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is <see langword="null" />.</exception>
		// Token: 0x060004C9 RID: 1225 RVA: 0x0001FF33 File Offset: 0x0001E133
		protected ApplicationSettingsBase(IComponent owner)
			: this(owner, string.Empty)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied settings key.</summary>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		// Token: 0x060004CA RID: 1226 RVA: 0x0001FF41 File Offset: 0x0001E141
		protected ApplicationSettingsBase(string settingsKey)
		{
			this._settingsKey = settingsKey;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component and settings key.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is <see langword="null" />.</exception>
		// Token: 0x060004CB RID: 1227 RVA: 0x0001FF64 File Offset: 0x0001E164
		protected ApplicationSettingsBase(IComponent owner, string settingsKey)
			: this(settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			if (owner.Site != null)
			{
				ISettingsProviderService settingsProviderService = owner.Site.GetService(typeof(ISettingsProviderService)) as ISettingsProviderService;
				if (settingsProviderService != null)
				{
					foreach (object obj in this.Properties)
					{
						SettingsProperty settingsProperty = (SettingsProperty)obj;
						SettingsProvider settingsProvider = settingsProviderService.GetSettingsProvider(settingsProperty);
						if (settingsProvider != null)
						{
							settingsProperty.Provider = settingsProvider;
						}
					}
					this.ResetProviders();
				}
			}
		}

		/// <summary>Gets the application settings context associated with the settings group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings group.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00020014 File Offset: 0x0001E214
		[Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (this._context == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._context == null)
							{
								this._context = new SettingsContext();
								this.EnsureInitialized();
							}
							goto IL_52;
						}
					}
					this._context = new SettingsContext();
					this.EnsureInitialized();
				}
				IL_52:
				return this._context;
			}
		}

		/// <summary>Gets the collection of settings properties in the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects used in the current wrapper.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The associated settings provider could not be found or its instantiation failed.</exception>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0002008C File Offset: 0x0001E28C
		[Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (this._settings == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._settings == null)
							{
								this._settings = new SettingsPropertyCollection();
								this.EnsureInitialized();
							}
							goto IL_52;
						}
					}
					this._settings = new SettingsPropertyCollection();
					this.EnsureInitialized();
				}
				IL_52:
				return this._settings;
			}
		}

		/// <summary>Gets a collection of property values.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> of property values.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00020104 File Offset: 0x0001E304
		[Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				return base.PropertyValues;
			}
		}

		/// <summary>Gets the collection of application settings providers used by the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing all the <see cref="T:System.Configuration.SettingsProvider" /> objects used by the settings properties of the current settings wrapper.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0002010C File Offset: 0x0001E30C
		[Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (this._providers == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._providers == null)
							{
								this._providers = new SettingsProviderCollection();
								this.EnsureInitialized();
							}
							goto IL_52;
						}
					}
					this._providers = new SettingsProviderCollection();
					this.EnsureInitialized();
				}
				IL_52:
				return this._providers;
			}
		}

		/// <summary>Gets or sets the settings key for the application settings group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current settings group.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00020184 File Offset: 0x0001E384
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0002018C File Offset: 0x0001E38C
		[Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this._settingsKey;
			}
			set
			{
				this._settingsKey = value;
				this.Context["SettingsKey"] = this._settingsKey;
			}
		}

		/// <summary>Occurs after the value of an application settings property is changed.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060004D2 RID: 1234 RVA: 0x000201AB File Offset: 0x0001E3AB
		// (remove) Token: 0x060004D3 RID: 1235 RVA: 0x000201C4 File Offset: 0x0001E3C4
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this._onPropertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._onPropertyChanged, value);
			}
			remove
			{
				this._onPropertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._onPropertyChanged, value);
			}
		}

		/// <summary>Occurs before the value of an application settings property is changed.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060004D4 RID: 1236 RVA: 0x000201DD File Offset: 0x0001E3DD
		// (remove) Token: 0x060004D5 RID: 1237 RVA: 0x000201F6 File Offset: 0x0001E3F6
		public event SettingChangingEventHandler SettingChanging
		{
			add
			{
				this._onSettingChanging = (SettingChangingEventHandler)Delegate.Combine(this._onSettingChanging, value);
			}
			remove
			{
				this._onSettingChanging = (SettingChangingEventHandler)Delegate.Remove(this._onSettingChanging, value);
			}
		}

		/// <summary>Occurs after the application settings are retrieved from storage.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060004D6 RID: 1238 RVA: 0x0002020F File Offset: 0x0001E40F
		// (remove) Token: 0x060004D7 RID: 1239 RVA: 0x00020228 File Offset: 0x0001E428
		public event SettingsLoadedEventHandler SettingsLoaded
		{
			add
			{
				this._onSettingsLoaded = (SettingsLoadedEventHandler)Delegate.Combine(this._onSettingsLoaded, value);
			}
			remove
			{
				this._onSettingsLoaded = (SettingsLoadedEventHandler)Delegate.Remove(this._onSettingsLoaded, value);
			}
		}

		/// <summary>Occurs before values are saved to the data store.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060004D8 RID: 1240 RVA: 0x00020241 File Offset: 0x0001E441
		// (remove) Token: 0x060004D9 RID: 1241 RVA: 0x0002025A File Offset: 0x0001E45A
		public event SettingsSavingEventHandler SettingsSaving
		{
			add
			{
				this._onSettingsSaving = (SettingsSavingEventHandler)Delegate.Combine(this._onSettingsSaving, value);
			}
			remove
			{
				this._onSettingsSaving = (SettingsSavingEventHandler)Delegate.Remove(this._onSettingsSaving, value);
			}
		}

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the settings property whose value is to be returned.</param>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the specified <see cref="T:System.Configuration.SettingsProperty" /> if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">The property does not exist. The property count is zero or the property cannot be found in the data store.</exception>
		// Token: 0x060004DA RID: 1242 RVA: 0x00020274 File Offset: 0x0001E474
		public object GetPreviousVersion(string propertyName)
		{
			if (this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException();
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			SettingsPropertyValue settingsPropertyValue = null;
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException();
			}
			IApplicationSettingsProvider applicationSettingsProvider = settingsProperty.Provider as IApplicationSettingsProvider;
			if (applicationSettingsProvider != null)
			{
				settingsPropertyValue = applicationSettingsProvider.GetPreviousVersion(this.Context, settingsProperty);
			}
			if (settingsPropertyValue != null)
			{
				return settingsPropertyValue.PropertyValue;
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.PropertyChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x000202D4 File Offset: 0x0001E4D4
		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this._onPropertyChanged != null)
			{
				this._onPropertyChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x060004DC RID: 1244 RVA: 0x000202EB File Offset: 0x0001E4EB
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this._onSettingChanging != null)
			{
				this._onSettingChanging(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x060004DD RID: 1245 RVA: 0x00020302 File Offset: 0x0001E502
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this._onSettingsLoaded != null)
			{
				this._onSettingsLoaded(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x060004DE RID: 1246 RVA: 0x00020319 File Offset: 0x0001E519
		protected virtual void OnSettingsSaving(object sender, CancelEventArgs e)
		{
			if (this._onSettingsSaving != null)
			{
				this._onSettingsSaving(this, e);
			}
		}

		/// <summary>Refreshes the application settings property values from persistent storage.</summary>
		// Token: 0x060004DF RID: 1247 RVA: 0x00020330 File Offset: 0x0001E530
		public void Reload()
		{
			if (this.PropertyValues != null)
			{
				this.PropertyValues.Clear();
			}
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(settingsProperty.Name);
				this.OnPropertyChanged(this, propertyChangedEventArgs);
			}
		}

		/// <summary>Restores the persisted application settings values to their corresponding default properties.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x060004E0 RID: 1248 RVA: 0x000203AC File Offset: 0x0001E5AC
		public void Reset()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					SettingsProvider settingsProvider = (SettingsProvider)obj;
					IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Reset(this.Context);
					}
				}
			}
			this.Reload();
		}

		/// <summary>Stores the current values of the application settings properties.</summary>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00020424 File Offset: 0x0001E624
		public override void Save()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnSettingsSaving(this, cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				base.Save();
			}
		}

		/// <summary>Gets or sets the value of the specified application settings property.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <returns>If found, the value of the named settings property; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current wrapper or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x170000AB RID: 171
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.SettingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					base[propertyName] = value;
					PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propertyName);
					this.OnPropertyChanged(this, propertyChangedEventArgs);
				}
			}
		}

		/// <summary>Updates application settings to reflect a more recent installation of the application.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x060004E4 RID: 1252 RVA: 0x000204F0 File Offset: 0x0001E6F0
		public virtual void Upgrade()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					SettingsProvider settingsProvider = (SettingsProvider)obj;
					IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Upgrade(this.Context, this.GetPropertiesForProvider(settingsProvider));
					}
				}
			}
			this.Reload();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00020570 File Offset: 0x0001E770
		private SettingsProperty CreateSetting(PropertyInfo propInfo)
		{
			object[] customAttributes = propInfo.GetCustomAttributes(false);
			SettingsProperty settingsProperty = new SettingsProperty(this.Initializer);
			bool flag = this._explicitSerializeOnClass;
			settingsProperty.Name = propInfo.Name;
			settingsProperty.PropertyType = propInfo.PropertyType;
			for (int i = 0; i < customAttributes.Length; i++)
			{
				Attribute attribute = customAttributes[i] as Attribute;
				if (attribute != null)
				{
					if (attribute is DefaultSettingValueAttribute)
					{
						settingsProperty.DefaultValue = ((DefaultSettingValueAttribute)attribute).Value;
					}
					else if (attribute is ReadOnlyAttribute)
					{
						settingsProperty.IsReadOnly = true;
					}
					else if (attribute is SettingsProviderAttribute)
					{
						string providerTypeName = ((SettingsProviderAttribute)attribute).ProviderTypeName;
						Type type = Type.GetType(providerTypeName);
						if (!(type != null))
						{
							throw new ConfigurationErrorsException(SR.GetString("ProviderTypeLoadFailed", new object[] { providerTypeName }));
						}
						SettingsProvider settingsProvider = SecurityUtils.SecureCreateInstance(type) as SettingsProvider;
						if (settingsProvider == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("ProviderInstantiationFailed", new object[] { providerTypeName }));
						}
						settingsProvider.Initialize(null, null);
						settingsProvider.ApplicationName = ConfigurationManagerInternalFactory.Instance.ExeProductName;
						SettingsProvider settingsProvider2 = this._providers[settingsProvider.Name];
						if (settingsProvider2 != null)
						{
							settingsProvider = settingsProvider2;
						}
						settingsProperty.Provider = settingsProvider;
					}
					else if (attribute is SettingsSerializeAsAttribute)
					{
						settingsProperty.SerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
						flag = true;
					}
					else
					{
						settingsProperty.Attributes.Add(attribute.GetType(), attribute);
					}
				}
			}
			if (!flag)
			{
				settingsProperty.SerializeAs = this.GetSerializeAs(propInfo.PropertyType);
			}
			return settingsProperty;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00020708 File Offset: 0x0001E908
		private void EnsureInitialized()
		{
			if (!this._initialized)
			{
				this._initialized = true;
				Type type = base.GetType();
				if (this._context == null)
				{
					this._context = new SettingsContext();
				}
				this._context["GroupName"] = type.FullName;
				this._context["SettingsKey"] = this.SettingsKey;
				this._context["SettingsClassType"] = type;
				PropertyInfo[] array = this.SettingsFilter(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
				this._classAttributes = type.GetCustomAttributes(false);
				if (this._settings == null)
				{
					this._settings = new SettingsPropertyCollection();
				}
				if (this._providers == null)
				{
					this._providers = new SettingsProviderCollection();
				}
				for (int i = 0; i < array.Length; i++)
				{
					SettingsProperty settingsProperty = this.CreateSetting(array[i]);
					if (settingsProperty != null)
					{
						this._settings.Add(settingsProperty);
						if (settingsProperty.Provider != null && this._providers[settingsProperty.Provider.Name] == null)
						{
							this._providers.Add(settingsProperty.Provider);
						}
					}
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00020818 File Offset: 0x0001EA18
		private SettingsProperty Initializer
		{
			get
			{
				if (this._init == null)
				{
					this._init = new SettingsProperty("");
					this._init.DefaultValue = null;
					this._init.IsReadOnly = false;
					this._init.PropertyType = null;
					SettingsProvider settingsProvider = new LocalFileSettingsProvider();
					if (this._classAttributes != null)
					{
						for (int i = 0; i < this._classAttributes.Length; i++)
						{
							Attribute attribute = this._classAttributes[i] as Attribute;
							if (attribute != null)
							{
								if (attribute is ReadOnlyAttribute)
								{
									this._init.IsReadOnly = true;
								}
								else if (attribute is SettingsGroupNameAttribute)
								{
									if (this._context == null)
									{
										this._context = new SettingsContext();
									}
									this._context["GroupName"] = ((SettingsGroupNameAttribute)attribute).GroupName;
								}
								else if (attribute is SettingsProviderAttribute)
								{
									string providerTypeName = ((SettingsProviderAttribute)attribute).ProviderTypeName;
									Type type = Type.GetType(providerTypeName);
									if (!(type != null))
									{
										throw new ConfigurationErrorsException(SR.GetString("ProviderTypeLoadFailed", new object[] { providerTypeName }));
									}
									SettingsProvider settingsProvider2 = SecurityUtils.SecureCreateInstance(type) as SettingsProvider;
									if (settingsProvider2 == null)
									{
										throw new ConfigurationErrorsException(SR.GetString("ProviderInstantiationFailed", new object[] { providerTypeName }));
									}
									settingsProvider = settingsProvider2;
								}
								else if (attribute is SettingsSerializeAsAttribute)
								{
									this._init.SerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
									this._explicitSerializeOnClass = true;
								}
								else
								{
									this._init.Attributes.Add(attribute.GetType(), attribute);
								}
							}
						}
					}
					settingsProvider.Initialize(null, null);
					settingsProvider.ApplicationName = ConfigurationManagerInternalFactory.Instance.ExeProductName;
					this._init.Provider = settingsProvider;
				}
				return this._init;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000209D0 File Offset: 0x0001EBD0
		private SettingsPropertyCollection GetPropertiesForProvider(SettingsProvider provider)
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
			return settingsPropertyCollection;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00020A3C File Offset: 0x0001EC3C
		private object GetPropertyValue(string propertyName)
		{
			if (this.PropertyValues[propertyName] == null)
			{
				if (this._firstLoad)
				{
					this._firstLoad = false;
					if (this.IsFirstRunOfClickOnceApp())
					{
						this.Upgrade();
					}
				}
				object obj = base[propertyName];
				SettingsProperty settingsProperty = this.Properties[propertyName];
				SettingsProvider settingsProvider = ((settingsProperty != null) ? settingsProperty.Provider : null);
				SettingsLoadedEventArgs settingsLoadedEventArgs = new SettingsLoadedEventArgs(settingsProvider);
				this.OnSettingsLoaded(this, settingsLoadedEventArgs);
				return base[propertyName];
			}
			return base[propertyName];
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		private SettingsSerializeAs GetSerializeAs(Type type)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			bool flag = converter.CanConvertTo(typeof(string));
			bool flag2 = converter.CanConvertFrom(typeof(string));
			if (flag && flag2)
			{
				return SettingsSerializeAs.String;
			}
			return SettingsSerializeAs.Xml;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		private bool IsFirstRunOfClickOnceApp()
		{
			ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
			return ApplicationSettingsBase.IsClickOnceDeployed(AppDomain.CurrentDomain) && InternalActivationContextHelper.IsFirstRun(activationContext);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00020B20 File Offset: 0x0001ED20
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static bool IsClickOnceDeployed(AppDomain appDomain)
		{
			ActivationContext activationContext = appDomain.ActivationContext;
			if (activationContext != null && activationContext.Form == ActivationContext.ContextForm.StoreBounded)
			{
				string fullName = activationContext.Identity.FullName;
				if (!string.IsNullOrEmpty(fullName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00020B58 File Offset: 0x0001ED58
		private PropertyInfo[] SettingsFilter(PropertyInfo[] allProps)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < allProps.Length; i++)
			{
				object[] customAttributes = allProps[i].GetCustomAttributes(false);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					Attribute attribute = customAttributes[j] as Attribute;
					if (attribute is SettingAttribute)
					{
						arrayList.Add(allProps[i]);
						break;
					}
				}
			}
			return (PropertyInfo[])arrayList.ToArray(typeof(PropertyInfo));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		private void ResetProviders()
		{
			this.Providers.Clear();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (this.Providers[settingsProperty.Provider.Name] == null)
				{
					this.Providers.Add(settingsProperty.Provider);
				}
			}
		}

		// Token: 0x04000BFB RID: 3067
		private bool _explicitSerializeOnClass;

		// Token: 0x04000BFC RID: 3068
		private object[] _classAttributes;

		// Token: 0x04000BFD RID: 3069
		private IComponent _owner;

		// Token: 0x04000BFE RID: 3070
		private PropertyChangedEventHandler _onPropertyChanged;

		// Token: 0x04000BFF RID: 3071
		private SettingsContext _context;

		// Token: 0x04000C00 RID: 3072
		private SettingsProperty _init;

		// Token: 0x04000C01 RID: 3073
		private SettingsPropertyCollection _settings;

		// Token: 0x04000C02 RID: 3074
		private SettingsProviderCollection _providers;

		// Token: 0x04000C03 RID: 3075
		private SettingChangingEventHandler _onSettingChanging;

		// Token: 0x04000C04 RID: 3076
		private SettingsLoadedEventHandler _onSettingsLoaded;

		// Token: 0x04000C05 RID: 3077
		private SettingsSavingEventHandler _onSettingsSaving;

		// Token: 0x04000C06 RID: 3078
		private string _settingsKey = string.Empty;

		// Token: 0x04000C07 RID: 3079
		private bool _firstLoad = true;

		// Token: 0x04000C08 RID: 3080
		private bool _initialized;
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides persistence for application settings classes.</summary>
	// Token: 0x02000094 RID: 148
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class LocalFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		/// <summary>Gets or sets the name of the currently running application.</summary>
		/// <returns>A string that contains the application's display name.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00021D14 File Offset: 0x0001FF14
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00021D1C File Offset: 0x0001FF1C
		public override string ApplicationName
		{
			get
			{
				return this._appName;
			}
			set
			{
				this._appName = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00021D25 File Offset: 0x0001FF25
		private LocalFileSettingsProvider.XmlEscaper Escaper
		{
			get
			{
				if (this._escaper == null)
				{
					this._escaper = new LocalFileSettingsProvider.XmlEscaper();
				}
				return this._escaper;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00021D40 File Offset: 0x0001FF40
		private ClientSettingsStore Store
		{
			get
			{
				if (this._store == null)
				{
					this._store = new ClientSettingsStore();
				}
				return this._store;
			}
		}

		/// <summary>Initializes the provider.</summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="values">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		// Token: 0x06000576 RID: 1398 RVA: 0x00021D5B File Offset: 0x0001FF5B
		public override void Initialize(string name, NameValueCollection values)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "LocalFileSettingsProvider";
			}
			base.Initialize(name, values);
		}

		/// <summary>Returns the collection of setting property values for the specified application instance and settings property group.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		// Token: 0x06000577 RID: 1399 RVA: 0x00021D74 File Offset: 0x0001FF74
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			string sectionName = this.GetSectionName(context);
			IDictionary dictionary = this.Store.ReadSettings(sectionName, false);
			IDictionary dictionary2 = this.Store.ReadSettings(sectionName, true);
			ConnectionStringSettingsCollection connectionStringSettingsCollection = this.Store.ReadConnectionStrings();
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				SpecialSettingAttribute specialSettingAttribute = settingsProperty.Attributes[typeof(SpecialSettingAttribute)] as SpecialSettingAttribute;
				bool flag = specialSettingAttribute != null && specialSettingAttribute.SpecialSetting == SpecialSetting.ConnectionString;
				if (flag)
				{
					string text = sectionName + "." + name;
					if (connectionStringSettingsCollection != null && connectionStringSettingsCollection[text] != null)
					{
						settingsPropertyValue.PropertyValue = connectionStringSettingsCollection[text].ConnectionString;
					}
					else if (settingsProperty.DefaultValue != null && settingsProperty.DefaultValue is string)
					{
						settingsPropertyValue.PropertyValue = settingsProperty.DefaultValue;
					}
					else
					{
						settingsPropertyValue.PropertyValue = string.Empty;
					}
					settingsPropertyValue.IsDirty = false;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
				else
				{
					bool flag2 = this.IsUserSetting(settingsProperty);
					if (flag2 && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
					{
						throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
					}
					IDictionary dictionary3 = (flag2 ? dictionary2 : dictionary);
					if (dictionary3.Contains(name))
					{
						StoredSetting storedSetting = (StoredSetting)dictionary3[name];
						string text2 = storedSetting.Value.InnerXml;
						if (storedSetting.SerializeAs == SettingsSerializeAs.String)
						{
							text2 = this.Escaper.Unescape(text2);
						}
						settingsPropertyValue.SerializedValue = text2;
					}
					else if (settingsProperty.DefaultValue != null)
					{
						settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
					}
					else
					{
						settingsPropertyValue.PropertyValue = null;
					}
					settingsPropertyValue.IsDirty = false;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
			}
			return settingsPropertyValueCollection;
		}

		/// <summary>Sets the values of the specified group of property settings.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="values">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.  
		///  -or-  
		///  There was a general failure saving the settings to the configuration file.</exception>
		// Token: 0x06000578 RID: 1400 RVA: 0x00021F88 File Offset: 0x00020188
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			string sectionName = this.GetSectionName(context);
			IDictionary dictionary = new Hashtable();
			IDictionary dictionary2 = new Hashtable();
			foreach (object obj in values)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				SettingsProperty property = settingsPropertyValue.Property;
				bool flag = this.IsUserSetting(property);
				if (settingsPropertyValue.IsDirty && flag)
				{
					bool flag2 = LocalFileSettingsProvider.IsRoamingSetting(property);
					StoredSetting storedSetting = new StoredSetting(property.SerializeAs, this.SerializeToXmlElement(property, settingsPropertyValue));
					if (flag2)
					{
						dictionary[property.Name] = storedSetting;
					}
					else
					{
						dictionary2[property.Name] = storedSetting;
					}
					settingsPropertyValue.IsDirty = false;
				}
			}
			if (dictionary.Count > 0)
			{
				this.Store.WriteSettings(sectionName, true, dictionary);
			}
			if (dictionary2.Count > 0)
			{
				this.Store.WriteSettings(sectionName, false, dictionary2);
			}
		}

		/// <summary>Resets all application settings properties associated with the specified application to their default values.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		// Token: 0x06000579 RID: 1401 RVA: 0x00022098 File Offset: 0x00020298
		public void Reset(SettingsContext context)
		{
			string sectionName = this.GetSectionName(context);
			this.Store.RevertToParent(sectionName, true);
			this.Store.RevertToParent(sectionName, false);
		}

		/// <summary>Attempts to migrate previous user-scoped settings from a previous version of the same application.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.  
		///  -or-  
		///  The previous version of the configuration file could not be accessed.</exception>
		// Token: 0x0600057A RID: 1402 RVA: 0x000220C8 File Offset: 0x000202C8
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			SettingsPropertyCollection settingsPropertyCollection2 = new SettingsPropertyCollection();
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				bool flag = LocalFileSettingsProvider.IsRoamingSetting(settingsProperty);
				if (flag)
				{
					settingsPropertyCollection2.Add(settingsProperty);
				}
				else
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection2.Count > 0)
			{
				this.Upgrade(context, settingsPropertyCollection2, true);
			}
			if (settingsPropertyCollection.Count > 0)
			{
				this.Upgrade(context, settingsPropertyCollection, false);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00022164 File Offset: 0x00020364
		private Version CreateVersion(string name)
		{
			Version version = null;
			try
			{
				version = new Version(name);
			}
			catch (ArgumentException)
			{
				version = null;
			}
			catch (OverflowException)
			{
				version = null;
			}
			catch (FormatException)
			{
				version = null;
			}
			return version;
		}

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> that describes where the application settings property is used.</param>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> whose value is to be returned.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValue" /> representing the application setting if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600057C RID: 1404 RVA: 0x000221B4 File Offset: 0x000203B4
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			bool flag = LocalFileSettingsProvider.IsRoamingSetting(property);
			string previousConfigFileName = this.GetPreviousConfigFileName(flag);
			if (!string.IsNullOrEmpty(previousConfigFileName))
			{
				SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
				settingsPropertyCollection.Add(property);
				SettingsPropertyValueCollection settingValuesFromFile = this.GetSettingValuesFromFile(previousConfigFileName, this.GetSectionName(context), true, settingsPropertyCollection);
				return settingValuesFromFile[property.Name];
			}
			return new SettingsPropertyValue(property)
			{
				PropertyValue = null
			};
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00022218 File Offset: 0x00020418
		private string GetPreviousConfigFileName(bool isRoaming)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			string text = (isRoaming ? this._prevRoamingConfigFileName : this._prevLocalConfigFileName);
			if (string.IsNullOrEmpty(text))
			{
				string text2 = (isRoaming ? ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigDirectory : ConfigurationManagerInternalFactory.Instance.ExeLocalConfigDirectory);
				Version version = this.CreateVersion(ConfigurationManagerInternalFactory.Instance.ExeProductVersion);
				Version version2 = null;
				DirectoryInfo directoryInfo = null;
				string text3 = null;
				if (version == null)
				{
					return null;
				}
				DirectoryInfo parent = Directory.GetParent(text2);
				if (parent.Exists)
				{
					foreach (DirectoryInfo directoryInfo2 in parent.GetDirectories())
					{
						Version version3 = this.CreateVersion(directoryInfo2.Name);
						if (version3 != null && version3 < version)
						{
							if (version2 == null)
							{
								version2 = version3;
								directoryInfo = directoryInfo2;
							}
							else if (version3 > version2)
							{
								version2 = version3;
								directoryInfo = directoryInfo2;
							}
						}
					}
					if (directoryInfo != null)
					{
						text3 = Path.Combine(directoryInfo.FullName, ConfigurationManagerInternalFactory.Instance.UserConfigFilename);
					}
					if (File.Exists(text3))
					{
						text = text3;
					}
				}
				if (isRoaming)
				{
					this._prevRoamingConfigFileName = text;
				}
				else
				{
					this._prevLocalConfigFileName = text;
				}
			}
			return text;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00022358 File Offset: 0x00020558
		private string GetSectionName(SettingsContext context)
		{
			string text = (string)context["GroupName"];
			string text2 = (string)context["SettingsKey"];
			string text3 = text;
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { text3, text2 });
			}
			return XmlConvert.EncodeLocalName(text3);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000223B8 File Offset: 0x000205B8
		private SettingsPropertyValueCollection GetSettingValuesFromFile(string configFileName, string sectionName, bool userScoped, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			IDictionary dictionary = ClientSettingsStore.ReadSettingsFromFile(configFileName, sectionName, userScoped);
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				if (dictionary.Contains(name))
				{
					StoredSetting storedSetting = (StoredSetting)dictionary[name];
					string text = storedSetting.Value.InnerXml;
					if (storedSetting.SerializeAs == SettingsSerializeAs.String)
					{
						text = this.Escaper.Unescape(text);
					}
					settingsPropertyValue.SerializedValue = text;
					settingsPropertyValue.IsDirty = true;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
			}
			return settingsPropertyValueCollection;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00022484 File Offset: 0x00020684
		private static bool IsRoamingSetting(SettingsProperty setting)
		{
			bool flag = !ApplicationSettingsBase.IsClickOnceDeployed(AppDomain.CurrentDomain);
			bool flag2 = false;
			if (flag)
			{
				SettingsManageabilityAttribute settingsManageabilityAttribute = setting.Attributes[typeof(SettingsManageabilityAttribute)] as SettingsManageabilityAttribute;
				flag2 = settingsManageabilityAttribute != null && (settingsManageabilityAttribute.Manageability & SettingsManageability.Roaming) == SettingsManageability.Roaming;
			}
			return flag2;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000224D4 File Offset: 0x000206D4
		private bool IsUserSetting(SettingsProperty setting)
		{
			bool flag = setting.Attributes[typeof(UserScopedSettingAttribute)] is UserScopedSettingAttribute;
			bool flag2 = setting.Attributes[typeof(ApplicationScopedSettingAttribute)] is ApplicationScopedSettingAttribute;
			if (flag && flag2)
			{
				throw new ConfigurationErrorsException(SR.GetString("BothScopeAttributes"));
			}
			if (!flag && !flag2)
			{
				throw new ConfigurationErrorsException(SR.GetString("NoScopeAttributes"));
			}
			return flag;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00022548 File Offset: 0x00020748
		private XmlNode SerializeToXmlElement(SettingsProperty setting, SettingsPropertyValue value)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("value");
			string text = value.SerializedValue as string;
			if (text == null && setting.SerializeAs == SettingsSerializeAs.Binary)
			{
				byte[] array = value.SerializedValue as byte[];
				if (array != null)
				{
					text = Convert.ToBase64String(array);
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (setting.SerializeAs == SettingsSerializeAs.String)
			{
				text = this.Escaper.Escape(text);
			}
			xmlElement.InnerXml = text;
			XmlNode xmlNode = null;
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType == XmlNodeType.XmlDeclaration)
				{
					xmlNode = xmlNode2;
					break;
				}
			}
			if (xmlNode != null)
			{
				xmlElement.RemoveChild(xmlNode);
			}
			return xmlElement;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00022628 File Offset: 0x00020828
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)]
		private void Upgrade(SettingsContext context, SettingsPropertyCollection properties, bool isRoaming)
		{
			string previousConfigFileName = this.GetPreviousConfigFileName(isRoaming);
			if (!string.IsNullOrEmpty(previousConfigFileName))
			{
				SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
				foreach (object obj in properties)
				{
					SettingsProperty settingsProperty = (SettingsProperty)obj;
					if (!(settingsProperty.Attributes[typeof(NoSettingsVersionUpgradeAttribute)] is NoSettingsVersionUpgradeAttribute))
					{
						settingsPropertyCollection.Add(settingsProperty);
					}
				}
				SettingsPropertyValueCollection settingValuesFromFile = this.GetSettingValuesFromFile(previousConfigFileName, this.GetSectionName(context), true, settingsPropertyCollection);
				this.SetPropertyValues(context, settingValuesFromFile);
			}
		}

		// Token: 0x04000C2A RID: 3114
		private string _appName = string.Empty;

		// Token: 0x04000C2B RID: 3115
		private ClientSettingsStore _store;

		// Token: 0x04000C2C RID: 3116
		private string _prevLocalConfigFileName;

		// Token: 0x04000C2D RID: 3117
		private string _prevRoamingConfigFileName;

		// Token: 0x04000C2E RID: 3118
		private LocalFileSettingsProvider.XmlEscaper _escaper;

		// Token: 0x020006E9 RID: 1769
		private class XmlEscaper
		{
			// Token: 0x06004046 RID: 16454 RVA: 0x0010D9A5 File Offset: 0x0010BBA5
			internal XmlEscaper()
			{
				this.doc = new XmlDocument();
				this.temp = this.doc.CreateElement("temp");
			}

			// Token: 0x06004047 RID: 16455 RVA: 0x0010D9CE File Offset: 0x0010BBCE
			internal string Escape(string xmlString)
			{
				if (string.IsNullOrEmpty(xmlString))
				{
					return xmlString;
				}
				this.temp.InnerText = xmlString;
				return this.temp.InnerXml;
			}

			// Token: 0x06004048 RID: 16456 RVA: 0x0010D9F1 File Offset: 0x0010BBF1
			internal string Unescape(string escapedString)
			{
				if (string.IsNullOrEmpty(escapedString))
				{
					return escapedString;
				}
				this.temp.InnerXml = escapedString;
				return this.temp.InnerText;
			}

			// Token: 0x04003056 RID: 12374
			private XmlDocument doc;

			// Token: 0x04003057 RID: 12375
			private XmlElement temp;
		}
	}
}

using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200007F RID: 127
	internal sealed class ClientSettingsStore
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		private Configuration GetUserConfig(bool isRoaming)
		{
			ConfigurationUserLevel configurationUserLevel = (isRoaming ? ConfigurationUserLevel.PerUserRoaming : ConfigurationUserLevel.PerUserRoamingAndLocal);
			return ClientSettingsStore.ClientSettingsConfigurationHost.OpenExeConfiguration(configurationUserLevel);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00020CD0 File Offset: 0x0001EED0
		private ClientSettingsSection GetConfigSection(Configuration config, string sectionName, bool declare)
		{
			string text = "userSettings/" + sectionName;
			ClientSettingsSection clientSettingsSection = null;
			if (config != null)
			{
				clientSettingsSection = config.GetSection(text) as ClientSettingsSection;
				if (clientSettingsSection == null && declare)
				{
					this.DeclareSection(config, sectionName);
					clientSettingsSection = config.GetSection(text) as ClientSettingsSection;
				}
			}
			return clientSettingsSection;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00020D1C File Offset: 0x0001EF1C
		private void DeclareSection(Configuration config, string sectionName)
		{
			if (config.GetSectionGroup("userSettings") == null)
			{
				ConfigurationSectionGroup configurationSectionGroup = new UserSettingsGroup();
				config.SectionGroups.Add("userSettings", configurationSectionGroup);
			}
			ConfigurationSectionGroup sectionGroup = config.GetSectionGroup("userSettings");
			if (sectionGroup != null && sectionGroup.Sections[sectionName] == null)
			{
				ConfigurationSection configurationSection = new ClientSettingsSection();
				configurationSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
				configurationSection.SectionInformation.RequirePermission = false;
				sectionGroup.Sections.Add(sectionName, configurationSection);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		internal IDictionary ReadSettings(string sectionName, bool isUserScoped)
		{
			IDictionary dictionary = new Hashtable();
			if (isUserScoped && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				return dictionary;
			}
			string text = (isUserScoped ? "userSettings/" : "applicationSettings/");
			ConfigurationManager.RefreshSection(text + sectionName);
			ClientSettingsSection clientSettingsSection = ConfigurationManager.GetSection(text + sectionName) as ClientSettingsSection;
			if (clientSettingsSection != null)
			{
				foreach (object obj in clientSettingsSection.Settings)
				{
					SettingElement settingElement = (SettingElement)obj;
					dictionary[settingElement.Name] = new StoredSetting(settingElement.SerializeAs, settingElement.Value.ValueXml);
				}
			}
			return dictionary;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00020E6C File Offset: 0x0001F06C
		internal static IDictionary ReadSettingsFromFile(string configFileName, string sectionName, bool isUserScoped)
		{
			IDictionary dictionary = new Hashtable();
			if (isUserScoped && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				return dictionary;
			}
			string text = (isUserScoped ? "userSettings/" : "applicationSettings/");
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			ConfigurationUserLevel configurationUserLevel = (isUserScoped ? ConfigurationUserLevel.PerUserRoaming : ConfigurationUserLevel.None);
			if (isUserScoped)
			{
				exeConfigurationFileMap.ExeConfigFilename = ConfigurationManagerInternalFactory.Instance.ApplicationConfigUri;
				exeConfigurationFileMap.RoamingUserConfigFilename = configFileName;
			}
			else
			{
				exeConfigurationFileMap.ExeConfigFilename = configFileName;
			}
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, configurationUserLevel);
			ClientSettingsSection clientSettingsSection = configuration.GetSection(text + sectionName) as ClientSettingsSection;
			if (clientSettingsSection != null)
			{
				foreach (object obj in clientSettingsSection.Settings)
				{
					SettingElement settingElement = (SettingElement)obj;
					dictionary[settingElement.Name] = new StoredSetting(settingElement.SerializeAs, settingElement.Value.ValueXml);
				}
			}
			return dictionary;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00020F70 File Offset: 0x0001F170
		internal ConnectionStringSettingsCollection ReadConnectionStrings()
		{
			return PrivilegedConfigurationManager.ConnectionStrings;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00020F78 File Offset: 0x0001F178
		internal void RevertToParent(string sectionName, bool isRoaming)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			Configuration userConfig = this.GetUserConfig(isRoaming);
			ClientSettingsSection configSection = this.GetConfigSection(userConfig, sectionName, false);
			if (configSection != null)
			{
				configSection.SectionInformation.RevertToParent();
				userConfig.Save();
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00020FC8 File Offset: 0x0001F1C8
		internal void WriteSettings(string sectionName, bool isRoaming, IDictionary newSettings)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			Configuration userConfig = this.GetUserConfig(isRoaming);
			ClientSettingsSection configSection = this.GetConfigSection(userConfig, sectionName, true);
			if (configSection != null)
			{
				SettingElementCollection settings = configSection.Settings;
				foreach (object obj in newSettings)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					SettingElement settingElement = settings.Get((string)dictionaryEntry.Key);
					if (settingElement == null)
					{
						settingElement = new SettingElement();
						settingElement.Name = (string)dictionaryEntry.Key;
						settings.Add(settingElement);
					}
					StoredSetting storedSetting = (StoredSetting)dictionaryEntry.Value;
					settingElement.SerializeAs = storedSetting.SerializeAs;
					settingElement.Value.ValueXml = storedSetting.Value;
				}
				try
				{
					userConfig.Save();
					return;
				}
				catch (ConfigurationErrorsException ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("SettingsSaveFailed", new object[] { ex.Message }), ex);
				}
			}
			throw new ConfigurationErrorsException(SR.GetString("SettingsSaveFailedNoSection"));
		}

		// Token: 0x04000C0E RID: 3086
		private const string ApplicationSettingsGroupName = "applicationSettings";

		// Token: 0x04000C0F RID: 3087
		private const string UserSettingsGroupName = "userSettings";

		// Token: 0x04000C10 RID: 3088
		private const string ApplicationSettingsGroupPrefix = "applicationSettings/";

		// Token: 0x04000C11 RID: 3089
		private const string UserSettingsGroupPrefix = "userSettings/";

		// Token: 0x020006E7 RID: 1767
		private sealed class ClientSettingsConfigurationHost : DelegatingConfigHost
		{
			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x06004027 RID: 16423 RVA: 0x0010D416 File Offset: 0x0010B616
			private IInternalConfigClientHost ClientHost
			{
				get
				{
					return (IInternalConfigClientHost)base.Host;
				}
			}

			// Token: 0x17000ED9 RID: 3801
			// (get) Token: 0x06004028 RID: 16424 RVA: 0x0010D423 File Offset: 0x0010B623
			internal static IInternalConfigConfigurationFactory ConfigFactory
			{
				get
				{
					if (ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory == null)
					{
						ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory = (IInternalConfigConfigurationFactory)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.Internal.InternalConfigConfigurationFactory,System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					}
					return ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory;
				}
			}

			// Token: 0x06004029 RID: 16425 RVA: 0x0010D44B File Offset: 0x0010B64B
			private ClientSettingsConfigurationHost()
			{
			}

			// Token: 0x0600402A RID: 16426 RVA: 0x0010D453 File Offset: 0x0010B653
			public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
			{
			}

			// Token: 0x0600402B RID: 16427 RVA: 0x0010D458 File Offset: 0x0010B658
			public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
			{
				ConfigurationUserLevel configurationUserLevel = (ConfigurationUserLevel)hostInitConfigurationParams[0];
				base.Host = (IInternalConfigHost)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.ClientConfigurationHost,System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				string text;
				if (configurationUserLevel != ConfigurationUserLevel.None)
				{
					if (configurationUserLevel != ConfigurationUserLevel.PerUserRoaming)
					{
						if (configurationUserLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
						{
							throw new ArgumentException(SR.GetString("UnknownUserLevel"));
						}
						text = this.ClientHost.GetLocalUserConfigPath();
					}
					else
					{
						text = this.ClientHost.GetRoamingUserConfigPath();
					}
				}
				else
				{
					text = this.ClientHost.GetExeConfigPath();
				}
				base.Host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, configRoot, new object[] { null, null, text });
			}

			// Token: 0x0600402C RID: 16428 RVA: 0x0010D4EC File Offset: 0x0010B6EC
			private bool IsKnownConfigFile(string filename)
			{
				return string.Equals(filename, ConfigurationManagerInternalFactory.Instance.MachineConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ApplicationConfigUri, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600402D RID: 16429 RVA: 0x0010D545 File Offset: 0x0010B745
			internal static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
			{
				return ClientSettingsStore.ClientSettingsConfigurationHost.ConfigFactory.Create(typeof(ClientSettingsStore.ClientSettingsConfigurationHost), new object[] { userLevel });
			}

			// Token: 0x0600402E RID: 16430 RVA: 0x0010D56A File Offset: 0x0010B76A
			public override Stream OpenStreamForRead(string streamName)
			{
				if (this.IsKnownConfigFile(streamName))
				{
					return base.Host.OpenStreamForRead(streamName, true);
				}
				return base.Host.OpenStreamForRead(streamName);
			}

			// Token: 0x0600402F RID: 16431 RVA: 0x0010D590 File Offset: 0x0010B790
			public override Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
			{
				Stream stream;
				if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					stream = new ClientSettingsStore.QuotaEnforcedStream(base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, true), false);
				}
				else if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					stream = new ClientSettingsStore.QuotaEnforcedStream(base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, true), true);
				}
				else
				{
					stream = base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
				}
				return stream;
			}

			// Token: 0x06004030 RID: 16432 RVA: 0x0010D608 File Offset: 0x0010B808
			public override void WriteCompleted(string streamName, bool success, object writeContext)
			{
				if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					base.Host.WriteCompleted(streamName, success, writeContext, true);
					return;
				}
				base.Host.WriteCompleted(streamName, success, writeContext);
			}

			// Token: 0x04003051 RID: 12369
			private const string ClientConfigurationHostTypeName = "System.Configuration.ClientConfigurationHost,System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04003052 RID: 12370
			private const string InternalConfigConfigurationFactoryTypeName = "System.Configuration.Internal.InternalConfigConfigurationFactory,System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04003053 RID: 12371
			private static volatile IInternalConfigConfigurationFactory s_configFactory;
		}

		// Token: 0x020006E8 RID: 1768
		private sealed class QuotaEnforcedStream : Stream
		{
			// Token: 0x06004031 RID: 16433 RVA: 0x0010D659 File Offset: 0x0010B859
			internal QuotaEnforcedStream(Stream originalStream, bool isRoaming)
			{
				this._originalStream = originalStream;
				this._isRoaming = isRoaming;
			}

			// Token: 0x17000EDA RID: 3802
			// (get) Token: 0x06004032 RID: 16434 RVA: 0x0010D66F File Offset: 0x0010B86F
			public override bool CanRead
			{
				get
				{
					return this._originalStream.CanRead;
				}
			}

			// Token: 0x17000EDB RID: 3803
			// (get) Token: 0x06004033 RID: 16435 RVA: 0x0010D67C File Offset: 0x0010B87C
			public override bool CanWrite
			{
				get
				{
					return this._originalStream.CanWrite;
				}
			}

			// Token: 0x17000EDC RID: 3804
			// (get) Token: 0x06004034 RID: 16436 RVA: 0x0010D689 File Offset: 0x0010B889
			public override bool CanSeek
			{
				get
				{
					return this._originalStream.CanSeek;
				}
			}

			// Token: 0x17000EDD RID: 3805
			// (get) Token: 0x06004035 RID: 16437 RVA: 0x0010D696 File Offset: 0x0010B896
			public override long Length
			{
				get
				{
					return this._originalStream.Length;
				}
			}

			// Token: 0x17000EDE RID: 3806
			// (get) Token: 0x06004036 RID: 16438 RVA: 0x0010D6A3 File Offset: 0x0010B8A3
			// (set) Token: 0x06004037 RID: 16439 RVA: 0x0010D6B0 File Offset: 0x0010B8B0
			public override long Position
			{
				get
				{
					return this._originalStream.Position;
				}
				set
				{
					if (value < 0L)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("PositionOutOfRange"));
					}
					this.Seek(value, SeekOrigin.Begin);
				}
			}

			// Token: 0x06004038 RID: 16440 RVA: 0x0010D6D5 File Offset: 0x0010B8D5
			public override void Close()
			{
				this._originalStream.Close();
			}

			// Token: 0x06004039 RID: 16441 RVA: 0x0010D6E2 File Offset: 0x0010B8E2
			protected override void Dispose(bool disposing)
			{
				if (disposing && this._originalStream != null)
				{
					((IDisposable)this._originalStream).Dispose();
					this._originalStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600403A RID: 16442 RVA: 0x0010D708 File Offset: 0x0010B908
			public override void Flush()
			{
				this._originalStream.Flush();
			}

			// Token: 0x0600403B RID: 16443 RVA: 0x0010D718 File Offset: 0x0010B918
			public override void SetLength(long value)
			{
				long length = this._originalStream.Length;
				this.EnsureQuota(Math.Max(length, value));
				this._originalStream.SetLength(value);
			}

			// Token: 0x0600403C RID: 16444 RVA: 0x0010D74C File Offset: 0x0010B94C
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this._originalStream.Read(buffer, offset, count);
			}

			// Token: 0x0600403D RID: 16445 RVA: 0x0010D75C File Offset: 0x0010B95C
			public override int ReadByte()
			{
				return this._originalStream.ReadByte();
			}

			// Token: 0x0600403E RID: 16446 RVA: 0x0010D76C File Offset: 0x0010B96C
			public override long Seek(long offset, SeekOrigin origin)
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = offset;
					break;
				case SeekOrigin.Current:
					num = this._originalStream.Position + offset;
					break;
				case SeekOrigin.End:
					num = length + offset;
					break;
				default:
					throw new ArgumentException(SR.GetString("UnknownSeekOrigin"), "origin");
				}
				this.EnsureQuota(Math.Max(length, num));
				return this._originalStream.Seek(offset, origin);
			}

			// Token: 0x0600403F RID: 16447 RVA: 0x0010D7F0 File Offset: 0x0010B9F0
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long num = (this._originalStream.CanSeek ? (this._originalStream.Position + (long)count) : (this._originalStream.Length + (long)count));
				this.EnsureQuota(Math.Max(length, num));
				this._originalStream.Write(buffer, offset, count);
			}

			// Token: 0x06004040 RID: 16448 RVA: 0x0010D860 File Offset: 0x0010BA60
			public override void WriteByte(byte value)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long num = (this._originalStream.CanSeek ? (this._originalStream.Position + 1L) : (this._originalStream.Length + 1L));
				this.EnsureQuota(Math.Max(length, num));
				this._originalStream.WriteByte(value);
			}

			// Token: 0x06004041 RID: 16449 RVA: 0x0010D8CC File Offset: 0x0010BACC
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
			{
				return this._originalStream.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
			}

			// Token: 0x06004042 RID: 16450 RVA: 0x0010D8E0 File Offset: 0x0010BAE0
			public override int EndRead(IAsyncResult asyncResult)
			{
				return this._originalStream.EndRead(asyncResult);
			}

			// Token: 0x06004043 RID: 16451 RVA: 0x0010D8F0 File Offset: 0x0010BAF0
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long num = (this._originalStream.CanSeek ? (this._originalStream.Position + (long)numBytes) : (this._originalStream.Length + (long)numBytes));
				this.EnsureQuota(Math.Max(length, num));
				return this._originalStream.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
			}

			// Token: 0x06004044 RID: 16452 RVA: 0x0010D962 File Offset: 0x0010BB62
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this._originalStream.EndWrite(asyncResult);
			}

			// Token: 0x06004045 RID: 16453 RVA: 0x0010D970 File Offset: 0x0010BB70
			private void EnsureQuota(long size)
			{
				new IsolatedStorageFilePermission(PermissionState.None)
				{
					UserQuota = size,
					UsageAllowed = (this._isRoaming ? IsolatedStorageContainment.DomainIsolationByRoamingUser : IsolatedStorageContainment.DomainIsolationByUser)
				}.Demand();
			}

			// Token: 0x04003054 RID: 12372
			private Stream _originalStream;

			// Token: 0x04003055 RID: 12373
			private bool _isRoaming;
		}
	}
}

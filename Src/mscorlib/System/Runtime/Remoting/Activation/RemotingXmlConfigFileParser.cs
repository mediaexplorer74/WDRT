using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089C RID: 2204
	internal static class RemotingXmlConfigFileParser
	{
		// Token: 0x06005D52 RID: 23890 RVA: 0x0014848F File Offset: 0x0014668F
		private static Hashtable CreateSyncCaseInsensitiveHashtable()
		{
			return Hashtable.Synchronized(RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable());
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0014849B File Offset: 0x0014669B
		private static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001484A8 File Offset: 0x001466A8
		public static RemotingXmlConfigFileData ParseDefaultConfiguration()
		{
			ConfigNode configNode = new ConfigNode("system.runtime.remoting", null);
			ConfigNode configNode2 = new ConfigNode("application", configNode);
			configNode.Children.Add(configNode2);
			ConfigNode configNode3 = new ConfigNode("channels", configNode2);
			configNode2.Children.Add(configNode3);
			ConfigNode configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "http client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "http client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "tcp client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "tcp client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "ipc client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "ipc client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode3 = new ConfigNode("channels", configNode);
			configNode.Children.Add(configNode3);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			ConfigNode configNode5 = new ConfigNode("channelSinkProviders", configNode);
			configNode.Children.Add(configNode5);
			ConfigNode configNode6 = new ConfigNode("clientProviders", configNode5);
			configNode5.Children.Add(configNode6);
			configNode4 = new ConfigNode("formatter", configNode6);
			configNode4.Attributes.Add(new DictionaryEntry("id", "soap"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.SoapClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode6.Children.Add(configNode4);
			configNode4 = new ConfigNode("formatter", configNode6);
			configNode4.Attributes.Add(new DictionaryEntry("id", "binary"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode6.Children.Add(configNode4);
			ConfigNode configNode7 = new ConfigNode("serverProviders", configNode5);
			configNode5.Children.Add(configNode7);
			configNode4 = new ConfigNode("formatter", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "soap"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.SoapServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			configNode4 = new ConfigNode("formatter", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "binary"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			configNode4 = new ConfigNode("provider", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "wsdl"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.MetadataServices.SdlChannelSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			return RemotingXmlConfigFileParser.ParseConfigNode(configNode);
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00148AC8 File Offset: 0x00146CC8
		public static RemotingXmlConfigFileData ParseConfigFile(string filename)
		{
			ConfigTreeParser configTreeParser = new ConfigTreeParser();
			ConfigNode configNode = configTreeParser.Parse(filename, "/configuration/system.runtime.remoting");
			return RemotingXmlConfigFileParser.ParseConfigNode(configNode);
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00148AF0 File Offset: 0x00146CF0
		private static RemotingXmlConfigFileData ParseConfigNode(ConfigNode rootNode)
		{
			RemotingXmlConfigFileData remotingXmlConfigFileData = new RemotingXmlConfigFileData();
			if (rootNode == null)
			{
				return null;
			}
			foreach (DictionaryEntry dictionaryEntry in rootNode.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				text == "version";
			}
			ConfigNode configNode = null;
			ConfigNode configNode2 = null;
			ConfigNode configNode3 = null;
			ConfigNode configNode4 = null;
			ConfigNode configNode5 = null;
			foreach (ConfigNode configNode6 in rootNode.Children)
			{
				string name = configNode6.Name;
				if (!(name == "application"))
				{
					if (!(name == "channels"))
					{
						if (!(name == "channelSinkProviders"))
						{
							if (!(name == "debug"))
							{
								if (name == "customErrors")
								{
									if (configNode5 != null)
									{
										RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode5, remotingXmlConfigFileData);
									}
									configNode5 = configNode6;
								}
							}
							else
							{
								if (configNode4 != null)
								{
									RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode4, remotingXmlConfigFileData);
								}
								configNode4 = configNode6;
							}
						}
						else
						{
							if (configNode3 != null)
							{
								RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode3, remotingXmlConfigFileData);
							}
							configNode3 = configNode6;
						}
					}
					else
					{
						if (configNode2 != null)
						{
							RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode2, remotingXmlConfigFileData);
						}
						configNode2 = configNode6;
					}
				}
				else
				{
					if (configNode != null)
					{
						RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode, remotingXmlConfigFileData);
					}
					configNode = configNode6;
				}
			}
			if (configNode4 != null)
			{
				RemotingXmlConfigFileParser.ProcessDebugNode(configNode4, remotingXmlConfigFileData);
			}
			if (configNode3 != null)
			{
				RemotingXmlConfigFileParser.ProcessChannelSinkProviderTemplates(configNode3, remotingXmlConfigFileData);
			}
			if (configNode2 != null)
			{
				RemotingXmlConfigFileParser.ProcessChannelTemplates(configNode2, remotingXmlConfigFileData);
			}
			if (configNode != null)
			{
				RemotingXmlConfigFileParser.ProcessApplicationNode(configNode, remotingXmlConfigFileData);
			}
			if (configNode5 != null)
			{
				RemotingXmlConfigFileParser.ProcessCustomErrorsNode(configNode5, remotingXmlConfigFileData);
			}
			return remotingXmlConfigFileData;
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x00148C90 File Offset: 0x00146E90
		private static void ReportError(string errorStr, RemotingXmlConfigFileData configData)
		{
			throw new RemotingException(errorStr);
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x00148C98 File Offset: 0x00146E98
		private static void ReportUniqueSectionError(ConfigNode parent, ConfigNode child, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NodeMustBeUnique"), child.Name, parent.Name), configData);
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x00148CC0 File Offset: 0x00146EC0
		private static void ReportUnknownValueError(ConfigNode node, string value, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnknownValue"), node.Name, value), configData);
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x00148CE3 File Offset: 0x00146EE3
		private static void ReportMissingAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportMissingAttributeError(node.Name, attributeName, configData);
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x00148CF2 File Offset: 0x00146EF2
		private static void ReportMissingAttributeError(string nodeDescription, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_RequiredXmlAttribute"), nodeDescription, attributeName), configData);
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x00148D10 File Offset: 0x00146F10
		private static void ReportMissingTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingTypeAttribute"), node.Name, attributeName), configData);
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x00148D33 File Offset: 0x00146F33
		private static void ReportMissingXmlTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingXmlTypeAttribute"), node.Name, attributeName), configData);
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x00148D56 File Offset: 0x00146F56
		private static void ReportInvalidTimeFormatError(string time, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidTimeFormat"), time), configData);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x00148D73 File Offset: 0x00146F73
		private static void ReportNonTemplateIdAttributeError(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NonTemplateIdAttribute"), node.Name), configData);
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x00148D95 File Offset: 0x00146F95
		private static void ReportTemplateCannotReferenceTemplateError(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TemplateCannotReferenceTemplate"), node.Name), configData);
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00148DB7 File Offset: 0x00146FB7
		private static void ReportUnableToResolveTemplateReferenceError(ConfigNode node, string referenceName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnableToResolveTemplate"), node.Name, referenceName), configData);
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x00148DDA File Offset: 0x00146FDA
		private static void ReportAssemblyVersionInfoPresent(string assemName, string entryDescription, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_VersionPresent"), assemName, entryDescription), configData);
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00148DF8 File Offset: 0x00146FF8
		private static void ProcessDebugNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text == "loadTypes")
				{
					RemotingXmlConfigFileData.LoadTypes = Convert.ToBoolean((string)dictionaryEntry.Value, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x00148E7C File Offset: 0x0014707C
		private static void ProcessApplicationNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text.Equals("name"))
				{
					configData.ApplicationName = (string)dictionaryEntry.Value;
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "channels"))
				{
					if (!(name == "client"))
					{
						if (!(name == "lifetime"))
						{
							if (!(name == "service"))
							{
								if (name == "soapInterop")
								{
									RemotingXmlConfigFileParser.ProcessSoapInteropNode(configNode, configData);
								}
							}
							else
							{
								RemotingXmlConfigFileParser.ProcessServiceNode(configNode, configData);
							}
						}
						else
						{
							RemotingXmlConfigFileParser.ProcessLifetimeNode(node, configNode, configData);
						}
					}
					else
					{
						RemotingXmlConfigFileParser.ProcessClientNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessChannelsNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x00148FB8 File Offset: 0x001471B8
		private static void ProcessCustomErrorsNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text.Equals("mode"))
				{
					string text2 = (string)dictionaryEntry.Value;
					CustomErrorsModes customErrorsModes = CustomErrorsModes.On;
					if (string.Compare(text2, "on", StringComparison.OrdinalIgnoreCase) == 0)
					{
						customErrorsModes = CustomErrorsModes.On;
					}
					else if (string.Compare(text2, "off", StringComparison.OrdinalIgnoreCase) == 0)
					{
						customErrorsModes = CustomErrorsModes.Off;
					}
					else if (string.Compare(text2, "remoteonly", StringComparison.OrdinalIgnoreCase) == 0)
					{
						customErrorsModes = CustomErrorsModes.RemoteOnly;
					}
					else
					{
						RemotingXmlConfigFileParser.ReportUnknownValueError(node, text2, configData);
					}
					configData.CustomErrors = new RemotingXmlConfigFileData.CustomErrorsEntry(customErrorsModes);
				}
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x00149084 File Offset: 0x00147284
		private static void ProcessLifetimeNode(ConfigNode parentNode, ConfigNode node, RemotingXmlConfigFileData configData)
		{
			if (configData.Lifetime != null)
			{
				RemotingXmlConfigFileParser.ReportUniqueSectionError(node, parentNode, configData);
			}
			configData.Lifetime = new RemotingXmlConfigFileData.LifetimeEntry();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (!(text == "leaseTime"))
				{
					if (!(text == "sponsorshipTimeout"))
					{
						if (!(text == "renewOnCallTime"))
						{
							if (text == "leaseManagerPollTime")
							{
								configData.Lifetime.LeaseManagerPollTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
							}
						}
						else
						{
							configData.Lifetime.RenewOnCallTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
						}
					}
					else
					{
						configData.Lifetime.SponsorshipTimeout = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
					}
				}
				else
				{
					configData.Lifetime.LeaseTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
				}
			}
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x001491B0 File Offset: 0x001473B0
		private static void ProcessServiceNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "wellknown"))
				{
					if (name == "activated")
					{
						RemotingXmlConfigFileParser.ProcessServiceActivatedNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessServiceWellKnownNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00149230 File Offset: 0x00147430
		private static void ProcessClientNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text2 = dictionaryEntry.Key.ToString();
				if (!(text2 == "url"))
				{
					if (!(text2 == "displayName"))
					{
					}
				}
				else
				{
					text = (string)dictionaryEntry.Value;
				}
			}
			RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = configData.AddRemoteAppEntry(text);
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "wellknown"))
				{
					if (name == "activated")
					{
						RemotingXmlConfigFileParser.ProcessClientActivatedNode(configNode, configData, remoteAppEntry);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessClientWellKnownNode(configNode, configData, remoteAppEntry);
				}
			}
			if (remoteAppEntry.ActivatedObjects.Count > 0 && text == null)
			{
				RemotingXmlConfigFileParser.ReportMissingAttributeError(node, "url", configData);
			}
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x00149354 File Offset: 0x00147554
		private static void ProcessSoapInteropNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text == "urlObjRef")
				{
					configData.UrlObjRefMode = Convert.ToBoolean(dictionaryEntry.Value, CultureInfo.InvariantCulture);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "preLoad"))
				{
					if (!(name == "interopXmlElement"))
					{
						if (name == "interopXmlType")
						{
							RemotingXmlConfigFileParser.ProcessInteropXmlTypeNode(configNode, configData);
						}
					}
					else
					{
						RemotingXmlConfigFileParser.ProcessInteropXmlElementNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessPreLoadNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x00149460 File Offset: 0x00147660
		private static void ProcessChannelsNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				if (configNode.Name.Equals("channel"))
				{
					RemotingXmlConfigFileData.ChannelEntry channelEntry = RemotingXmlConfigFileParser.ProcessChannelsChannelNode(configNode, configData, false);
					configData.ChannelEntries.Add(channelEntry);
				}
			}
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x001494D4 File Offset: 0x001476D4
		private static void ProcessServiceWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			string text3 = null;
			WellKnownObjectMode wellKnownObjectMode = WellKnownObjectMode.Singleton;
			bool flag = false;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text4 = dictionaryEntry.Key.ToString();
				if (!(text4 == "displayName"))
				{
					if (!(text4 == "mode"))
					{
						if (!(text4 == "objectUri"))
						{
							if (text4 == "type")
							{
								RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
							}
						}
						else
						{
							text3 = (string)dictionaryEntry.Value;
						}
					}
					else
					{
						string text5 = (string)dictionaryEntry.Value;
						flag = true;
						if (string.CompareOrdinal(text5, "Singleton") == 0)
						{
							wellKnownObjectMode = WellKnownObjectMode.Singleton;
						}
						else if (string.CompareOrdinal(text5, "SingleCall") == 0)
						{
							wellKnownObjectMode = WellKnownObjectMode.SingleCall;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "contextAttribute"))
				{
					if (!(name == "lifetime"))
					{
					}
				}
				else
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (!flag)
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_MissingWellKnownModeAttribute"), configData);
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (text3 == null)
			{
				text3 = text + ".soap";
			}
			configData.AddServerWellKnownEntry(text, text2, arrayList, text3, wellKnownObjectMode);
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x0014969C File Offset: 0x0014789C
		private static void ProcessServiceActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = dictionaryEntry.Key.ToString();
				if (text3 == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "contextAttribute"))
				{
					if (!(name == "lifetime"))
					{
					}
				}
				else
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(text2))
			{
				RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(text2, "service activated", configData);
			}
			configData.AddServerActivatedEntry(text, text2, arrayList);
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x001497C8 File Offset: 0x001479C8
		private static void ProcessClientWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text4 = dictionaryEntry.Key.ToString();
				if (!(text4 == "displayName"))
				{
					if (!(text4 == "type"))
					{
						if (text4 == "url")
						{
							text3 = (string)dictionaryEntry.Value;
						}
					}
					else
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
					}
				}
			}
			if (text3 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingAttributeError("WellKnown client", "url", configData);
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(text2))
			{
				RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(text2, "client wellknown", configData);
			}
			remoteApp.AddWellKnownEntry(text, text2, text3);
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x001498BC File Offset: 0x00147ABC
		private static void ProcessClientActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = dictionaryEntry.Key.ToString();
				if (text3 == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (name == "contextAttribute")
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			remoteApp.AddActivatedEntry(text, text2, arrayList);
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001499C4 File Offset: 0x00147BC4
		private static void ProcessInteropXmlElementNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text5 = dictionaryEntry.Key.ToString();
				if (!(text5 == "xml"))
				{
					if (text5 == "clr")
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text3, out text4);
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
			}
			if (text3 == null || text4 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
			}
			configData.AddInteropXmlElementEntry(text, text2, text3, text4);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x00149AA0 File Offset: 0x00147CA0
		private static void ProcessInteropXmlTypeNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text5 = dictionaryEntry.Key.ToString();
				if (!(text5 == "xml"))
				{
					if (text5 == "clr")
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text3, out text4);
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
			}
			if (text3 == null || text4 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
			}
			configData.AddInteropXmlTypeEntry(text, text2, text3, text4);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x00149B7C File Offset: 0x00147D7C
		private static void ProcessPreLoadNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = dictionaryEntry.Key.ToString();
				if (!(text3 == "type"))
				{
					if (text3 == "assembly")
					{
						text2 = (string)dictionaryEntry.Value;
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			if (text2 == null)
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_PreloadRequiresTypeOrAssembly"), configData);
			}
			configData.AddPreLoadEntry(text, text2);
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00149C38 File Offset: 0x00147E38
		private static RemotingXmlConfigFileData.ContextAttributeEntry ProcessContextAttributeNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = ((string)dictionaryEntry.Key).ToLower(CultureInfo.InvariantCulture);
				if (text3 == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
				else
				{
					hashtable[text3] = dictionaryEntry.Value;
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			return new RemotingXmlConfigFileData.ContextAttributeEntry(text, text2, hashtable);
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x00149CF8 File Offset: 0x00147EF8
		private static RemotingXmlConfigFileData.ChannelEntry ProcessChannelsChannelNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			bool flag = false;
			RemotingXmlConfigFileData.ChannelEntry channelEntry = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text4 = (string)dictionaryEntry.Key;
				if (!(text4 == "displayName"))
				{
					if (!(text4 == "id"))
					{
						if (!(text4 == "ref"))
						{
							if (!(text4 == "type"))
							{
								if (!(text4 == "delayLoadAsClientChannel"))
								{
									hashtable[text4] = dictionaryEntry.Value;
									continue;
								}
								flag = Convert.ToBoolean((string)dictionaryEntry.Value, CultureInfo.InvariantCulture);
								continue;
							}
						}
						else
						{
							if (isTemplate)
							{
								RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
								continue;
							}
							channelEntry = (RemotingXmlConfigFileData.ChannelEntry)RemotingXmlConfigFileParser._channelTemplates[dictionaryEntry.Value];
							if (channelEntry == null)
							{
								RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, dictionaryEntry.Value.ToString(), configData);
								continue;
							}
							text2 = channelEntry.TypeName;
							text3 = channelEntry.AssemblyName;
							using (IDictionaryEnumerator enumerator2 = channelEntry.Properties.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj = enumerator2.Current;
									DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj;
									hashtable[dictionaryEntry2.Key] = dictionaryEntry2.Value;
								}
								continue;
							}
						}
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text2, out text3);
					}
					else if (!isTemplate)
					{
						RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
					}
					else
					{
						text = ((string)dictionaryEntry.Value).ToLower(CultureInfo.InvariantCulture);
					}
				}
			}
			if (text2 == null || text3 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			RemotingXmlConfigFileData.ChannelEntry channelEntry2 = new RemotingXmlConfigFileData.ChannelEntry(text2, text3, hashtable);
			channelEntry2.DelayLoad = flag;
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "clientProviders"))
				{
					if (name == "serverProviders")
					{
						RemotingXmlConfigFileParser.ProcessSinkProviderNodes(configNode, channelEntry2, configData, true);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessSinkProviderNodes(configNode, channelEntry2, configData, false);
				}
			}
			if (channelEntry != null)
			{
				if (channelEntry2.ClientSinkProviders.Count == 0)
				{
					channelEntry2.ClientSinkProviders = channelEntry.ClientSinkProviders;
				}
				if (channelEntry2.ServerSinkProviders.Count == 0)
				{
					channelEntry2.ServerSinkProviders = channelEntry.ServerSinkProviders;
				}
			}
			if (isTemplate)
			{
				RemotingXmlConfigFileParser._channelTemplates[text] = channelEntry2;
				return null;
			}
			return channelEntry2;
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x00149FF4 File Offset: 0x001481F4
		private static void ProcessSinkProviderNodes(ConfigNode node, RemotingXmlConfigFileData.ChannelEntry channelEntry, RemotingXmlConfigFileData configData, bool isServer)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = RemotingXmlConfigFileParser.ProcessSinkProviderNode(configNode, configData, false, isServer);
				if (isServer)
				{
					channelEntry.ServerSinkProviders.Add(sinkProviderEntry);
				}
				else
				{
					channelEntry.ClientSinkProviders.Add(sinkProviderEntry);
				}
			}
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x0014A06C File Offset: 0x0014826C
		private static RemotingXmlConfigFileData.SinkProviderEntry ProcessSinkProviderNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate, bool isServer)
		{
			bool flag = false;
			string name = node.Name;
			if (name.Equals("formatter"))
			{
				flag = true;
			}
			else if (name.Equals("provider"))
			{
				flag = false;
			}
			else
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_ProviderNeedsElementName"), configData);
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text4 = (string)dictionaryEntry.Key;
				if (!(text4 == "id"))
				{
					if (!(text4 == "ref"))
					{
						if (!(text4 == "type"))
						{
							hashtable[text4] = dictionaryEntry.Value;
							continue;
						}
					}
					else
					{
						if (isTemplate)
						{
							RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
							continue;
						}
						if (isServer)
						{
							sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)RemotingXmlConfigFileParser._serverChannelSinkTemplates[dictionaryEntry.Value];
						}
						else
						{
							sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)RemotingXmlConfigFileParser._clientChannelSinkTemplates[dictionaryEntry.Value];
						}
						if (sinkProviderEntry == null)
						{
							RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, dictionaryEntry.Value.ToString(), configData);
							continue;
						}
						text2 = sinkProviderEntry.TypeName;
						text3 = sinkProviderEntry.AssemblyName;
						using (IDictionaryEnumerator enumerator2 = sinkProviderEntry.Properties.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj;
								hashtable[dictionaryEntry2.Key] = dictionaryEntry2.Value;
							}
							continue;
						}
					}
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text2, out text3);
				}
				else if (!isTemplate)
				{
					RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
				}
				else
				{
					text = (string)dictionaryEntry.Value;
				}
			}
			if (text2 == null || text3 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry2 = new RemotingXmlConfigFileData.SinkProviderEntry(text2, text3, hashtable, flag);
			foreach (ConfigNode configNode in node.Children)
			{
				SinkProviderData sinkProviderData = RemotingXmlConfigFileParser.ProcessSinkProviderData(configNode, configData);
				sinkProviderEntry2.ProviderData.Add(sinkProviderData);
			}
			if (sinkProviderEntry != null && sinkProviderEntry2.ProviderData.Count == 0)
			{
				sinkProviderEntry2.ProviderData = sinkProviderEntry.ProviderData;
			}
			if (isTemplate)
			{
				if (isServer)
				{
					RemotingXmlConfigFileParser._serverChannelSinkTemplates[text] = sinkProviderEntry2;
				}
				else
				{
					RemotingXmlConfigFileParser._clientChannelSinkTemplates[text] = sinkProviderEntry2;
				}
				return null;
			}
			return sinkProviderEntry2;
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0014A344 File Offset: 0x00148544
		private static SinkProviderData ProcessSinkProviderData(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			SinkProviderData sinkProviderData = new SinkProviderData(node.Name);
			foreach (ConfigNode configNode in node.Children)
			{
				SinkProviderData sinkProviderData2 = RemotingXmlConfigFileParser.ProcessSinkProviderData(configNode, configData);
				sinkProviderData.Children.Add(sinkProviderData2);
			}
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				sinkProviderData.Properties[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return sinkProviderData;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0014A408 File Offset: 0x00148608
		private static void ProcessChannelTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (name == "channel")
				{
					RemotingXmlConfigFileParser.ProcessChannelsChannelNode(configNode, configData, true);
				}
			}
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x0014A474 File Offset: 0x00148674
		private static void ProcessChannelSinkProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "clientProviders"))
				{
					if (name == "serverProviders")
					{
						RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(configNode, configData, true);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(configNode, configData, false);
				}
			}
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x0014A4F8 File Offset: 0x001486F8
		private static void ProcessChannelProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData, bool isServer)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				RemotingXmlConfigFileParser.ProcessSinkProviderNode(configNode, configData, true, isServer);
			}
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x0014A550 File Offset: 0x00148750
		private static bool CheckAssemblyNameForVersionInfo(string assemName)
		{
			if (assemName == null)
			{
				return false;
			}
			int num = assemName.IndexOf(',');
			return num != -1;
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x0014A574 File Offset: 0x00148774
		private static TimeSpan ParseTime(string time, RemotingXmlConfigFileData configData)
		{
			string text = time;
			string text2 = "s";
			int num = 0;
			char c = ' ';
			if (time.Length > 0)
			{
				c = time[time.Length - 1];
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds(0.0);
			try
			{
				if (!char.IsDigit(c))
				{
					if (time.Length == 0)
					{
						RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(text, configData);
					}
					time = time.ToLower(CultureInfo.InvariantCulture);
					num = 1;
					if (time.EndsWith("ms", StringComparison.Ordinal))
					{
						num = 2;
					}
					text2 = time.Substring(time.Length - num, num);
				}
				int num2 = int.Parse(time.Substring(0, time.Length - num), CultureInfo.InvariantCulture);
				if (!(text2 == "d"))
				{
					if (!(text2 == "h"))
					{
						if (!(text2 == "m"))
						{
							if (!(text2 == "s"))
							{
								if (!(text2 == "ms"))
								{
									RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(text, configData);
								}
								else
								{
									timeSpan = TimeSpan.FromMilliseconds((double)num2);
								}
							}
							else
							{
								timeSpan = TimeSpan.FromSeconds((double)num2);
							}
						}
						else
						{
							timeSpan = TimeSpan.FromMinutes((double)num2);
						}
					}
					else
					{
						timeSpan = TimeSpan.FromHours((double)num2);
					}
				}
				else
				{
					timeSpan = TimeSpan.FromDays((double)num2);
				}
			}
			catch (Exception)
			{
				RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(text, configData);
			}
			return timeSpan;
		}

		// Token: 0x04002A0F RID: 10767
		private static Hashtable _channelTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();

		// Token: 0x04002A10 RID: 10768
		private static Hashtable _clientChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();

		// Token: 0x04002A11 RID: 10769
		private static Hashtable _serverChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();
	}
}

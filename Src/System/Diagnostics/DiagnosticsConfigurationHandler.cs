using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Handles the diagnostics section of configuration files.</summary>
	// Token: 0x020004C5 RID: 1221
	[Obsolete("This class has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public class DiagnosticsConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>Parses the configuration settings for the &lt;system.diagnostics&gt; Element section of configuration files.</summary>
		/// <param name="parent">The object inherited from the parent path</param>
		/// <param name="configContext">Reserved. Used in ASP.NET to convey the virtual path of the configuration being evaluated.</param>
		/// <param name="section">The root XML node at the section to handle.</param>
		/// <returns>A new configuration object, in the form of a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Switches could not be found.  
		///  -or-  
		///  Assert could not be found.  
		///  -or-  
		///  Trace could not be found.  
		///  -or-  
		///  Performance counters could not be found.</exception>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000CCD94 File Offset: 0x000CAF94
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			HandlerBase.CheckForUnrecognizedAttributes(section);
			Hashtable hashtable = (Hashtable)parent;
			Hashtable hashtable2;
			if (hashtable == null)
			{
				hashtable2 = new Hashtable();
			}
			else
			{
				hashtable2 = (Hashtable)hashtable.Clone();
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					string name = xmlNode.Name;
					if (!(name == "switches"))
					{
						if (!(name == "assert"))
						{
							if (!(name == "trace"))
							{
								if (!(name == "performanceCounters"))
								{
									HandlerBase.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									if (flag4)
									{
										throw new ConfigurationErrorsException(SR.GetString("ConfigSectionsUnique", new object[] { "performanceCounters" }));
									}
									flag4 = true;
									DiagnosticsConfigurationHandler.HandleCounters((Hashtable)parent, hashtable2, xmlNode, configContext);
								}
							}
							else
							{
								if (flag3)
								{
									throw new ConfigurationErrorsException(SR.GetString("ConfigSectionsUnique", new object[] { "trace" }));
								}
								flag3 = true;
								DiagnosticsConfigurationHandler.HandleTrace(hashtable2, xmlNode, configContext);
							}
						}
						else
						{
							if (flag2)
							{
								throw new ConfigurationErrorsException(SR.GetString("ConfigSectionsUnique", new object[] { "assert" }));
							}
							flag2 = true;
							DiagnosticsConfigurationHandler.HandleAssert(hashtable2, xmlNode, configContext);
						}
					}
					else
					{
						if (flag)
						{
							throw new ConfigurationErrorsException(SR.GetString("ConfigSectionsUnique", new object[] { "switches" }));
						}
						flag = true;
						DiagnosticsConfigurationHandler.HandleSwitches(hashtable2, xmlNode, configContext);
					}
					HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
				}
			}
			return hashtable2;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000CCF5C File Offset: 0x000CB15C
		private static void HandleSwitches(Hashtable config, XmlNode switchesNode, object context)
		{
			Hashtable hashtable = (Hashtable)new SwitchesDictionarySectionHandler().Create(config["switches"], context, switchesNode);
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					int.Parse((string)enumerator.Value, CultureInfo.InvariantCulture);
				}
				catch
				{
					throw new ConfigurationErrorsException(SR.GetString("Value_must_be_numeric", new object[] { enumerator.Key }));
				}
			}
			config["switches"] = hashtable;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000CCFEC File Offset: 0x000CB1EC
		private static void HandleAssert(Hashtable config, XmlNode assertNode, object context)
		{
			bool flag = false;
			if (HandlerBase.GetAndRemoveBooleanAttribute(assertNode, "assertuienabled", ref flag) != null)
			{
				config["assertuienabled"] = flag;
			}
			string text = null;
			if (HandlerBase.GetAndRemoveStringAttribute(assertNode, "logfilename", ref text) != null)
			{
				config["logfilename"] = text;
			}
			HandlerBase.CheckForChildNodes(assertNode);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000CD040 File Offset: 0x000CB240
		private static void HandleCounters(Hashtable parent, Hashtable config, XmlNode countersNode, object context)
		{
			int num = 0;
			if (HandlerBase.GetAndRemoveIntegerAttribute(countersNode, "filemappingsize", ref num) != null && parent == null)
			{
				config["filemappingsize"] = num;
			}
			HandlerBase.CheckForChildNodes(countersNode);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000CD078 File Offset: 0x000CB278
		private static void HandleTrace(Hashtable config, XmlNode traceNode, object context)
		{
			bool flag = false;
			bool flag2 = false;
			if (HandlerBase.GetAndRemoveBooleanAttribute(traceNode, "autoflush", ref flag2) != null)
			{
				config["autoflush"] = flag2;
			}
			int num = 0;
			if (HandlerBase.GetAndRemoveIntegerAttribute(traceNode, "indentsize", ref num) != null)
			{
				config["indentsize"] = num;
			}
			foreach (object obj in traceNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "listeners")
					{
						if (flag)
						{
							throw new ConfigurationErrorsException(SR.GetString("ConfigSectionsUnique", new object[] { "listeners" }));
						}
						flag = true;
						DiagnosticsConfigurationHandler.HandleListeners(config, xmlNode, context);
					}
					else
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
				}
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000CD168 File Offset: 0x000CB368
		private static void HandleListeners(Hashtable config, XmlNode listenersNode, object context)
		{
			HandlerBase.CheckForUnrecognizedAttributes(listenersNode);
			foreach (object obj in listenersNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					string text = null;
					string text2 = null;
					string text3 = null;
					string name = xmlNode.Name;
					if (!(name == "add") && !(name == "remove") && !(name == "clear"))
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
					HandlerBase.GetAndRemoveStringAttribute(xmlNode, "name", ref text);
					HandlerBase.GetAndRemoveStringAttribute(xmlNode, "type", ref text2);
					HandlerBase.GetAndRemoveStringAttribute(xmlNode, "initializeData", ref text3);
					HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
					HandlerBase.CheckForChildNodes(xmlNode);
					TraceListener traceListener = null;
					if (text2 != null)
					{
						Type type = Type.GetType(text2);
						if (type == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Could_not_find_type", new object[] { text2 }));
						}
						if (!typeof(TraceListener).IsAssignableFrom(type))
						{
							throw new ConfigurationErrorsException(SR.GetString("Type_isnt_tracelistener", new object[] { text2 }));
						}
						if (text3 == null)
						{
							ConstructorInfo constructor = type.GetConstructor(new Type[0]);
							if (constructor == null)
							{
								throw new ConfigurationErrorsException(SR.GetString("Could_not_get_constructor", new object[] { text2 }));
							}
							traceListener = (TraceListener)SecurityUtils.ConstructorInfoInvoke(constructor, new object[0]);
						}
						else
						{
							ConstructorInfo constructor2 = type.GetConstructor(new Type[] { typeof(string) });
							if (constructor2 == null)
							{
								throw new ConfigurationErrorsException(SR.GetString("Could_not_get_constructor", new object[] { text2 }));
							}
							traceListener = (TraceListener)SecurityUtils.ConstructorInfoInvoke(constructor2, new object[] { text3 });
						}
						if (text != null)
						{
							traceListener.Name = text;
						}
					}
					char c = name[0];
					if (c != 'a')
					{
						if (c != 'c')
						{
							if (c != 'r')
							{
								HandlerBase.ThrowUnrecognizedElement(xmlNode);
							}
							else if (traceListener == null)
							{
								if (text == null)
								{
									throw new ConfigurationErrorsException(SR.GetString("Cannot_remove_with_null"));
								}
								Trace.Listeners.Remove(text);
							}
							else
							{
								Trace.Listeners.Remove(traceListener);
							}
						}
						else
						{
							Trace.Listeners.Clear();
						}
					}
					else
					{
						if (traceListener == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Could_not_create_listener", new object[] { text }));
						}
						Trace.Listeners.Add(traceListener);
					}
				}
			}
		}
	}
}

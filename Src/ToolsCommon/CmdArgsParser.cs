﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x0200000C RID: 12
	public class CmdArgsParser
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003D66 File Offset: 0x00001F66
		public static T ParseArgs<T>(string[] args, params object[] configuration) where T : class, new()
		{
			return CmdArgsParser.ParseArgs<T>(args.ToList<string>(), configuration);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public static T ParseArgs<T>(List<string> args, params object[] configuration) where T : class, new()
		{
			List<CmdModes> list = new List<CmdModes>();
			foreach (object obj in configuration)
			{
				if (!(obj is CmdModes))
				{
					throw new ArgumentException(string.Format("A configuration argument was passed that was not of type \"CmdModes\". ARG={0}", obj));
				}
				list.Add((CmdModes)obj);
			}
			List<string> list2 = new List<string>();
			foreach (string text in args)
			{
				if (text.StartsWith("@"))
				{
					if (!File.Exists(text.Substring(1)))
					{
						throw new FileNotFoundException(string.Format("Response file '{0}' could not be found.", text.Substring(1)));
					}
					foreach (string text2 in File.ReadAllLines(text.Substring(1)))
					{
						list2.Add(text2);
					}
				}
				else
				{
					list2.Add(text);
				}
			}
			args = list2;
			Type typeFromHandle = typeof(T);
			Dictionary<string, string> dictionary = CmdArgsParser.ProcessCommandLine<T>(args, list);
			if (dictionary.ContainsKey("?"))
			{
				CmdArgsParser.ParseUsage<T>(list);
				return default(T);
			}
			while (!list.Contains(CmdModes.DisableCFG) && dictionary.ContainsKey("cfg"))
			{
				string text3 = dictionary["cfg"];
				dictionary.Remove("cfg");
				dictionary = CmdArgsParser.ParseConfig(text3, dictionary, list);
			}
			CmdArgsParser.MissingArguments<T>(dictionary, list);
			dictionary = CmdArgsParser.ExtraArguments<T>(dictionary, list);
			T t = new T();
			using (Dictionary<string, string>.Enumerator enumerator2 = dictionary.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<string, string> commandEntry = enumerator2.Current;
					Type type = typeFromHandle;
					KeyValuePair<string, string> commandEntry14 = commandEntry;
					PropertyInfo property = type.GetProperty(commandEntry14.Key);
					Type type2 = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
					if (type2.IsGenericType)
					{
						if (type2.GetGenericTypeDefinition() == typeof(List<>))
						{
							Type type3 = type2.GetGenericArguments()[0];
							Type type4 = type3;
							KeyValuePair<string, string> commandEntry2 = commandEntry;
							IList list3 = CmdArgsParser.ReflectionListFactory(type4, commandEntry2.Value);
							property.SetValue(t, list3, null);
						}
						else if (type2.GetGenericTypeDefinition() == typeof(Dictionary<, >))
						{
							Type type5 = type2.GetGenericArguments()[0];
							Type type6 = type2.GetGenericArguments()[1];
							Type type7 = type5;
							Type type8 = type6;
							KeyValuePair<string, string> commandEntry3 = commandEntry;
							IDictionary dictionary2 = CmdArgsParser.ReflectionDictionaryFactory(type7, type8, commandEntry3.Value);
							property.SetValue(t, dictionary2, null);
						}
						else
						{
							if (!(type2.GetGenericTypeDefinition() == typeof(HashSet<>)))
							{
								throw new NotImplementedException(string.Format("CmdArgsParser does not support generic type '{0}'.", type2.Name));
							}
							Type type9 = type2.GetGenericArguments()[0];
							Type type10 = type9;
							KeyValuePair<string, string> commandEntry4 = commandEntry;
							string value = commandEntry4.Value;
							KeyValuePair<string, string> commandEntry5 = commandEntry;
							IEnumerable enumerable = CmdArgsParser.ReflectionSetFactory(type10, value, commandEntry5.Key);
							property.SetValue(t, enumerable, null);
						}
					}
					else if (type2.IsEnum)
					{
						if (!Enum.GetNames(type2).Any(delegate(string x)
						{
							KeyValuePair<string, string> commandEntry13 = commandEntry;
							return x.Equals(commandEntry13.Value, StringComparison.OrdinalIgnoreCase);
						}))
						{
							string text4 = Enum.GetNames(type2)[0];
							foreach (string text5 in Enum.GetNames(type2).Skip(1))
							{
								text4 += string.Format(" {0}", text5);
							}
							string text6 = "The value for \"{0}\" is incorrect.\nValue: {1}\nSupported Values: {2}";
							KeyValuePair<string, string> commandEntry6 = commandEntry;
							object key = commandEntry6.Key;
							KeyValuePair<string, string> commandEntry7 = commandEntry;
							throw new ArgumentException(string.Format(text6, key, commandEntry7.Value, text4));
						}
						PropertyInfo propertyInfo = property;
						object obj2 = t;
						Type type11 = type2;
						KeyValuePair<string, string> commandEntry8 = commandEntry;
						propertyInfo.SetValue(obj2, Convert.ChangeType(Enum.Parse(type11, commandEntry8.Value, true), type2), null);
					}
					else
					{
						TypeConverter converter = TypeDescriptor.GetConverter(type2);
						TypeConverter typeConverter = converter;
						KeyValuePair<string, string> commandEntry9 = commandEntry;
						if (!typeConverter.IsValid(commandEntry9.Value))
						{
							string text7 = "The value for \"{0}\" is incorrect.\nValue '{1}' cannot be converted to switch type '{2}'.";
							KeyValuePair<string, string> commandEntry10 = commandEntry;
							object key2 = commandEntry10.Key;
							KeyValuePair<string, string> commandEntry11 = commandEntry;
							throw new ArgumentException(string.Format(text7, key2, commandEntry11.Value, type2.Name));
						}
						PropertyInfo propertyInfo2 = property;
						object obj3 = t;
						KeyValuePair<string, string> commandEntry12 = commandEntry;
						propertyInfo2.SetValue(obj3, Convert.ChangeType(commandEntry12.Value, type2), null);
					}
				}
			}
			return t;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004280 File Offset: 0x00002480
		public static void ParseUsage<T>(List<CmdModes> modes) where T : class, new()
		{
			if (modes.Contains(CmdModes.DisableUsage))
			{
				return;
			}
			int num = 12;
			Type typeFromHandle = typeof(T);
			PropertyInfo[] properties = typeFromHandle.GetProperties();
			T t = new T();
			string text = string.Format("Usage: {0}", Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName));
			foreach (PropertyInfo propertyInfo in properties)
			{
				string text2;
				if (!propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false).Any<object>())
				{
					text2 = string.Format("[{0}]", propertyInfo.Name);
				}
				else
				{
					text2 = propertyInfo.Name;
				}
				if (text2.Length + 1 > num)
				{
					num = text2.Length + 1;
				}
				text += string.Format(" {0}", text2);
			}
			if (!modes.Contains(CmdModes.DisableCFG))
			{
				text += " [cfg]";
			}
			foreach (PropertyInfo propertyInfo2 in properties)
			{
				string text3 = "";
				foreach (DescriptionAttribute descriptionAttribute in propertyInfo2.GetCustomAttributes(typeof(DescriptionAttribute), false))
				{
					text3 = descriptionAttribute.Description;
				}
				string text4 = null;
				string text5;
				if (!propertyInfo2.GetCustomAttributes(typeof(RequiredAttribute), false).Any<object>())
				{
					text5 = string.Format("[{0}]", propertyInfo2.Name);
					if (propertyInfo2.GetValue(t, null) == null)
					{
						text4 = null;
					}
					else
					{
						text4 = propertyInfo2.GetValue(t, null).ToString();
					}
				}
				else
				{
					text5 = propertyInfo2.Name;
				}
				string text6 = new string('·', num - text5.Length);
				string text7 = new string(' ', num);
				string text8 = string.Format("  {0}{1} {2}\n{3}  ", new object[] { text5, text6, text3, text7 });
				Type type = Nullable.GetUnderlyingType(propertyInfo2.PropertyType) ?? propertyInfo2.PropertyType;
				if (type.IsGenericType)
				{
					if (type.GetGenericTypeDefinition() == typeof(List<>))
					{
						text8 += string.Format(" Values:<{0}>, Multiple values seperated by ';'", type.GetGenericArguments()[0].Name);
					}
					else if (type.GetGenericTypeDefinition() == typeof(Dictionary<, >))
					{
						text8 += string.Format(" Values:<{0}={1}>, Multiple values seperated by ';'", type.GetGenericArguments()[0].Name, type.GetGenericArguments()[1].Name);
					}
					else if (type.GetGenericTypeDefinition() == typeof(HashSet<>))
					{
						text8 += string.Format(" Values:<{0}>, Multiple values seperated by ';'\n{1}   Duplicates are not supported.", type.GetGenericArguments()[0].Name, text7);
					}
				}
				else if (type.IsEnum)
				{
					text8 += string.Format(" Values:<{0}", Enum.GetNames(type)[0]);
					foreach (string text9 in Enum.GetNames(type).Skip(1))
					{
						text8 += string.Format(" | {0}", text9);
					}
					text8 += ">";
				}
				else if (type == typeof(bool))
				{
					text8 += string.Format(" Values:<true | false>", new object[0]);
				}
				else if (type == typeof(int))
				{
					text8 += string.Format(" Values:<Number>", new object[0]);
				}
				else if (type == typeof(float))
				{
					text8 += string.Format(" Values:<Decimal>", new object[0]);
				}
				else
				{
					text8 += string.Format(" Values:<Free Text>", new object[0]);
				}
				if (!propertyInfo2.GetCustomAttributes(typeof(RequiredAttribute), false).Any<object>())
				{
					if (text4 == null)
					{
						text8 += string.Format(" Default=NULL", new object[0]);
					}
					else if (propertyInfo2.PropertyType == typeof(string))
					{
						text8 += string.Format(" Default=\"{0}\"", text4);
					}
					else
					{
						text8 += string.Format(" Default={0}", text4);
					}
				}
				text += string.Format("\n{0}", text8);
			}
			if (!modes.Contains(CmdModes.DisableCFG))
			{
				string text10 = new string('·', num - "[cfg]".Length);
				string text11 = new string(' ', num);
				text += string.Format("\n  {0}{1} {2}\n{3}   {4}\n{3}   Values:<Free Text>", new object[] { "[cfg]", text10, "A configuration file used to configure the enviornment.", text11, "If supplied the configuration file will override the command line. Used as named argument only." });
			}
			Console.WriteLine(text);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000047B4 File Offset: 0x000029B4
		public static Dictionary<string, string> ParseConfig(string configLocation, Dictionary<string, string> cmds, List<CmdModes> modes)
		{
			if (string.IsNullOrEmpty(configLocation) || configLocation.Equals("true"))
			{
				throw new ArgumentNullException(string.Format("No configuration file was specified on switch \"cfg\". Please specify a config location with: /cfg:<location>", new object[0]));
			}
			if (!File.Exists(configLocation))
			{
				throw new FileNotFoundException(string.Format("Can't find configuration file {0}.", configLocation));
			}
			XDocument xdocument = XDocument.Load(configLocation, LoadOptions.SetLineInfo);
			if (!xdocument.Root.Name.LocalName.Equals("Configuration"))
			{
				throw new FormatException(string.Format("The format of the provided configuration file is incorrect. Root element was \"{0}\" instead of \"Configuration\". CFG:{1}", xdocument.Root.Name.LocalName, configLocation));
			}
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>();
			foreach (XElement xelement in xdocument.Root.Elements())
			{
				string text = xelement.Name.LocalName.ToString();
				IXmlLineInfo xmlLineInfo = xelement;
				if (xelement.HasElements)
				{
					if (xelement.Attribute("name") == null)
					{
						throw new FormatException(string.Format("Container '{0}' is missing a 'name' attribute. Containers require a name attribute. Error at ({1},{2})", xelement.Name.LocalName, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition));
					}
					text = xelement.Attribute("name").Value.ToString();
				}
				if (string.IsNullOrEmpty(xelement.Value.ToString()) && !xelement.HasElements)
				{
					throw new FormatException(string.Format("Key \"{0}\" does not have an associated value. Null values are not supported in the configuration file. Keys need to be in to format: <\"key\">value</\"key\">.", xelement.Name.LocalName));
				}
				string text2 = xelement.Value.ToString();
				if (hashSet.Contains(text))
				{
					list.Add(string.Format("Key: {0} at ({1},{2})", text, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition));
				}
				else
				{
					hashSet.Add(text);
					bool flag = false;
					if (xelement.HasElements)
					{
						flag = true;
						XElement xelement2 = xelement.Elements().First<XElement>();
						if (xelement.Name.LocalName == "List" || xelement.Name.LocalName == "HashSet")
						{
							if (xelement2.Name.LocalName != "Value")
							{
								throw new FormatException(string.Format("Element under Container '{0}' is not reconized. '{1}' is not supported. Use 'Value' for tags. Error at ({2},{3})", new object[]
								{
									xelement.Name.LocalName,
									xelement2.Name.LocalName,
									xmlLineInfo.LineNumber,
									xmlLineInfo.LinePosition
								}));
							}
							text2 = string.Format("{0}", xelement2.Value.ToString());
						}
						else
						{
							if (!(xelement.Name.LocalName == "Dictionary"))
							{
								throw new FormatException(string.Format("Container '{0}' is not reconized. Supported: 'List', 'HashSet', 'Dictionary'. Error at ({1},{2})", xelement.Name.LocalName, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition));
							}
							if (xelement2.Name.LocalName.ToString().Contains(' '))
							{
								throw new FormatException(string.Format("'{0}' is not allowed for use as a Key in a Dictionary. Spaces are not supported in Key names. Error at ({1},{2})", xelement2.Name.LocalName, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition));
							}
							text2 = string.Format("{0}={1}", xelement2.Name.LocalName.ToString(), xelement2.Value.ToString());
						}
						foreach (XElement xelement3 in xelement.Elements().Skip(1))
						{
							if (xelement.Name.LocalName == "List" || xelement.Name.LocalName == "HashSet")
							{
								if (xelement3.Name.LocalName != "Value")
								{
									throw new FormatException(string.Format("Element under Container '{0}' is not reconized. '{1}' is not supported. Use 'Value' for tags. Error at ({2},{3})", new object[]
									{
										xelement.Name.LocalName,
										xelement3.Name.LocalName,
										xmlLineInfo.LineNumber,
										xmlLineInfo.LinePosition
									}));
								}
								text2 += string.Format(";{0}", xelement3.Value.ToString());
							}
							else
							{
								if (xelement3.Name.LocalName.ToString().Contains(' '))
								{
									throw new FormatException(string.Format("'{0}' is not allowed for use as a Key in a Dictionary. Spaces are not supported in Key names. Error at ({1},{2})", xelement3.Name.LocalName, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition));
								}
								text2 += string.Format(";{0}={1}", xelement3.Name.LocalName.ToString(), xelement3.Value.ToString());
							}
						}
					}
					if (cmds.ContainsKey(text))
					{
						if (modes.Contains(CmdModes.CFGOverride))
						{
							cmds[text] = text2;
						}
						else if (flag)
						{
							string text3;
							cmds[text3 = text] = cmds[text3] + string.Format(";{0}", text2);
						}
					}
					else
					{
						cmds.Add(text, text2);
					}
				}
			}
			if (list.Any<string>())
			{
				string text4 = list.First<string>();
				foreach (string text5 in list.Skip(1))
				{
					text4 += string.Format("\n{0}", text5);
				}
				throw new FormatException(string.Format("There were {0} duplicate entries in {1}. Duplicate Keys:\n{2}", list.Count<string>(), configLocation, text4));
			}
			return cmds;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004DC4 File Offset: 0x00002FC4
		private static void MissingArguments<T>(Dictionary<string, string> commandTable, List<CmdModes> modes) where T : class, new()
		{
			Type typeFromHandle = typeof(T);
			List<string> list = new List<string>();
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false).Any<object>() && !commandTable.ContainsKey(propertyInfo.Name))
				{
					list.Add(propertyInfo.Name);
				}
			}
			if (list.Any<string>())
			{
				CmdArgsParser.ParseUsage<T>(modes);
				string text = string.Format("\"{0}\"", list.First<string>());
				foreach (string text2 in list.Skip(1))
				{
					text += string.Format(", \"{0}\"", text2);
				}
				throw new ArgumentNullException(string.Format("Required argument {0} {1} not specified.", text, (list.Count > 1) ? "were" : "was"));
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004ED0 File Offset: 0x000030D0
		private static Dictionary<string, string> ExtraArguments<T>(Dictionary<string, string> commandTable, List<CmdModes> modes) where T : class, new()
		{
			Type typeFromHandle = typeof(T);
			List<string> list = new List<string>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in commandTable)
			{
				if (typeFromHandle.GetProperty(keyValuePair.Key) == null)
				{
					bool flag = true;
					if (modes.Contains(CmdModes.LegacySwitchFormat))
					{
						foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
						{
							if (propertyInfo.Name.StartsWith(keyValuePair.Key, StringComparison.InvariantCultureIgnoreCase))
							{
								dictionary[propertyInfo.Name] = keyValuePair.Value;
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						list.Add(keyValuePair.Key);
					}
				}
				else
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			if (list.Any<string>())
			{
				CmdArgsParser.ParseUsage<T>(modes);
				string text = string.Format("\"{0}\"", list.First<string>());
				foreach (string text2 in list.Skip(1))
				{
					text += string.Format(", \"{0}\"", text2);
				}
				throw new ArgumentOutOfRangeException(string.Format("Unknown argument {0} {1} provided.", text, (list.Count > 1) ? "were" : "was"));
			}
			return dictionary;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000506C File Offset: 0x0000326C
		private static Dictionary<string, string> ProcessCommandLine<T>(List<string> args, List<CmdModes> modes) where T : class, new()
		{
			Type typeFromHandle = typeof(T);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = false;
			foreach (string text in args)
			{
				string text2 = "true";
				bool flag2 = false;
				if (modes.Contains(CmdModes.LegacySwitchFormat))
				{
					if (text.First<char>() == '-')
					{
						flag2 = true;
						text2 = "false";
					}
					else if (text.First<char>() == '+')
					{
						flag2 = true;
					}
				}
				else if (text.First<char>() == '-' || text.First<char>() == '+')
				{
					throw new FormatException(string.Format("Argument {0} is in the wrong format. Legacy arguments are not supported. ARG={1}", dictionary.Count + 1, text));
				}
				string text3;
				if (text.First<char>() != '/' && !flag2)
				{
					if (flag)
					{
						throw new FormatException(string.Format("Argument {0} is in the wrong format. After a named argument all arguments must be named. ARG={1}", dictionary.Count + 1, text));
					}
					if (dictionary.Count >= typeFromHandle.GetProperties().Count<PropertyInfo>())
					{
						throw new ArgumentException(string.Format("To many positional arguments supplied. Amount allowed: {0}\nOffending Argument: {1}", typeFromHandle.GetProperties().Count<PropertyInfo>(), text));
					}
					text3 = typeFromHandle.GetProperties()[dictionary.Count].Name;
					text2 = text;
				}
				else
				{
					flag = true;
					if (text.IndexOf(":") == -1)
					{
						text3 = text.Substring(1);
					}
					else
					{
						text3 = text.Substring(1, text.IndexOf(":") - 1);
					}
					if (text.IndexOf(":") != -1)
					{
						text2 = text.Substring(text.IndexOf(":") + 1);
					}
					else if (typeFromHandle.GetProperty(text3) != null)
					{
						PropertyInfo property = typeFromHandle.GetProperty(text3);
						Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
						if (!type.IsAssignableFrom(typeof(bool)))
						{
							throw new ArgumentException(string.Format("{0} was used as a 'Boolean' switch however the switch type is '{1}'.", text3, type.Name));
						}
					}
				}
				if (dictionary.ContainsKey(text3))
				{
					throw new FormatException(string.Format("Argument {0} has already been declared. ARG={1}", text3, text));
				}
				dictionary.Add(text3, text2);
			}
			return dictionary;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000052A0 File Offset: 0x000034A0
		public static IList ReflectionListFactory(Type contentType, string dataSource)
		{
			Type type = typeof(List<>).MakeGenericType(new Type[] { contentType });
			IList list = (IList)Activator.CreateInstance(type);
			foreach (string text in dataSource.Split(new char[] { ';' }))
			{
				if (contentType.IsEnum)
				{
					list.Add(Convert.ChangeType(Enum.Parse(contentType, text), contentType));
				}
				else
				{
					list.Add(Convert.ChangeType(text, contentType));
				}
			}
			return list;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005334 File Offset: 0x00003534
		public static IDictionary ReflectionDictionaryFactory(Type keyType, Type valueType, string dataSource)
		{
			Type type = typeof(Dictionary<, >).MakeGenericType(new Type[] { keyType, valueType });
			IDictionary dictionary = (IDictionary)Activator.CreateInstance(type);
			HashSet<object> hashSet = new HashSet<object>();
			foreach (string text in dataSource.Split(new char[] { ';' }))
			{
				string[] array2 = text.Split(new char[] { '=' });
				if (array2.Count<string>() != 2)
				{
					throw new FormatException(string.Format("The format of a dictionary argument is incorrect. The format is 'key=value'. Offending value: '{0}'", text));
				}
				object obj;
				if (keyType.IsEnum)
				{
					obj = Convert.ChangeType(Enum.Parse(keyType, array2[0]), keyType);
				}
				else
				{
					obj = Convert.ChangeType(array2[0], keyType);
				}
				if (!hashSet.Contains(obj))
				{
					hashSet.Add(obj);
					object obj2;
					if (valueType.IsEnum)
					{
						obj2 = Convert.ChangeType(Enum.Parse(valueType, array2[1]), valueType);
					}
					else
					{
						obj2 = Convert.ChangeType(array2[1], valueType);
					}
					dictionary.Add(obj, obj2);
				}
			}
			return dictionary;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005450 File Offset: 0x00003650
		public static IEnumerable ReflectionSetFactory(Type contentType, string dataSource, string name)
		{
			Type type = typeof(HashSet<>).MakeGenericType(new Type[] { contentType });
			IEnumerable enumerable = (IEnumerable)Activator.CreateInstance(type);
			MethodInfo method = type.GetMethod("Add");
			MethodInfo method2 = type.GetMethod("Contains");
			new HashSet<object>();
			List<string> list = new List<string>();
			foreach (string text in dataSource.Split(new char[] { ';' }))
			{
				if (contentType.IsEnum)
				{
					object obj = Convert.ChangeType(Enum.Parse(contentType, text), contentType);
					if ((bool)method2.Invoke(enumerable, new object[] { obj }))
					{
						list.Add(obj.ToString());
					}
					else
					{
						method.Invoke(enumerable, new object[] { obj });
					}
				}
				else
				{
					object obj2 = Convert.ChangeType(text, contentType);
					if ((bool)method2.Invoke(enumerable, new object[] { obj2 }))
					{
						list.Add(obj2.ToString());
					}
					else
					{
						method.Invoke(enumerable, new object[] { obj2 });
					}
				}
			}
			if (list.Any<string>())
			{
				string text2 = string.Format("\"{0}\"", list.First<string>());
				foreach (string text3 in list.Skip(1))
				{
					text2 += string.Format(", \"{0}\"", text3);
				}
				throw new FormatException(string.Format("HashSet '{0}' had {1} duplicate value{2}. Duplicates: {3}", new object[]
				{
					name,
					list.Count<string>(),
					(list.Count<string>() > 1) ? "s" : "",
					text2
				}));
			}
			return enumerable;
		}

		// Token: 0x0400002D RID: 45
		private const int HELP_INDENTATION = 12;
	}
}

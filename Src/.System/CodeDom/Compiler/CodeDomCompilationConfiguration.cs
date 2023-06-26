using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066C RID: 1644
	internal class CodeDomCompilationConfiguration
	{
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000F5D03 File Offset: 0x000F3F03
		internal static CodeDomCompilationConfiguration Default
		{
			get
			{
				return CodeDomCompilationConfiguration.defaultInstance;
			}
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000F5D0C File Offset: 0x000F3F0C
		internal CodeDomCompilationConfiguration()
		{
			this._compilerLanguages = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._compilerExtensions = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._allCompilerInfo = new ArrayList();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			string text = "Microsoft.CSharp.CSharpCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			CompilerInfo compilerInfo = new CompilerInfo(compilerParameters, text);
			compilerInfo._compilerLanguages = new string[] { "c#", "cs", "csharp" };
			compilerInfo._compilerExtensions = new string[] { ".cs", "cs" };
			compilerInfo._providerOptions = new Dictionary<string, string>();
			compilerInfo._providerOptions["CompilerVersion"] = "v4.0";
			this.AddCompilerInfo(compilerInfo);
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			text = "Microsoft.VisualBasic.VBCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			compilerInfo = new CompilerInfo(compilerParameters, text);
			compilerInfo._compilerLanguages = new string[] { "vb", "vbs", "visualbasic", "vbscript" };
			compilerInfo._compilerExtensions = new string[] { ".vb", "vb" };
			compilerInfo._providerOptions = new Dictionary<string, string>();
			compilerInfo._providerOptions["CompilerVersion"] = "v4.0";
			this.AddCompilerInfo(compilerInfo);
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			text = "Microsoft.JScript.JScriptCodeProvider, Microsoft.JScript, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			this.AddCompilerInfo(new CompilerInfo(compilerParameters, text)
			{
				_compilerLanguages = new string[] { "js", "jscript", "javascript" },
				_compilerExtensions = new string[] { ".js", "js" },
				_providerOptions = new Dictionary<string, string>()
			});
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			text = "Microsoft.VisualC.CppCodeProvider, CppCodeProvider, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			this.AddCompilerInfo(new CompilerInfo(compilerParameters, text)
			{
				_compilerLanguages = new string[] { "c++", "mc", "cpp" },
				_compilerExtensions = new string[] { ".h", "h" },
				_providerOptions = new Dictionary<string, string>()
			});
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000F5F30 File Offset: 0x000F4130
		private CodeDomCompilationConfiguration(CodeDomCompilationConfiguration original)
		{
			if (original._compilerLanguages != null)
			{
				this._compilerLanguages = (Hashtable)original._compilerLanguages.Clone();
			}
			if (original._compilerExtensions != null)
			{
				this._compilerExtensions = (Hashtable)original._compilerExtensions.Clone();
			}
			if (original._allCompilerInfo != null)
			{
				this._allCompilerInfo = (ArrayList)original._allCompilerInfo.Clone();
			}
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000F5FA0 File Offset: 0x000F41A0
		private void AddCompilerInfo(CompilerInfo compilerInfo)
		{
			foreach (string text in compilerInfo._compilerLanguages)
			{
				this._compilerLanguages[text] = compilerInfo;
			}
			foreach (string text2 in compilerInfo._compilerExtensions)
			{
				this._compilerExtensions[text2] = compilerInfo;
			}
			this._allCompilerInfo.Add(compilerInfo);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000F6010 File Offset: 0x000F4210
		private void RemoveUnmapped()
		{
			for (int i = 0; i < this._allCompilerInfo.Count; i++)
			{
				((CompilerInfo)this._allCompilerInfo[i])._mapped = false;
			}
			foreach (object obj in this._compilerLanguages.Values)
			{
				CompilerInfo compilerInfo = (CompilerInfo)obj;
				compilerInfo._mapped = true;
			}
			foreach (object obj2 in this._compilerExtensions.Values)
			{
				CompilerInfo compilerInfo2 = (CompilerInfo)obj2;
				compilerInfo2._mapped = true;
			}
			for (int j = this._allCompilerInfo.Count - 1; j >= 0; j--)
			{
				if (!((CompilerInfo)this._allCompilerInfo[j])._mapped)
				{
					this._allCompilerInfo.RemoveAt(j);
				}
			}
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000F6130 File Offset: 0x000F4330
		private CompilerInfo FindExistingCompilerInfo(string[] languageList, string[] extensionList)
		{
			CompilerInfo compilerInfo = null;
			foreach (object obj in this._allCompilerInfo)
			{
				CompilerInfo compilerInfo2 = (CompilerInfo)obj;
				if (compilerInfo2._compilerExtensions.Length == extensionList.Length && compilerInfo2._compilerLanguages.Length == languageList.Length)
				{
					bool flag = false;
					for (int i = 0; i < compilerInfo2._compilerExtensions.Length; i++)
					{
						if (compilerInfo2._compilerExtensions[i] != extensionList[i])
						{
							flag = true;
							break;
						}
					}
					for (int j = 0; j < compilerInfo2._compilerLanguages.Length; j++)
					{
						if (compilerInfo2._compilerLanguages[j] != languageList[j])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						compilerInfo = compilerInfo2;
						break;
					}
				}
			}
			return compilerInfo;
		}

		// Token: 0x04002C4B RID: 11339
		internal const string sectionName = "system.codedom";

		// Token: 0x04002C4C RID: 11340
		private static readonly char[] s_fieldSeparators = new char[] { ';' };

		// Token: 0x04002C4D RID: 11341
		internal Hashtable _compilerLanguages;

		// Token: 0x04002C4E RID: 11342
		internal Hashtable _compilerExtensions;

		// Token: 0x04002C4F RID: 11343
		internal ArrayList _allCompilerInfo;

		// Token: 0x04002C50 RID: 11344
		private static CodeDomCompilationConfiguration defaultInstance = new CodeDomCompilationConfiguration();

		// Token: 0x020008B0 RID: 2224
		internal class SectionHandler
		{
			// Token: 0x060045FF RID: 17919 RVA: 0x00123E8C File Offset: 0x0012208C
			private SectionHandler()
			{
			}

			// Token: 0x06004600 RID: 17920 RVA: 0x00123E94 File Offset: 0x00122094
			internal static object CreateStatic(object inheritedObject, XmlNode node)
			{
				CodeDomCompilationConfiguration codeDomCompilationConfiguration = (CodeDomCompilationConfiguration)inheritedObject;
				CodeDomCompilationConfiguration codeDomCompilationConfiguration2;
				if (codeDomCompilationConfiguration == null)
				{
					codeDomCompilationConfiguration2 = new CodeDomCompilationConfiguration();
				}
				else
				{
					codeDomCompilationConfiguration2 = new CodeDomCompilationConfiguration(codeDomCompilationConfiguration);
				}
				HandlerBase.CheckForUnrecognizedAttributes(node);
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						if (xmlNode.Name == "compilers")
						{
							CodeDomCompilationConfiguration.SectionHandler.ProcessCompilersElement(codeDomCompilationConfiguration2, xmlNode);
						}
						else
						{
							HandlerBase.ThrowUnrecognizedElement(xmlNode);
						}
					}
				}
				return codeDomCompilationConfiguration2;
			}

			// Token: 0x06004601 RID: 17921 RVA: 0x00123F34 File Offset: 0x00122134
			private static IDictionary<string, string> GetProviderOptions(XmlNode compilerNode)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (object obj in compilerNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name != "providerOption")
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
					string text = null;
					string text2 = null;
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref text);
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "value", ref text2);
					HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
					HandlerBase.CheckForChildNodes(xmlNode);
					dictionary[text] = text2;
				}
				return dictionary;
			}

			// Token: 0x06004602 RID: 17922 RVA: 0x00123FDC File Offset: 0x001221DC
			private static void ProcessCompilersElement(CodeDomCompilationConfiguration result, XmlNode node)
			{
				HandlerBase.CheckForUnrecognizedAttributes(node);
				string filename = ConfigurationErrorsException.GetFilename(node);
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					int lineNumber = ConfigurationErrorsException.GetLineNumber(xmlNode);
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						if (xmlNode.Name != "compiler")
						{
							HandlerBase.ThrowUnrecognizedElement(xmlNode);
						}
						string empty = string.Empty;
						XmlNode andRemoveRequiredNonEmptyStringAttribute = HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "language", ref empty);
						string empty2 = string.Empty;
						HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "extension", ref empty2);
						string text = null;
						HandlerBase.GetAndRemoveStringAttribute(xmlNode, "type", ref text);
						CompilerParameters compilerParameters = new CompilerParameters();
						int num = 0;
						if (HandlerBase.GetAndRemoveNonNegativeIntegerAttribute(xmlNode, "warningLevel", ref num) != null)
						{
							compilerParameters.WarningLevel = num;
							compilerParameters.TreatWarningsAsErrors = num > 0;
						}
						string text2 = null;
						if (HandlerBase.GetAndRemoveStringAttribute(xmlNode, "compilerOptions", ref text2) != null)
						{
							compilerParameters.CompilerOptions = text2;
						}
						IDictionary<string, string> providerOptions = CodeDomCompilationConfiguration.SectionHandler.GetProviderOptions(xmlNode);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						string[] array = empty.Split(CodeDomCompilationConfiguration.s_fieldSeparators);
						string[] array2 = empty2.Split(CodeDomCompilationConfiguration.s_fieldSeparators);
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = array[i].Trim();
						}
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j] = array2[j].Trim();
						}
						foreach (string text3 in array)
						{
							if (text3.Length == 0)
							{
								throw new ConfigurationErrorsException(SR.GetString("Language_Names_Cannot_Be_Empty"));
							}
						}
						foreach (string text4 in array2)
						{
							if (text4.Length == 0 || text4[0] != '.')
							{
								throw new ConfigurationErrorsException(SR.GetString("Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based"));
							}
						}
						CompilerInfo compilerInfo;
						if (text != null)
						{
							compilerInfo = new CompilerInfo(compilerParameters, text);
						}
						else
						{
							compilerInfo = result.FindExistingCompilerInfo(array, array2);
							if (compilerInfo == null)
							{
								throw new ConfigurationErrorsException();
							}
						}
						compilerInfo.configFileName = filename;
						compilerInfo.configFileLineNumber = lineNumber;
						if (text != null)
						{
							compilerInfo._compilerLanguages = array;
							compilerInfo._compilerExtensions = array2;
							compilerInfo._providerOptions = providerOptions;
							result.AddCompilerInfo(compilerInfo);
						}
						else
						{
							foreach (KeyValuePair<string, string> keyValuePair in providerOptions)
							{
								compilerInfo._providerOptions[keyValuePair.Key] = keyValuePair.Value;
							}
						}
					}
				}
				result.RemoveUnmapped();
			}
		}
	}
}

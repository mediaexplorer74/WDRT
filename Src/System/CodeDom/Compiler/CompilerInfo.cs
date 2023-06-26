using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the configuration settings of a language provider. This class cannot be inherited.</summary>
	// Token: 0x02000676 RID: 1654
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class CompilerInfo
	{
		// Token: 0x06003CD0 RID: 15568 RVA: 0x000FA75B File Offset: 0x000F895B
		private CompilerInfo()
		{
		}

		/// <summary>Gets the language names supported by the language provider.</summary>
		/// <returns>An array of language names supported by the language provider.</returns>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x000FA763 File Offset: 0x000F8963
		public string[] GetLanguages()
		{
			return this.CloneCompilerLanguages();
		}

		/// <summary>Returns the file name extensions supported by the language provider.</summary>
		/// <returns>An array of file name extensions supported by the language provider.</returns>
		// Token: 0x06003CD2 RID: 15570 RVA: 0x000FA76B File Offset: 0x000F896B
		public string[] GetExtensions()
		{
			return this.CloneCompilerExtensions();
		}

		/// <summary>Gets the type of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> instance that represents the configured language provider type.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The language provider is not configured on this computer.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Cannot locate the type because it is a <see langword="null" /> or empty string.  
		///  -or-  
		///  Cannot locate the type because the name for the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> cannot be found in the configuration file.</exception>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000FA774 File Offset: 0x000F8974
		public Type CodeDomProviderType
		{
			get
			{
				if (this.type == null)
				{
					lock (this)
					{
						if (this.type == null)
						{
							this.type = Type.GetType(this._codeDomProviderTypeName);
							if (this.type == null)
							{
								if (this.configFileName == null)
								{
									throw new ConfigurationErrorsException(SR.GetString("Unable_To_Locate_Type", new object[]
									{
										this._codeDomProviderTypeName,
										string.Empty,
										0
									}));
								}
								throw new ConfigurationErrorsException(SR.GetString("Unable_To_Locate_Type", new object[] { this._codeDomProviderTypeName }), this.configFileName, this.configFileLineNumber);
							}
						}
					}
				}
				return this.type;
			}
		}

		/// <summary>Returns a value indicating whether the language provider implementation is configured on the computer.</summary>
		/// <returns>
		///   <see langword="true" /> if the language provider implementation type is configured on the computer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000FA854 File Offset: 0x000F8A54
		public bool IsCodeDomProviderTypeValid
		{
			get
			{
				Type type = Type.GetType(this._codeDomProviderTypeName);
				return type != null;
			}
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings.</summary>
		/// <returns>A CodeDOM provider associated with the language provider configuration.</returns>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000FA874 File Offset: 0x000F8A74
		public CodeDomProvider CreateProvider()
		{
			if (this._providerOptions.Count > 0)
			{
				ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
				if (constructor != null)
				{
					return (CodeDomProvider)constructor.Invoke(new object[] { this._providerOptions });
				}
			}
			return (CodeDomProvider)Activator.CreateInstance(this.CodeDomProviderType);
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings and specified options.</summary>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		/// <returns>A CodeDOM provider associated with the language provider configuration and specified options.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The provider does not support options.</exception>
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000FA8E4 File Offset: 0x000F8AE4
		public CodeDomProvider CreateProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
			if (constructor != null)
			{
				return (CodeDomProvider)constructor.Invoke(new object[] { providerOptions });
			}
			throw new InvalidOperationException(SR.GetString("Provider_does_not_support_options", new object[] { this.CodeDomProviderType.ToString() }));
		}

		/// <summary>Gets the configured compiler settings for the language provider implementation.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> instance that contains the compiler options and settings configured for the language provider.</returns>
		// Token: 0x06003CD7 RID: 15575 RVA: 0x000FA960 File Offset: 0x000F8B60
		public CompilerParameters CreateDefaultCompilerParameters()
		{
			return this.CloneCompilerParameters();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000FA968 File Offset: 0x000F8B68
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName, string[] compilerLanguages, string[] compilerExtensions)
		{
			this._compilerLanguages = compilerLanguages;
			this._compilerExtensions = compilerExtensions;
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			if (compilerParams == null)
			{
				compilerParams = new CompilerParameters();
			}
			this._compilerParams = compilerParams;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000FA997 File Offset: 0x000F8B97
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName)
		{
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			if (compilerParams == null)
			{
				compilerParams = new CompilerParameters();
			}
			this._compilerParams = compilerParams;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> instance, suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		// Token: 0x06003CDA RID: 15578 RVA: 0x000FA9B7 File Offset: 0x000F8BB7
		public override int GetHashCode()
		{
			return this._codeDomProviderTypeName.GetHashCode();
		}

		/// <summary>Determines whether the specified object represents the same language provider and compiler settings as the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</summary>
		/// <param name="o">The object to compare with the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object and its value is the same as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003CDB RID: 15579 RVA: 0x000FA9C4 File Offset: 0x000F8BC4
		public override bool Equals(object o)
		{
			CompilerInfo compilerInfo = o as CompilerInfo;
			return o != null && (this.CodeDomProviderType == compilerInfo.CodeDomProviderType && this.CompilerParams.WarningLevel == compilerInfo.CompilerParams.WarningLevel && this.CompilerParams.IncludeDebugInformation == compilerInfo.CompilerParams.IncludeDebugInformation) && this.CompilerParams.CompilerOptions == compilerInfo.CompilerParams.CompilerOptions;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000FAA40 File Offset: 0x000F8C40
		private CompilerParameters CloneCompilerParameters()
		{
			return new CompilerParameters
			{
				IncludeDebugInformation = this._compilerParams.IncludeDebugInformation,
				TreatWarningsAsErrors = this._compilerParams.TreatWarningsAsErrors,
				WarningLevel = this._compilerParams.WarningLevel,
				CompilerOptions = this._compilerParams.CompilerOptions
			};
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000FAA98 File Offset: 0x000F8C98
		private string[] CloneCompilerLanguages()
		{
			string[] array = new string[this._compilerLanguages.Length];
			Array.Copy(this._compilerLanguages, array, this._compilerLanguages.Length);
			return array;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000FAAC8 File Offset: 0x000F8CC8
		private string[] CloneCompilerExtensions()
		{
			string[] array = new string[this._compilerExtensions.Length];
			Array.Copy(this._compilerExtensions, array, this._compilerExtensions.Length);
			return array;
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000FAAF8 File Offset: 0x000F8CF8
		internal CompilerParameters CompilerParams
		{
			get
			{
				return this._compilerParams;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000FAB00 File Offset: 0x000F8D00
		internal IDictionary<string, string> ProviderOptions
		{
			get
			{
				return this._providerOptions;
			}
		}

		// Token: 0x04002C60 RID: 11360
		internal string _codeDomProviderTypeName;

		// Token: 0x04002C61 RID: 11361
		internal CompilerParameters _compilerParams;

		// Token: 0x04002C62 RID: 11362
		internal string[] _compilerLanguages;

		// Token: 0x04002C63 RID: 11363
		internal string[] _compilerExtensions;

		// Token: 0x04002C64 RID: 11364
		internal string configFileName;

		// Token: 0x04002C65 RID: 11365
		internal IDictionary<string, string> _providerOptions;

		// Token: 0x04002C66 RID: 11366
		internal int configFileLineNumber;

		// Token: 0x04002C67 RID: 11367
		internal bool _mapped;

		// Token: 0x04002C68 RID: 11368
		private Type type;
	}
}

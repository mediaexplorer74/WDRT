using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x02000677 RID: 1655
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06003CE1 RID: 15585 RVA: 0x000FAB08 File Offset: 0x000F8D08
		public CompilerParameters()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		// Token: 0x06003CE2 RID: 15586 RVA: 0x000FAB12 File Offset: 0x000F8D12
		public CompilerParameters(string[] assemblyNames)
			: this(assemblyNames, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		/// <param name="outputName">The output file name.</param>
		// Token: 0x06003CE3 RID: 15587 RVA: 0x000FAB1D File Offset: 0x000F8D1D
		public CompilerParameters(string[] assemblyNames, string outputName)
			: this(assemblyNames, outputName, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		/// <param name="outputName">The output file name.</param>
		/// <param name="includeDebugInformation">
		///   <see langword="true" /> to include debug information; <see langword="false" /> to exclude debug information.</param>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000FAB28 File Offset: 0x000F8D28
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.outputName = outputName;
			this.includeDebugInformation = includeDebugInformation;
		}

		/// <summary>Gets or sets the name of the core or standard assembly that contains basic types such as <see cref="T:System.Object" />, <see cref="T:System.String" />, or <see cref="T:System.Int32" />.</summary>
		/// <returns>The name of the core assembly that contains basic types.</returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000FAB8B File Offset: 0x000F8D8B
		// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x000FAB93 File Offset: 0x000F8D93
		public string CoreAssemblyFileName
		{
			get
			{
				return this.coreAssemblyFileName;
			}
			set
			{
				this.coreAssemblyFileName = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>
		///   <see langword="true" /> if an executable should be generated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000FAB9C File Offset: 0x000F8D9C
		// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x000FABA4 File Offset: 0x000F8DA4
		public bool GenerateExecutable
		{
			get
			{
				return this.generateExecutable;
			}
			set
			{
				this.generateExecutable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the compiler should generate the output in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000FABAD File Offset: 0x000F8DAD
		// (set) Token: 0x06003CEA RID: 15594 RVA: 0x000FABB5 File Offset: 0x000F8DB5
		public bool GenerateInMemory
		{
			get
			{
				return this.generateInMemory;
			}
			set
			{
				this.generateInMemory = value;
			}
		}

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A collection that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000FABBE File Offset: 0x000F8DBE
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this.assemblyNames;
			}
		}

		/// <summary>Gets or sets the name of the main class.</summary>
		/// <returns>The name of the main class.</returns>
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000FABC6 File Offset: 0x000F8DC6
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x000FABCE File Offset: 0x000F8DCE
		public string MainClass
		{
			get
			{
				return this.mainClass;
			}
			set
			{
				this.mainClass = value;
			}
		}

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000FABD7 File Offset: 0x000F8DD7
		// (set) Token: 0x06003CEF RID: 15599 RVA: 0x000FABDF File Offset: 0x000F8DDF
		public string OutputAssembly
		{
			get
			{
				return this.outputName;
			}
			set
			{
				this.outputName = value;
			}
		}

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A collection that contains the temporary files.</returns>
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000FABE8 File Offset: 0x000F8DE8
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x000FAC03 File Offset: 0x000F8E03
		public TempFileCollection TempFiles
		{
			get
			{
				if (this.tempFiles == null)
				{
					this.tempFiles = new TempFileCollection();
				}
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>
		///   <see langword="true" /> if debug information should be generated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000FAC0C File Offset: 0x000F8E0C
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x000FAC14 File Offset: 0x000F8E14
		public bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>
		///   <see langword="true" /> if warnings should be treated as errors; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000FAC1D File Offset: 0x000F8E1D
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000FAC25 File Offset: 0x000F8E25
		public bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000FAC2E File Offset: 0x000F8E2E
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000FAC36 File Offset: 0x000F8E36
		public int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		/// <summary>Gets or sets optional command-line arguments to use when invoking the compiler.</summary>
		/// <returns>Any additional command-line arguments for the compiler.</returns>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000FAC3F File Offset: 0x000F8E3F
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000FAC47 File Offset: 0x000F8E47
		public string CompilerOptions
		{
			get
			{
				return this.compilerOptions;
			}
			set
			{
				this.compilerOptions = value;
			}
		}

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000FAC50 File Offset: 0x000F8E50
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x000FAC58 File Offset: 0x000F8E58
		public string Win32Resource
		{
			get
			{
				return this.win32Resource;
			}
			set
			{
				this.win32Resource = value;
			}
		}

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000FAC61 File Offset: 0x000F8E61
		[ComVisible(false)]
		public StringCollection EmbeddedResources
		{
			get
			{
				return this.embeddedResources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000FAC69 File Offset: 0x000F8E69
		[ComVisible(false)]
		public StringCollection LinkedResources
		{
			get
			{
				return this.linkedResources;
			}
		}

		/// <summary>Gets or sets the user token to use when creating the compiler process.</summary>
		/// <returns>The user token to use.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000FAC71 File Offset: 0x000F8E71
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x000FAC8C File Offset: 0x000F8E8C
		public IntPtr UserToken
		{
			get
			{
				if (this.userToken != null)
				{
					return this.userToken.DangerousGetHandle();
				}
				return IntPtr.Zero;
			}
			set
			{
				if (this.userToken != null)
				{
					this.userToken.Close();
				}
				this.userToken = new SafeUserTokenHandle(value, false);
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000FACAE File Offset: 0x000F8EAE
		internal SafeUserTokenHandle SafeUserToken
		{
			get
			{
				return this.userToken;
			}
		}

		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An  object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000FACB8 File Offset: 0x000F8EB8
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000FACDC File Offset: 0x000F8EDC
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence evidence = null;
				if (this.evidence != null)
				{
					evidence = this.evidence.Clone();
				}
				return evidence;
			}
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			set
			{
				if (value != null)
				{
					this.evidence = value.Clone();
					return;
				}
				this.evidence = null;
			}
		}

		// Token: 0x04002C69 RID: 11369
		[OptionalField]
		private string coreAssemblyFileName = string.Empty;

		// Token: 0x04002C6A RID: 11370
		private StringCollection assemblyNames = new StringCollection();

		// Token: 0x04002C6B RID: 11371
		[OptionalField]
		private StringCollection embeddedResources = new StringCollection();

		// Token: 0x04002C6C RID: 11372
		[OptionalField]
		private StringCollection linkedResources = new StringCollection();

		// Token: 0x04002C6D RID: 11373
		private string outputName;

		// Token: 0x04002C6E RID: 11374
		private string mainClass;

		// Token: 0x04002C6F RID: 11375
		private bool generateInMemory;

		// Token: 0x04002C70 RID: 11376
		private bool includeDebugInformation;

		// Token: 0x04002C71 RID: 11377
		private int warningLevel = -1;

		// Token: 0x04002C72 RID: 11378
		private string compilerOptions;

		// Token: 0x04002C73 RID: 11379
		private string win32Resource;

		// Token: 0x04002C74 RID: 11380
		private bool treatWarningsAsErrors;

		// Token: 0x04002C75 RID: 11381
		private bool generateExecutable;

		// Token: 0x04002C76 RID: 11382
		private TempFileCollection tempFiles;

		// Token: 0x04002C77 RID: 11383
		[NonSerialized]
		private SafeUserTokenHandle userToken;

		// Token: 0x04002C78 RID: 11384
		private Evidence evidence;
	}
}

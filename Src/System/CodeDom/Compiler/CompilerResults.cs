using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x02000678 RID: 1656
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		// Token: 0x06003D03 RID: 15619 RVA: 0x000FACF5 File Offset: 0x000F8EF5
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public CompilerResults(TempFileCollection tempFiles)
		{
			this.tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the temporary file collection to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</returns>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000FAD1A File Offset: 0x000F8F1A
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x000FAD22 File Offset: 0x000F8F22
		public TempFileCollection TempFiles
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.tempFiles;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.tempFiles = value;
			}
		}

		/// <summary>Indicates the evidence object that represents the security policy permissions of the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions of the compiled assembly.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000FAD2C File Offset: 0x000F8F2C
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x000FAD50 File Offset: 0x000F8F50
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				Evidence evidence = null;
				if (this.evidence != null)
				{
					evidence = this.evidence.Clone();
				}
				return evidence;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
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

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000FAD6C File Offset: 0x000F8F6C
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x000FADB9 File Offset: 0x000F8FB9
		public Assembly CompiledAssembly
		{
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlEvidence)]
			get
			{
				if (this.compiledAssembly == null && this.pathToAssembly != null)
				{
					this.compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.pathToAssembly
					}, this.evidence);
				}
				return this.compiledAssembly;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000FADC2 File Offset: 0x000F8FC2
		public CompilerErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000FADCA File Offset: 0x000F8FCA
		public StringCollection Output
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or <see langword="null" /> if the assembly was generated in memory.</returns>
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000FADD2 File Offset: 0x000F8FD2
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000FADDA File Offset: 0x000F8FDA
		public string PathToAssembly
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.pathToAssembly;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.pathToAssembly = value;
			}
		}

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000FADE3 File Offset: 0x000F8FE3
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000FADEB File Offset: 0x000F8FEB
		public int NativeCompilerReturnValue
		{
			get
			{
				return this.nativeCompilerReturnValue;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.nativeCompilerReturnValue = value;
			}
		}

		// Token: 0x04002C79 RID: 11385
		private CompilerErrorCollection errors = new CompilerErrorCollection();

		// Token: 0x04002C7A RID: 11386
		private StringCollection output = new StringCollection();

		// Token: 0x04002C7B RID: 11387
		private Assembly compiledAssembly;

		// Token: 0x04002C7C RID: 11388
		private string pathToAssembly;

		// Token: 0x04002C7D RID: 11389
		private int nativeCompilerReturnValue;

		// Token: 0x04002C7E RID: 11390
		private TempFileCollection tempFiles;

		// Token: 0x04002C7F RID: 11391
		private Evidence evidence;
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed.</summary>
	// Token: 0x020005A6 RID: 1446
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">
		///   <see langword="true" /> if an installer should be invoked during installation of an assembly; otherwise, <see langword="false" />.</param>
		// Token: 0x060035F7 RID: 13815 RVA: 0x000EBC2F File Offset: 0x000E9E2F
		public RunInstallerAttribute(bool runInstaller)
		{
			this.runInstaller = runInstaller;
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if an installer should be invoked during installation of an assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000EBC3E File Offset: 0x000E9E3E
		public bool RunInstaller
		{
			get
			{
				return this.runInstaller;
			}
		}

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060035F9 RID: 13817 RVA: 0x000EBC48 File Offset: 0x000E9E48
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.runInstaller;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x060035FA RID: 13818 RVA: 0x000EBC75 File Offset: 0x000E9E75
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060035FB RID: 13819 RVA: 0x000EBC7D File Offset: 0x000E9E7D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		// Token: 0x04002A7A RID: 10874
		private bool runInstaller;

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A7B RID: 10875
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should not be invoked when the assembly is installed. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A7C RID: 10876
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A7D RID: 10877
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}

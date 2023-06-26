using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the version of the .NET Framework that a particular assembly was compiled against.</summary>
	// Token: 0x02000722 RID: 1826
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> class by specifying the .NET Framework version against which an assembly was built.</summary>
		/// <param name="frameworkName">The version of the .NET Framework against which the assembly was built.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is <see langword="null" />.</exception>
		// Token: 0x06005179 RID: 20857 RVA: 0x001207A9 File Offset: 0x0011E9A9
		[__DynamicallyInvokable]
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		/// <summary>Gets the name of the .NET Framework version against which a particular assembly was compiled.</summary>
		/// <returns>The name of the .NET Framework version with which the assembly was compiled.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x001207C6 File Offset: 0x0011E9C6
		[__DynamicallyInvokable]
		public string FrameworkName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._frameworkName;
			}
		}

		/// <summary>Gets the display name of the .NET Framework version against which an assembly was built.</summary>
		/// <returns>The display name of the .NET Framework version.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x001207CE File Offset: 0x0011E9CE
		// (set) Token: 0x0600517C RID: 20860 RVA: 0x001207D6 File Offset: 0x0011E9D6
		[__DynamicallyInvokable]
		public string FrameworkDisplayName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._frameworkDisplayName;
			}
			[__DynamicallyInvokable]
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x0400242B RID: 9259
		private string _frameworkName;

		// Token: 0x0400242C RID: 9260
		private string _frameworkDisplayName;
	}
}

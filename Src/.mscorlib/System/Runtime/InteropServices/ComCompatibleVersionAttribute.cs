using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates to a COM client that all classes in the current version of an assembly are compatible with classes in an earlier version of the assembly.</summary>
	// Token: 0x0200093B RID: 2363
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> class with the major version, minor version, build, and revision numbers of the assembly.</summary>
		/// <param name="major">The major version number of the assembly.</param>
		/// <param name="minor">The minor version number of the assembly.</param>
		/// <param name="build">The build number of the assembly.</param>
		/// <param name="revision">The revision number of the assembly.</param>
		// Token: 0x0600606F RID: 24687 RVA: 0x0014D6B4 File Offset: 0x0014B8B4
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		/// <summary>Gets the major version number of the assembly.</summary>
		/// <returns>The major version number of the assembly.</returns>
		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x0014D6D9 File Offset: 0x0014B8D9
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the assembly.</summary>
		/// <returns>The minor version number of the assembly.</returns>
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x0014D6E1 File Offset: 0x0014B8E1
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		/// <summary>Gets the build number of the assembly.</summary>
		/// <returns>The build number of the assembly.</returns>
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x0014D6E9 File Offset: 0x0014B8E9
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		/// <summary>Gets the revision number of the assembly.</summary>
		/// <returns>The revision number of the assembly.</returns>
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x0014D6F1 File Offset: 0x0014B8F1
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002B32 RID: 11058
		internal int _major;

		// Token: 0x04002B33 RID: 11059
		internal int _minor;

		// Token: 0x04002B34 RID: 11060
		internal int _build;

		// Token: 0x04002B35 RID: 11061
		internal int _revision;
	}
}

using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed assembly is a primary interop assembly.</summary>
	// Token: 0x02000937 RID: 2359
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> class with the major and minor version numbers of the type library for which this assembly is the primary interop assembly.</summary>
		/// <param name="major">The major version of the type library for which this assembly is the primary interop assembly.</param>
		/// <param name="minor">The minor version of the type library for which this assembly is the primary interop assembly.</param>
		// Token: 0x06006064 RID: 24676 RVA: 0x0014D62B File Offset: 0x0014B82B
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The major version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06006065 RID: 24677 RVA: 0x0014D641 File Offset: 0x0014B841
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The minor version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06006066 RID: 24678 RVA: 0x0014D649 File Offset: 0x0014B849
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002B2B RID: 11051
		internal int _major;

		// Token: 0x04002B2C RID: 11052
		internal int _minor;
	}
}

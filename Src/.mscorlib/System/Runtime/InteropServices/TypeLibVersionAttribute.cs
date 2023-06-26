using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the version number of an exported type library.</summary>
	// Token: 0x0200093A RID: 2362
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVersionAttribute" /> class with the major and minor version numbers of the type library.</summary>
		/// <param name="major">The major version number of the type library.</param>
		/// <param name="minor">The minor version number of the type library.</param>
		// Token: 0x0600606C RID: 24684 RVA: 0x0014D68E File Offset: 0x0014B88E
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library.</summary>
		/// <returns>The major version number of the type library.</returns>
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x0014D6A4 File Offset: 0x0014B8A4
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library.</summary>
		/// <returns>The minor version number of the type library.</returns>
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600606E RID: 24686 RVA: 0x0014D6AC File Offset: 0x0014B8AC
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002B30 RID: 11056
		internal int _major;

		// Token: 0x04002B31 RID: 11057
		internal int _minor;
	}
}

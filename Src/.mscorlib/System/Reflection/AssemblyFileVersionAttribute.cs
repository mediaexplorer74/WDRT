using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs a compiler to use a specific version number for the Win32 file version resource. The Win32 file version is not required to be the same as the assembly's version number.</summary>
	// Token: 0x020005BB RID: 1467
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> class, specifying the file version.</summary>
		/// <param name="version">The file version.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x0600447B RID: 17531 RVA: 0x000FDBEB File Offset: 0x000FBDEB
		[__DynamicallyInvokable]
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		/// <summary>Gets the Win32 file version resource name.</summary>
		/// <returns>A string containing the file version resource name.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x000FDC08 File Offset: 0x000FBE08
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x04001C0E RID: 7182
		private string _version;
	}
}

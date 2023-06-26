using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that types that are ordinarily visible only within the current assembly are visible to a specified assembly.</summary>
	// Token: 0x020008B7 RID: 2231
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> class with the name of the specified friend assembly.</summary>
		/// <param name="assemblyName">The name of a friend assembly.</param>
		// Token: 0x06005DC5 RID: 24005 RVA: 0x0014AF48 File Offset: 0x00149148
		[__DynamicallyInvokable]
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		/// <summary>Gets the name of the friend assembly to which all types and type members that are marked with the <see langword="internal" /> keyword are to be made visible.</summary>
		/// <returns>A string that represents the name of the friend assembly.</returns>
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x0014AF5E File Offset: 0x0014915E
		[__DynamicallyInvokable]
		public string AssemblyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._assemblyName;
			}
		}

		/// <summary>This property is not implemented.</summary>
		/// <returns>This property does not return a value.</returns>
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x0014AF66 File Offset: 0x00149166
		// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x0014AF6E File Offset: 0x0014916E
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04002A20 RID: 10784
		private string _assemblyName;

		// Token: 0x04002A21 RID: 10785
		private bool _allInternalsVisible = true;
	}
}

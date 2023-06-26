using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Instructs a <see cref="T:System.Resources.ResourceManager" /> object to ask for a particular version of a satellite assembly.</summary>
	// Token: 0x0200039C RID: 924
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> class.</summary>
		/// <param name="version">A string that specifies the version of the satellite assemblies to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="version" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B023C File Offset: 0x000AE43C
		[__DynamicallyInvokable]
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		/// <summary>Gets the version of the satellite assemblies with the required resources.</summary>
		/// <returns>A string that contains the version of the satellite assemblies with the required resources.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000B0259 File Offset: 0x000AE459
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x0400129A RID: 4762
		private string _version;
	}
}

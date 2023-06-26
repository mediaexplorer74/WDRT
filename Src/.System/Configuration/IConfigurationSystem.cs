using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	// Token: 0x02000090 RID: 144
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <param name="configKey">The configuration key.</param>
		/// <returns>The object representing the configuration.</returns>
		// Token: 0x06000566 RID: 1382
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		// Token: 0x06000567 RID: 1383
		void Init();
	}
}

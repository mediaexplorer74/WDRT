using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles the access to certain configuration sections.</summary>
	// Token: 0x0200008F RID: 143
	public interface IConfigurationSectionHandler
	{
		/// <summary>Creates a configuration section handler.</summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>The created section handler object.</returns>
		// Token: 0x06000565 RID: 1381
		object Create(object parent, object configContext, XmlNode section);
	}
}

using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a legacy section-handler definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	// Token: 0x02000091 RID: 145
	public class IgnoreSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds the specified configuration object to the section-handler collection.</summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section.</param>
		/// <param name="configContext">The virtual path for which the configuration section handler computes configuration values. Normally this parameter is reserved and is <see langword="null" />.</param>
		/// <param name="section">An <see cref="T:System.Xml.XmlNode" /> that contains the configuration information to be handled. Provides direct access to the XML contents of the configuration section.</param>
		/// <returns>The created configuration handler object.</returns>
		// Token: 0x06000568 RID: 1384 RVA: 0x00021D09 File Offset: 0x0001FF09
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return null;
		}
	}
}

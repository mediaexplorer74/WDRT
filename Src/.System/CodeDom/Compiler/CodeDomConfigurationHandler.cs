using System;
using System.Configuration;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066D RID: 1645
	internal class CodeDomConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003B8E RID: 15246 RVA: 0x000F622C File Offset: 0x000F442C
		internal CodeDomConfigurationHandler()
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000F6234 File Offset: 0x000F4434
		public virtual object Create(object inheritedObject, object configContextObj, XmlNode node)
		{
			return CodeDomCompilationConfiguration.SectionHandler.CreateStatic(inheritedObject, node);
		}
	}
}

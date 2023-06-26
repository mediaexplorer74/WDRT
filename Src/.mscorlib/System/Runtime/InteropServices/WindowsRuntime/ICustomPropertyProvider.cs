using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A09 RID: 2569
	[Guid("7C925755-3E48-42B4-8677-76372267033F")]
	[ComImport]
	internal interface ICustomPropertyProvider
	{
		// Token: 0x06006597 RID: 26007
		ICustomProperty GetCustomProperty(string name);

		// Token: 0x06006598 RID: 26008
		ICustomProperty GetIndexedProperty(string name, Type indexParameterType);

		// Token: 0x06006599 RID: 26009
		string GetStringRepresentation();

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x0600659A RID: 26010
		Type Type { get; }
	}
}

using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the callbacks that the type library exporter makes when exporting a type library.</summary>
	// Token: 0x0200096C RID: 2412
	[ComVisible(true)]
	[Serializable]
	public enum ExporterEventKind
	{
		/// <summary>Specifies that the event is invoked when a type has been exported.</summary>
		// Token: 0x04002BBF RID: 11199
		NOTIF_TYPECONVERTED,
		/// <summary>Specifies that the event is invoked when a warning occurs during conversion.</summary>
		// Token: 0x04002BC0 RID: 11200
		NOTIF_CONVERTWARNING,
		/// <summary>This value is not supported in this version of the .NET Framework.</summary>
		// Token: 0x04002BC1 RID: 11201
		ERROR_REFTOINVALIDASSEMBLY
	}
}

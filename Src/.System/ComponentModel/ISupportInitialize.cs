using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x02000576 RID: 1398
	[SRDescription("ISupportInitializeDescr")]
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x060033D4 RID: 13268
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x060033D5 RID: 13269
		void EndInit();
	}
}

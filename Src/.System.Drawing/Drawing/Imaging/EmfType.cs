﻿using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the nature of the records that are placed in an Enhanced Metafile (EMF) file. This enumeration is used by several constructors in the <see cref="T:System.Drawing.Imaging.Metafile" /> class.</summary>
	// Token: 0x02000097 RID: 151
	public enum EmfType
	{
		/// <summary>Specifies that all the records in the metafile are EMF records, which can be displayed by GDI or GDI+.</summary>
		// Token: 0x04000871 RID: 2161
		EmfOnly = 3,
		/// <summary>Specifies that all the records in the metafile are EMF+ records, which can be displayed by GDI+ but not by GDI.</summary>
		// Token: 0x04000872 RID: 2162
		EmfPlusOnly,
		/// <summary>Specifies that all EMF+ records in the metafile are associated with an alternate EMF record. Metafiles of type <see cref="F:System.Drawing.Imaging.EmfType.EmfPlusDual" /> can be displayed by GDI or by GDI+.</summary>
		// Token: 0x04000873 RID: 2163
		EmfPlusDual
	}
}

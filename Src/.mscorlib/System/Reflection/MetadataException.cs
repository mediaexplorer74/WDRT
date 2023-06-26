using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005FD RID: 1533
	internal class MetadataException : Exception
	{
		// Token: 0x060046E8 RID: 18152 RVA: 0x0010470E File Offset: 0x0010290E
		internal MetadataException(int hr)
		{
			this.m_hr = hr;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x0010471D File Offset: 0x0010291D
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "MetadataException HResult = {0:x}.", this.m_hr);
		}

		// Token: 0x04001D5E RID: 7518
		private int m_hr;
	}
}

using System;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094F RID: 2383
	internal class ImporterCallback : ITypeLibImporterNotifySink
	{
		// Token: 0x060061C1 RID: 25025 RVA: 0x0014FC41 File Offset: 0x0014DE41
		public void ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
		{
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x0014FC44 File Offset: 0x0014DE44
		[SecuritySafeCritical]
		public Assembly ResolveRef(object TypeLib)
		{
			Assembly assembly;
			try
			{
				ITypeLibConverter typeLibConverter = new TypeLibConverter();
				assembly = typeLibConverter.ConvertTypeLibToAssembly(TypeLib, Marshal.GetTypeLibName((ITypeLib)TypeLib) + ".dll", TypeLibImporterFlags.None, new ImporterCallback(), null, null, null, null);
			}
			catch (Exception)
			{
				assembly = null;
			}
			return assembly;
		}
	}
}

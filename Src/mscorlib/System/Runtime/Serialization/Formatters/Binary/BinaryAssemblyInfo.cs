using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077F RID: 1919
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x060053DA RID: 21466 RVA: 0x0012859F File Offset: 0x0012679F
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x001285AE File Offset: 0x001267AE
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x001285C4 File Offset: 0x001267C4
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyNotFound", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x040025C0 RID: 9664
		internal string assemblyString;

		// Token: 0x040025C1 RID: 9665
		private Assembly assembly;
	}
}

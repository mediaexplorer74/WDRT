using System;
using System.Collections.Specialized;

namespace System.Management.Instrumentation
{
	// Token: 0x020000B2 RID: 178
	internal class ReferencesCollection
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000227C1 File Offset: 0x000209C1
		public StringCollection Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000227C9 File Offset: 0x000209C9
		public StringCollection Assemblies
		{
			get
			{
				return this.assemblies;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000227D1 File Offset: 0x000209D1
		public CodeWriter UsingCode
		{
			get
			{
				return this.usingCode;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000227DC File Offset: 0x000209DC
		public void Add(Type type)
		{
			if (!this.namespaces.Contains(type.Namespace))
			{
				this.namespaces.Add(type.Namespace);
				this.usingCode.Line(string.Format("using {0};", type.Namespace));
			}
			if (!this.assemblies.Contains(type.Assembly.Location))
			{
				this.assemblies.Add(type.Assembly.Location);
			}
		}

		// Token: 0x040004EC RID: 1260
		private StringCollection namespaces = new StringCollection();

		// Token: 0x040004ED RID: 1261
		private StringCollection assemblies = new StringCollection();

		// Token: 0x040004EE RID: 1262
		private CodeWriter usingCode = new CodeWriter();
	}
}

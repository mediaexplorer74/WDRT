using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000627 RID: 1575
	internal sealed class InternalAssemblyBuilder : RuntimeAssembly
	{
		// Token: 0x0600491C RID: 18716 RVA: 0x001095D7 File Offset: 0x001077D7
		private InternalAssemblyBuilder()
		{
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x001095DF File Offset: 0x001077DF
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalAssemblyBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x001095FA File Offset: 0x001077FA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00109602 File Offset: 0x00107802
		public override string[] GetManifestResourceNames()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x00109613 File Offset: 0x00107813
		public override FileStream GetFile(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00109624 File Offset: 0x00107824
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00109635 File Offset: 0x00107835
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00109646 File Offset: 0x00107846
		public override Stream GetManifestResourceStream(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00109657 File Offset: 0x00107857
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x00109668 File Offset: 0x00107868
		public override string Location
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x00109679 File Offset: 0x00107879
		public override string CodeBase
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0010968A File Offset: 0x0010788A
		public override Type[] GetExportedTypes()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x0010969B File Offset: 0x0010789B
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeEnvironment.GetSystemVersion();
			}
		}
	}
}

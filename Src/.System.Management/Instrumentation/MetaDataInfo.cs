using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Management.Instrumentation
{
	// Token: 0x020000C6 RID: 198
	internal class MetaDataInfo : IDisposable
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x00026C4B File Offset: 0x00024E4B
		public MetaDataInfo(Assembly assembly)
			: this(assembly.Location)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00026C5C File Offset: 0x00024E5C
		public MetaDataInfo(string assemblyName)
		{
			Guid guid = new Guid(((GuidAttribute)Attribute.GetCustomAttribute(typeof(IMetaDataImportInternalOnly), typeof(GuidAttribute), false)).Value);
			IMetaDataDispenser metaDataDispenser = (IMetaDataDispenser)new CorMetaDataDispenser();
			this.importInterface = (IMetaDataImportInternalOnly)metaDataDispenser.OpenScope(assemblyName, 0U, ref guid);
			Marshal.ReleaseComObject(metaDataDispenser);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00026CC4 File Offset: 0x00024EC4
		private void InitNameAndMvid()
		{
			if (this.name == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Capacity = 0;
				uint num;
				this.importInterface.GetScopeProps(stringBuilder, (uint)stringBuilder.Capacity, out num, out this.mvid);
				stringBuilder.Capacity = (int)num;
				this.importInterface.GetScopeProps(stringBuilder, (uint)stringBuilder.Capacity, out num, out this.mvid);
				this.name = stringBuilder.ToString();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00026D2D File Offset: 0x00024F2D
		public string Name
		{
			get
			{
				this.InitNameAndMvid();
				return this.name;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00026D3B File Offset: 0x00024F3B
		public Guid Mvid
		{
			get
			{
				this.InitNameAndMvid();
				return this.mvid;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00026D49 File Offset: 0x00024F49
		public void Dispose()
		{
			if (this.importInterface == null)
			{
				Marshal.ReleaseComObject(this.importInterface);
			}
			this.importInterface = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00026D6C File Offset: 0x00024F6C
		~MetaDataInfo()
		{
			this.Dispose();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00026D98 File Offset: 0x00024F98
		public static Guid GetMvid(Assembly assembly)
		{
			Guid guid;
			using (MetaDataInfo metaDataInfo = new MetaDataInfo(assembly))
			{
				guid = metaDataInfo.Mvid;
			}
			return guid;
		}

		// Token: 0x04000538 RID: 1336
		private IMetaDataImportInternalOnly importInterface;

		// Token: 0x04000539 RID: 1337
		private string name;

		// Token: 0x0400053A RID: 1338
		private Guid mvid;
	}
}

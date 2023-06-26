using System;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x02000092 RID: 146
	internal sealed class BinaryTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00014F2B File Offset: 0x0001312B
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00014F32 File Offset: 0x00013132
		internal static Type BinaryType { get; set; }

		// Token: 0x06000545 RID: 1349 RVA: 0x00014F3C File Offset: 0x0001313C
		internal override object Parse(string text)
		{
			return Activator.CreateInstance(BinaryTypeConverter.BinaryType, new object[] { Convert.FromBase64String(text) });
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00014F64 File Offset: 0x00013164
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00014F6C File Offset: 0x0001316C
		internal byte[] ToArray(object instance)
		{
			if (this.convertToByteArrayMethodInfo == null)
			{
				this.convertToByteArrayMethodInfo = instance.GetType().GetMethod("ToArray", BindingFlags.Instance | BindingFlags.Public);
			}
			return (byte[])this.convertToByteArrayMethodInfo.Invoke(instance, null);
		}

		// Token: 0x04000306 RID: 774
		private MethodInfo convertToByteArrayMethodInfo;
	}
}

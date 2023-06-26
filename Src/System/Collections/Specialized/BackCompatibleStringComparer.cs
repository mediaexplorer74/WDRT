using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020003B9 RID: 953
	internal class BackCompatibleStringComparer : IEqualityComparer
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x000A8969 File Offset: 0x000A6B69
		internal BackCompatibleStringComparer()
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A8974 File Offset: 0x000A6B74
		public unsafe static int GetHashCode(string obj)
		{
			char* ptr = obj;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 5381;
			char* ptr2 = ptr;
			int num2;
			while ((num2 = (int)(*ptr2)) != 0)
			{
				num = ((num << 5) + num) ^ num2;
				ptr2++;
			}
			return num;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A89B2 File Offset: 0x000A6BB2
		bool IEqualityComparer.Equals(object a, object b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000A89BC File Offset: 0x000A6BBC
		public virtual int GetHashCode(object o)
		{
			string text = o as string;
			if (text == null)
			{
				return o.GetHashCode();
			}
			return BackCompatibleStringComparer.GetHashCode(text);
		}

		// Token: 0x04001FDE RID: 8158
		internal static IEqualityComparer Default = new BackCompatibleStringComparer();
	}
}

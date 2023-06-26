using System;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;

namespace System.Data.Services.Common
{
	// Token: 0x020000F9 RID: 249
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class DataServiceKeyAttribute : Attribute
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00023310 File Offset: 0x00021510
		public DataServiceKeyAttribute(string keyName)
		{
			Util.CheckArgumentNull<string>(keyName, "keyName");
			Util.CheckArgumentNullAndEmpty(keyName, "KeyName");
			this.keyNames = new ReadOnlyCollection<string>(new string[] { keyName });
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00023364 File Offset: 0x00021564
		public DataServiceKeyAttribute(params string[] keyNames)
		{
			Util.CheckArgumentNull<string[]>(keyNames, "keyNames");
			if (keyNames.Length != 0)
			{
				if (!keyNames.Any((string f) => f == null || f.Length == 0))
				{
					this.keyNames = new ReadOnlyCollection<string>(keyNames);
					return;
				}
			}
			throw Error.Argument(Strings.DSKAttribute_MustSpecifyAtleastOnePropertyName, "keyNames");
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x000233C9 File Offset: 0x000215C9
		public ReadOnlyCollection<string> KeyNames
		{
			get
			{
				return this.keyNames;
			}
		}

		// Token: 0x040004E5 RID: 1253
		private readonly ReadOnlyCollection<string> keyNames;
	}
}

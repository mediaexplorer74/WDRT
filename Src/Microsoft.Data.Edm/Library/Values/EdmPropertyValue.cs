﻿using System;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001D9 RID: 473
	public class EdmPropertyValue : IEdmPropertyValue, IEdmDelayedValue
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00020DBF File Offset: 0x0001EFBF
		public EdmPropertyValue(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00020DDA File Offset: 0x0001EFDA
		public EdmPropertyValue(string name, IEdmValue value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00020E08 File Offset: 0x0001F008
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00020E10 File Offset: 0x0001F010
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00020E18 File Offset: 0x0001F018
		public IEdmValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
				if (this.value != null)
				{
					throw new InvalidOperationException(Strings.ValueHasAlreadyBeenSet);
				}
				this.value = value;
			}
		}

		// Token: 0x04000549 RID: 1353
		private readonly string name;

		// Token: 0x0400054A RID: 1354
		private IEdmValue value;
	}
}

using System;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000494 RID: 1172
	internal class Com2Enum
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x00142C84 File Offset: 0x00140E84
		public Com2Enum(string[] names, object[] values, bool allowUnknownValues)
		{
			this.allowUnknownValues = allowUnknownValues;
			if (names == null || values == null || names.Length != values.Length)
			{
				throw new ArgumentException(SR.GetString("COM2NamesAndValuesNotEqual"));
			}
			this.PopulateArrays(names, values);
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x00142CB9 File Offset: 0x00140EB9
		public bool IsStrictEnum
		{
			get
			{
				return !this.allowUnknownValues;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x00142CC4 File Offset: 0x00140EC4
		public virtual object[] Values
		{
			get
			{
				return (object[])this.values.Clone();
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x00142CD6 File Offset: 0x00140ED6
		public virtual string[] Names
		{
			get
			{
				return (string[])this.names.Clone();
			}
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00142CE8 File Offset: 0x00140EE8
		public virtual object FromString(string s)
		{
			int num = -1;
			for (int i = 0; i < this.stringValues.Length; i++)
			{
				if (string.Compare(this.names[i], s, true, CultureInfo.InvariantCulture) == 0 || string.Compare(this.stringValues[i], s, true, CultureInfo.InvariantCulture) == 0)
				{
					return this.values[i];
				}
				if (num == -1 && string.Compare(this.names[i], s, true, CultureInfo.InvariantCulture) == 0)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				return this.values[num];
			}
			if (!this.allowUnknownValues)
			{
				return null;
			}
			return s;
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00142D74 File Offset: 0x00140F74
		protected virtual void PopulateArrays(string[] names, object[] values)
		{
			this.names = new string[names.Length];
			this.stringValues = new string[names.Length];
			this.values = new object[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				this.names[i] = names[i];
				this.values[i] = values[i];
				if (values[i] != null)
				{
					this.stringValues[i] = values[i].ToString();
				}
			}
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00142DE4 File Offset: 0x00140FE4
		public virtual string ToString(object v)
		{
			if (v != null)
			{
				if (this.values.Length != 0 && v.GetType() != this.values[0].GetType())
				{
					try
					{
						v = Convert.ChangeType(v, this.values[0].GetType(), CultureInfo.InvariantCulture);
					}
					catch
					{
					}
				}
				string text = v.ToString();
				for (int i = 0; i < this.values.Length; i++)
				{
					if (string.Compare(this.stringValues[i], text, true, CultureInfo.InvariantCulture) == 0)
					{
						return this.names[i];
					}
				}
				if (this.allowUnknownValues)
				{
					return text;
				}
			}
			return "";
		}

		// Token: 0x040033FD RID: 13309
		private string[] names;

		// Token: 0x040033FE RID: 13310
		private object[] values;

		// Token: 0x040033FF RID: 13311
		private string[] stringValues;

		// Token: 0x04003400 RID: 13312
		private bool allowUnknownValues;
	}
}

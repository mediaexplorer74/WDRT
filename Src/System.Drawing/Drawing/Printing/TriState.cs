using System;

namespace System.Drawing.Printing
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	internal struct TriState
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00020BDF File Offset: 0x0001EDDF
		private TriState(byte value)
		{
			this.value = value;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00020BE8 File Offset: 0x0001EDE8
		public bool IsDefault
		{
			get
			{
				return this == TriState.Default;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00020BFA File Offset: 0x0001EDFA
		public bool IsFalse
		{
			get
			{
				return this == TriState.False;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public bool IsNotDefault
		{
			get
			{
				return this != TriState.Default;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00020C1E File Offset: 0x0001EE1E
		public bool IsTrue
		{
			get
			{
				return this == TriState.True;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00020C30 File Offset: 0x0001EE30
		public static bool operator ==(TriState left, TriState right)
		{
			return left.value == right.value;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00020C40 File Offset: 0x0001EE40
		public static bool operator !=(TriState left, TriState right)
		{
			return !(left == right);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public override bool Equals(object o)
		{
			TriState triState = (TriState)o;
			return this.value == triState.value;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00020C6E File Offset: 0x0001EE6E
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00020C76 File Offset: 0x0001EE76
		public static implicit operator TriState(bool value)
		{
			if (!value)
			{
				return TriState.False;
			}
			return TriState.True;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00020C86 File Offset: 0x0001EE86
		public static explicit operator bool(TriState value)
		{
			if (value.IsDefault)
			{
				throw new InvalidCastException(SR.GetString("TriStateCompareError"));
			}
			return value == TriState.True;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00020CAC File Offset: 0x0001EEAC
		public override string ToString()
		{
			if (this == TriState.Default)
			{
				return "Default";
			}
			if (this == TriState.False)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x04000700 RID: 1792
		private byte value;

		// Token: 0x04000701 RID: 1793
		public static readonly TriState Default = new TriState(0);

		// Token: 0x04000702 RID: 1794
		public static readonly TriState False = new TriState(1);

		// Token: 0x04000703 RID: 1795
		public static readonly TriState True = new TriState(2);
	}
}

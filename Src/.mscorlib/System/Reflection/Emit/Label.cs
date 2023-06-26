using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a label in the instruction stream. <see langword="Label" /> is used in conjunction with the <see cref="T:System.Reflection.Emit.ILGenerator" /> class.</summary>
	// Token: 0x02000642 RID: 1602
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x06004B1D RID: 19229 RVA: 0x00111594 File Offset: 0x0010F794
		internal Label(int label)
		{
			this.m_label = label;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0011159D File Offset: 0x0010F79D
		internal int GetLabelValue()
		{
			return this.m_label;
		}

		/// <summary>Generates a hash code for this instance.</summary>
		/// <returns>A hash code for this instance.</returns>
		// Token: 0x06004B1F RID: 19231 RVA: 0x001115A5 File Offset: 0x0010F7A5
		public override int GetHashCode()
		{
			return this.m_label;
		}

		/// <summary>Checks if the given object is an instance of <see langword="Label" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this <see langword="Label" /> instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="Label" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B20 RID: 19232 RVA: 0x001115AD File Offset: 0x0010F7AD
		public override bool Equals(object obj)
		{
			return obj is Label && this.Equals((Label)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.Label" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.Label" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B21 RID: 19233 RVA: 0x001115C5 File Offset: 0x0010F7C5
		public bool Equals(Label obj)
		{
			return obj.m_label == this.m_label;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B22 RID: 19234 RVA: 0x001115D5 File Offset: 0x0010F7D5
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B23 RID: 19235 RVA: 0x001115DF File Offset: 0x0010F7DF
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04001F0D RID: 7949
		internal int m_label;
	}
}

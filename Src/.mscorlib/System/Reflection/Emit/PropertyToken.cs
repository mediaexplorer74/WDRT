using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="PropertyToken" /> struct is an opaque representation of the <see langword="Token" /> returned by the metadata to represent a property.</summary>
	// Token: 0x0200065C RID: 1628
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x06004D0A RID: 19722 RVA: 0x00118AC4 File Offset: 0x00116CC4
		internal PropertyToken(int str)
		{
			this.m_property = str;
		}

		/// <summary>Retrieves the metadata token for this property.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this instance.</returns>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x00118ACD File Offset: 0x00116CCD
		public int Token
		{
			get
			{
				return this.m_property;
			}
		}

		/// <summary>Generates the hash code for this property.</summary>
		/// <returns>The hash code for this property.</returns>
		// Token: 0x06004D0C RID: 19724 RVA: 0x00118AD5 File Offset: 0x00116CD5
		public override int GetHashCode()
		{
			return this.m_property;
		}

		/// <summary>Checks if the given object is an instance of <see langword="PropertyToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="PropertyToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D0D RID: 19725 RVA: 0x00118ADD File Offset: 0x00116CDD
		public override bool Equals(object obj)
		{
			return obj is PropertyToken && this.Equals((PropertyToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.PropertyToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D0E RID: 19726 RVA: 0x00118AF5 File Offset: 0x00116CF5
		public bool Equals(PropertyToken obj)
		{
			return obj.m_property == this.m_property;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D0F RID: 19727 RVA: 0x00118B05 File Offset: 0x00116D05
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D10 RID: 19728 RVA: 0x00118B0F File Offset: 0x00116D0F
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="PropertyToken" /> with <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> value 0.</summary>
		// Token: 0x040021A3 RID: 8611
		public static readonly PropertyToken Empty;

		// Token: 0x040021A4 RID: 8612
		internal int m_property;
	}
}

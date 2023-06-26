using System;

namespace System.Globalization
{
	/// <summary>Provides information about the version of Unicode used to compare and order strings.</summary>
	// Token: 0x020003DE RID: 990
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		/// <summary>Gets the full version number of the <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <returns>The version number of this <see cref="T:System.Globalization.SortVersion" /> object.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000C609E File Offset: 0x000C429E
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		/// <summary>Gets a globally unique identifier for this <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <returns>A globally unique identifier for this <see cref="T:System.Globalization.SortVersion" /> object.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000C60A6 File Offset: 0x000C42A6
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Globalization.SortVersion" /> class.</summary>
		/// <param name="fullVersion">A version number.</param>
		/// <param name="sortId">A sort ID.</param>
		// Token: 0x060032FE RID: 13054 RVA: 0x000C60AE File Offset: 0x000C42AE
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000C60C4 File Offset: 0x000C42C4
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte[] bytes = BitConverter.GetBytes(effectiveId);
				byte b = (byte)((uint)effectiveId >> 24);
				byte b2 = (byte)((effectiveId & 16711680) >> 16);
				byte b3 = (byte)((effectiveId & 65280) >> 8);
				byte b4 = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, b, b2, b3, b4);
			}
			this.m_SortId = customVersion;
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Globalization.SortVersion" /> instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Globalization.SortVersion" /> object that represents the same version as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003300 RID: 13056 RVA: 0x000C6134 File Offset: 0x000C4334
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Globalization.SortVersion" /> instance is equal to a specified <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> represents the same version as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003301 RID: 13057 RVA: 0x000C615A File Offset: 0x000C435A
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003302 RID: 13058 RVA: 0x000C6188 File Offset: 0x000C4388
		public override int GetHashCode()
		{
			return (this.m_NlsVersion * 7) | this.m_SortId.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Globalization.SortVersion" /> instances are equal.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003303 RID: 13059 RVA: 0x000C61A4 File Offset: 0x000C43A4
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		/// <summary>Indicates whether two <see cref="T:System.Globalization.SortVersion" /> instances are not equal.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003304 RID: 13060 RVA: 0x000C61BD File Offset: 0x000C43BD
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x0400169E RID: 5790
		private int m_NlsVersion;

		// Token: 0x0400169F RID: 5791
		private Guid m_SortId;
	}
}

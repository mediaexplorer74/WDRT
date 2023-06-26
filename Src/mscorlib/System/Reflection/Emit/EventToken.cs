using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the <see langword="Token" /> returned by the metadata to represent an event.</summary>
	// Token: 0x02000638 RID: 1592
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x06004A88 RID: 19080 RVA: 0x0010ECA1 File Offset: 0x0010CEA1
		internal EventToken(int str)
		{
			this.m_event = str;
		}

		/// <summary>Retrieves the metadata token for this event.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this event.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0010ECAA File Offset: 0x0010CEAA
		public int Token
		{
			get
			{
				return this.m_event;
			}
		}

		/// <summary>Generates the hash code for this event.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06004A8A RID: 19082 RVA: 0x0010ECB2 File Offset: 0x0010CEB2
		public override int GetHashCode()
		{
			return this.m_event;
		}

		/// <summary>Checks if the given object is an instance of <see langword="EventToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to be compared with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="EventToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A8B RID: 19083 RVA: 0x0010ECBA File Offset: 0x0010CEBA
		public override bool Equals(object obj)
		{
			return obj is EventToken && this.Equals((EventToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.EventToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A8C RID: 19084 RVA: 0x0010ECD2 File Offset: 0x0010CED2
		public bool Equals(EventToken obj)
		{
			return obj.m_event == this.m_event;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A8D RID: 19085 RVA: 0x0010ECE2 File Offset: 0x0010CEE2
		public static bool operator ==(EventToken a, EventToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A8E RID: 19086 RVA: 0x0010ECEC File Offset: 0x0010CEEC
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="EventToken" /> with <see cref="P:System.Reflection.Emit.EventToken.Token" /> value 0.</summary>
		// Token: 0x04001EBB RID: 7867
		public static readonly EventToken Empty;

		// Token: 0x04001EBC RID: 7868
		internal int m_event;
	}
}

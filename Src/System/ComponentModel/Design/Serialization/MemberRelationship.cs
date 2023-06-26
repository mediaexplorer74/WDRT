using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents a single relationship between an object and a member.</summary>
	// Token: 0x02000610 RID: 1552
	public struct MemberRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> class.</summary>
		/// <param name="owner">The object that owns <paramref name="member" />.</param>
		/// <param name="member">The member which is to be related to <paramref name="owner" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> or <paramref name="member" /> is <see langword="null" />.</exception>
		// Token: 0x060038C9 RID: 14537 RVA: 0x000F1846 File Offset: 0x000EFA46
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this._owner = owner;
			this._member = member;
		}

		/// <summary>Gets a value indicating whether this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship.</summary>
		/// <returns>
		///   <see langword="true" /> if this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000F1872 File Offset: 0x000EFA72
		public bool IsEmpty
		{
			get
			{
				return this._owner == null;
			}
		}

		/// <summary>Gets the related member.</summary>
		/// <returns>The member that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x000F187D File Offset: 0x000EFA7D
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		/// <summary>Gets the owning object.</summary>
		/// <returns>The owning object that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000F1885 File Offset: 0x000EFA85
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> to compare with the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> is equal to the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060038CD RID: 14541 RVA: 0x000F1890 File Offset: 0x000EFA90
		public override bool Equals(object obj)
		{
			if (!(obj is MemberRelationship))
			{
				return false;
			}
			MemberRelationship memberRelationship = (MemberRelationship)obj;
			return memberRelationship.Owner == this.Owner && memberRelationship.Member == this.Member;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</returns>
		// Token: 0x060038CE RID: 14542 RVA: 0x000F18CE File Offset: 0x000EFACE
		public override int GetHashCode()
		{
			if (this._owner == null)
			{
				return base.GetHashCode();
			}
			return this._owner.GetHashCode() ^ this._member.GetHashCode();
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the equality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060038CF RID: 14543 RVA: 0x000F1900 File Offset: 0x000EFB00
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different.</summary>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the inequality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x060038D0 RID: 14544 RVA: 0x000F1924 File Offset: 0x000EFB24
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		// Token: 0x04002B62 RID: 11106
		private object _owner;

		// Token: 0x04002B63 RID: 11107
		private MemberDescriptor _member;

		/// <summary>Represents the empty member relationship. This field is read-only.</summary>
		// Token: 0x04002B64 RID: 11108
		public static readonly MemberRelationship Empty;
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that an object's text representation is obscured by characters such as asterisks. This class cannot be inherited.</summary>
	// Token: 0x02000594 RID: 1428
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class.</summary>
		// Token: 0x06003509 RID: 13577 RVA: 0x000E7382 File Offset: 0x000E5582
		public PasswordPropertyTextAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class, optionally showing password text.</summary>
		/// <param name="password">
		///   <see langword="true" /> to indicate that the property should be shown as password text; otherwise, <see langword="false" />. The default is <see langword="false" />.</param>
		// Token: 0x0600350A RID: 13578 RVA: 0x000E738B File Offset: 0x000E558B
		public PasswordPropertyTextAttribute(bool password)
		{
			this._password = password;
		}

		/// <summary>Gets a value indicating if the property for which the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is defined should be shown as password text.</summary>
		/// <returns>
		///   <see langword="true" /> if the property should be shown as password text; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000E739A File Offset: 0x000E559A
		public bool Password
		{
			get
			{
				return this._password;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> instances are equal.</summary>
		/// <param name="o">The <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> to compare with the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is equal to the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600350C RID: 13580 RVA: 0x000E73A2 File Offset: 0x000E55A2
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this._password;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</returns>
		// Token: 0x0600350D RID: 13581 RVA: 0x000E73C1 File Offset: 0x000E55C1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns an indication whether the value of this instance is the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600350E RID: 13582 RVA: 0x000E73C9 File Offset: 0x000E55C9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PasswordPropertyTextAttribute.Default);
		}

		/// <summary>Specifies that a text property is used as a password. This <see langword="static" /> (<see langword="Shared" /> in Visual Basic) field is read-only.</summary>
		// Token: 0x04002A21 RID: 10785
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		/// <summary>Specifies that a text property is not used as a password. This <see langword="static" /> (<see langword="Shared" /> in Visual Basic) field is read-only.</summary>
		// Token: 0x04002A22 RID: 10786
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</summary>
		// Token: 0x04002A23 RID: 10787
		public static readonly PasswordPropertyTextAttribute Default = PasswordPropertyTextAttribute.No;

		// Token: 0x04002A24 RID: 10788
		private bool _password;
	}
}

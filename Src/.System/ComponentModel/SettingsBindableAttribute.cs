using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x020005AA RID: 1450
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to specify that a property is appropriate to bind settings to; otherwise, <see langword="false" />.</param>
		// Token: 0x0600360A RID: 13834 RVA: 0x000EBD35 File Offset: 0x000E9F35
		public SettingsBindableAttribute(bool bindable)
		{
			this._bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is appropriate to bind settings to; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000EBD44 File Offset: 0x000E9F44
		public bool Bindable
		{
			get
			{
				return this._bindable;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600360C RID: 13836 RVA: 0x000EBD4C File Offset: 0x000E9F4C
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this._bindable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600360D RID: 13837 RVA: 0x000EBD74 File Offset: 0x000E9F74
		public override int GetHashCode()
		{
			return this._bindable.GetHashCode();
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04002A7F RID: 10879
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04002A80 RID: 10880
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);

		// Token: 0x04002A81 RID: 10881
		private bool _bindable;
	}
}

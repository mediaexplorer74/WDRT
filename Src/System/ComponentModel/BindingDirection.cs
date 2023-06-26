using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the template can be bound one way or two ways.</summary>
	// Token: 0x0200051B RID: 1307
	public enum BindingDirection
	{
		/// <summary>The template can only accept property values. Used with a generic <see cref="T:System.Web.UI.ITemplate" />.</summary>
		// Token: 0x04002917 RID: 10519
		OneWay,
		/// <summary>The template can accept and expose property values. Used with an <see cref="T:System.Web.UI.IBindableTemplate" />.</summary>
		// Token: 0x04002918 RID: 10520
		TwoWay
	}
}

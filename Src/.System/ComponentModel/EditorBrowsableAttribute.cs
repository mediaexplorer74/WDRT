using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a property or method is viewable in an editor. This class cannot be inherited.</summary>
	// Token: 0x0200054B RID: 1355
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	[global::__DynamicallyInvokable]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with an <see cref="T:System.ComponentModel.EditorBrowsableState" />.</summary>
		/// <param name="state">The <see cref="T:System.ComponentModel.EditorBrowsableState" /> to set <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> to.</param>
		// Token: 0x060032E2 RID: 13026 RVA: 0x000E2593 File Offset: 0x000E0793
		[global::__DynamicallyInvokable]
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.browsableState = state;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> set to the default state.</summary>
		// Token: 0x060032E3 RID: 13027 RVA: 0x000E25A2 File Offset: 0x000E07A2
		public EditorBrowsableAttribute()
			: this(EditorBrowsableState.Always)
		{
		}

		/// <summary>Gets the browsable state of the property or method.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EditorBrowsableState" /> that is the browsable state of the property or method.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000E25AB File Offset: 0x000E07AB
		[global::__DynamicallyInvokable]
		public EditorBrowsableState State
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.browsableState;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorBrowsableAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032E5 RID: 13029 RVA: 0x000E25B4 File Offset: 0x000E07B4
		[global::__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorBrowsableAttribute editorBrowsableAttribute = obj as EditorBrowsableAttribute;
			return editorBrowsableAttribute != null && editorBrowsableAttribute.browsableState == this.browsableState;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032E6 RID: 13030 RVA: 0x000E25E1 File Offset: 0x000E07E1
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400298F RID: 10639
		private EditorBrowsableState browsableState;
	}
}

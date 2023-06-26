using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x0200053D RID: 1341
	[AttributeUsage(AttributeTargets.All)]
	[global::__DynamicallyInvokable]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to.</param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture.</param>
		// Token: 0x06003279 RID: 12921 RVA: 0x000E1C68 File Offset: 0x000DFE68
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value.</param>
		// Token: 0x0600327A RID: 12922 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(char value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value.</param>
		// Token: 0x0600327B RID: 12923 RVA: 0x000E1CB8 File Offset: 0x000DFEB8
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(byte value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value.</param>
		// Token: 0x0600327C RID: 12924 RVA: 0x000E1CCC File Offset: 0x000DFECC
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(short value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value.</param>
		// Token: 0x0600327D RID: 12925 RVA: 0x000E1CE0 File Offset: 0x000DFEE0
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(int value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value.</param>
		// Token: 0x0600327E RID: 12926 RVA: 0x000E1CF4 File Offset: 0x000DFEF4
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(long value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value.</param>
		// Token: 0x0600327F RID: 12927 RVA: 0x000E1D08 File Offset: 0x000DFF08
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(float value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value.</param>
		// Token: 0x06003280 RID: 12928 RVA: 0x000E1D1C File Offset: 0x000DFF1C
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(double value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value.</param>
		// Token: 0x06003281 RID: 12929 RVA: 0x000E1D30 File Offset: 0x000DFF30
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(bool value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value.</param>
		// Token: 0x06003282 RID: 12930 RVA: 0x000E1D44 File Offset: 0x000DFF44
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value.</param>
		// Token: 0x06003283 RID: 12931 RVA: 0x000E1D53 File Offset: 0x000DFF53
		[global::__DynamicallyInvokable]
		public DefaultValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000E1D62 File Offset: 0x000DFF62
		[global::__DynamicallyInvokable]
		public virtual object Value
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003285 RID: 12933 RVA: 0x000E1D6C File Offset: 0x000DFF6C
		[global::__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003286 RID: 12934 RVA: 0x000E1DAE File Offset: 0x000DFFAE
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x06003287 RID: 12935 RVA: 0x000E1DB6 File Offset: 0x000DFFB6
		protected void SetValue(object value)
		{
			this.value = value;
		}

		// Token: 0x0400296A RID: 10602
		private object value;
	}
}

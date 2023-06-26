using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x0200050D RID: 1293
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter.</param>
		/// <param name="value">The value for this attribute.</param>
		// Token: 0x060030FD RID: 12541 RVA: 0x000DE398 File Offset: 0x000DC598
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x060030FE RID: 12542 RVA: 0x000DE3D4 File Offset: 0x000DC5D4
		public AmbientValueAttribute(char value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x060030FF RID: 12543 RVA: 0x000DE3E8 File Offset: 0x000DC5E8
		public AmbientValueAttribute(byte value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003100 RID: 12544 RVA: 0x000DE3FC File Offset: 0x000DC5FC
		public AmbientValueAttribute(short value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003101 RID: 12545 RVA: 0x000DE410 File Offset: 0x000DC610
		public AmbientValueAttribute(int value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003102 RID: 12546 RVA: 0x000DE424 File Offset: 0x000DC624
		public AmbientValueAttribute(long value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003103 RID: 12547 RVA: 0x000DE438 File Offset: 0x000DC638
		public AmbientValueAttribute(float value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003104 RID: 12548 RVA: 0x000DE44C File Offset: 0x000DC64C
		public AmbientValueAttribute(double value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003105 RID: 12549 RVA: 0x000DE460 File Offset: 0x000DC660
		public AmbientValueAttribute(bool value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003106 RID: 12550 RVA: 0x000DE474 File Offset: 0x000DC674
		public AmbientValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06003107 RID: 12551 RVA: 0x000DE483 File Offset: 0x000DC683
		public AmbientValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000DE492 File Offset: 0x000DC692
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003109 RID: 12553 RVA: 0x000DE49C File Offset: 0x000DC69C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.value != null)
			{
				return this.value.Equals(ambientValueAttribute.Value);
			}
			return ambientValueAttribute.Value == null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x0600310A RID: 12554 RVA: 0x000DE4DE File Offset: 0x000DC6DE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040028EE RID: 10478
		private readonly object value;
	}
}

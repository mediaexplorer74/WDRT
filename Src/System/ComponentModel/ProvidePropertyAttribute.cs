using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x0200059D RID: 1437
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type.</param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property.</param>
		// Token: 0x06003574 RID: 13684 RVA: 0x000E8312 File Offset: 0x000E6512
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type.</param>
		/// <param name="receiverTypeName">The name of the data type this property can extend.</param>
		// Token: 0x06003575 RID: 13685 RVA: 0x000E832D File Offset: 0x000E652D
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverTypeName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000E8343 File Offset: 0x000E6543
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000E834B File Offset: 0x000E654B
		public string ReceiverTypeName
		{
			get
			{
				return this.receiverTypeName;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000E8353 File Offset: 0x000E6553
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.propertyName;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003579 RID: 13689 RVA: 0x000E836C File Offset: 0x000E656C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.propertyName == this.propertyName && providePropertyAttribute.receiverTypeName == this.receiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x0600357A RID: 13690 RVA: 0x000E83AF File Offset: 0x000E65AF
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode() ^ this.receiverTypeName.GetHashCode();
		}

		// Token: 0x04002A38 RID: 10808
		private readonly string propertyName;

		// Token: 0x04002A39 RID: 10809
		private readonly string receiverTypeName;
	}
}

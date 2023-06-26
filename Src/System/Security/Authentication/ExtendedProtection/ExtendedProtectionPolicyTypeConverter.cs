using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicyTypeConverter" /> class represents the type converter for extended protection policy used by the server to validate incoming client connections.</summary>
	// Token: 0x02000443 RID: 1091
	public class ExtendedProtectionPolicyTypeConverter : TypeConverter
	{
		/// <summary>Returns whether this converter can convert the object to the specified type.</summary>
		/// <param name="context">The object to convert.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise <see langword="false" />.</returns>
		// Token: 0x06002884 RID: 10372 RVA: 0x000BA031 File Offset: 0x000B8231
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Convert the object to the specified type</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. This should be a <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		// Token: 0x06002885 RID: 10373 RVA: 0x000BA050 File Offset: 0x000B8250
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				ExtendedProtectionPolicy extendedProtectionPolicy = value as ExtendedProtectionPolicy;
				if (extendedProtectionPolicy != null)
				{
					Type[] array;
					object[] array2;
					if (extendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Never)
					{
						array = new Type[] { typeof(PolicyEnforcement) };
						array2 = new object[] { PolicyEnforcement.Never };
					}
					else
					{
						array = new Type[]
						{
							typeof(PolicyEnforcement),
							typeof(ProtectionScenario),
							typeof(ICollection)
						};
						object[] array3 = null;
						if (extendedProtectionPolicy.CustomServiceNames != null && extendedProtectionPolicy.CustomServiceNames.Count > 0)
						{
							array3 = new object[extendedProtectionPolicy.CustomServiceNames.Count];
							((ICollection)extendedProtectionPolicy.CustomServiceNames).CopyTo(array3, 0);
						}
						array2 = new object[] { extendedProtectionPolicy.PolicyEnforcement, extendedProtectionPolicy.ProtectionScenario, array3 };
					}
					ConstructorInfo constructor = typeof(ExtendedProtectionPolicy).GetConstructor(array);
					return new InstanceDescriptor(constructor, array2);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}

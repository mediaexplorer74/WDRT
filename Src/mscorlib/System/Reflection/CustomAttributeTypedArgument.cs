using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	/// <summary>Represents an argument of a custom attribute in the reflection-only context, or an element of an array argument.</summary>
	// Token: 0x020005D4 RID: 1492
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct CustomAttributeTypedArgument
	{
		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600456C RID: 17772 RVA: 0x00100588 File Offset: 0x000FE788
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600456D RID: 17773 RVA: 0x0010059D File Offset: 0x000FE79D
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x001005B8 File Offset: 0x000FE7B8
		private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
		{
			if (encodedType <= CustomAttributeEncoding.Array)
			{
				switch (encodedType)
				{
				case CustomAttributeEncoding.Boolean:
					return typeof(bool);
				case CustomAttributeEncoding.Char:
					return typeof(char);
				case CustomAttributeEncoding.SByte:
					return typeof(sbyte);
				case CustomAttributeEncoding.Byte:
					return typeof(byte);
				case CustomAttributeEncoding.Int16:
					return typeof(short);
				case CustomAttributeEncoding.UInt16:
					return typeof(ushort);
				case CustomAttributeEncoding.Int32:
					return typeof(int);
				case CustomAttributeEncoding.UInt32:
					return typeof(uint);
				case CustomAttributeEncoding.Int64:
					return typeof(long);
				case CustomAttributeEncoding.UInt64:
					return typeof(ulong);
				case CustomAttributeEncoding.Float:
					return typeof(float);
				case CustomAttributeEncoding.Double:
					return typeof(double);
				case CustomAttributeEncoding.String:
					return typeof(string);
				default:
					if (encodedType == CustomAttributeEncoding.Array)
					{
						return typeof(Array);
					}
					break;
				}
			}
			else
			{
				if (encodedType == CustomAttributeEncoding.Type)
				{
					return typeof(Type);
				}
				if (encodedType == CustomAttributeEncoding.Object)
				{
					return typeof(object);
				}
				if (encodedType == CustomAttributeEncoding.Enum)
				{
					return typeof(Enum);
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)encodedType }), "encodedType");
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00100704 File Offset: 0x000FE904
		[SecuritySafeCritical]
		private unsafe static object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
		{
			switch (encodedType)
			{
			case CustomAttributeEncoding.Boolean:
				return (byte)val > 0;
			case CustomAttributeEncoding.Char:
				return (char)val;
			case CustomAttributeEncoding.SByte:
				return (sbyte)val;
			case CustomAttributeEncoding.Byte:
				return (byte)val;
			case CustomAttributeEncoding.Int16:
				return (short)val;
			case CustomAttributeEncoding.UInt16:
				return (ushort)val;
			case CustomAttributeEncoding.Int32:
				return (int)val;
			case CustomAttributeEncoding.UInt32:
				return (uint)val;
			case CustomAttributeEncoding.Int64:
				return val;
			case CustomAttributeEncoding.UInt64:
				return (ulong)val;
			case CustomAttributeEncoding.Float:
				return *(float*)(&val);
			case CustomAttributeEncoding.Double:
				return *(double*)(&val);
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)val }), "val");
			}
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x001007D4 File Offset: 0x000FE9D4
		private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
		{
			RuntimeType typeByNameUsingCARules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
			if (typeByNameUsingCARules == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_CATypeResolutionFailed"), typeName));
			}
			return typeByNameUsingCARules;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified type and value.</summary>
		/// <param name="argumentType">The type of the custom attribute argument.</param>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argumentType" /> is <see langword="null" />.</exception>
		// Token: 0x06004571 RID: 17777 RVA: 0x0010080E File Offset: 0x000FEA0E
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.m_value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.m_argumentType = argumentType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified value.</summary>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06004572 RID: 17778 RVA: 0x0010083D File Offset: 0x000FEA3D
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.m_argumentType = value.GetType();
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00100865 File Offset: 0x000FEA65
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00100884 File Offset: 0x000FEA84
		internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
		{
			CustomAttributeEncoding customAttributeEncoding = encodedArg.CustomAttributeType.EncodedType;
			if (customAttributeEncoding == CustomAttributeEncoding.Undefined)
			{
				throw new ArgumentException("encodedArg");
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum)
			{
				this.m_argumentType = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedArg.CustomAttributeType.EncodedEnumType);
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.String)
			{
				this.m_argumentType = typeof(string);
				this.m_value = encodedArg.StringValue;
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Type)
			{
				this.m_argumentType = typeof(Type);
				this.m_value = null;
				if (encodedArg.StringValue != null)
				{
					this.m_value = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
					return;
				}
			}
			else if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				customAttributeEncoding = encodedArg.CustomAttributeType.EncodedArrayType;
				Type type;
				if (customAttributeEncoding == CustomAttributeEncoding.Enum)
				{
					type = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				}
				else
				{
					type = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				}
				this.m_argumentType = type.MakeArrayType();
				if (encodedArg.ArrayValue == null)
				{
					this.m_value = null;
					return;
				}
				CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[i]);
				}
				this.m_value = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				return;
			}
			else
			{
				this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, customAttributeEncoding);
			}
		}

		/// <summary>Returns a string consisting of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string consisting of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06004575 RID: 17781 RVA: 0x00100A12 File Offset: 0x000FEC12
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00100A1C File Offset: 0x000FEC1C
		internal string ToString(bool typed)
		{
			if (this.m_argumentType == null)
			{
				return base.ToString();
			}
			if (this.ArgumentType.IsEnum)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullName);
			}
			if (this.Value == null)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.Name);
			}
			if (this.ArgumentType == typeof(string))
			{
				return string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
			}
			if (this.ArgumentType == typeof(char))
			{
				return string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
			}
			if (this.ArgumentType == typeof(Type))
			{
				return string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullName);
			}
			if (this.ArgumentType.IsArray)
			{
				IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
				Type elementType = this.ArgumentType.GetElementType();
				string text = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullName : elementType.Name, list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					text += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
				}
				return text + " }";
			}
			return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.Name);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06004577 RID: 17783 RVA: 0x00100C22 File Offset: 0x000FEE22
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004578 RID: 17784 RVA: 0x00100C34 File Offset: 0x000FEE34
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Gets the type of the argument or of the array argument element.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the argument or of the array element.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x00100C44 File Offset: 0x000FEE44
		[__DynamicallyInvokable]
		public Type ArgumentType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_argumentType;
			}
		}

		/// <summary>Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.</summary>
		/// <returns>An object that represents the value of the argument or element, or a generic <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> objects that represent the values of an array-type argument.</returns>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x00100C4C File Offset: 0x000FEE4C
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001C6A RID: 7274
		private object m_value;

		// Token: 0x04001C6B RID: 7275
		private Type m_argumentType;
	}
}

using System;
using System.Globalization;

namespace System
{
	/// <summary>Represents a composite format string, along with the arguments to be formatted.</summary>
	// Token: 0x020000E5 RID: 229
	[__DynamicallyInvokable]
	public abstract class FormattableString : IFormattable
	{
		/// <summary>Returns the composite format string.</summary>
		/// <returns>The composite format string.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000E87 RID: 3719
		[__DynamicallyInvokable]
		public abstract string Format
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Returns an object array that contains one or more objects to format.</summary>
		/// <returns>An object array that contains one or more objects to format.</returns>
		// Token: 0x06000E88 RID: 3720
		[__DynamicallyInvokable]
		public abstract object[] GetArguments();

		/// <summary>Gets the number of arguments to be formatted.</summary>
		/// <returns>The number of arguments to be formatted.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000E89 RID: 3721
		[__DynamicallyInvokable]
		public abstract int ArgumentCount
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Returns the argument at the specified index position.</summary>
		/// <param name="index">The index of the argument. Its value can range from zero to one less than the value of <see cref="P:System.FormattableString.ArgumentCount" />.</param>
		/// <returns>The argument.</returns>
		// Token: 0x06000E8A RID: 3722
		[__DynamicallyInvokable]
		public abstract object GetArgument(int index);

		/// <summary>Returns the string that results from formatting the composite format string along with its arguments by using the formatting conventions of a specified culture.</summary>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A result string formatted by using the conventions of <paramref name="formatProvider" />.</returns>
		// Token: 0x06000E8B RID: 3723
		[__DynamicallyInvokable]
		public abstract string ToString(IFormatProvider formatProvider);

		/// <summary>Returns the string that results from formatting the format string along with its arguments by using the formatting conventions of a specified culture.</summary>
		/// <param name="ignored">A string. This argument is ignored.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A string formatted using the conventions of the <paramref name="formatProvider" /> parameter.</returns>
		// Token: 0x06000E8C RID: 3724 RVA: 0x0002CD0F File Offset: 0x0002AF0F
		[__DynamicallyInvokable]
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		/// <summary>Returns a result string in which arguments are formatted by using the conventions of the invariant culture.</summary>
		/// <param name="formattable">The object to convert to a result string.</param>
		/// <returns>The string that results from formatting the current instance by using the conventions of the invariant culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="formattable" /> is <see langword="null" />.</exception>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0002CD18 File Offset: 0x0002AF18
		[__DynamicallyInvokable]
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Returns the string that results from formatting the composite format string along with its arguments by using the formatting conventions of the current culture.</summary>
		/// <returns>A result string formatted by using the conventions of the current culture.</returns>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0002CD33 File Offset: 0x0002AF33
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Instantiates a new instance of the <see cref="T:System.FormattableString" /> class.</summary>
		// Token: 0x06000E8F RID: 3727 RVA: 0x0002CD40 File Offset: 0x0002AF40
		[__DynamicallyInvokable]
		protected FormattableString()
		{
		}
	}
}

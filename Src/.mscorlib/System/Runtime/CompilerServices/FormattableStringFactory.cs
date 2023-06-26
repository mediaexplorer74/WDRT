using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a static method to create a <see cref="T:System.FormattableString" /> object from a composite format string and its arguments.</summary>
	// Token: 0x020008F9 RID: 2297
	[__DynamicallyInvokable]
	public static class FormattableStringFactory
	{
		/// <summary>Creates a <see cref="T:System.FormattableString" /> instance from a composite format string and its arguments.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arguments">The arguments whose string representations are to be inserted in the result string.</param>
		/// <returns>The object that represents the composite format string and its arguments.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="arguments" /> is <see langword="null" />.</exception>
		// Token: 0x06005E67 RID: 24167 RVA: 0x0014CC5E File Offset: 0x0014AE5E
		[__DynamicallyInvokable]
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x02000C91 RID: 3217
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06007126 RID: 28966 RVA: 0x00186ACC File Offset: 0x00184CCC
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17001363 RID: 4963
			// (get) Token: 0x06007127 RID: 28967 RVA: 0x00186AE2 File Offset: 0x00184CE2
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06007128 RID: 28968 RVA: 0x00186AEA File Offset: 0x00184CEA
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17001364 RID: 4964
			// (get) Token: 0x06007129 RID: 28969 RVA: 0x00186AF2 File Offset: 0x00184CF2
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x0600712A RID: 28970 RVA: 0x00186AFC File Offset: 0x00184CFC
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x0600712B RID: 28971 RVA: 0x00186B06 File Offset: 0x00184D06
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x04003850 RID: 14416
			private readonly string _format;

			// Token: 0x04003851 RID: 14417
			private readonly object[] _arguments;
		}
	}
}

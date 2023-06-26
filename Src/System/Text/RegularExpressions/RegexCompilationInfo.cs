using System;
using System.Runtime.Serialization;

namespace System.Text.RegularExpressions
{
	/// <summary>Provides information about a regular expression that is used to compile a regular expression to a stand-alone assembly.</summary>
	// Token: 0x02000690 RID: 1680
	[Serializable]
	public class RegexCompilationInfo
	{
		// Token: 0x06003E1B RID: 15899 RVA: 0x00100095 File Offset: 0x000FE295
		[OnDeserializing]
		private void InitMatchTimeoutDefaultForOldVersionDeserialization(StreamingContext unusedContext)
		{
			this.matchTimeout = Regex.DefaultMatchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression to be included in an assembly.</summary>
		/// <param name="pattern">The regular expression to compile.</param>
		/// <param name="options">The regular expression options to use when compiling the regular expression.</param>
		/// <param name="name">The name of the type that represents the compiled regular expression.</param>
		/// <param name="fullnamespace">The namespace to which the new type belongs.</param>
		/// <param name="ispublic">
		///   <see langword="true" /> to make the compiled regular expression publicly visible; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fullnamespace" /> is <see langword="null" />.</exception>
		// Token: 0x06003E1C RID: 15900 RVA: 0x001000A2 File Offset: 0x000FE2A2
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic)
			: this(pattern, options, name, fullnamespace, ispublic, Regex.DefaultMatchTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression with a specified time-out value to be included in an assembly.</summary>
		/// <param name="pattern">The regular expression to compile.</param>
		/// <param name="options">The regular expression options to use when compiling the regular expression.</param>
		/// <param name="name">The name of the type that represents the compiled regular expression.</param>
		/// <param name="fullnamespace">The namespace to which the new type belongs.</param>
		/// <param name="ispublic">
		///   <see langword="true" /> to make the compiled regular expression publicly visible; otherwise, <see langword="false" />.</param>
		/// <param name="matchTimeout">The default time-out interval for the regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fullnamespace" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06003E1D RID: 15901 RVA: 0x001000B6 File Offset: 0x000FE2B6
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic, TimeSpan matchTimeout)
		{
			this.Pattern = pattern;
			this.Name = name;
			this.Namespace = fullnamespace;
			this.options = options;
			this.isPublic = ispublic;
			this.MatchTimeout = matchTimeout;
		}

		/// <summary>Gets or sets the regular expression to compile.</summary>
		/// <returns>The regular expression to compile.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x001000EB File Offset: 0x000FE2EB
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x001000F3 File Offset: 0x000FE2F3
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.pattern = value;
			}
		}

		/// <summary>Gets or sets the options to use when compiling the regular expression.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x0010010A File Offset: 0x000FE30A
		// (set) Token: 0x06003E21 RID: 15905 RVA: 0x00100112 File Offset: 0x000FE312
		public RegexOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Gets or sets the name of the type that represents the compiled regular expression.</summary>
		/// <returns>The name of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value for this property is an empty string.</exception>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x0010011B File Offset: 0x000FE31B
		// (set) Token: 0x06003E23 RID: 15907 RVA: 0x00100124 File Offset: 0x000FE324
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "value" }), "value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the namespace to which the new type belongs.</summary>
		/// <returns>The namespace of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x00100171 File Offset: 0x000FE371
		// (set) Token: 0x06003E25 RID: 15909 RVA: 0x00100179 File Offset: 0x000FE379
		public string Namespace
		{
			get
			{
				return this.nspace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.nspace = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the compiled regular expression has public visibility.</summary>
		/// <returns>
		///   <see langword="true" /> if the regular expression has public visibility; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x00100190 File Offset: 0x000FE390
		// (set) Token: 0x06003E27 RID: 15911 RVA: 0x00100198 File Offset: 0x000FE398
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		/// <summary>Gets or sets the regular expression's default time-out interval.</summary>
		/// <returns>The default maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x001001A1 File Offset: 0x000FE3A1
		// (set) Token: 0x06003E29 RID: 15913 RVA: 0x001001A9 File Offset: 0x000FE3A9
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.matchTimeout;
			}
			set
			{
				Regex.ValidateMatchTimeout(value);
				this.matchTimeout = value;
			}
		}

		// Token: 0x04002D55 RID: 11605
		private string pattern;

		// Token: 0x04002D56 RID: 11606
		private RegexOptions options;

		// Token: 0x04002D57 RID: 11607
		private string name;

		// Token: 0x04002D58 RID: 11608
		private string nspace;

		// Token: 0x04002D59 RID: 11609
		private bool isPublic;

		// Token: 0x04002D5A RID: 11610
		[OptionalField(VersionAdded = 2)]
		private TimeSpan matchTimeout;
	}
}

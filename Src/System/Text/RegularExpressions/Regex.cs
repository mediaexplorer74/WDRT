using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents an immutable regular expression.</summary>
	// Token: 0x02000685 RID: 1669
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Regex : ISerializable
	{
		/// <summary>Gets or sets a dictionary that maps numbered capturing groups to their index values.</summary>
		/// <returns>A dictionary that maps numbered capturing groups to their index values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Text.RegularExpressions.Regex.Caps" /> property in a set operation is <see langword="null" />.</exception>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000FBF63 File Offset: 0x000FA163
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000FBF6B File Offset: 0x000FA16B
		[CLSCompliant(false)]
		protected IDictionary Caps
		{
			get
			{
				return this.caps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.caps = value as Hashtable;
				if (this.caps == null)
				{
					this.caps = new Hashtable(value);
				}
			}
		}

		/// <summary>Gets or sets a dictionary that maps named capturing groups to their index values.</summary>
		/// <returns>A dictionary that maps named capturing groups to their index values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Text.RegularExpressions.Regex.CapNames" /> property in a set operation is <see langword="null" />.</exception>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000FBF9B File Offset: 0x000FA19B
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x000FBFA3 File Offset: 0x000FA1A3
		[CLSCompliant(false)]
		protected IDictionary CapNames
		{
			get
			{
				return this.capnames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.capnames = value as Hashtable;
				if (this.capnames == null)
				{
					this.capnames = new Hashtable(value);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</summary>
		// Token: 0x06003D84 RID: 15748 RVA: 0x000FBFD3 File Offset: 0x000FA1D3
		[global::__DynamicallyInvokable]
		protected Regex()
		{
			this.internalMatchTimeout = Regex.DefaultMatchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		// Token: 0x06003D85 RID: 15749 RVA: 0x000FBFE6 File Offset: 0x000FA1E6
		[global::__DynamicallyInvokable]
		public Regex(string pattern)
			: this(pattern, RegexOptions.None, Regex.DefaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> contains an invalid flag.</exception>
		// Token: 0x06003D86 RID: 15750 RVA: 0x000FBFF6 File Offset: 0x000FA1F6
		[global::__DynamicallyInvokable]
		public Regex(string pattern, RegexOptions options)
			: this(pattern, options, Regex.DefaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern and a value that specifies how long a pattern matching method should attempt a match before it times out.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06003D87 RID: 15751 RVA: 0x000FC006 File Offset: 0x000FA206
		[global::__DynamicallyInvokable]
		public Regex(string pattern, RegexOptions options, TimeSpan matchTimeout)
			: this(pattern, options, matchTimeout, false)
		{
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000FC014 File Offset: 0x000FA214
		private Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, bool useCache)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (options < RegexOptions.None || options >> 10 != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			Regex.ValidateMatchTimeout(matchTimeout);
			string text;
			if ((options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				text = CultureInfo.InvariantCulture.ToString();
			}
			else
			{
				text = CultureInfo.CurrentCulture.ToString();
			}
			string[] array = new string[5];
			int num = 0;
			int num2 = (int)options;
			array[num] = num2.ToString(NumberFormatInfo.InvariantInfo);
			array[1] = ":";
			array[2] = text;
			array[3] = ":";
			array[4] = pattern;
			string text2 = string.Concat(array);
			CachedCodeEntry cachedCodeEntry = Regex.LookupCachedAndUpdate(text2);
			this.pattern = pattern;
			this.roptions = options;
			this.internalMatchTimeout = matchTimeout;
			if (cachedCodeEntry == null)
			{
				RegexTree regexTree = RegexParser.Parse(pattern, this.roptions);
				this.capnames = regexTree._capnames;
				this.capslist = regexTree._capslist;
				this.code = RegexWriter.Write(regexTree);
				this.caps = this.code._caps;
				this.capsize = this.code._capsize;
				this.InitializeReferences();
				if (useCache)
				{
					cachedCodeEntry = this.CacheCode(text2);
				}
			}
			else
			{
				this.caps = cachedCodeEntry._caps;
				this.capnames = cachedCodeEntry._capnames;
				this.capslist = cachedCodeEntry._capslist;
				this.capsize = cachedCodeEntry._capsize;
				this.code = cachedCodeEntry._code;
				this.factory = cachedCodeEntry._factory;
				this.runnerref = cachedCodeEntry._runnerref;
				this.replref = cachedCodeEntry._replref;
				this.refsInitialized = true;
			}
			if (this.UseOptionC() && this.factory == null)
			{
				this.factory = this.Compile(this.code, this.roptions);
				if (useCache && cachedCodeEntry != null)
				{
					cachedCodeEntry.AddCompiled(this.factory);
				}
				this.code = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class by using serialized data.</summary>
		/// <param name="info">The object that contains a serialized pattern and <see cref="T:System.Text.RegularExpressions.RegexOptions" /> information.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">The pattern that <paramref name="info" /> contains is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="info" /> contains an invalid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> flag.</exception>
		// Token: 0x06003D89 RID: 15753 RVA: 0x000FC1FC File Offset: 0x000FA3FC
		protected Regex(SerializationInfo info, StreamingContext context)
			: this(info.GetString("pattern"), (RegexOptions)info.GetInt32("options"))
		{
			try
			{
				long @int = info.GetInt64("matchTimeout");
				TimeSpan timeSpan = new TimeSpan(@int);
				Regex.ValidateMatchTimeout(timeSpan);
				this.internalMatchTimeout = timeSpan;
			}
			catch (SerializationException)
			{
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.RegularExpressions.Regex" /> object.</summary>
		/// <param name="si">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. This parameter is reserved for future use.</param>
		// Token: 0x06003D8A RID: 15754 RVA: 0x000FC25C File Offset: 0x000FA45C
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("pattern", this.ToString());
			si.AddValue("options", this.Options);
			si.AddValue("matchTimeout", this.MatchTimeout.Ticks);
		}

		/// <summary>Checks whether a time-out interval is within an acceptable range.</summary>
		/// <param name="matchTimeout">The time-out interval to check.</param>
		// Token: 0x06003D8B RID: 15755 RVA: 0x000FC2A9 File Offset: 0x000FA4A9
		protected internal static void ValidateMatchTimeout(TimeSpan matchTimeout)
		{
			if (Regex.InfiniteMatchTimeout == matchTimeout)
			{
				return;
			}
			if (TimeSpan.Zero < matchTimeout && matchTimeout <= Regex.MaximumMatchTimeout)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("matchTimeout");
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000FC2E0 File Offset: 0x000FA4E0
		private static TimeSpan InitDefaultMatchTimeout()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			object data = currentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			if (data == null)
			{
				return Regex.FallbackDefaultMatchTimeout;
			}
			if (!(data is TimeSpan))
			{
				throw new InvalidCastException(SR.GetString("IllegalDefaultRegexMatchTimeoutInAppDomain", new object[] { "REGEX_DEFAULT_MATCH_TIMEOUT" }));
			}
			TimeSpan timeSpan = (TimeSpan)data;
			try
			{
				Regex.ValidateMatchTimeout(timeSpan);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("IllegalDefaultRegexMatchTimeoutInAppDomain", new object[] { "REGEX_DEFAULT_MATCH_TIMEOUT" }));
			}
			return timeSpan;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000FC370 File Offset: 0x000FA570
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal RegexRunnerFactory Compile(RegexCode code, RegexOptions roptions)
		{
			return RegexCompiler.Compile(code, roptions);
		}

		/// <summary>Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $, ., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.</summary>
		/// <param name="str">The input string that contains the text to convert.</param>
		/// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06003D8E RID: 15758 RVA: 0x000FC379 File Offset: 0x000FA579
		[global::__DynamicallyInvokable]
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Escape(str);
		}

		/// <summary>Converts any escaped characters in the input string.</summary>
		/// <param name="str">The input string containing the text to convert.</param>
		/// <returns>A string of characters with any escaped characters converted to their unescaped form.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="str" /> includes an unrecognized escape sequence.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06003D8F RID: 15759 RVA: 0x000FC38F File Offset: 0x000FA58F
		[global::__DynamicallyInvokable]
		public static string Unescape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Unescape(str);
		}

		/// <summary>Gets or sets the maximum number of entries in the current static cache of compiled regular expressions.</summary>
		/// <returns>The maximum number of entries in the static cache.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.</exception>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000FC3A5 File Offset: 0x000FA5A5
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x000FC3AC File Offset: 0x000FA5AC
		[global::__DynamicallyInvokable]
		public static int CacheSize
		{
			[global::__DynamicallyInvokable]
			get
			{
				return Regex.cacheSize;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Regex.cacheSize = value;
				if (Regex.livecode.Count > Regex.cacheSize)
				{
					LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
					lock (linkedList)
					{
						while (Regex.livecode.Count > Regex.cacheSize)
						{
							Regex.livecode.RemoveLast();
						}
					}
				}
			}
		}

		/// <summary>Gets the options that were passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>One or more members of the <see cref="T:System.Text.RegularExpressions.RegexOptions" /> enumeration that represent options that were passed to the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000FC428 File Offset: 0x000FA628
		[global::__DynamicallyInvokable]
		public RegexOptions Options
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.roptions;
			}
		}

		/// <summary>Gets the time-out interval of the current instance.</summary>
		/// <returns>The maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000FC430 File Offset: 0x000FA630
		[global::__DynamicallyInvokable]
		public TimeSpan MatchTimeout
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.internalMatchTimeout;
			}
		}

		/// <summary>Gets a value that indicates whether the regular expression searches from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the regular expression searches from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x000FC438 File Offset: 0x000FA638
		[global::__DynamicallyInvokable]
		public bool RightToLeft
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.UseOptionR();
			}
		}

		/// <summary>Returns the regular expression pattern that was passed into the <see langword="Regex" /> constructor.</summary>
		/// <returns>The <paramref name="pattern" /> parameter that was passed into the <see langword="Regex" /> constructor.</returns>
		// Token: 0x06003D95 RID: 15765 RVA: 0x000FC440 File Offset: 0x000FA640
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			return this.pattern;
		}

		/// <summary>Returns an array of capturing group names for the regular expression.</summary>
		/// <returns>A string array of group names.</returns>
		// Token: 0x06003D96 RID: 15766 RVA: 0x000FC448 File Offset: 0x000FA648
		[global::__DynamicallyInvokable]
		public string[] GetGroupNames()
		{
			string[] array;
			if (this.capslist == null)
			{
				int num = this.capsize;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = Convert.ToString(i, CultureInfo.InvariantCulture);
				}
			}
			else
			{
				array = new string[this.capslist.Length];
				Array.Copy(this.capslist, 0, array, 0, this.capslist.Length);
			}
			return array;
		}

		/// <summary>Returns an array of capturing group numbers that correspond to group names in an array.</summary>
		/// <returns>An integer array of group numbers.</returns>
		// Token: 0x06003D97 RID: 15767 RVA: 0x000FC4AC File Offset: 0x000FA6AC
		[global::__DynamicallyInvokable]
		public int[] GetGroupNumbers()
		{
			int[] array;
			if (this.caps == null)
			{
				int num = this.capsize;
				array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				array = new int[this.caps.Count];
				IDictionaryEnumerator enumerator = this.caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					array[(int)enumerator.Value] = (int)enumerator.Key;
				}
			}
			return array;
		}

		/// <summary>Gets the group name that corresponds to the specified group number.</summary>
		/// <param name="i">The group number to convert to the corresponding group name.</param>
		/// <returns>A string that contains the group name associated with the specified group number. If there is no group name that corresponds to <paramref name="i" />, the method returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06003D98 RID: 15768 RVA: 0x000FC524 File Offset: 0x000FA724
		[global::__DynamicallyInvokable]
		public string GroupNameFromNumber(int i)
		{
			if (this.capslist == null)
			{
				if (i >= 0 && i < this.capsize)
				{
					return i.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if (this.caps != null)
				{
					object obj = this.caps[i];
					if (obj == null)
					{
						return string.Empty;
					}
					i = (int)obj;
				}
				if (i >= 0 && i < this.capslist.Length)
				{
					return this.capslist[i];
				}
				return string.Empty;
			}
		}

		/// <summary>Returns the group number that corresponds to the specified group name.</summary>
		/// <param name="name">The group name to convert to the corresponding group number.</param>
		/// <returns>The group number that corresponds to the specified group name, or -1 if <paramref name="name" /> is not a valid group name.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06003D99 RID: 15769 RVA: 0x000FC5A4 File Offset: 0x000FA7A4
		[global::__DynamicallyInvokable]
		public int GroupNumberFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.capnames != null)
			{
				object obj = this.capnames[name];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			else
			{
				int num = 0;
				foreach (char c in name)
				{
					if (c > '9' || c < '0')
					{
						return -1;
					}
					num *= 10;
					num += (int)(c - '0');
				}
				if (num >= 0 && num < this.capsize)
				{
					return num;
				}
				return -1;
			}
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003D9A RID: 15770 RVA: 0x000FC625 File Offset: 0x000FA825
		[global::__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003D9B RID: 15771 RVA: 0x000FC634 File Offset: 0x000FA834
		[global::__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			return Regex.IsMatch(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06003D9C RID: 15772 RVA: 0x000FC643 File Offset: 0x000FA843
		[global::__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).IsMatch(input);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in a specified input string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000FC654 File Offset: 0x000FA854
		[global::__DynamicallyInvokable]
		public bool IsMatch(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.IsMatch(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the specified input string, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The character position at which to start the search.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000FC67C File Offset: 0x000FA87C
		[global::__DynamicallyInvokable]
		public bool IsMatch(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(true, -1, input, 0, input.Length, startat) == null;
		}

		/// <summary>Searches the specified input string for the first occurrence of the specified regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003D9F RID: 15775 RVA: 0x000FC6A0 File Offset: 0x000FA8A0
		[global::__DynamicallyInvokable]
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DA0 RID: 15776 RVA: 0x000FC6AF File Offset: 0x000FA8AF
		[global::__DynamicallyInvokable]
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			return Regex.Match(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DA1 RID: 15777 RVA: 0x000FC6BE File Offset: 0x000FA8BE
		[global::__DynamicallyInvokable]
		public static Match Match(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Match(input);
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DA2 RID: 15778 RVA: 0x000FC6CF File Offset: 0x000FA8CF
		[global::__DynamicallyInvokable]
		public Match Match(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Match(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The zero-based character position at which to start the search.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DA3 RID: 15779 RVA: 0x000FC6F7 File Offset: 0x000FA8F7
		[global::__DynamicallyInvokable]
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, 0, input.Length, startat);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position and searching only the specified number of characters.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="beginning">The zero-based character position in the input string that defines the leftmost position to be searched.</param>
		/// <param name="length">The number of characters in the substring to include in the search.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="beginning" /> is less than zero or greater than the length of <paramref name="input" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero or greater than the length of <paramref name="input" />.  
		/// -or-  
		/// <paramref name="beginning" /><see langword="+" /><paramref name="length" /><see langword="-1" /> identifies a position that is outside the range of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000FC718 File Offset: 0x000FA918
		[global::__DynamicallyInvokable]
		public Match Match(string input, int beginning, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, beginning, length, this.UseOptionR() ? (beginning + length) : beginning);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000FC741 File Offset: 0x000FA941
		[global::__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern)
		{
			return Regex.Matches(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		// Token: 0x06003DA6 RID: 15782 RVA: 0x000FC750 File Offset: 0x000FA950
		[global::__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern, RegexOptions options)
		{
			return Regex.Matches(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000FC75F File Offset: 0x000FA95F
		[global::__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Matches(input);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		// Token: 0x06003DA8 RID: 15784 RVA: 0x000FC770 File Offset: 0x000FA970
		[global::__DynamicallyInvokable]
		public MatchCollection Matches(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Matches(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The character position in the input string at which to start the search.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		// Token: 0x06003DA9 RID: 15785 RVA: 0x000FC798 File Offset: 0x000FA998
		[global::__DynamicallyInvokable]
		public MatchCollection Matches(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return new MatchCollection(this, input, 0, input.Length, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAA RID: 15786 RVA: 0x000FC7B7 File Offset: 0x000FA9B7
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAB RID: 15787 RVA: 0x000FC7C7 File Offset: 0x000FA9C7
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			return Regex.Replace(input, pattern, replacement, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAC RID: 15788 RVA: 0x000FC7D7 File Offset: 0x000FA9D7
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, replacement);
		}

		/// <summary>In a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAD RID: 15789 RVA: 0x000FC7EA File Offset: 0x000FA9EA
		[global::__DynamicallyInvokable]
		public string Replace(string input, string replacement)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="count">The maximum number of times the replacement can occur.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAE RID: 15790 RVA: 0x000FC814 File Offset: 0x000FAA14
		[global::__DynamicallyInvokable]
		public string Replace(string input, string replacement, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="count">Maximum number of times the replacement can occur.</param>
		/// <param name="startat">The character position in the input string where the search begins.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DAF RID: 15791 RVA: 0x000FC840 File Offset: 0x000FAA40
		[global::__DynamicallyInvokable]
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			RegexReplacement regexReplacement = (RegexReplacement)this.replref.Get();
			if (regexReplacement == null || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, this.caps, this.capsize, this.capnames, this.roptions);
				this.replref.Cache(regexReplacement);
			}
			return regexReplacement.Replace(this, input, count, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB0 RID: 15792 RVA: 0x000FC8C1 File Offset: 0x000FAAC1
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000FC8D1 File Offset: 0x000FAAD1
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
		{
			return Regex.Replace(input, pattern, evaluator, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all substrings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB2 RID: 15794 RVA: 0x000FC8E1 File Offset: 0x000FAAE1
		[global::__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, evaluator);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB3 RID: 15795 RVA: 0x000FC8F4 File Offset: 0x000FAAF4
		[global::__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000FC91E File Offset: 0x000FAB1E
		[global::__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur.</param>
		/// <param name="startat">The character position in the input string where the search begins.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000FC948 File Offset: 0x000FAB48
		[global::__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Replace(evaluator, this, input, count, startat);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000FC963 File Offset: 0x000FAB63
		[global::__DynamicallyInvokable]
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000FC972 File Offset: 0x000FAB72
		[global::__DynamicallyInvokable]
		public static string[] Split(string input, string pattern, RegexOptions options)
		{
			return Regex.Split(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A string array.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000FC981 File Offset: 0x000FAB81
		[global::__DynamicallyInvokable]
		public static string[] Split(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Split(input);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to split.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000FC992 File Offset: 0x000FAB92
		[global::__DynamicallyInvokable]
		public string[] Split(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Split(input, 0, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to be split.</param>
		/// <param name="count">The maximum number of times the split can occur.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000FC9BB File Offset: 0x000FABBB
		[global::__DynamicallyInvokable]
		public string[] Split(string input, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor. The search for the regular expression pattern starts at a specified character position in the input string.</summary>
		/// <param name="input">The string to be split.</param>
		/// <param name="count">The maximum number of times the split can occur.</param>
		/// <param name="startat">The character position in the input string where the search will begin.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000FC9E4 File Offset: 0x000FABE4
		[global::__DynamicallyInvokable]
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, startat);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000FC9FD File Offset: 0x000FABFD
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, null, null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000FCA08 File Offset: 0x000FAC08
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects and a specified resource file to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly.</param>
		/// <param name="resourceFile">The name of the Win32 resource file to include in the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The <paramref name="resourceFile" /> parameter designates an invalid Win32 resource file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file designated by the <paramref name="resourceFile" /> parameter cannot be found.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000FCA13 File Offset: 0x000FAC13
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, resourceFile);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000FCA1E File Offset: 0x000FAC1E
		private static void CompileToAssemblyInternal(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			if (assemblyname == null)
			{
				throw new ArgumentNullException("assemblyname");
			}
			if (regexinfos == null)
			{
				throw new ArgumentNullException("regexinfos");
			}
			RegexCompiler.CompileToAssembly(regexinfos, assemblyname, attributes, resourceFile);
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <exception cref="T:System.NotSupportedException">References have already been initialized.</exception>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000FCA45 File Offset: 0x000FAC45
		protected void InitializeReferences()
		{
			if (this.refsInitialized)
			{
				throw new NotSupportedException(SR.GetString("OnlyAllowedOnce"));
			}
			this.refsInitialized = true;
			this.runnerref = new ExclusiveReference();
			this.replref = new SharedReference();
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000FCA7C File Offset: 0x000FAC7C
		internal Match Run(bool quick, int prevlen, string input, int beginning, int length, int startat)
		{
			RegexRunner regexRunner = null;
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("BeginIndexNotNegative"));
			}
			if (length < 0 || length > input.Length)
			{
				throw new ArgumentOutOfRangeException("length", SR.GetString("LengthNotNegative"));
			}
			regexRunner = (RegexRunner)this.runnerref.Get();
			if (regexRunner == null)
			{
				if (this.factory != null)
				{
					regexRunner = this.factory.CreateInstance();
				}
				else
				{
					regexRunner = new RegexInterpreter(this.code, this.UseOptionInvariant() ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
				}
			}
			Match match;
			try
			{
				match = regexRunner.Scan(this, input, beginning, beginning + length, startat, prevlen, quick, this.internalMatchTimeout);
			}
			finally
			{
				this.runnerref.Release(regexRunner);
			}
			return match;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000FCB58 File Offset: 0x000FAD58
		private static CachedCodeEntry LookupCachedAndUpdate(string key)
		{
			LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
			lock (linkedList)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000FCBE0 File Offset: 0x000FADE0
		private CachedCodeEntry CacheCode(string key)
		{
			CachedCodeEntry cachedCodeEntry = null;
			LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
			lock (linkedList)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
				if (Regex.cacheSize != 0)
				{
					cachedCodeEntry = new CachedCodeEntry(key, this.capnames, this.capslist, this.code, this.caps, this.capsize, this.runnerref, this.replref);
					Regex.livecode.AddFirst(cachedCodeEntry);
					if (Regex.livecode.Count > Regex.cacheSize)
					{
						Regex.livecode.RemoveLast();
					}
				}
			}
			return cachedCodeEntry;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> option; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000FCCC8 File Offset: 0x000FAEC8
		protected bool UseOptionC()
		{
			return (this.roptions & RegexOptions.Compiled) > RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003DC5 RID: 15813 RVA: 0x000FCCD5 File Offset: 0x000FAED5
		protected bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000FCCE3 File Offset: 0x000FAEE3
		internal bool UseOptionInvariant()
		{
			return (this.roptions & RegexOptions.CultureInvariant) > RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CB8 RID: 11448
		protected internal string pattern;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CB9 RID: 11449
		protected internal RegexRunnerFactory factory;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CBA RID: 11450
		protected internal RegexOptions roptions;

		// Token: 0x04002CBB RID: 11451
		[NonSerialized]
		private static readonly TimeSpan MaximumMatchTimeout = TimeSpan.FromMilliseconds(2147483646.0);

		/// <summary>Specifies that a pattern-matching operation should not time out.</summary>
		// Token: 0x04002CBC RID: 11452
		[global::__DynamicallyInvokable]
		[NonSerialized]
		public static readonly TimeSpan InfiniteMatchTimeout = Timeout.InfiniteTimeSpan;

		/// <summary>The maximum amount of time that can elapse in a pattern-matching operation before the operation times out.</summary>
		// Token: 0x04002CBD RID: 11453
		[OptionalField(VersionAdded = 2)]
		protected internal TimeSpan internalMatchTimeout;

		// Token: 0x04002CBE RID: 11454
		private const string DefaultMatchTimeout_ConfigKeyName = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x04002CBF RID: 11455
		[NonSerialized]
		internal static readonly TimeSpan FallbackDefaultMatchTimeout = Regex.InfiniteMatchTimeout;

		// Token: 0x04002CC0 RID: 11456
		[NonSerialized]
		internal static readonly TimeSpan DefaultMatchTimeout = Regex.InitDefaultMatchTimeout();

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CC1 RID: 11457
		protected internal Hashtable caps;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CC2 RID: 11458
		protected internal Hashtable capnames;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CC3 RID: 11459
		protected internal string[] capslist;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04002CC4 RID: 11460
		protected internal int capsize;

		// Token: 0x04002CC5 RID: 11461
		internal ExclusiveReference runnerref;

		// Token: 0x04002CC6 RID: 11462
		internal SharedReference replref;

		// Token: 0x04002CC7 RID: 11463
		internal RegexCode code;

		// Token: 0x04002CC8 RID: 11464
		internal bool refsInitialized;

		// Token: 0x04002CC9 RID: 11465
		internal static LinkedList<CachedCodeEntry> livecode = new LinkedList<CachedCodeEntry>();

		// Token: 0x04002CCA RID: 11466
		internal static int cacheSize = 15;

		// Token: 0x04002CCB RID: 11467
		internal const int MaxOptionShift = 10;
	}
}

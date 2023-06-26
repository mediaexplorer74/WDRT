using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Represents the version number of an assembly, operating system, or the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x0200015B RID: 347
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class with the specified major, minor, build, and revision numbers.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <param name="build">The build number.</param>
		/// <param name="revision">The revision number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, <paramref name="build" />, or <paramref name="revision" /> is less than zero.</exception>
		// Token: 0x06001594 RID: 5524 RVA: 0x0003F964 File Offset: 0x0003DB64
		[__DynamicallyInvokable]
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major, minor, and build values.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <param name="build">The build number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, or <paramref name="build" /> is less than zero.</exception>
		// Token: 0x06001595 RID: 5525 RVA: 0x0003FA08 File Offset: 0x0003DC08
		[__DynamicallyInvokable]
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major and minor values.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" /> or <paramref name="minor" /> is less than zero.</exception>
		// Token: 0x06001596 RID: 5526 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		[__DynamicallyInvokable]
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified string.</summary>
		/// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.').</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> has fewer than two components or more than four components.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A major, minor, build, or revision component is less than zero.</exception>
		/// <exception cref="T:System.FormatException">At least one component of <paramref name="version" /> does not parse to an integer.</exception>
		/// <exception cref="T:System.OverflowException">At least one component of <paramref name="version" /> represents a number greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001597 RID: 5527 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
		[__DynamicallyInvokable]
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class.</summary>
		// Token: 0x06001598 RID: 5528 RVA: 0x0003FB48 File Offset: 0x0003DD48
		public Version()
		{
			this._Major = 0;
			this._Minor = 0;
		}

		/// <summary>Gets the value of the major component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The major version number.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0003FB6C File Offset: 0x0003DD6C
		[__DynamicallyInvokable]
		public int Major
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Major;
			}
		}

		/// <summary>Gets the value of the minor component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The minor version number.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0003FB74 File Offset: 0x0003DD74
		[__DynamicallyInvokable]
		public int Minor
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Minor;
			}
		}

		/// <summary>Gets the value of the build component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The build number, or -1 if the build number is undefined.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0003FB7C File Offset: 0x0003DD7C
		[__DynamicallyInvokable]
		public int Build
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Build;
			}
		}

		/// <summary>Gets the value of the revision component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The revision number, or -1 if the revision number is undefined.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0003FB84 File Offset: 0x0003DD84
		[__DynamicallyInvokable]
		public int Revision
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Revision;
			}
		}

		/// <summary>Gets the high 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0003FB8C File Offset: 0x0003DD8C
		[__DynamicallyInvokable]
		public short MajorRevision
		{
			[__DynamicallyInvokable]
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		/// <summary>Gets the low 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0003FB98 File Offset: 0x0003DD98
		[__DynamicallyInvokable]
		public short MinorRevision
		{
			[__DynamicallyInvokable]
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		/// <summary>Returns a new <see cref="T:System.Version" /> object whose value is the same as the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A new <see cref="T:System.Object" /> whose values are a copy of the current <see cref="T:System.Version" /> object.</returns>
		// Token: 0x0600159F RID: 5535 RVA: 0x0003FBA8 File Offset: 0x0003DDA8
		public object Clone()
		{
			return new Version
			{
				_Major = this._Major,
				_Minor = this._Minor,
				_Build = this._Build,
				_Revision = this._Revision
			};
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified object and returns an indication of their relative values.</summary>
		/// <param name="version">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version before <paramref name="version" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.Version" /> object is the same version as <paramref name="version" />.  
		///
		///   Greater than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="version" />.  
		///
		///  -or-  
		///
		///  <paramref name="version" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> is not of type <see cref="T:System.Version" />.</exception>
		// Token: 0x060015A0 RID: 5536 RVA: 0x0003FBEC File Offset: 0x0003DDEC
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeVersion"));
			}
			if (this._Major != version2._Major)
			{
				if (this._Major > version2._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != version2._Minor)
			{
				if (this._Minor > version2._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != version2._Build)
			{
				if (this._Build > version2._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == version2._Revision)
				{
					return 0;
				}
				if (this._Revision > version2._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified <see cref="T:System.Version" /> object and returns an indication of their relative values.</summary>
		/// <param name="value">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version before <paramref name="value" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.Version" /> object is the same version as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		// Token: 0x060015A1 RID: 5537 RVA: 0x0003FCA0 File Offset: 0x0003DEA0
		[__DynamicallyInvokable]
		public int CompareTo(Version value)
		{
			if (value == null)
			{
				return 1;
			}
			if (this._Major != value._Major)
			{
				if (this._Major > value._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != value._Minor)
			{
				if (this._Minor > value._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != value._Build)
			{
				if (this._Build > value._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == value._Revision)
				{
					return 0;
				}
				if (this._Revision > value._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Version" /> object and <paramref name="obj" /> are both <see cref="T:System.Version" /> objects, and every component of the current <see cref="T:System.Version" /> object matches the corresponding component of <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015A2 RID: 5538 RVA: 0x0003FD3C File Offset: 0x0003DF3C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			Version version = obj as Version;
			return !(version == null) && this._Major == version._Major && this._Minor == version._Minor && this._Build == version._Build && this._Revision == version._Revision;
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object and a specified <see cref="T:System.Version" /> object represent the same value.</summary>
		/// <param name="obj">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Version" /> object matches the corresponding component of the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015A3 RID: 5539 RVA: 0x0003FD98 File Offset: 0x0003DF98
		[__DynamicallyInvokable]
		public bool Equals(Version obj)
		{
			return !(obj == null) && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision;
		}

		/// <summary>Returns a hash code for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060015A4 RID: 5540 RVA: 0x0003FDEC File Offset: 0x0003DFEC
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this._Major & 15) << 28;
			num |= (this._Minor & 255) << 20;
			num |= (this._Build & 255) << 12;
			return num | (this._Revision & 4095);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, as depicted in the following format. Each component is separated by a period character ('.'). Square brackets ('[' and ']') indicate a component that will not appear in the return value if the component is not defined:  
		///  major.minor[.build[.revision]]  
		///  For example, if you create a <see cref="T:System.Version" /> object using the constructor Version(1,1), the returned string is "1.1". If you create a <see cref="T:System.Version" /> object using the constructor Version(1,3,4,2), the returned string is "1.3.4.2".</returns>
		// Token: 0x060015A5 RID: 5541 RVA: 0x0003FE3E File Offset: 0x0003E03E
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this._Build == -1)
			{
				return this.ToString(2);
			}
			if (this._Revision == -1)
			{
				return this.ToString(3);
			}
			return this.ToString(4);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation. A specified count indicates the number of components to return.</summary>
		/// <param name="fieldCount">The number of components to return. The <paramref name="fieldCount" /> ranges from 0 to 4.</param>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, each separated by a period character ('.'). The <paramref name="fieldCount" /> parameter determines how many components are returned.  
		///   fieldCount  
		///
		///   Return Value  
		///
		///   0  
		///
		///   An empty string ("").  
		///
		///   1  
		///
		///   major  
		///
		///   2  
		///
		///   major.minor  
		///
		///   3  
		///
		///   major.minor.build  
		///
		///   4  
		///
		///   major.minor.build.revision  
		///
		///
		///
		///  For example, if you create <see cref="T:System.Version" /> object using the constructor Version(1,3,5), ToString(2) returns "1.3" and ToString(4) throws an exception.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fieldCount" /> is less than 0, or more than 4.  
		/// -or-  
		/// <paramref name="fieldCount" /> is more than the number of components defined in the current <see cref="T:System.Version" /> object.</exception>
		// Token: 0x060015A6 RID: 5542 RVA: 0x0003FE6C File Offset: 0x0003E06C
		[__DynamicallyInvokable]
		public string ToString(int fieldCount)
		{
			switch (fieldCount)
			{
			case 0:
				return string.Empty;
			case 1:
				return this._Major.ToString();
			case 2:
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				Version.AppendPositiveNumber(this._Major, stringBuilder);
				stringBuilder.Append('.');
				Version.AppendPositiveNumber(this._Minor, stringBuilder);
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
			default:
				if (this._Build == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { "0", "2" }), "fieldCount");
				}
				if (fieldCount == 3)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				if (this._Revision == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { "0", "3" }), "fieldCount");
				}
				if (fieldCount == 4)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Revision, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { "0", "4" }), "fieldCount");
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00040008 File Offset: 0x0003E208
		private static void AppendPositiveNumber(int num, StringBuilder sb)
		{
			int length = sb.Length;
			do
			{
				int num2 = num % 10;
				num /= 10;
				sb.Insert(length, (char)(48 + num2));
			}
			while (num > 0);
		}

		/// <summary>Converts the string representation of a version number to an equivalent <see cref="T:System.Version" /> object.</summary>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <returns>An object that is equivalent to the version number specified in the <paramref name="input" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="input" /> has fewer than two or more than four version components.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">At least one component in <paramref name="input" /> is less than zero.</exception>
		/// <exception cref="T:System.FormatException">At least one component in <paramref name="input" /> is not an integer.</exception>
		/// <exception cref="T:System.OverflowException">At least one component in <paramref name="input" /> represents a number that is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060015A8 RID: 5544 RVA: 0x00040038 File Offset: 0x0003E238
		[__DynamicallyInvokable]
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", true);
			if (!Version.TryParseVersion(input, ref versionResult))
			{
				throw versionResult.GetVersionParseException();
			}
			return versionResult.m_parsedVersion;
		}

		/// <summary>Tries to convert the string representation of a version number to an equivalent <see cref="T:System.Version" /> object, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Version" /> equivalent of the number that is contained in <paramref name="input" />, if the conversion succeeded. If <paramref name="input" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or if the conversion fails, <paramref name="result" /> is <see langword="null" /> when the method returns.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015A9 RID: 5545 RVA: 0x00040080 File Offset: 0x0003E280
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out Version result)
		{
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", false);
			bool flag = Version.TryParseVersion(input, ref versionResult);
			result = versionResult.m_parsedVersion;
			return flag;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000400B4 File Offset: 0x0003E2B4
		private static bool TryParseVersion(string version, ref Version.VersionResult result)
		{
			if (version == null)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentNullException);
				return false;
			}
			string[] array = version.Split(Version.SeparatorsArray);
			int num = array.Length;
			if (num < 2 || num > 4)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentException);
				return false;
			}
			int num2;
			if (!Version.TryParseComponent(array[0], "version", ref result, out num2))
			{
				return false;
			}
			int num3;
			if (!Version.TryParseComponent(array[1], "version", ref result, out num3))
			{
				return false;
			}
			num -= 2;
			if (num > 0)
			{
				int num4;
				if (!Version.TryParseComponent(array[2], "build", ref result, out num4))
				{
					return false;
				}
				num--;
				if (num > 0)
				{
					int num5;
					if (!Version.TryParseComponent(array[3], "revision", ref result, out num5))
					{
						return false;
					}
					result.m_parsedVersion = new Version(num2, num3, num4, num5);
				}
				else
				{
					result.m_parsedVersion = new Version(num2, num3, num4);
				}
			}
			else
			{
				result.m_parsedVersion = new Version(num2, num3);
			}
			return true;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0004018C File Offset: 0x0003E38C
		private static bool TryParseComponent(string component, string componentName, ref Version.VersionResult result, out int parsedComponent)
		{
			if (!int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent))
			{
				result.SetFailure(Version.ParseFailureKind.FormatException, component);
				return false;
			}
			if (parsedComponent < 0)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentOutOfRangeException, componentName);
				return false;
			}
			return true;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are equal.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> equals <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015AC RID: 5548 RVA: 0x000401B7 File Offset: 0x0003E3B7
		[__DynamicallyInvokable]
		public static bool operator ==(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 == null;
			}
			return v1.Equals(v2);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are not equal.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> does not equal <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015AD RID: 5549 RVA: 0x000401C8 File Offset: 0x0003E3C8
		[__DynamicallyInvokable]
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is less than <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is <see langword="null" />.</exception>
		// Token: 0x060015AE RID: 5550 RVA: 0x000401D4 File Offset: 0x0003E3D4
		[__DynamicallyInvokable]
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) < 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than or equal to the second <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is less than or equal to <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is <see langword="null" />.</exception>
		// Token: 0x060015AF RID: 5551 RVA: 0x000401EE File Offset: 0x0003E3EE
		[__DynamicallyInvokable]
		public static bool operator <=(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) <= 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is greater than <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015B0 RID: 5552 RVA: 0x0004020B File Offset: 0x0003E40B
		[__DynamicallyInvokable]
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than or equal to the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is greater than or equal to <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015B1 RID: 5553 RVA: 0x00040214 File Offset: 0x0003E414
		[__DynamicallyInvokable]
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x04000742 RID: 1858
		private int _Major;

		// Token: 0x04000743 RID: 1859
		private int _Minor;

		// Token: 0x04000744 RID: 1860
		private int _Build = -1;

		// Token: 0x04000745 RID: 1861
		private int _Revision = -1;

		// Token: 0x04000746 RID: 1862
		private static readonly char[] SeparatorsArray = new char[] { '.' };

		// Token: 0x04000747 RID: 1863
		private const int ZERO_CHAR_VALUE = 48;

		// Token: 0x02000B02 RID: 2818
		internal enum ParseFailureKind
		{
			// Token: 0x04003237 RID: 12855
			ArgumentNullException,
			// Token: 0x04003238 RID: 12856
			ArgumentException,
			// Token: 0x04003239 RID: 12857
			ArgumentOutOfRangeException,
			// Token: 0x0400323A RID: 12858
			FormatException
		}

		// Token: 0x02000B03 RID: 2819
		internal struct VersionResult
		{
			// Token: 0x06006A9B RID: 27291 RVA: 0x00170EDD File Offset: 0x0016F0DD
			internal void Init(string argumentName, bool canThrow)
			{
				this.m_canThrow = canThrow;
				this.m_argumentName = argumentName;
			}

			// Token: 0x06006A9C RID: 27292 RVA: 0x00170EED File Offset: 0x0016F0ED
			internal void SetFailure(Version.ParseFailureKind failure)
			{
				this.SetFailure(failure, string.Empty);
			}

			// Token: 0x06006A9D RID: 27293 RVA: 0x00170EFB File Offset: 0x0016F0FB
			internal void SetFailure(Version.ParseFailureKind failure, string argument)
			{
				this.m_failure = failure;
				this.m_exceptionArgument = argument;
				if (this.m_canThrow)
				{
					throw this.GetVersionParseException();
				}
			}

			// Token: 0x06006A9E RID: 27294 RVA: 0x00170F1C File Offset: 0x0016F11C
			internal Exception GetVersionParseException()
			{
				switch (this.m_failure)
				{
				case Version.ParseFailureKind.ArgumentNullException:
					return new ArgumentNullException(this.m_argumentName);
				case Version.ParseFailureKind.ArgumentException:
					return new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
				case Version.ParseFailureKind.ArgumentOutOfRangeException:
					return new ArgumentOutOfRangeException(this.m_exceptionArgument, Environment.GetResourceString("ArgumentOutOfRange_Version"));
				case Version.ParseFailureKind.FormatException:
					try
					{
						int.Parse(this.m_exceptionArgument, CultureInfo.InvariantCulture);
					}
					catch (FormatException ex)
					{
						return ex;
					}
					catch (OverflowException ex2)
					{
						return ex2;
					}
					return new FormatException(Environment.GetResourceString("Format_InvalidString"));
				default:
					return new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
				}
			}

			// Token: 0x0400323B RID: 12859
			internal Version m_parsedVersion;

			// Token: 0x0400323C RID: 12860
			internal Version.ParseFailureKind m_failure;

			// Token: 0x0400323D RID: 12861
			internal string m_exceptionArgument;

			// Token: 0x0400323E RID: 12862
			internal string m_argumentName;

			// Token: 0x0400323F RID: 12863
			internal bool m_canThrow;
		}
	}
}

using System;
using System.Text;

namespace System.Runtime.Versioning
{
	/// <summary>Represents the name of a version of the .NET Framework.</summary>
	// Token: 0x020003D9 RID: 985
	[global::__DynamicallyInvokable]
	[Serializable]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		/// <summary>Gets the identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x000AFC25 File Offset: 0x000ADE25
		[global::__DynamicallyInvokable]
		public string Identifier
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_identifier;
			}
		}

		/// <summary>Gets the version of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>An object that contains version information about this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000AFC2D File Offset: 0x000ADE2D
		[global::__DynamicallyInvokable]
		public Version Version
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_version;
			}
		}

		/// <summary>Gets the profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000AFC35 File Offset: 0x000ADE35
		[global::__DynamicallyInvokable]
		public string Profile
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_profile;
			}
		}

		/// <summary>Gets the full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000AFC40 File Offset: 0x000ADE40
		[global::__DynamicallyInvokable]
		public string FullName
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_fullName == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.Identifier);
					stringBuilder.Append(',');
					stringBuilder.Append("Version").Append('=');
					stringBuilder.Append('v');
					stringBuilder.Append(this.Version);
					if (!string.IsNullOrEmpty(this.Profile))
					{
						stringBuilder.Append(',');
						stringBuilder.Append("Profile").Append('=');
						stringBuilder.Append(this.Profile);
					}
					this.m_fullName = stringBuilder.ToString();
				}
				return this.m_fullName;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified object.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025DA RID: 9690 RVA: 0x000AFCE5 File Offset: 0x000ADEE5
		[global::__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance.</summary>
		/// <param name="other">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025DB RID: 9691 RVA: 0x000AFCF3 File Offset: 0x000ADEF3
		[global::__DynamicallyInvokable]
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A 32-bit signed integer that represents the hash code of this instance.</returns>
		// Token: 0x060025DC RID: 9692 RVA: 0x000AFD33 File Offset: 0x000ADF33
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x060025DD RID: 9693 RVA: 0x000AFD58 File Offset: 0x000ADF58
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string and a <see cref="T:System.Version" /> object that identify a .NET Framework version.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x060025DE RID: 9694 RVA: 0x000AFD60 File Offset: 0x000ADF60
		[global::__DynamicallyInvokable]
		public FrameworkName(string identifier, Version version)
			: this(identifier, version, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string, a <see cref="T:System.Version" /> object that identifies a .NET Framework version, and a profile name.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <param name="profile">A profile name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x060025DF RID: 9695 RVA: 0x000AFD6C File Offset: 0x000ADF6C
		[global::__DynamicallyInvokable]
		public FrameworkName(string identifier, Version version, string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (identifier.Trim().Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "identifier" }), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_identifier = identifier.Trim();
			this.m_version = (Version)version.Clone();
			this.m_profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string that contains information about a version of the .NET Framework.</summary>
		/// <param name="frameworkName">A string that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="frameworkName" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="frameworkName" /> has fewer than two components or more than three components.  
		/// -or-  
		/// <paramref name="frameworkName" /> does not include a major and minor version number.  
		/// -or-  
		/// <paramref name="frameworkName" /> does not include a valid version number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is <see langword="null" />.</exception>
		// Token: 0x060025E0 RID: 9696 RVA: 0x000AFE04 File Offset: 0x000AE004
		[global::__DynamicallyInvokable]
		public FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "frameworkName" }), "frameworkName");
			}
			string[] array = frameworkName.Split(new char[] { ',' });
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameTooShort"), "frameworkName");
			}
			this.m_identifier = array[0].Trim();
			if (this.m_identifier.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
			}
			bool flag = false;
			this.m_profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length != 2)
				{
					throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					try
					{
						this.m_version = new Version(text2);
						goto IL_196;
					}
					catch (Exception ex)
					{
						throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalidVersion"), "frameworkName", ex);
					}
					goto IL_15F;
				}
				goto IL_15F;
				IL_196:
				i++;
				continue;
				IL_15F:
				if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_profile = text2;
					goto IL_196;
				}
				goto IL_196;
			}
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameMissingVersion"), "frameworkName");
			}
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent the same .NET Framework version.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters represent the same .NET Framework version; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025E1 RID: 9697 RVA: 0x000AFFDC File Offset: 0x000AE1DC
		[global::__DynamicallyInvokable]
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent different .NET Framework versions.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters represent different .NET Framework versions; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025E2 RID: 9698 RVA: 0x000AFFED File Offset: 0x000AE1ED
		[global::__DynamicallyInvokable]
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x0400205B RID: 8283
		private readonly string m_identifier;

		// Token: 0x0400205C RID: 8284
		private readonly Version m_version;

		// Token: 0x0400205D RID: 8285
		private readonly string m_profile;

		// Token: 0x0400205E RID: 8286
		private string m_fullName;

		// Token: 0x0400205F RID: 8287
		private const char c_componentSeparator = ',';

		// Token: 0x04002060 RID: 8288
		private const char c_keyValueSeparator = '=';

		// Token: 0x04002061 RID: 8289
		private const char c_versionValuePrefix = 'v';

		// Token: 0x04002062 RID: 8290
		private const string c_versionKey = "Version";

		// Token: 0x04002063 RID: 8291
		private const string c_profileKey = "Profile";
	}
}

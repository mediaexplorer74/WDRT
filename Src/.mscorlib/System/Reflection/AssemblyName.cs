using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Reflection
{
	/// <summary>Describes an assembly's unique identity in full.</summary>
	// Token: 0x020005C5 RID: 1477
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AssemblyName : _AssemblyName, ICloneable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class.</summary>
		// Token: 0x06004495 RID: 17557 RVA: 0x000FDD32 File Offset: 0x000FBF32
		[__DynamicallyInvokable]
		public AssemblyName()
		{
			this._HashAlgorithm = AssemblyHashAlgorithm.None;
			this._VersionCompatibility = AssemblyVersionCompatibility.SameMachine;
			this._Flags = AssemblyNameFlags.None;
		}

		/// <summary>Gets or sets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.</summary>
		/// <returns>The simple name of the assembly.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x000FDD4F File Offset: 0x000FBF4F
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x000FDD57 File Offset: 0x000FBF57
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Name;
			}
			[__DynamicallyInvokable]
			set
			{
				this._Name = value;
			}
		}

		/// <summary>Gets or sets the major, minor, build, and revision numbers of the assembly.</summary>
		/// <returns>An object that represents the major, minor, build, and revision numbers of the assembly.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000FDD60 File Offset: 0x000FBF60
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x000FDD68 File Offset: 0x000FBF68
		[__DynamicallyInvokable]
		public Version Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Version;
			}
			[__DynamicallyInvokable]
			set
			{
				this._Version = value;
			}
		}

		/// <summary>Gets or sets the culture supported by the assembly.</summary>
		/// <returns>An object that represents the culture supported by the assembly.</returns>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000FDD71 File Offset: 0x000FBF71
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000FDD79 File Offset: 0x000FBF79
		[__DynamicallyInvokable]
		public CultureInfo CultureInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CultureInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				this._CultureInfo = value;
			}
		}

		/// <summary>Gets or sets the name of the culture associated with the assembly.</summary>
		/// <returns>The culture name.</returns>
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x000FDD82 File Offset: 0x000FBF82
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x000FDD99 File Offset: 0x000FBF99
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._CultureInfo != null)
				{
					return this._CultureInfo.Name;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				this._CultureInfo = ((value == null) ? null : new CultureInfo(value));
			}
		}

		/// <summary>Gets or sets the location of the assembly as a URL.</summary>
		/// <returns>A string that is the URL location of the assembly.</returns>
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x000FDDAD File Offset: 0x000FBFAD
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x000FDDB5 File Offset: 0x000FBFB5
		public string CodeBase
		{
			get
			{
				return this._CodeBase;
			}
			set
			{
				this._CodeBase = value;
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x000FDDBE File Offset: 0x000FBFBE
		public string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				if (this._CodeBase == null)
				{
					return null;
				}
				return AssemblyName.EscapeCodeBase(this._CodeBase);
			}
		}

		/// <summary>Gets or sets a value that identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
		/// <returns>One of the enumeration values that identifies the processor and bits-per-word of the platform targeted by an executable.</returns>
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x000FDDD8 File Offset: 0x000FBFD8
		// (set) Token: 0x060044A2 RID: 17570 RVA: 0x000FDDF8 File Offset: 0x000FBFF8
		[__DynamicallyInvokable]
		public ProcessorArchitecture ProcessorArchitecture
		{
			[__DynamicallyInvokable]
			get
			{
				int num = (int)((this._Flags & (AssemblyNameFlags)112) >> 4);
				if (num > 5)
				{
					num = 0;
				}
				return (ProcessorArchitecture)num;
			}
			[__DynamicallyInvokable]
			set
			{
				int num = (int)(value & (ProcessorArchitecture)7);
				if (num <= 5)
				{
					this._Flags = (AssemblyNameFlags)((long)this._Flags & (long)((ulong)(-241)));
					this._Flags |= (AssemblyNameFlags)(num << 4);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates what type of content the assembly contains.</summary>
		/// <returns>A value that indicates what type of content the assembly contains.</returns>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x000FDE34 File Offset: 0x000FC034
		// (set) Token: 0x060044A4 RID: 17572 RVA: 0x000FDE58 File Offset: 0x000FC058
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public AssemblyContentType ContentType
		{
			[__DynamicallyInvokable]
			get
			{
				int num = (int)((this._Flags & (AssemblyNameFlags)3584) >> 9);
				if (num > 1)
				{
					num = 0;
				}
				return (AssemblyContentType)num;
			}
			[__DynamicallyInvokable]
			set
			{
				int num = (int)(value & (AssemblyContentType)7);
				if (num <= 1)
				{
					this._Flags = (AssemblyNameFlags)((long)this._Flags & (long)((ulong)(-3585)));
					this._Flags |= (AssemblyNameFlags)(num << 9);
				}
			}
		}

		/// <summary>Makes a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>An object that is a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</returns>
		// Token: 0x060044A5 RID: 17573 RVA: 0x000FDE94 File Offset: 0x000FC094
		public object Clone()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(this._Name, this._PublicKey, this._PublicKeyToken, this._Version, this._CultureInfo, this._HashAlgorithm, this._VersionCompatibility, this._CodeBase, this._Flags, this._StrongNameKeyPair);
			assemblyName._HashForControl = this._HashForControl;
			assemblyName._HashAlgorithmForControl = this._HashAlgorithmForControl;
			return assemblyName;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> for a given file.</summary>
		/// <param name="assemblyFile">The path for the assembly whose <see cref="T:System.Reflection.AssemblyName" /> is to be returned.</param>
		/// <returns>An object that represents the given assembly file.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is invalid, such as an assembly with an invalid culture.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence.</exception>
		// Token: 0x060044A6 RID: 17574 RVA: 0x000FDF04 File Offset: 0x000FC104
		[SecuritySafeCritical]
		public static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			string fullPathInternal = Path.GetFullPathInternal(assemblyFile);
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPathInternal).Demand();
			return AssemblyName.nGetFileInformation(fullPathInternal);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x000FDF38 File Offset: 0x000FC138
		internal void SetHashControl(byte[] hash, AssemblyHashAlgorithm hashAlgorithm)
		{
			this._HashForControl = hash;
			this._HashAlgorithmForControl = hashAlgorithm;
		}

		/// <summary>Gets the public key of the assembly.</summary>
		/// <returns>A byte array that contains the public key of the assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">A public key was provided (for example, by using the <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> method), but no public key token was provided.</exception>
		// Token: 0x060044A8 RID: 17576 RVA: 0x000FDF48 File Offset: 0x000FC148
		[__DynamicallyInvokable]
		public byte[] GetPublicKey()
		{
			return this._PublicKey;
		}

		/// <summary>Sets the public key identifying the assembly.</summary>
		/// <param name="publicKey">A byte array containing the public key of the assembly.</param>
		// Token: 0x060044A9 RID: 17577 RVA: 0x000FDF50 File Offset: 0x000FC150
		[__DynamicallyInvokable]
		public void SetPublicKey(byte[] publicKey)
		{
			this._PublicKey = publicKey;
			if (publicKey == null)
			{
				this._Flags &= ~AssemblyNameFlags.PublicKey;
				return;
			}
			this._Flags |= AssemblyNameFlags.PublicKey;
		}

		/// <summary>Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <returns>A byte array that contains the public key token.</returns>
		// Token: 0x060044AA RID: 17578 RVA: 0x000FDF7A File Offset: 0x000FC17A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public byte[] GetPublicKeyToken()
		{
			if (this._PublicKeyToken == null)
			{
				this._PublicKeyToken = this.nGetPublicKeyToken();
			}
			return this._PublicKeyToken;
		}

		/// <summary>Sets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <param name="publicKeyToken">A byte array containing the public key token of the assembly.</param>
		// Token: 0x060044AB RID: 17579 RVA: 0x000FDF96 File Offset: 0x000FC196
		[__DynamicallyInvokable]
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this._PublicKeyToken = publicKeyToken;
		}

		/// <summary>Gets or sets the attributes of the assembly.</summary>
		/// <returns>A value that represents the attributes of the assembly.</returns>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x000FDF9F File Offset: 0x000FC19F
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x000FDFAD File Offset: 0x000FC1AD
		[__DynamicallyInvokable]
		public AssemblyNameFlags Flags
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Flags & (AssemblyNameFlags)(-3825);
			}
			[__DynamicallyInvokable]
			set
			{
				this._Flags &= (AssemblyNameFlags)3824;
				this._Flags |= value & (AssemblyNameFlags)(-3825);
			}
		}

		/// <summary>Gets or sets the hash algorithm used by the assembly manifest.</summary>
		/// <returns>The hash algorithm used by the assembly manifest.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x000FDFD5 File Offset: 0x000FC1D5
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x000FDFDD File Offset: 0x000FC1DD
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this._HashAlgorithm;
			}
			set
			{
				this._HashAlgorithm = value;
			}
		}

		/// <summary>Gets or sets the information related to the assembly's compatibility with other assemblies.</summary>
		/// <returns>A value that represents information about the assembly's compatibility with other assemblies.</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x000FDFE6 File Offset: 0x000FC1E6
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x000FDFEE File Offset: 0x000FC1EE
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this._VersionCompatibility;
			}
			set
			{
				this._VersionCompatibility = value;
			}
		}

		/// <summary>Gets or sets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.</summary>
		/// <returns>The public and private cryptographic key pair to be used to create a strong name for the assembly.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x000FDFF7 File Offset: 0x000FC1F7
		// (set) Token: 0x060044B3 RID: 17587 RVA: 0x000FDFFF File Offset: 0x000FC1FF
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this._StrongNameKeyPair;
			}
			set
			{
				this._StrongNameKeyPair = value;
			}
		}

		/// <summary>Gets the full name of the assembly, also known as the display name.</summary>
		/// <returns>A string that is the full name of the assembly, also known as the display name.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000FE008 File Offset: 0x000FC208
		[__DynamicallyInvokable]
		public string FullName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				string text = this.nToString();
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && string.IsNullOrEmpty(text))
				{
					return base.ToString();
				}
				return text;
			}
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name cannot be determined.</returns>
		// Token: 0x060044B5 RID: 17589 RVA: 0x000FE034 File Offset: 0x000FC234
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of this <see langword="AssemblyName" />.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060044B6 RID: 17590 RVA: 0x000FE054 File Offset: 0x000FC254
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this._Name);
			info.AddValue("_PublicKey", this._PublicKey, typeof(byte[]));
			info.AddValue("_PublicKeyToken", this._PublicKeyToken, typeof(byte[]));
			info.AddValue("_CultureInfo", (this._CultureInfo == null) ? (-1) : this._CultureInfo.LCID);
			info.AddValue("_CodeBase", this._CodeBase);
			info.AddValue("_Version", this._Version);
			info.AddValue("_HashAlgorithm", this._HashAlgorithm, typeof(AssemblyHashAlgorithm));
			info.AddValue("_HashAlgorithmForControl", this._HashAlgorithmForControl, typeof(AssemblyHashAlgorithm));
			info.AddValue("_StrongNameKeyPair", this._StrongNameKeyPair, typeof(StrongNameKeyPair));
			info.AddValue("_VersionCompatibility", this._VersionCompatibility, typeof(AssemblyVersionCompatibility));
			info.AddValue("_Flags", this._Flags, typeof(AssemblyNameFlags));
			info.AddValue("_HashForControl", this._HashForControl, typeof(byte[]));
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060044B7 RID: 17591 RVA: 0x000FE1B0 File Offset: 0x000FC3B0
		public void OnDeserialization(object sender)
		{
			if (this.m_siInfo == null)
			{
				return;
			}
			this._Name = this.m_siInfo.GetString("_Name");
			this._PublicKey = (byte[])this.m_siInfo.GetValue("_PublicKey", typeof(byte[]));
			this._PublicKeyToken = (byte[])this.m_siInfo.GetValue("_PublicKeyToken", typeof(byte[]));
			int @int = this.m_siInfo.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this._CultureInfo = new CultureInfo(@int);
			}
			this._CodeBase = this.m_siInfo.GetString("_CodeBase");
			this._Version = (Version)this.m_siInfo.GetValue("_Version", typeof(Version));
			this._HashAlgorithm = (AssemblyHashAlgorithm)this.m_siInfo.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm));
			this._StrongNameKeyPair = (StrongNameKeyPair)this.m_siInfo.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this._VersionCompatibility = (AssemblyVersionCompatibility)this.m_siInfo.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility));
			this._Flags = (AssemblyNameFlags)this.m_siInfo.GetValue("_Flags", typeof(AssemblyNameFlags));
			try
			{
				this._HashAlgorithmForControl = (AssemblyHashAlgorithm)this.m_siInfo.GetValue("_HashAlgorithmForControl", typeof(AssemblyHashAlgorithm));
				this._HashForControl = (byte[])this.m_siInfo.GetValue("_HashForControl", typeof(byte[]));
			}
			catch (SerializationException)
			{
				this._HashAlgorithmForControl = AssemblyHashAlgorithm.None;
				this._HashForControl = null;
			}
			this.m_siInfo = null;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000FE38C File Offset: 0x000FC58C
		internal AssemblyName(SerializationInfo info, StreamingContext context)
		{
			this.m_siInfo = info;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class with the specified display name.</summary>
		/// <param name="assemblyName">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is a zero length string.</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  The referenced assembly could not be found, or could not be loaded.</exception>
		// Token: 0x060044B9 RID: 17593 RVA: 0x000FE39C File Offset: 0x000FC59C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0 || assemblyName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
			}
			this._Name = assemblyName;
			this.nInit();
		}

		/// <summary>Returns a value indicating whether two assembly names are the same. The comparison is based on the simple assembly names.</summary>
		/// <param name="reference">The reference assembly name.</param>
		/// <param name="definition">The assembly name that is compared to the reference assembly.</param>
		/// <returns>
		///   <see langword="true" /> if the simple assembly names are the same; otherwise, <see langword="false" />.</returns>
		// Token: 0x060044BA RID: 17594 RVA: 0x000FE3EB File Offset: 0x000FC5EB
		[SecuritySafeCritical]
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			return reference == definition || AssemblyName.ReferenceMatchesDefinitionInternal(reference, definition, true);
		}

		// Token: 0x060044BB RID: 17595
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ReferenceMatchesDefinitionInternal(AssemblyName reference, AssemblyName definition, bool parse);

		// Token: 0x060044BC RID: 17596
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void nInit(out RuntimeAssembly assembly, bool forIntrospection, bool raiseResolveEvent);

		// Token: 0x060044BD RID: 17597 RVA: 0x000FE3FC File Offset: 0x000FC5FC
		[SecurityCritical]
		internal void nInit()
		{
			RuntimeAssembly runtimeAssembly = null;
			this.nInit(out runtimeAssembly, false, false);
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000FE415 File Offset: 0x000FC615
		internal void SetProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm)
		{
			this.ProcessorArchitecture = AssemblyName.CalculateProcArchIndex(pek, ifm, this._Flags);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000FE42C File Offset: 0x000FC62C
		internal static ProcessorArchitecture CalculateProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm, AssemblyNameFlags flags)
		{
			if ((flags & (AssemblyNameFlags)240) == (AssemblyNameFlags)112)
			{
				return ProcessorArchitecture.None;
			}
			if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
			{
				if (ifm != ImageFileMachine.I386)
				{
					if (ifm == ImageFileMachine.IA64)
					{
						return ProcessorArchitecture.IA64;
					}
					if (ifm == ImageFileMachine.AMD64)
					{
						return ProcessorArchitecture.Amd64;
					}
				}
				else if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
			}
			else if (ifm == ImageFileMachine.I386)
			{
				if ((pek & PortableExecutableKinds.Required32Bit) == PortableExecutableKinds.Required32Bit)
				{
					return ProcessorArchitecture.X86;
				}
				if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
				return ProcessorArchitecture.X86;
			}
			else if (ifm == ImageFileMachine.ARM)
			{
				return ProcessorArchitecture.Arm;
			}
			return ProcessorArchitecture.None;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000FE498 File Offset: 0x000FC698
		internal void Init(string name, byte[] publicKey, byte[] publicKeyToken, Version version, CultureInfo cultureInfo, AssemblyHashAlgorithm hashAlgorithm, AssemblyVersionCompatibility versionCompatibility, string codeBase, AssemblyNameFlags flags, StrongNameKeyPair keyPair)
		{
			this._Name = name;
			if (publicKey != null)
			{
				this._PublicKey = new byte[publicKey.Length];
				Array.Copy(publicKey, this._PublicKey, publicKey.Length);
			}
			if (publicKeyToken != null)
			{
				this._PublicKeyToken = new byte[publicKeyToken.Length];
				Array.Copy(publicKeyToken, this._PublicKeyToken, publicKeyToken.Length);
			}
			if (version != null)
			{
				this._Version = (Version)version.Clone();
			}
			this._CultureInfo = cultureInfo;
			this._HashAlgorithm = hashAlgorithm;
			this._VersionCompatibility = versionCompatibility;
			this._CodeBase = codeBase;
			this._Flags = flags;
			this._StrongNameKeyPair = keyPair;
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060044C1 RID: 17601 RVA: 0x000FE538 File Offset: 0x000FC738
		void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060044C2 RID: 17602 RVA: 0x000FE53F File Offset: 0x000FC73F
		void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060044C3 RID: 17603 RVA: 0x000FE546 File Offset: 0x000FC746
		void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060044C4 RID: 17604 RVA: 0x000FE54D File Offset: 0x000FC74D
		void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000FE554 File Offset: 0x000FC754
		internal string GetNameWithPublicKey()
		{
			byte[] publicKey = this.GetPublicKey();
			return this.Name + ", PublicKey=" + Hex.EncodeHexString(publicKey);
		}

		// Token: 0x060044C6 RID: 17606
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssemblyName nGetFileInformation(string s);

		// Token: 0x060044C7 RID: 17607
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string nToString();

		// Token: 0x060044C8 RID: 17608
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] nGetPublicKeyToken();

		// Token: 0x060044C9 RID: 17609
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string EscapeCodeBase(string codeBase);

		// Token: 0x04001C1A RID: 7194
		private string _Name;

		// Token: 0x04001C1B RID: 7195
		private byte[] _PublicKey;

		// Token: 0x04001C1C RID: 7196
		private byte[] _PublicKeyToken;

		// Token: 0x04001C1D RID: 7197
		private CultureInfo _CultureInfo;

		// Token: 0x04001C1E RID: 7198
		private string _CodeBase;

		// Token: 0x04001C1F RID: 7199
		private Version _Version;

		// Token: 0x04001C20 RID: 7200
		private StrongNameKeyPair _StrongNameKeyPair;

		// Token: 0x04001C21 RID: 7201
		private SerializationInfo m_siInfo;

		// Token: 0x04001C22 RID: 7202
		private byte[] _HashForControl;

		// Token: 0x04001C23 RID: 7203
		private AssemblyHashAlgorithm _HashAlgorithm;

		// Token: 0x04001C24 RID: 7204
		private AssemblyHashAlgorithm _HashAlgorithmForControl;

		// Token: 0x04001C25 RID: 7205
		private AssemblyVersionCompatibility _VersionCompatibility;

		// Token: 0x04001C26 RID: 7206
		private AssemblyNameFlags _Flags;
	}
}

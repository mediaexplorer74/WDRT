using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.</summary>
	// Token: 0x0200011C RID: 284
	[ComVisible(true)]
	[Serializable]
	public sealed class OperatingSystem : ICloneable, ISerializable
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x00032900 File Offset: 0x00030B00
		private OperatingSystem()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperatingSystem" /> class, using the specified platform identifier value and version object.</summary>
		/// <param name="platform">One of the <see cref="T:System.PlatformID" /> values that indicates the operating system platform.</param>
		/// <param name="version">A <see cref="T:System.Version" /> object that indicates the version of the operating system.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="platform" /> is not a <see cref="T:System.PlatformID" /> enumeration value.</exception>
		// Token: 0x060010C3 RID: 4291 RVA: 0x00032908 File Offset: 0x00030B08
		public OperatingSystem(PlatformID platform, Version version)
			: this(platform, version, null)
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00032914 File Offset: 0x00030B14
		internal OperatingSystem(PlatformID platform, Version version, string servicePack)
		{
			if (platform < PlatformID.Win32S || platform > PlatformID.MacOSX)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)platform }), "platform");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = (Version)version.Clone();
			this._servicePack = servicePack;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00032980 File Offset: 0x00030B80
		private OperatingSystem(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "_version"))
				{
					if (!(name == "_platform"))
					{
						if (name == "_servicePack")
						{
							this._servicePack = info.GetString("_servicePack");
						}
					}
					else
					{
						this._platform = (PlatformID)info.GetValue("_platform", typeof(PlatformID));
					}
				}
				else
				{
					this._version = (Version)info.GetValue("_version", typeof(Version));
				}
			}
			if (this._version == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissField", new object[] { "_version" }));
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize this instance.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060010C6 RID: 4294 RVA: 0x00032A5C File Offset: 0x00030C5C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_version", this._version);
			info.AddValue("_platform", this._platform);
			info.AddValue("_servicePack", this._servicePack);
		}

		/// <summary>Gets a <see cref="T:System.PlatformID" /> enumeration value that identifies the operating system platform.</summary>
		/// <returns>One of the <see cref="T:System.PlatformID" /> values.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00032AAF File Offset: 0x00030CAF
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		/// <summary>Gets the service pack version represented by this <see cref="T:System.OperatingSystem" /> object.</summary>
		/// <returns>The service pack version, if service packs are supported and at least one is installed; otherwise, an empty string ("").</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00032AB7 File Offset: 0x00030CB7
		public string ServicePack
		{
			get
			{
				if (this._servicePack == null)
				{
					return string.Empty;
				}
				return this._servicePack;
			}
		}

		/// <summary>Gets a <see cref="T:System.Version" /> object that identifies the operating system.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that describes the major version, minor version, build, and revision numbers for the operating system.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00032ACD File Offset: 0x00030CCD
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>Creates an <see cref="T:System.OperatingSystem" /> object that is identical to this instance.</summary>
		/// <returns>An <see cref="T:System.OperatingSystem" /> object that is a copy of this instance.</returns>
		// Token: 0x060010CA RID: 4298 RVA: 0x00032AD5 File Offset: 0x00030CD5
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version, this._servicePack);
		}

		/// <summary>Converts the value of this <see cref="T:System.OperatingSystem" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		// Token: 0x060010CB RID: 4299 RVA: 0x00032AEE File Offset: 0x00030CEE
		public override string ToString()
		{
			return this.VersionString;
		}

		/// <summary>Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.</summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00032AF8 File Offset: 0x00030CF8
		public string VersionString
		{
			get
			{
				if (this._versionString != null)
				{
					return this._versionString;
				}
				string text;
				switch (this._platform)
				{
				case PlatformID.Win32S:
					text = "Microsoft Win32S ";
					goto IL_9A;
				case PlatformID.Win32Windows:
					if (this._version.Major > 4 || (this._version.Major == 4 && this._version.Minor > 0))
					{
						text = "Microsoft Windows 98 ";
						goto IL_9A;
					}
					text = "Microsoft Windows 95 ";
					goto IL_9A;
				case PlatformID.Win32NT:
					text = "Microsoft Windows NT ";
					goto IL_9A;
				case PlatformID.WinCE:
					text = "Microsoft Windows CE ";
					goto IL_9A;
				case PlatformID.MacOSX:
					text = "Mac OS X ";
					goto IL_9A;
				}
				text = "<unknown> ";
				IL_9A:
				if (string.IsNullOrEmpty(this._servicePack))
				{
					this._versionString = text + this._version.ToString();
				}
				else
				{
					this._versionString = text + this._version.ToString(3) + " " + this._servicePack;
				}
				return this._versionString;
			}
		}

		// Token: 0x040005CF RID: 1487
		private Version _version;

		// Token: 0x040005D0 RID: 1488
		private PlatformID _platform;

		// Token: 0x040005D1 RID: 1489
		private string _servicePack;

		// Token: 0x040005D2 RID: 1490
		private string _versionString;
	}
}

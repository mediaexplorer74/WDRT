using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for a <see cref="T:System.Security.PermissionSet" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002FF RID: 767
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PermissionSetAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the enumeration values that specifies a security action.</param>
		// Token: 0x06002716 RID: 10006 RVA: 0x0008E802 File Offset: 0x0008CA02
		public PermissionSetAttribute(SecurityAction action)
			: base(action)
		{
			this.m_unicode = false;
		}

		/// <summary>Gets or sets a file containing the XML representation of a custom permission set to be declared.</summary>
		/// <returns>The physical path to the file containing the XML representation of the permission set.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x0008E812 File Offset: 0x0008CA12
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x0008E81A File Offset: 0x0008CA1A
		public string File
		{
			get
			{
				return this.m_file;
			}
			set
			{
				this.m_file = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the file specified by <see cref="P:System.Security.Permissions.PermissionSetAttribute.File" /> is Unicode or ASCII encoded.</summary>
		/// <returns>
		///   <see langword="true" /> if the file is Unicode encoded; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0008E823 File Offset: 0x0008CA23
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x0008E82B File Offset: 0x0008CA2B
		public bool UnicodeEncoded
		{
			get
			{
				return this.m_unicode;
			}
			set
			{
				this.m_unicode = value;
			}
		}

		/// <summary>Gets or sets the name of the permission set.</summary>
		/// <returns>The name of an immutable <see cref="T:System.Security.NamedPermissionSet" /> (one of several permission sets that are contained in the default policy and cannot be altered).</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0008E834 File Offset: 0x0008CA34
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x0008E83C File Offset: 0x0008CA3C
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets the XML representation of a permission set.</summary>
		/// <returns>The XML representation of a permission set.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0008E845 File Offset: 0x0008CA45
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x0008E84D File Offset: 0x0008CA4D
		public string XML
		{
			get
			{
				return this.m_xml;
			}
			set
			{
				this.m_xml = value;
			}
		}

		/// <summary>Gets or sets the hexadecimal representation of the XML encoded permission set.</summary>
		/// <returns>The hexadecimal representation of the XML encoded permission set.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0008E856 File Offset: 0x0008CA56
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0008E85E File Offset: 0x0008CA5E
		public string Hex
		{
			get
			{
				return this.m_hex;
			}
			set
			{
				this.m_hex = value;
			}
		}

		/// <summary>This method is not used.</summary>
		/// <returns>A null reference (<see langword="nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x06002721 RID: 10017 RVA: 0x0008E867 File Offset: 0x0008CA67
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0008E86C File Offset: 0x0008CA6C
		private PermissionSet BruteForceParseStream(Stream stream)
		{
			Encoding[] array = new Encoding[]
			{
				Encoding.UTF8,
				Encoding.ASCII,
				Encoding.Unicode
			};
			StreamReader streamReader = null;
			Exception ex = null;
			int num = 0;
			while (streamReader == null && num < array.Length)
			{
				try
				{
					stream.Position = 0L;
					streamReader = new StreamReader(stream, array[num]);
					return this.ParsePermissionSet(new Parser(streamReader));
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				num++;
			}
			throw ex;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
		private PermissionSet ParsePermissionSet(Parser parser)
		{
			SecurityElement topElement = parser.GetTopElement();
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.FromXml(topElement);
			return permissionSet;
		}

		/// <summary>Creates and returns a new permission set based on this permission set attribute object.</summary>
		/// <returns>A new permission set.</returns>
		// Token: 0x06002724 RID: 10020 RVA: 0x0008E914 File Offset: 0x0008CB14
		[SecuritySafeCritical]
		public PermissionSet CreatePermissionSet()
		{
			if (this.m_unrestricted)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (this.m_name != null)
			{
				return PolicyLevel.GetBuiltInSet(this.m_name);
			}
			if (this.m_xml != null)
			{
				return this.ParsePermissionSet(new Parser(this.m_xml.ToCharArray()));
			}
			if (this.m_hex != null)
			{
				return this.BruteForceParseStream(new MemoryStream(System.Security.Util.Hex.DecodeHexString(this.m_hex)));
			}
			if (this.m_file != null)
			{
				return this.BruteForceParseStream(new FileStream(this.m_file, FileMode.Open, FileAccess.Read));
			}
			return new PermissionSet(PermissionState.None);
		}

		// Token: 0x04000F18 RID: 3864
		private string m_file;

		// Token: 0x04000F19 RID: 3865
		private string m_name;

		// Token: 0x04000F1A RID: 3866
		private bool m_unicode;

		// Token: 0x04000F1B RID: 3867
		private string m_xml;

		// Token: 0x04000F1C RID: 3868
		private string m_hex;
	}
}

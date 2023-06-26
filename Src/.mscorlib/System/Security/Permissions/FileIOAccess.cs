using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	internal sealed class FileIOAccess
	{
		// Token: 0x0600261D RID: 9757 RVA: 0x0008C994 File Offset: 0x0008AB94
		public FileIOAccess()
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = false;
			this.m_allLocalFiles = false;
			this.m_pathDiscovery = false;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0008C9CA File Offset: 0x0008ABCA
		public FileIOAccess(bool pathDiscovery)
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = false;
			this.m_allLocalFiles = false;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0008CA00 File Offset: 0x0008AC00
		[SecurityCritical]
		public FileIOAccess(string value)
		{
			if (value == null)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
				this.m_allFiles = false;
				this.m_allLocalFiles = false;
			}
			else if (value.Length >= "*AllFiles*".Length && string.Compare("*AllFiles*", value, StringComparison.Ordinal) == 0)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
				this.m_allFiles = true;
				this.m_allLocalFiles = false;
			}
			else if (value.Length >= "*AllLocalFiles*".Length && string.Compare("*AllLocalFiles*", 0, value, 0, "*AllLocalFiles*".Length, StringComparison.Ordinal) == 0)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, value.Substring("*AllLocalFiles*".Length), true);
				this.m_allFiles = false;
				this.m_allLocalFiles = true;
			}
			else
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, value, true);
				this.m_allFiles = false;
				this.m_allLocalFiles = false;
			}
			this.m_pathDiscovery = false;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0008CB0A File Offset: 0x0008AD0A
		public FileIOAccess(bool allFiles, bool allLocalFiles, bool pathDiscovery)
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = allFiles;
			this.m_allLocalFiles = allLocalFiles;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0008CB40 File Offset: 0x0008AD40
		public FileIOAccess(StringExpressionSet set, bool allFiles, bool allLocalFiles, bool pathDiscovery)
		{
			this.m_set = set;
			this.m_set.SetThrowOnRelative(true);
			this.m_allFiles = allFiles;
			this.m_allLocalFiles = allLocalFiles;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0008CB78 File Offset: 0x0008AD78
		private FileIOAccess(FileIOAccess operand)
		{
			this.m_set = operand.m_set.Copy();
			this.m_allFiles = operand.m_allFiles;
			this.m_allLocalFiles = operand.m_allLocalFiles;
			this.m_pathDiscovery = operand.m_pathDiscovery;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0008CBC7 File Offset: 0x0008ADC7
		[SecurityCritical]
		public void AddExpressions(ArrayList values, bool checkForDuplicates)
		{
			this.m_allFiles = false;
			this.m_set.AddExpressions(values, checkForDuplicates);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0008CBDD File Offset: 0x0008ADDD
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x0008CBE5 File Offset: 0x0008ADE5
		public bool AllFiles
		{
			get
			{
				return this.m_allFiles;
			}
			set
			{
				this.m_allFiles = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x0008CBEE File Offset: 0x0008ADEE
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x0008CBF6 File Offset: 0x0008ADF6
		public bool AllLocalFiles
		{
			get
			{
				return this.m_allLocalFiles;
			}
			set
			{
				this.m_allLocalFiles = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x0008CBFF File Offset: 0x0008ADFF
		public bool PathDiscovery
		{
			set
			{
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0008CC08 File Offset: 0x0008AE08
		public bool IsEmpty()
		{
			return !this.m_allFiles && !this.m_allLocalFiles && (this.m_set == null || this.m_set.IsEmpty());
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0008CC31 File Offset: 0x0008AE31
		public FileIOAccess Copy()
		{
			return new FileIOAccess(this);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0008CC3C File Offset: 0x0008AE3C
		[SecuritySafeCritical]
		public FileIOAccess Union(FileIOAccess operand)
		{
			if (operand == null)
			{
				if (!this.IsEmpty())
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.m_allFiles || operand.m_allFiles)
				{
					return new FileIOAccess(true, false, this.m_pathDiscovery);
				}
				return new FileIOAccess(this.m_set.Union(operand.m_set), false, this.m_allLocalFiles || operand.m_allLocalFiles, this.m_pathDiscovery);
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0008CCAC File Offset: 0x0008AEAC
		[SecuritySafeCritical]
		public FileIOAccess Intersect(FileIOAccess operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.m_allFiles)
			{
				if (operand.m_allFiles)
				{
					return new FileIOAccess(true, false, this.m_pathDiscovery);
				}
				return new FileIOAccess(operand.m_set.Copy(), false, operand.m_allLocalFiles, this.m_pathDiscovery);
			}
			else
			{
				if (operand.m_allFiles)
				{
					return new FileIOAccess(this.m_set.Copy(), false, this.m_allLocalFiles, this.m_pathDiscovery);
				}
				StringExpressionSet stringExpressionSet = new StringExpressionSet(this.m_ignoreCase, true);
				if (this.m_allLocalFiles)
				{
					string[] array = operand.m_set.UnsafeToStringArray();
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							string root = FileIOAccess.GetRoot(array[i]);
							if (root != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
							{
								stringExpressionSet.AddExpressions(new string[] { array[i] }, true, false);
							}
						}
					}
				}
				if (operand.m_allLocalFiles)
				{
					string[] array2 = this.m_set.UnsafeToStringArray();
					if (array2 != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							string root2 = FileIOAccess.GetRoot(array2[j]);
							if (root2 != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root2)))
							{
								stringExpressionSet.AddExpressions(new string[] { array2[j] }, true, false);
							}
						}
					}
				}
				string[] array3 = this.m_set.Intersect(operand.m_set).UnsafeToStringArray();
				if (array3 != null)
				{
					stringExpressionSet.AddExpressions(array3, !stringExpressionSet.IsEmpty(), false);
				}
				return new FileIOAccess(stringExpressionSet, false, this.m_allLocalFiles && operand.m_allLocalFiles, this.m_pathDiscovery);
			}
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0008CE2C File Offset: 0x0008B02C
		[SecuritySafeCritical]
		public bool IsSubsetOf(FileIOAccess operand)
		{
			if (operand == null)
			{
				return this.IsEmpty();
			}
			if (operand.m_allFiles)
			{
				return true;
			}
			if ((!this.m_pathDiscovery || !this.m_set.IsSubsetOfPathDiscovery(operand.m_set)) && !this.m_set.IsSubsetOf(operand.m_set))
			{
				if (!operand.m_allLocalFiles)
				{
					return false;
				}
				string[] array = this.m_set.UnsafeToStringArray();
				for (int i = 0; i < array.Length; i++)
				{
					string root = FileIOAccess.GetRoot(array[i]);
					if (root == null || !FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0008CEC0 File Offset: 0x0008B0C0
		private static string GetRoot(string path)
		{
			string text = path.Substring(0, 3);
			if (text.EndsWith(":\\", StringComparison.Ordinal))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0008CEE8 File Offset: 0x0008B0E8
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this.m_allFiles)
			{
				return "*AllFiles*";
			}
			if (this.m_allLocalFiles)
			{
				string text = "*AllLocalFiles*";
				string text2 = this.m_set.UnsafeToString();
				if (text2 != null && text2.Length > 0)
				{
					text = text + ";" + text2;
				}
				return text;
			}
			return this.m_set.UnsafeToString();
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0008CF43 File Offset: 0x0008B143
		[SecuritySafeCritical]
		public string[] ToStringArray()
		{
			return this.m_set.UnsafeToStringArray();
		}

		// Token: 0x06002631 RID: 9777
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsLocalDrive(string path);

		// Token: 0x06002632 RID: 9778 RVA: 0x0008CF50 File Offset: 0x0008B150
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			FileIOAccess fileIOAccess = obj as FileIOAccess;
			if (fileIOAccess == null)
			{
				return this.IsEmpty() && obj == null;
			}
			if (this.m_pathDiscovery)
			{
				return (this.m_allFiles && fileIOAccess.m_allFiles) || (this.m_allLocalFiles == fileIOAccess.m_allLocalFiles && this.m_set.IsSubsetOf(fileIOAccess.m_set) && fileIOAccess.m_set.IsSubsetOf(this.m_set));
			}
			return this.IsSubsetOf(fileIOAccess) && fileIOAccess.IsSubsetOf(this);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0008CFDF File Offset: 0x0008B1DF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000E92 RID: 3730
		private bool m_ignoreCase = true;

		// Token: 0x04000E93 RID: 3731
		private StringExpressionSet m_set;

		// Token: 0x04000E94 RID: 3732
		private bool m_allFiles;

		// Token: 0x04000E95 RID: 3733
		private bool m_allLocalFiles;

		// Token: 0x04000E96 RID: 3734
		private bool m_pathDiscovery;

		// Token: 0x04000E97 RID: 3735
		private const string m_strAllFiles = "*AllFiles*";

		// Token: 0x04000E98 RID: 3736
		private const string m_strAllLocalFiles = "*AllLocalFiles*";
	}
}

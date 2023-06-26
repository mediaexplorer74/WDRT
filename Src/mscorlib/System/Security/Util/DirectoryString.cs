using System;
using System.Collections;

namespace System.Security.Util
{
	// Token: 0x0200037F RID: 895
	[Serializable]
	internal class DirectoryString : SiteString
	{
		// Token: 0x06002CB6 RID: 11446 RVA: 0x000A88BF File Offset: 0x000A6ABF
		public DirectoryString()
		{
			this.m_site = "";
			this.m_separatedSite = new ArrayList();
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000A88DD File Offset: 0x000A6ADD
		public DirectoryString(string directory, bool checkForIllegalChars)
		{
			this.m_site = directory;
			this.m_checkForIllegalChars = checkForIllegalChars;
			this.m_separatedSite = this.CreateSeparatedString(directory);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000A8900 File Offset: 0x000A6B00
		private ArrayList CreateSeparatedString(string directory)
		{
			if (directory == null || directory.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
			}
			ArrayList arrayList = new ArrayList();
			string[] array = directory.Split(DirectoryString.m_separators);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					if (array[i].Equals("*"))
					{
						if (i != array.Length - 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
					else
					{
						if (this.m_checkForIllegalChars && array[i].IndexOfAny(DirectoryString.m_illegalDirectoryCharacters) != -1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000A89C5 File Offset: 0x000A6BC5
		public virtual bool IsSubsetOf(DirectoryString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000A89D0 File Offset: 0x000A6BD0
		public virtual bool IsSubsetOf(DirectoryString operand, bool ignoreCase)
		{
			if (operand == null)
			{
				return false;
			}
			if (operand.m_separatedSite.Count == 0)
			{
				return this.m_separatedSite.Count == 0 || (this.m_separatedSite.Count > 0 && string.Compare((string)this.m_separatedSite[0], "*", StringComparison.Ordinal) == 0);
			}
			if (this.m_separatedSite.Count == 0)
			{
				return string.Compare((string)operand.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
			}
			return base.IsSubsetOf(operand, ignoreCase);
		}

		// Token: 0x040011EF RID: 4591
		private bool m_checkForIllegalChars;

		// Token: 0x040011F0 RID: 4592
		private new static char[] m_separators = new char[] { '/' };

		// Token: 0x040011F1 RID: 4593
		protected static char[] m_illegalDirectoryCharacters = new char[] { '\\', ':', '*', '?', '"', '<', '>', '|' };
	}
}

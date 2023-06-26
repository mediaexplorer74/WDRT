using System;

namespace System.Windows.Forms
{
	// Token: 0x02000272 RID: 626
	internal class HelpInfo
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x000BA740 File Offset: 0x000B8940
		public HelpInfo(string helpfilepath)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = HelpNavigator.TableOfContents;
			this.param = null;
			this.option = 1;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000BA773 File Offset: 0x000B8973
		public HelpInfo(string helpfilepath, string keyword)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = keyword;
			this.navigator = HelpNavigator.TableOfContents;
			this.param = null;
			this.option = 2;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000BA7A2 File Offset: 0x000B89A2
		public HelpInfo(string helpfilepath, HelpNavigator navigator)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = navigator;
			this.param = null;
			this.option = 3;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000BA7D1 File Offset: 0x000B89D1
		public HelpInfo(string helpfilepath, HelpNavigator navigator, object param)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = navigator;
			this.param = param;
			this.option = 4;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000BA800 File Offset: 0x000B8A00
		public int Option
		{
			get
			{
				return this.option;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x000BA808 File Offset: 0x000B8A08
		public string HelpFilePath
		{
			get
			{
				return this.helpFilePath;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000BA810 File Offset: 0x000B8A10
		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x000BA818 File Offset: 0x000B8A18
		public HelpNavigator Navigator
		{
			get
			{
				return this.navigator;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x000BA820 File Offset: 0x000B8A20
		public object Param
		{
			get
			{
				return this.param;
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000BA828 File Offset: 0x000B8A28
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{HelpFilePath=",
				this.helpFilePath,
				", keyword =",
				this.keyword,
				", navigator=",
				this.navigator.ToString(),
				"}"
			});
		}

		// Token: 0x04001081 RID: 4225
		private string helpFilePath;

		// Token: 0x04001082 RID: 4226
		private string keyword;

		// Token: 0x04001083 RID: 4227
		private HelpNavigator navigator;

		// Token: 0x04001084 RID: 4228
		private object param;

		// Token: 0x04001085 RID: 4229
		private int option;
	}
}

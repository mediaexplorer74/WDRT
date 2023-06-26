using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x02000137 RID: 311
	internal class WebRequestPrefixElement
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0003DD38 File Offset: 0x0003BF38
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(SR.GetString("net_invalid_cast", new object[] { creatorType.AssemblyQualifiedName, "IWebRequestCreate" }));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0003DE1E File Offset: 0x0003C01E
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x0400106A RID: 4202
		public string Prefix;

		// Token: 0x0400106B RID: 4203
		internal IWebRequestCreate creator;

		// Token: 0x0400106C RID: 4204
		internal Type creatorType;
	}
}

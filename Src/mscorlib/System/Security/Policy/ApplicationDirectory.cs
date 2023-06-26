using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Provides the application directory as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200033F RID: 831
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> class.</summary>
		/// <param name="name">The path of the application directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002984 RID: 10628 RVA: 0x0009A7B0 File Offset: 0x000989B0
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_appDirectory = new URLString(name);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0009A7D2 File Offset: 0x000989D2
		private ApplicationDirectory(URLString appDirectory)
		{
			this.m_appDirectory = appDirectory;
		}

		/// <summary>Gets the path of the application directory.</summary>
		/// <returns>The path of the application directory.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x0009A7E1 File Offset: 0x000989E1
		public string Directory
		{
			get
			{
				return this.m_appDirectory.ToString();
			}
		}

		/// <summary>Determines whether instances of the same type of an evidence object are equivalent.</summary>
		/// <param name="o">An object of same type as the current evidence object.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002987 RID: 10631 RVA: 0x0009A7F0 File Offset: 0x000989F0
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			return applicationDirectory != null && this.m_appDirectory.Equals(applicationDirectory.m_appDirectory);
		}

		/// <summary>Gets the hash code of the current application directory.</summary>
		/// <returns>The hash code of the current application directory.</returns>
		// Token: 0x06002988 RID: 10632 RVA: 0x0009A81A File Offset: 0x00098A1A
		public override int GetHashCode()
		{
			return this.m_appDirectory.GetHashCode();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002989 RID: 10633 RVA: 0x0009A827 File Offset: 0x00098A27
		public override EvidenceBase Clone()
		{
			return new ApplicationDirectory(this.m_appDirectory);
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</returns>
		// Token: 0x0600298A RID: 10634 RVA: 0x0009A834 File Offset: 0x00098A34
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0009A83C File Offset: 0x00098A3C
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			if (this.m_appDirectory != null)
			{
				securityElement.AddChild(new SecurityElement("Directory", this.m_appDirectory.ToString()));
			}
			return securityElement;
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</returns>
		// Token: 0x0600298C RID: 10636 RVA: 0x0009A888 File Offset: 0x00098A88
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x04001111 RID: 4369
		private URLString m_appDirectory;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Holds the name/value pair of the property name and the object representing the property of a context.</summary>
	// Token: 0x02000809 RID: 2057
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ContextProperty
	{
		/// <summary>Gets the name of the T:System.Runtime.Remoting.Contexts.ContextProperty class.</summary>
		/// <returns>The name of the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> class.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x0013A59D File Offset: 0x0013879D
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the object representing the property of a context.</summary>
		/// <returns>The object representing the property of a context.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060058D4 RID: 22740 RVA: 0x0013A5A5 File Offset: 0x001387A5
		public virtual object Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0013A5AD File Offset: 0x001387AD
		internal ContextProperty(string name, object prop)
		{
			this._name = name;
			this._property = prop;
		}

		// Token: 0x0400287A RID: 10362
		internal string _name;

		// Token: 0x0400287B RID: 10363
		internal object _property;
	}
}

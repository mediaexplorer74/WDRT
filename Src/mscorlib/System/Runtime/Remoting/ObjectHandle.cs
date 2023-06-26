using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Wraps marshal-by-value object references, allowing them to be returned through an indirection.</summary>
	// Token: 0x020007CD RID: 1997
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x060056DD RID: 22237 RVA: 0x00135AAF File Offset: 0x00133CAF
		private ObjectHandle()
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Remoting.ObjectHandle" /> class, wrapping the given object <paramref name="o" />.</summary>
		/// <param name="o">The object that is wrapped by the new <see cref="T:System.Runtime.Remoting.ObjectHandle" />.</param>
		// Token: 0x060056DE RID: 22238 RVA: 0x00135AB7 File Offset: 0x00133CB7
		public ObjectHandle(object o)
		{
			this.WrappedObject = o;
		}

		/// <summary>Returns the wrapped object.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x060056DF RID: 22239 RVA: 0x00135AC6 File Offset: 0x00133CC6
		public object Unwrap()
		{
			return this.WrappedObject;
		}

		/// <summary>Initializes the lifetime lease of the wrapped object.</summary>
		/// <returns>An initialized <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> that allows you to control the lifetime of the wrapped object.</returns>
		// Token: 0x060056E0 RID: 22240 RVA: 0x00135AD0 File Offset: 0x00133CD0
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			MarshalByRefObject marshalByRefObject = this.WrappedObject as MarshalByRefObject;
			if (marshalByRefObject != null && marshalByRefObject.InitializeLifetimeService() == null)
			{
				return null;
			}
			return (ILease)base.InitializeLifetimeService();
		}

		// Token: 0x040027BC RID: 10172
		private object WrappedObject;
	}
}

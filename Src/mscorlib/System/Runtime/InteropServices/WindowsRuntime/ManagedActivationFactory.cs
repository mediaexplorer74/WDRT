using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F0 RID: 2544
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal sealed class ManagedActivationFactory : IActivationFactory, IManagedActivationFactory
	{
		// Token: 0x060064D6 RID: 25814 RVA: 0x001589E8 File Offset: 0x00156BE8
		[SecurityCritical]
		internal ManagedActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType) || !type.IsExportedToWindowsRuntime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotActivatableViaWindowsRuntime", new object[] { type }), "type");
			}
			this.m_type = type;
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x00158A48 File Offset: 0x00156C48
		public object ActivateInstance()
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(this.m_type);
			}
			catch (MissingMethodException)
			{
				throw new NotImplementedException();
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return obj;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x00158A90 File Offset: 0x00156C90
		void IManagedActivationFactory.RunClassConstructor()
		{
			RuntimeHelpers.RunClassConstructor(this.m_type.TypeHandle);
		}

		// Token: 0x04002D04 RID: 11524
		private Type m_type;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.DesignerServices
{
	/// <summary>Provides customized assembly binding for designers that are used to create Windows 8.x Store apps.</summary>
	// Token: 0x02000719 RID: 1817
	public sealed class WindowsRuntimeDesignerContext
	{
		// Token: 0x06005150 RID: 20816
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr CreateDesignerContext([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] string[] paths, int count, bool shared);

		// Token: 0x06005151 RID: 20817 RVA: 0x0011FE24 File Offset: 0x0011E024
		[SecurityCritical]
		internal static IntPtr CreateDesignerContext(IEnumerable<string> paths, [MarshalAs(UnmanagedType.Bool)] bool shared)
		{
			List<string> list = new List<string>(paths);
			string[] array = list.ToArray();
			foreach (string text in array)
			{
				if (text == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Path"));
				}
				if (Path.IsRelative(text))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
				}
			}
			return WindowsRuntimeDesignerContext.CreateDesignerContext(array, array.Length, shared);
		}

		// Token: 0x06005152 RID: 20818
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetCurrentContext([MarshalAs(UnmanagedType.Bool)] bool isDesignerContext, IntPtr context);

		// Token: 0x06005153 RID: 20819 RVA: 0x0011FE8C File Offset: 0x0011E08C
		[SecurityCritical]
		private WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name, bool designModeRequired)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (!AppDomain.IsAppXModel())
			{
				throw new NotSupportedException();
			}
			if (designModeRequired && !AppDomain.IsAppXDesignMode())
			{
				throw new NotSupportedException();
			}
			this.m_name = name;
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext == IntPtr.Zero)
				{
					WindowsRuntimeDesignerContext.InitializeSharedContext(new string[0]);
				}
			}
			this.m_contextObject = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.DesignerServices.WindowsRuntimeDesignerContext" /> class, specifying the set of paths to search for third-party Windows Runtime types and for managed assemblies, and specifying the name of the context.</summary>
		/// <param name="paths">The paths to search.</param>
		/// <param name="name">The name of the context.</param>
		/// <exception cref="T:System.NotSupportedException">The current application domain is not the default application domain.  
		///  -or-  
		///  The process is not running in the app container.  
		///  -or-  
		///  The computer does not have a developer license.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="paths" /> is <see langword="null" />.</exception>
		// Token: 0x06005154 RID: 20820 RVA: 0x0011FF48 File Offset: 0x0011E148
		[SecurityCritical]
		public WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name)
			: this(paths, name, true)
		{
		}

		/// <summary>Creates a context and sets it as the shared context.</summary>
		/// <param name="paths">An enumerable collection of paths that are used to resolve binding requests that cannot be satisfied by the iteration context.</param>
		/// <exception cref="T:System.NotSupportedException">The shared context has already been set in this application domain.  
		///  -or-  
		///  The current application domain is not the default application domain.  
		///  -or-  
		///  The process is not running in the app container.  
		///  -or-  
		///  The computer does not have a developer license.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="paths" /> is <see langword="null" />.</exception>
		// Token: 0x06005155 RID: 20821 RVA: 0x0011FF54 File Offset: 0x0011E154
		[SecurityCritical]
		public static void InitializeSharedContext(IEnumerable<string> paths)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext != IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				IntPtr intPtr = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, true);
				WindowsRuntimeDesignerContext.SetCurrentContext(false, intPtr);
				WindowsRuntimeDesignerContext.s_sharedContext = intPtr;
			}
		}

		/// <summary>Sets a context to handle iterations of assembly binding requests, as assemblies are recompiled during the design process.</summary>
		/// <param name="context">The context that handles iterations of assembly binding requests.</param>
		/// <exception cref="T:System.NotSupportedException">The current application domain is not the default application domain.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is <see langword="null" />.</exception>
		// Token: 0x06005156 RID: 20822 RVA: 0x0011FFDC File Offset: 0x0011E1DC
		[SecurityCritical]
		public static void SetIterationContext(WindowsRuntimeDesignerContext context)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				WindowsRuntimeDesignerContext.SetCurrentContext(true, context.m_contextObject);
			}
		}

		/// <summary>Loads the specified assembly from the current context.</summary>
		/// <param name="assemblyName">The full name of the assembly to load. For a description of full assembly names, see the <see cref="P:System.Reflection.Assembly.FullName" /> property.</param>
		/// <returns>The assembly, if it is found in the current context; otherwise, <see langword="null" />.</returns>
		// Token: 0x06005157 RID: 20823 RVA: 0x00120044 File Offset: 0x0011E244
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly GetAssembly(string assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyName, null, ref stackCrawlMark, this.m_contextObject, false);
		}

		/// <summary>Loads the specified type from the current context.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to load. For a description of assembly-qualified names, see the <see cref="P:System.Type.AssemblyQualifiedName" /> property.</param>
		/// <returns>The type, if it is found in the current context; otherwise, <see langword="null" />.</returns>
		// Token: 0x06005158 RID: 20824 RVA: 0x00120064 File Offset: 0x0011E264
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, this.m_contextObject, false);
		}

		/// <summary>Gets the name of the designer binding context.</summary>
		/// <returns>The name of the context.</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06005159 RID: 20825 RVA: 0x00120093 File Offset: 0x0011E293
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x04002405 RID: 9221
		private static object s_lock = new object();

		// Token: 0x04002406 RID: 9222
		private static IntPtr s_sharedContext;

		// Token: 0x04002407 RID: 9223
		private IntPtr m_contextObject;

		// Token: 0x04002408 RID: 9224
		private string m_name;
	}
}

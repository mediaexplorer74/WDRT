using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Holds a message returned in response to a method call on a remote object.</summary>
	// Token: 0x02000866 RID: 2150
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class with all the information returning to the caller after the method call.</summary>
		/// <param name="ret">The object returned by the invoked method from which the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> instance originated.</param>
		/// <param name="outArgs">The objects returned from the invoked method as <see langword="out" /> parameters.</param>
		/// <param name="outArgsCount">The number of <see langword="out" /> parameters returned from the invoked method.</param>
		/// <param name="callCtx">The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the method call.</param>
		/// <param name="mcm">The original method call to the invoked method.</param>
		// Token: 0x06005B61 RID: 23393 RVA: 0x001416D0 File Offset: 0x0013F8D0
		[SecurityCritical]
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._ret = ret;
			this._outArgs = outArgs;
			this._outArgsCount = outArgsCount;
			if (callCtx != null)
			{
				this._callContext = callCtx;
			}
			else
			{
				this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			}
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class.</summary>
		/// <param name="e">The exception that was thrown during execution of the remotely called method.</param>
		/// <param name="mcm">An <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> with which to create an instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class.</param>
		// Token: 0x06005B62 RID: 23394 RVA: 0x00141768 File Offset: 0x0013F968
		[SecurityCritical]
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._e = (ReturnMessage.IsCustomErrorEnabled() ? new RemotingException(Environment.GetResourceString("Remoting_InternalError")) : e);
			this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		/// <summary>Gets or sets the URI of the remote object on which the remote method was called.</summary>
		/// <returns>The URI of the remote object on which the remote method was called.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x001417F5 File Offset: 0x0013F9F5
		// (set) Token: 0x06005B64 RID: 23396 RVA: 0x001417FD File Offset: 0x0013F9FD
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		/// <summary>Gets the name of the called method.</summary>
		/// <returns>The name of the method that the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> originated from.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x00141806 File Offset: 0x0013FA06
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._methodName;
			}
		}

		/// <summary>Gets the name of the type on which the remote method was called.</summary>
		/// <returns>The type name of the remote object on which the remote method was called.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0014180E File Offset: 0x0013FA0E
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._typeName;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Type" /> objects containing the method signature.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects containing the method signature.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005B67 RID: 23399 RVA: 0x00141816 File Offset: 0x0013FA16
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
				{
					this._methodSignature = Message.GenerateMethodSignature(this._methodBase);
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005B68 RID: 23400 RVA: 0x00141845 File Offset: 0x0013FA45
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._methodBase;
			}
		}

		/// <summary>Gets a value indicating whether the called method accepts a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the called method accepts a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x0014184D File Offset: 0x0013FA4D
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._hasVarArgs;
			}
		}

		/// <summary>Gets the number of arguments of the called method.</summary>
		/// <returns>The number of arguments of the called method.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005B6A RID: 23402 RVA: 0x00141855 File Offset: 0x0013FA55
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null)
				{
					return this._outArgsCount;
				}
				return this._outArgs.Length;
			}
		}

		/// <summary>Returns a specified argument passed to the remote method during the method call.</summary>
		/// <param name="argNum">The zero-based index of the requested argument.</param>
		/// <returns>An argument passed to the remote method during the method call.</returns>
		// Token: 0x06005B6B RID: 23403 RVA: 0x00141870 File Offset: 0x0013FA70
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._outArgs == null)
			{
				if (argNum < 0 || argNum >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return null;
			}
			else
			{
				if (argNum < 0 || argNum >= this._outArgs.Length)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return this._outArgs[argNum];
			}
		}

		/// <summary>Returns the name of a specified method argument.</summary>
		/// <param name="index">The zero-based index of the requested argument name.</param>
		/// <returns>The name of a specified method argument.</returns>
		// Token: 0x06005B6C RID: 23404 RVA: 0x001418C4 File Offset: 0x0013FAC4
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (this._outArgs == null)
			{
				if (index < 0 || index >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
			}
			else if (index < 0 || index >= this._outArgs.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._methodBase != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._methodBase);
				return reflectionCachedData.Parameters[index].Name;
			}
			return "__param" + index.ToString();
		}

		/// <summary>Gets a specified argument passed to the method called on the remote object.</summary>
		/// <returns>An argument passed to the method called on the remote object.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005B6D RID: 23405 RVA: 0x00141944 File Offset: 0x0013FB44
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null)
				{
					return new object[this._outArgsCount];
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets the number of <see langword="out" /> or <see langword="ref" /> arguments on the called method.</summary>
		/// <returns>The number of <see langword="out" /> or <see langword="ref" /> arguments on the called method.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x00141960 File Offset: 0x0013FB60
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		/// <summary>Returns the object passed as an <see langword="out" /> or <see langword="ref" /> parameter during the remote method call.</summary>
		/// <param name="argNum">The zero-based index of the requested <see langword="out" /> or <see langword="ref" /> parameter.</param>
		/// <returns>The object passed as an <see langword="out" /> or <see langword="ref" /> parameter during the remote method call.</returns>
		// Token: 0x06005B6F RID: 23407 RVA: 0x00141982 File Offset: 0x0013FB82
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		/// <summary>Returns the name of a specified <see langword="out" /> or <see langword="ref" /> parameter passed to the remote method.</summary>
		/// <param name="index">The zero-based index of the requested argument.</param>
		/// <returns>A string representing the name of the specified <see langword="out" /> or <see langword="ref" /> parameter, or <see langword="null" /> if the current method is not implemented.</returns>
		// Token: 0x06005B70 RID: 23408 RVA: 0x001419A5 File Offset: 0x0013FBA5
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		/// <summary>Gets a specified object passed as an <see langword="out" /> or <see langword="ref" /> parameter to the called method.</summary>
		/// <returns>An object passed as an <see langword="out" /> or <see langword="ref" /> parameter to the called method.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005B71 RID: 23409 RVA: 0x001419C8 File Offset: 0x0013FBC8
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		/// <summary>Gets the exception that was thrown during the remote method call.</summary>
		/// <returns>The exception thrown during the method call, or <see langword="null" /> if an exception did not occur during the call.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005B72 RID: 23410 RVA: 0x001419EA File Offset: 0x0013FBEA
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._e;
			}
		}

		/// <summary>Gets the object returned by the called method.</summary>
		/// <returns>The object returned by the called method.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005B73 RID: 23411 RVA: 0x001419F2 File Offset: 0x0013FBF2
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._ret;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x001419FA File Offset: 0x0013FBFA
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MRMDictionary(this, null);
				}
				return (MRMDictionary)this._properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005B75 RID: 23413 RVA: 0x00141A1C File Offset: 0x0013FC1C
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00141A24 File Offset: 0x0013FC24
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x00141A40 File Offset: 0x0013FC40
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = ctx;
			return callContext;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x00141A5C File Offset: 0x0013FC5C
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00141A68 File Offset: 0x0013FC68
		[SecurityCritical]
		internal static bool IsCustomErrorEnabled()
		{
			object data = CallContext.GetData("__CustomErrorsEnabled");
			return data != null && (bool)data;
		}

		// Token: 0x04002955 RID: 10581
		internal object _ret;

		// Token: 0x04002956 RID: 10582
		internal object _properties;

		// Token: 0x04002957 RID: 10583
		internal string _URI;

		// Token: 0x04002958 RID: 10584
		internal Exception _e;

		// Token: 0x04002959 RID: 10585
		internal object[] _outArgs;

		// Token: 0x0400295A RID: 10586
		internal int _outArgsCount;

		// Token: 0x0400295B RID: 10587
		internal string _methodName;

		// Token: 0x0400295C RID: 10588
		internal string _typeName;

		// Token: 0x0400295D RID: 10589
		internal Type[] _methodSignature;

		// Token: 0x0400295E RID: 10590
		internal bool _hasVarArgs;

		// Token: 0x0400295F RID: 10591
		internal LogicalCallContext _callContext;

		// Token: 0x04002960 RID: 10592
		internal ArgMapper _argMapper;

		// Token: 0x04002961 RID: 10593
		internal MethodBase _methodBase;
	}
}

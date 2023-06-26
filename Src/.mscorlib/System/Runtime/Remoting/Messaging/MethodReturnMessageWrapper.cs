using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a response to a method call on a remote object.</summary>
	// Token: 0x02000872 RID: 2162
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Wraps an <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> to create a <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />.</summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x06005C29 RID: 23593 RVA: 0x00144510 File Offset: 0x00142710
		public MethodReturnMessageWrapper(IMethodReturnMessage msg)
			: base(msg)
		{
			this._msg = msg;
			this._args = this._msg.Args;
			this._returnValue = this._msg.ReturnValue;
			this._exception = this._msg.Exception;
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x0014455E File Offset: 0x0014275E
		// (set) Token: 0x06005C2B RID: 23595 RVA: 0x0014456B File Offset: 0x0014276B
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._msg.Uri;
			}
			set
			{
				this._msg.Properties[Message.UriKey] = value;
			}
		}

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00144583 File Offset: 0x00142783
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodName;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x00144590 File Offset: 0x00142790
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._msg.TypeName;
			}
		}

		/// <summary>Gets an object that contains the method signature.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005C2E RID: 23598 RVA: 0x0014459D File Offset: 0x0014279D
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodSignature;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x001445AA File Offset: 0x001427AA
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._msg.LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005C30 RID: 23600 RVA: 0x001445B7 File Offset: 0x001427B7
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodBase;
			}
		}

		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x001445C4 File Offset: 0x001427C4
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args != null)
				{
					return this._args.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets the name of a method argument at a specified index.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument.</returns>
		// Token: 0x06005C32 RID: 23602 RVA: 0x001445D8 File Offset: 0x001427D8
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return this._msg.GetArgName(index);
		}

		/// <summary>Gets a method argument, as an object, at a specified index.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument as an object.</returns>
		// Token: 0x06005C33 RID: 23603 RVA: 0x001445E6 File Offset: 0x001427E6
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005C34 RID: 23604 RVA: 0x001445F0 File Offset: 0x001427F0
		// (set) Token: 0x06005C35 RID: 23605 RVA: 0x001445F8 File Offset: 0x001427F8
		public virtual object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		/// <summary>Gets a flag that indicates whether the method can accept a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005C36 RID: 23606 RVA: 0x00144601 File Offset: 0x00142801
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._msg.HasVarArgs;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</returns>
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x0014460E File Offset: 0x0014280E
		public virtual int OutArgCount
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

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</returns>
		// Token: 0x06005C38 RID: 23608 RVA: 0x00144630 File Offset: 0x00142830
		[SecurityCritical]
		public virtual object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		/// <summary>Returns the name of the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The argument name, or <see langword="null" /> if the current method is not implemented.</returns>
		// Token: 0x06005C39 RID: 23609 RVA: 0x00144653 File Offset: 0x00142853
		[SecurityCritical]
		public virtual string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		/// <summary>Gets an array of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</returns>
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005C3A RID: 23610 RVA: 0x00144676 File Offset: 0x00142876
		public virtual object[] OutArgs
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

		/// <summary>Gets the exception thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</returns>
		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x00144698 File Offset: 0x00142898
		// (set) Token: 0x06005C3C RID: 23612 RVA: 0x001446A0 File Offset: 0x001428A0
		public virtual Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x001446A9 File Offset: 0x001428A9
		// (set) Token: 0x06005C3E RID: 23614 RVA: 0x001446B1 File Offset: 0x001428B1
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._returnValue;
			}
			set
			{
				this._returnValue = value;
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005C3F RID: 23615 RVA: 0x001446BA File Offset: 0x001428BA
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.MRMWrapperDictionary(this, this._msg.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x040029A0 RID: 10656
		private IMethodReturnMessage _msg;

		// Token: 0x040029A1 RID: 10657
		private IDictionary _properties;

		// Token: 0x040029A2 RID: 10658
		private ArgMapper _argMapper;

		// Token: 0x040029A3 RID: 10659
		private object[] _args;

		// Token: 0x040029A4 RID: 10660
		private object _returnValue;

		// Token: 0x040029A5 RID: 10661
		private Exception _exception;

		// Token: 0x02000C74 RID: 3188
		private class MRMWrapperDictionary : Hashtable
		{
			// Token: 0x060070D2 RID: 28882 RVA: 0x0018623C File Offset: 0x0018443C
			public MRMWrapperDictionary(IMethodReturnMessage msg, IDictionary idict)
			{
				this._mrmsg = msg;
				this._idict = idict;
			}

			// Token: 0x17001354 RID: 4948
			public override object this[object key]
			{
				[SecuritySafeCritical]
				get
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__Uri")
						{
							return this._mrmsg.Uri;
						}
						if (text == "__MethodName")
						{
							return this._mrmsg.MethodName;
						}
						if (text == "__MethodSignature")
						{
							return this._mrmsg.MethodSignature;
						}
						if (text == "__TypeName")
						{
							return this._mrmsg.TypeName;
						}
						if (text == "__Return")
						{
							return this._mrmsg.ReturnValue;
						}
						if (text == "__OutArgs")
						{
							return this._mrmsg.OutArgs;
						}
					}
					return this._idict[key];
				}
				[SecuritySafeCritical]
				set
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__MethodName" || text == "__MethodSignature" || text == "__TypeName" || text == "__Return" || text == "__OutArgs")
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
						}
						this._idict[key] = value;
					}
				}
			}

			// Token: 0x04003806 RID: 14342
			private IMethodReturnMessage _mrmsg;

			// Token: 0x04003807 RID: 14343
			private IDictionary _idict;
		}
	}
}

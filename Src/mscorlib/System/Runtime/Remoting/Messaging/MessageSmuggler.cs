using System;
using System.Collections;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000873 RID: 2163
	internal class MessageSmuggler
	{
		// Token: 0x06005C40 RID: 23616 RVA: 0x001446E1 File Offset: 0x001428E1
		private static bool CanSmuggleObjectDirectly(object obj)
		{
			return obj is string || obj.GetType() == typeof(void) || obj.GetType().IsPrimitive;
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x00144714 File Offset: 0x00142914
		[SecurityCritical]
		protected static object[] FixupArgs(object[] args, ref ArrayList argsToSerialize)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.FixupArg(args[i], ref argsToSerialize);
			}
			return array;
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x00144748 File Offset: 0x00142948
		[SecurityCritical]
		protected static object FixupArg(object arg, ref ArrayList argsToSerialize)
		{
			if (arg == null)
			{
				return null;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			int num;
			if (marshalByRefObject != null)
			{
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject) || RemotingServices.GetRealProxy(marshalByRefObject) is RemotingProxy)
				{
					ObjRef objRef = RemotingServices.MarshalInternal(marshalByRefObject, null, null);
					if (objRef.CanSmuggle())
					{
						if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
						{
							ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(marshalByRefObject);
							serverIdentity.SetHandle();
							objRef.SetServerIdentity(serverIdentity.GetHandle());
							objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
						}
						ObjRef objRef2 = objRef.CreateSmuggleableCopy();
						objRef2.SetMarshaledObject();
						return new SmuggledObjRef(objRef2);
					}
				}
				if (argsToSerialize == null)
				{
					argsToSerialize = new ArrayList();
				}
				num = argsToSerialize.Count;
				argsToSerialize.Add(arg);
				return new MessageSmuggler.SerializedArg(num);
			}
			if (MessageSmuggler.CanSmuggleObjectDirectly(arg))
			{
				return arg;
			}
			Array array = arg as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				if (elementType.IsPrimitive || elementType == typeof(string))
				{
					return array.Clone();
				}
			}
			if (argsToSerialize == null)
			{
				argsToSerialize = new ArrayList();
			}
			num = argsToSerialize.Count;
			argsToSerialize.Add(arg);
			return new MessageSmuggler.SerializedArg(num);
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x00144868 File Offset: 0x00142A68
		[SecurityCritical]
		protected static object[] UndoFixupArgs(object[] args, ArrayList deserializedArgs)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.UndoFixupArg(args[i], deserializedArgs);
			}
			return array;
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x0014489C File Offset: 0x00142A9C
		[SecurityCritical]
		protected static object UndoFixupArg(object arg, ArrayList deserializedArgs)
		{
			SmuggledObjRef smuggledObjRef = arg as SmuggledObjRef;
			if (smuggledObjRef != null)
			{
				return smuggledObjRef.ObjRef.GetRealObjectHelper();
			}
			MessageSmuggler.SerializedArg serializedArg = arg as MessageSmuggler.SerializedArg;
			if (serializedArg != null)
			{
				return deserializedArgs[serializedArg.Index];
			}
			return arg;
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x001448D8 File Offset: 0x00142AD8
		[SecurityCritical]
		protected static int StoreUserPropertiesForMethodMessage(IMethodMessage msg, ref ArrayList argsToSerialize)
		{
			IDictionary properties = msg.Properties;
			MessageDictionary messageDictionary = properties as MessageDictionary;
			if (messageDictionary == null)
			{
				int num = 0;
				foreach (object obj in properties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry);
					num++;
				}
				return num;
			}
			if (messageDictionary.HasUserData())
			{
				int num2 = 0;
				foreach (object obj2 in messageDictionary.InternalDictionary)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry2);
					num2++;
				}
				return num2;
			}
			return 0;
		}

		// Token: 0x02000C75 RID: 3189
		protected class SerializedArg
		{
			// Token: 0x060070D5 RID: 28885 RVA: 0x00186389 File Offset: 0x00184589
			public SerializedArg(int index)
			{
				this._index = index;
			}

			// Token: 0x17001355 RID: 4949
			// (get) Token: 0x060070D6 RID: 28886 RVA: 0x00186398 File Offset: 0x00184598
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x04003808 RID: 14344
			private int _index;
		}
	}
}

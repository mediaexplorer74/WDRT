using System;

namespace System.Windows.Forms
{
	// Token: 0x02000161 RID: 353
	internal class Command : WeakReference
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x0002BE19 File Offset: 0x0002A019
		public Command(ICommandExecutor target)
			: base(target, false)
		{
			Command.AssignID(this);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002BE29 File Offset: 0x0002A029
		public virtual int ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002BE34 File Offset: 0x0002A034
		protected static void AssignID(Command cmd)
		{
			object obj = Command.internalSyncObject;
			lock (obj)
			{
				int i;
				if (Command.cmds == null)
				{
					Command.cmds = new Command[20];
					i = 0;
				}
				else
				{
					int num = Command.cmds.Length;
					if (Command.icmdTry >= num)
					{
						Command.icmdTry = 0;
					}
					for (i = Command.icmdTry; i < num; i++)
					{
						if (Command.cmds[i] == null)
						{
							goto IL_102;
						}
					}
					for (i = 0; i < Command.icmdTry; i++)
					{
						if (Command.cmds[i] == null)
						{
							goto IL_102;
						}
					}
					for (i = 0; i < num; i++)
					{
						if (Command.cmds[i].Target == null)
						{
							goto IL_102;
						}
					}
					i = Command.cmds.Length;
					num = Math.Min(65280, 2 * i);
					if (num <= i)
					{
						GC.Collect();
						for (i = 0; i < num; i++)
						{
							if (Command.cmds[i] == null || Command.cmds[i].Target == null)
							{
								goto IL_102;
							}
						}
						throw new ArgumentException(SR.GetString("CommandIdNotAllocated"));
					}
					Command[] array = new Command[num];
					Array.Copy(Command.cmds, 0, array, 0, i);
					Command.cmds = array;
				}
				IL_102:
				cmd.id = i + 256;
				Command.cmds[i] = cmd;
				Command.icmdTry = i + 1;
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002BF88 File Offset: 0x0002A188
		public static bool DispatchID(int id)
		{
			Command commandFromID = Command.GetCommandFromID(id);
			return commandFromID != null && commandFromID.Invoke();
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
		protected static void Dispose(Command cmd)
		{
			object obj = Command.internalSyncObject;
			lock (obj)
			{
				if (cmd.id >= 256)
				{
					cmd.Target = null;
					if (Command.cmds[cmd.id - 256] == cmd)
					{
						Command.cmds[cmd.id - 256] = null;
					}
					cmd.id = 0;
				}
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002C024 File Offset: 0x0002A224
		public virtual void Dispose()
		{
			if (this.id >= 256)
			{
				Command.Dispose(this);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002C03C File Offset: 0x0002A23C
		public static Command GetCommandFromID(int id)
		{
			object obj = Command.internalSyncObject;
			Command command;
			lock (obj)
			{
				if (Command.cmds == null)
				{
					command = null;
				}
				else
				{
					int num = id - 256;
					if (num < 0 || num >= Command.cmds.Length)
					{
						command = null;
					}
					else
					{
						command = Command.cmds[num];
					}
				}
			}
			return command;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
		public virtual bool Invoke()
		{
			object target = this.Target;
			if (!(target is ICommandExecutor))
			{
				return false;
			}
			((ICommandExecutor)target).Execute();
			return true;
		}

		// Token: 0x040007E4 RID: 2020
		private static Command[] cmds;

		// Token: 0x040007E5 RID: 2021
		private static int icmdTry;

		// Token: 0x040007E6 RID: 2022
		private static object internalSyncObject = new object();

		// Token: 0x040007E7 RID: 2023
		private const int idMin = 256;

		// Token: 0x040007E8 RID: 2024
		private const int idLim = 65536;

		// Token: 0x040007E9 RID: 2025
		internal int id;
	}
}

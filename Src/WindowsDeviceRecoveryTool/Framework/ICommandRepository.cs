using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x020000E5 RID: 229
	public interface ICommandRepository : IDictionary<string, IDelegateCommand>, 
		ICollection<KeyValuePair<string, IDelegateCommand>>, 
		IEnumerable<KeyValuePair<string, IDelegateCommand>>, IEnumerable
	{
		//void Run(Action<Controllers.SettingsController> value);
        //delegate 
	    //void Run(Action<Controllers.SettingsController> value);
        void Run(Action<AppController> value);
    }
}

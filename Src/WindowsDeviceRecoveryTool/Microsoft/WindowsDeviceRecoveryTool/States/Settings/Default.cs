using System;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
    internal class Default
    {
        internal static string Theme;
        internal static string Style;
        internal static string PackagesPath;
        internal static bool CustomPackagesPathEnabled;
        internal static bool UseManualProxy;
        internal static string ProxyPassword;
        internal static string ProxyUsername;
        internal static int ProxyPort;
        internal static string ProxyAddress;
        internal static bool TraceEnabled;
        internal static string ZipFilePath;

        internal static void Save()
        {
            throw new NotImplementedException();
        }
    }
}
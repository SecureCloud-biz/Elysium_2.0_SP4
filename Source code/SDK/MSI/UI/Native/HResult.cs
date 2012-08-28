namespace Elysium.SDK.MSI.UI.Native
{
    internal static class HResult
    {
        public static bool Succeeded(int status)
        {
            return status >= 0;
        }
    }
}

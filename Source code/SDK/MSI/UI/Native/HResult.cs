namespace Elysium.SDK.MSI.UI.Native
{
    public static class HResult
    {
        public static bool Succeeded(int status)
        {
            return status >= 0;
        }
    }
} ;
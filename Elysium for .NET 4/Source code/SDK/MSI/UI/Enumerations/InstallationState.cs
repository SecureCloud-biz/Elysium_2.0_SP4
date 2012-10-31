namespace Elysium.SDK.MSI.UI.Enumerations
{
    internal enum InstallationState
    {
        Initializing,
        Layout,
        DetectedAbsent,
        DetectedPresent,
        DetectedNewer,
        Applying,
        Successful,
        Failed,
        Help,
        RebootRequired
    }
}

namespace Elysium.SDK.MSI.UI.Enumerations
{
    internal enum InstallationState
    {
        Initializing,
        DetectedAbsent,
        DetectedPresent,
        DetectedNewer,
        Applying,
        Successful,
        Failed,
        RebootRequired
    }
}

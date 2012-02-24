using JetBrains.Annotations;

namespace Elysium.Platform.Permissions
{
    internal enum PermissionState
    {
        [PublicAPI]
        Deny,

        [PublicAPI]
        Allow
    }
} ;
using System;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [Flags]
    public enum Animation
    {
        None,
        Custom,
        Fade,
        Flip,
        Invitation,
        Reorder,
        Scale,
        Slide,
        Stack,
        Switch,
        Unzoom,
        Update,
        Zoom
    }
}
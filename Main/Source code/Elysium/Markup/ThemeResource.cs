using JetBrains.Annotations;

namespace Elysium.Markup
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public enum ThemeResource
    {
        TransparentColor,
        TransparentBrush,
        SemitransparentColor,
        SemitransparentBrush,

        AccentColor,
        AccentBrush,
        ContrastColor,
        ContrastBrush,
        SemitransparentContrastColor,
        SemitransparentContrastBrush,

        BackgroundColor,
        BackgroundBrush,
        BackgroundContrastColor,
        BackgroundContrastBrush,
        SemitransparentBackgroundContrastColor,
        SemitransparentBackgroundContrastBrush,

        ForegroundColor,
        ForegroundBrush,
        ForegroundContrastColor,
        ForegroundContrastBrush,
        SemitransparentForegroundContrastColor,
        SemitransparentForegroundContrastBrush,

        HighlightColor,
        HighlightBrush,
        NormalColor,
        NormalBrush,
        PaleColor,
        PaleBrush,

        ValidationColor,
        ValidationBrush,
        DisabledColor,
        DisabledBrush,

        DefaultDuration,
        MinimumDuration,
        OptimumDuration,
        MaximumDuration
    }
}
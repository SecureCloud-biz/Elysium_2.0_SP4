using System;
using System.Diagnostics.Contracts;
using System.Windows.Media;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public static class AccentBrushes
    {
        [PublicAPI]
        public static SolidColorBrush Amber
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _amber ?? (_amber = new SolidColorBrush(AccentColors.Amber).AsFrozen());
            }
        }

        private static SolidColorBrush _amber;

        [PublicAPI]
        public static SolidColorBrush Blue
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _blue ?? (_blue = new SolidColorBrush(AccentColors.Blue).AsFrozen());
            }
        }

        private static SolidColorBrush _blue;

        [PublicAPI]
        public static SolidColorBrush Brown
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _brown ?? (_brown = new SolidColorBrush(AccentColors.Brown).AsFrozen());
            }
        }

        private static SolidColorBrush _brown;

        [PublicAPI]
        public static SolidColorBrush Citron
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _citron ?? (_citron = new SolidColorBrush(AccentColors.Citron).AsFrozen());
            }
        }

        private static SolidColorBrush _citron;

        [PublicAPI]
        public static SolidColorBrush Cobalt
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _cobalt ?? (_cobalt = new SolidColorBrush(AccentColors.Cobalt).AsFrozen());
            }
        }

        private static SolidColorBrush _cobalt;

        [PublicAPI]
        public static SolidColorBrush Crimson
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _crimson ?? (_crimson = new SolidColorBrush(AccentColors.Crimson).AsFrozen());
            }
        }

        private static SolidColorBrush _crimson;

        [PublicAPI]
        public static SolidColorBrush Emerald
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _emerald ?? (_emerald = new SolidColorBrush(AccentColors.Emerald).AsFrozen());
            }
        }

        private static SolidColorBrush _emerald;

        [PublicAPI]
        public static SolidColorBrush Green
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _green ?? (_green = new SolidColorBrush(AccentColors.Green).AsFrozen());
            }
        }

        private static SolidColorBrush _green;

        [PublicAPI]
        public static SolidColorBrush Indigo
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _indigo ?? (_indigo = new SolidColorBrush(AccentColors.Indigo).AsFrozen());
            }
        }

        private static SolidColorBrush _indigo;

        [PublicAPI]
        public static SolidColorBrush Jade
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _jade ?? (_jade = new SolidColorBrush(AccentColors.Jade).AsFrozen());
            }
        }

        private static SolidColorBrush _jade;

        [PublicAPI]
        public static SolidColorBrush Lime
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _lime ?? (_lime = new SolidColorBrush(AccentColors.Lime).AsFrozen());
            }
        }

        private static SolidColorBrush _lime;

        [PublicAPI]
        public static SolidColorBrush Magenta
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _magenta ?? (_magenta = new SolidColorBrush(AccentColors.Magenta).AsFrozen());
            }
        }

        private static SolidColorBrush _magenta;

        [PublicAPI]
        public static SolidColorBrush Mango
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _mango ?? (_mango = new SolidColorBrush(AccentColors.Mango).AsFrozen());
            }
        }

        private static SolidColorBrush _mango;

        [PublicAPI]
        public static SolidColorBrush Mauve
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _mauve ?? (_mauve = new SolidColorBrush(AccentColors.Mauve).AsFrozen());
            }
        }

        private static SolidColorBrush _mauve;

        [PublicAPI]
        public static SolidColorBrush Olive
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _olive ?? (_olive = new SolidColorBrush(AccentColors.Olive).AsFrozen());
            }
        }

        private static SolidColorBrush _olive;

        [PublicAPI]
        public static SolidColorBrush Orange
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _orange ?? (_orange = new SolidColorBrush(AccentColors.Orange).AsFrozen());
            }
        }

        private static SolidColorBrush _orange;

        [PublicAPI]
        public static SolidColorBrush Pink
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _pink ?? (_pink = new SolidColorBrush(AccentColors.Pink).AsFrozen());
            }
        }

        private static SolidColorBrush _pink;

        [PublicAPI]
        public static SolidColorBrush Purple
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _purple ?? (_purple = new SolidColorBrush(AccentColors.Purple).AsFrozen());
            }
        }

        private static SolidColorBrush _purple;

        [PublicAPI]
        public static SolidColorBrush Red
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _red ?? (_red = new SolidColorBrush(AccentColors.Red).AsFrozen());
            }
        }

        private static SolidColorBrush _red;

        [PublicAPI]
        public static SolidColorBrush Rose
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _rose ?? (_rose = new SolidColorBrush(AccentColors.Rose).AsFrozen());
            }
        }

        private static SolidColorBrush _rose;

        [PublicAPI]
        public static SolidColorBrush Rust
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _rust ?? (_rust = new SolidColorBrush(AccentColors.Rust).AsFrozen());
            }
        }

        private static SolidColorBrush _rust;

        [PublicAPI]
        public static SolidColorBrush Sienna
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _sienna ?? (_sienna = new SolidColorBrush(AccentColors.Sienna).AsFrozen());
            }
        }

        private static SolidColorBrush _sienna;

        [PublicAPI]
        public static SolidColorBrush Sky
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _sky ?? (_sky = new SolidColorBrush(AccentColors.Sky).AsFrozen());
            }
        }

        private static SolidColorBrush _sky;

        [PublicAPI]
        public static SolidColorBrush Steel
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _steel ?? (_steel = new SolidColorBrush(AccentColors.Steel).AsFrozen());
            }
        }

        private static SolidColorBrush _steel;

        [PublicAPI]
        public static SolidColorBrush Teal
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _teal ?? (_teal = new SolidColorBrush(AccentColors.Teal).AsFrozen());
            }
        }

        private static SolidColorBrush _teal;

        [PublicAPI]
        [Obsolete("Use Teal brush and color instead of Viridian.")]
        public static SolidColorBrush Viridian
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _teal ?? (_teal = new SolidColorBrush(AccentColors.Teal).AsFrozen());
            }
        }

        [PublicAPI]
        public static SolidColorBrush Violet
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _violet ?? (_violet = new SolidColorBrush(AccentColors.Violet).AsFrozen());
            }
        }

        private static SolidColorBrush _violet;

        [PublicAPI]
        public static SolidColorBrush Walnut
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _walnut ?? (_walnut = new SolidColorBrush(AccentColors.Walnut).AsFrozen());
            }
        }

        private static SolidColorBrush _walnut;

        [PublicAPI]
        public static SolidColorBrush Yellow
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                return _yellow ?? (_yellow = new SolidColorBrush(AccentColors.Yellow).AsFrozen());
            }
        }

        private static SolidColorBrush _yellow;
    }
}

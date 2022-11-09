using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MGPJC
{
    /// <summary>
    /// Input class for key properties
    /// revoked in future, into a full class that takes care of all input instead of spread out in other classes
    /// </summary>
    public class Input
    {
        public Keys Up { get; set; }

        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Keys Shoot { get; set; }
    }
}

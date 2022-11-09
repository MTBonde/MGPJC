using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    /// <summary>
    /// allows classes using the interface to have a reaction to collision
    /// </summary>
    internal interface ICanCollide
    {
        void OnCollision(GameObject gameObject);
    }
}

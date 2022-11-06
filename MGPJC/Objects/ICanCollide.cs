using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    internal interface ICanCollide
    {
        void OnCollision(GameObject gameObject);
    }
}

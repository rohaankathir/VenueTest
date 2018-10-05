using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Framework.Contracts
{
    public interface IBootStrap : IDependency
    {
        int Rank { get; }
        void SetUp();
    }
}

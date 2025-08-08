using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradientspace.NodeGraph
{
    public interface ICodeGen
    {
        void GetCodeOutputNames(out string[]? OutputNames);
        string GenerateCode(string[]? Arguments, string[]? UseOutputNames);
    }

}

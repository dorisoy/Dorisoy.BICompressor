using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorisoy.BICompressor
{
    public class TaskFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
    }
}

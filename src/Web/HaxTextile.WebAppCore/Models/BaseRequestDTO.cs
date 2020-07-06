using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaxTextile.WebAppCore.Models
{
    public class BaseRequestDTO
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}

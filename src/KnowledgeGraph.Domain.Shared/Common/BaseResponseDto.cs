using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeGraph.Common
{
    public class BaseResponseDto<T>
    {
        public bool? success { get; set; } = true;
        public string? message { get; set; } = string.Empty;
        public T? data { get; set; }
    }
}

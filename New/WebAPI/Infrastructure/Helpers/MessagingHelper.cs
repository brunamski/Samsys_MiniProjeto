using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure.Helpers
{
    public class MessagingHelper<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Obj { get; set; }
    }
}

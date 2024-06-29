using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public class Return
    {
        public Return(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
    }
}

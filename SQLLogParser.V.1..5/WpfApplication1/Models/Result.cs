using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    class Result
    {
        public string ResultString { get; set; }

        public Result(string resStr)
        {
            ResultString = resStr;
        }
    }
}

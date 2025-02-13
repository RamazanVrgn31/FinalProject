using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        //this ile success'i kod tekrarı yapmamak için tek parametreli olan constructorda set ettirdik.
        public Result(bool success, string message) : this(success)
        {
            Message=message;
            
        }

        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}

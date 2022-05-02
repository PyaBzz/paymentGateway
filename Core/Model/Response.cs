using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum Status { Invalid, Success, Declined }
    public class Response
    {//doc: This is a simple Dto with built-in factory method
        public Status Status { get; private set; }
        public int Id { get; private set; }

        public Response(IRequestable req)
        {
            this.Id = req.Id;
            if (req.IsValid == false)
                this.Status = Status.Invalid;
            else
            {
                if (req.IsSuccess == null)
                    throw new ArgumentException("Unprocessed request!");
                this.Status = req.IsSuccess.Value ? Status.Success : Status.Declined;
            }
        }
    }
}
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum Status { Invalid, Success, Declined }
    public class Response
    {//doc: This is a simple Dto with built-in factory method
        private Response() { }
        public Status Status { get; private set; }
        public int Id { get; private set; }

        public static Response Create(IRequestable req)
        {
            var instance = new Response();
            instance.Id = req.Id;
            if (req.IsValid == false)
                instance.Status = Status.Invalid;
            else
            {
                if (req.IsSuccess == null)
                    throw new ArgumentException("Unprocessed request!");
                instance.Status = req.IsSuccess.Value ? Status.Success : Status.Declined;
            }
            return instance;
        }
    }
}
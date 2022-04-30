using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum QueryStatus { Denied, Resolved }

    public class Response
    {
        public IOrder Order { get; private set; }
        public QueryStatus Status { get; private set; }
        private Response() { }

        public static Response Create(IOrder order, QueryStatus status)
        {
            var instance = new Response();
            instance.Order = order.ObscureClone();
            instance.Status = status;
            return instance;
        }
    }
}
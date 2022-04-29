using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{

    public enum QueryStatus { Denied, Resolved }

    // public interface IResponseFactory
    // {
    //     Response Create(Order order, OrderStatus status);
    // }

    public class Response
    {
        public Order Order { get; }
        public QueryStatus Status { get; }
        public Response(Order order, QueryStatus status)
        {
            Order = order.ObscureClone();
            Status = status;
        }
        // public static Response Create(Order order, QueryStatus status)
        // {
        //     Order = order.ObscureClone();
        //     Status = status;
        // }
    }
}
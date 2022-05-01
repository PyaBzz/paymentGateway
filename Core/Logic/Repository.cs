using System;

namespace Core
{
    public interface IRepository
    {
        int Save(Request item);
        Request Get(int id);
    }

    public class RequestRepoFake : IRepository
    {
        public int Save(Request item)
        {
            throw new NotImplementedException();
        }
        public Request Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
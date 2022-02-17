using System;

namespace UrlPinger.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException()
        {

        }

        public NotFoundException(string message) : base(message)
        {
        }
    }

}

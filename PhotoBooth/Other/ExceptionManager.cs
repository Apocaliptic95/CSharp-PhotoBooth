using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.Other
{
    public class ExceptionManager
    {
        private Queue<Exception> _exceptions = new Queue<Exception>();

        public void registerException(Exception e)
        {
            _exceptions.Enqueue(e);
        }

        public Queue<Exception> getExceptionQueue()
        {
            return _exceptions;
        }

        public Exception getException()
        {
            if (_exceptions.Count == 0)
                return null;
            else
                return _exceptions.Dequeue();
        }

        public int getExceptionsCount()
        {
            return _exceptions.Count;
        }

        public void clear()
        {
            _exceptions.Clear();
        }
    }
}

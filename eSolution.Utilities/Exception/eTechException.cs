using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Utilities.Exceptions
{
    public class eTechException : Exception
    {
        public eTechException()
        {

        }
        public eTechException(string message) : base(message)
        {

        }

        public eTechException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
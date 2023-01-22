using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardUtilities
{
    public sealed class UtilityForExceptions
    {
        private static UtilityForExceptions obj = null;

        public static UtilityForExceptions GetInstance()
        {
            if (obj == null)
            {
                obj = new UtilityForExceptions();

            }

            return obj;
        }
        public void LogExceptions(string msg, string stackTrace)
        {
            // Coding
        }
    }
}

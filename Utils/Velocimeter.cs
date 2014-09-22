using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Velocimeter
    {
        DateTime initialTime;
        double numberOfMessages;

        public Velocimeter()
        {
            initialTime = DateTime.Now;
            numberOfMessages = 0;
        }

        public void IncrementMessages()
        {
            numberOfMessages++;

        }

        public double GetSpeed()
        {

            return numberOfMessages / (DateTime.Now - initialTime).TotalSeconds;

        }

    }
}

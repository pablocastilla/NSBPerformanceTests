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
        double numberOfMessagesProcessed;
        double numberOfTotalMessagesToProcess;

        private static Velocimeter instance;


        /// <summary>
        /// for senders
        /// </summary>
        /// <param name="totalMessages"></param>
        /// <returns></returns>
        public static Velocimeter getInstance(int totalMessages)
        {
            if (instance == null)
                instance = new Velocimeter(totalMessages);

            return instance;
        }

        /// <summary>
        /// For clients
        /// </summary>
        /// <returns></returns>
        public static Velocimeter getInstance()
        {
            if (instance == null)
                instance = new Velocimeter();

            return instance;
        }

        private Velocimeter()
        {
            initialTime = DateTime.Now;
            numberOfMessagesProcessed = 0;
           
        }

        private Velocimeter(int totalMessages)
        {
            initialTime = DateTime.Now;
            numberOfMessagesProcessed = 0;
            numberOfTotalMessagesToProcess = totalMessages;
        }

        public void IncrementMessages()
        {
            numberOfMessagesProcessed++;

        }

        public double GetSpeed()
        {

            return numberOfMessagesProcessed / (DateTime.Now - initialTime).TotalSeconds;

        }

        public bool IsFinished()
        {
            return numberOfMessagesProcessed == numberOfTotalMessagesToProcess;
        
        }

        public double TotalTime()
        {

            return (DateTime.Now - initialTime).TotalSeconds;

        }

    }
}

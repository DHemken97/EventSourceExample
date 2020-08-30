using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebGrease.Css.Extensions;

namespace EventSourceTesting
{
    public static class MQ_Broker
    {
        public static Dictionary<string, List<string>> messages;

        private static Timer _timer;

        public static List<string> GetMessages(Guid MyId)
        {
            if (messages == null)
            {
                start();
                return new List<string>();
            }

            if (!messages.ContainsKey(MyId.ToString()))
            {
                messages.Add(MyId.ToString(),new List<string>());
                return new List<string>();
            }

            var msg = messages[MyId.ToString()];
            messages[MyId.ToString()] = new List<string>();
            return msg;
        }
        public static void start()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 1000;
                _timer.Tick += TimerOnTick;
                messages = new Dictionary<string, List<string>>();

            }
            if (!_timer.Enabled)
            {
                _timer.Enabled = true;
            }
        }

        public static void remove(Guid MyId)
        {
            messages.Remove(MyId.ToString());
        }

        public static void TimerOnTick(object sender, EventArgs e)
        {
            try
            {
                messages?.Keys.ForEach(client => messages[client].Add(messages.Keys.Count.ToString()+" Clients\r\n"));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
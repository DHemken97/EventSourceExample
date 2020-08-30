using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventSourceTesting
{
    public partial class EventSource : System.Web.UI.Page
    {
        private Guid myId = Guid.NewGuid();
        protected void Page_Load(object sender, EventArgs e)
        {
          //  EventMessages = new List<string>();
          //  MQ_Broker.Register((message) => EventMessages.Add(message));
            Message();

        }

        public void Message()
        {
            Response.ContentType = "text/event-stream";

            DateTime startDate = DateTime.Now;
            while (startDate.AddMinutes(1) > DateTime.Now)
            {
                MQ_Broker.TimerOnTick($"{myId}\n\n", null);
               MQ_Broker.GetMessages(myId).ForEach(Response.Write);
               // Response.Write($"data: {DateTime.Now.ToString()}\n\n");
                Response.Flush();

                System.Threading.Thread.Sleep(1000);
            }
            Response.Close();

        }

        protected void OnUnload(object sender, EventArgs e)
        {
            MQ_Broker.remove(myId);

        }
    }
}
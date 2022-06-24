using Newtonsoft.Json;
using Publish.Base;
using Publish.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publish.Entities
{
    [Serializable]
    public class MessageDataEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }


        public void PublishMess()
        {
            try
            {
                var mess = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
                RabbitProcess.ObjSender.Send(mess, "MessageDataEntity");
            }
            catch (Exception ex)
            {

                FileHelper.WriteLog("PublishMess", ex);
            }
        }

    }
}

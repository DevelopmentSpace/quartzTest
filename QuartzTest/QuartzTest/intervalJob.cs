using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTest
{
    [PersistJobDataAfterExecutionAttribute()]
    [DisallowConcurrentExecution()]
    class intervalJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            int index = context.Trigger.JobDataMap.GetIntValue("index");
            int newIndex = context.Trigger.JobDataMap.GetIntValue("newIndex");
            bool upd = context.Trigger.JobDataMap.GetBooleanValue("updDisp");

            if (upd)
            {
                context.Trigger.JobDataMap.Put("index", newIndex);
                context.Trigger.JobDataMap.Put("updDone",true);
            }
            else
            {
                context.Trigger.JobDataMap.Put("index", index+1);
            }
        }
    }
}

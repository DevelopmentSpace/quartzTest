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
    class AnotherIntervalJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            int newIndex = (int)context.Trigger.JobDataMap.Get("newIndex");

            context.Trigger.JobDataMap.Put("newIndex", newIndex*100);
            context.Trigger.JobDataMap.Put("updDisp",true);
        }
    }
}

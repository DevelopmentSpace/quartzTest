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
            string attr = (string) context.JobDetail.JobDataMap.Get("attr");

            attr = attr + "A";
            context.JobDetail.JobDataMap.Add(new KeyValuePair<string, object>("attr", attr));
            Console.WriteLine(attr);
        }
    }
}

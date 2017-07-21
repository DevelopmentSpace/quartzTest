using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTest
{
    class Program
    {
        static Service s;

        static void Main(string[] args)
        {
            s= new Service();
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            throw new NotImplementedException();
        }
    }

}

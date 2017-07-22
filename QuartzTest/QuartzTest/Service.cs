using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTest
{

    class Service : IJobListener
    {

        IScheduler scheduler;
        JobKey intervalJobKey, anotherIntervalJobKey;
        int index;
        bool updDisp;
        bool updDone;
        int newIndex;


        public Service()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();


            index = 1;
            newIndex = 1;
            updDisp = false;
            updDone = false;

            intervalJobKey = new JobKey("iJK");
            anotherIntervalJobKey = new JobKey("aIJK");


            

            scheduler.Start();


            scheduler.ListenerManager.AddJobListener(this, OrMatcher<JobKey>.Or(KeyMatcher<JobKey>.KeyEquals<JobKey>(intervalJobKey), KeyMatcher<JobKey>.KeyEquals<JobKey>(anotherIntervalJobKey)));

            startIntervalJob(1);
            startAnotherIntervalJob(5);



            }


        private void startIntervalJob(int seconds)
        {
            IJobDetail intervalJob = JobBuilder.Create<intervalJob>()
            .WithIdentity(intervalJobKey)
            .Build();

            ITrigger intervalJobTrigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now.AddSeconds(seconds))
                .WithPriority(1)
                .UsingJobData("index", index)
                .UsingJobData("newIndex", newIndex)
                .UsingJobData("updDisp", updDisp)
                .Build();

            scheduler.DeleteJob(intervalJobKey);
            scheduler.ScheduleJob(intervalJob, intervalJobTrigger);


        }

        private void startAnotherIntervalJob(int seconds)
        {
            
            IJobDetail anotherIntervalJob = JobBuilder.Create<AnotherIntervalJob>()
               .WithIdentity(anotherIntervalJobKey)
               .Build();

            ITrigger anotherIntervalJobTrigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now.AddSeconds(seconds))
                .UsingJobData("newIndex", newIndex)
                .WithPriority(1)
                .Build();


            scheduler.DeleteJob(anotherIntervalJobKey);
            scheduler.ScheduleJob(anotherIntervalJob, anotherIntervalJobTrigger);

        }








        public string Name => "Service";

        public void JobExecutionVetoed(IJobExecutionContext context)
        {

        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {

        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {

            if (context.JobDetail.Key == intervalJobKey)
            {
                index = context.Trigger.JobDataMap.GetIntValue("index");
                updDone = context.Trigger.JobDataMap.GetBooleanValue("updDone");
                if (updDone)
                {
                    updDisp = false;
                    updDisp = false;
                }

                Console.WriteLine(index);
                this.startIntervalJob(1);
            }
            else if(context.JobDetail.Key == anotherIntervalJobKey)
            {
                newIndex = context.Trigger.JobDataMap.GetIntValue("newIndex");
                updDisp = context.Trigger.JobDataMap.GetBooleanValue("updDisp");
                this.startAnotherIntervalJob(5);
            }


        }
    }
}

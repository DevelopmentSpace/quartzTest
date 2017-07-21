using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
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
        JobKey intervalJobKey, AnotherintervalJobKey;
        string str;

        public Service()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();

            str = "";

            IJobDetail intervalJob = JobBuilder.Create<intervalJob>()
                .UsingJobData("attr", str)
                .Build();
            intervalJobKey = intervalJob.Key;


            ITrigger intervalJobTrigger = TriggerBuilder.Create()
                .WithIdentity("intervalJob", "Refresh")
                .StartNow()
                .WithSimpleSchedule(s => s
                    .WithIntervalInSeconds(1)
                    .RepeatForever())
                //.WithPriority(2)
                .Build();





            IJobDetail anotherIntervalJob = JobBuilder.Create<AnotherIntervalJob>()
                .UsingJobData("attr", str)
                .Build();
            AnotherintervalJobKey = anotherIntervalJob.Key;

            ITrigger anotherIntervalJobTrigger = TriggerBuilder.Create()
                .WithIdentity("AnotherintervalJob", "Refresh")
                .StartNow()
                .WithSimpleSchedule(s => s
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .WithPriority(1)
                .Build();




            scheduler.ScheduleJob(anotherIntervalJob, anotherIntervalJobTrigger);

            scheduler.ListenerManager.AddJobListener(this, KeyMatcher<JobKey>.KeyEquals(intervalJobKey));
            scheduler.ListenerManager.AddJobListener(this, KeyMatcher<JobKey>.KeyEquals(AnotherintervalJobKey));


            scheduler.Start();
            }

        public string Name => "service";

        public void JobExecutionVetoed(IJobExecutionContext context)
        {

        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {

        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            Console.WriteLine("->" + context.JobDetail.JobDataMap.Get("attr"));
        }
    }
}

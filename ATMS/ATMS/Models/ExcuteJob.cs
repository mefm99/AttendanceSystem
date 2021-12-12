using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMS_TestingSubject.Models
{
    public class ExcuteJob
    {

        public static void Start()
        {

            Random rand = new Random();
            int n = rand.Next(2,7);
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.Start();

            IJobDetail job = JobBuilder.Create<ScheduleJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow().WithDailyTimeIntervalSchedule(x => x.OnEveryDay().WithIntervalInSeconds(n))
                .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}
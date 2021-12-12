using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using ATMS_TestingSubject.Controllers;

namespace ATMS_TestingSubject.Models
{
    public class ScheduleJob : IJob
    {
        public static int n = 0;

       Task IJob.Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    n++;  
                }
                catch (Exception ex)
                {

                }
            }
            );

            return task;
        }
    }
}
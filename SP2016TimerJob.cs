using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
namespace SPTrainingTimerJob
{
    class SP2016TimerJob:SPJobDefinition
    {
        public SP2016TimerJob():base()
        {

        }

        public SP2016TimerJob(string jobName, SPService service) : base(jobName,service,null,SPJobLockType.None)
        {

        }

        public SP2016TimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {

        }

        public override void Execute(Guid targetInstanceId)
        {
            //base.Execute(targetInstanceId);
            string title = string.Empty;
            string description = string.Empty;
            string url = string.Empty;
            string template = string.Empty;

            using (SPSite site = new SPSite("http://mypc:29024/sites/HydTraining/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["SiteCreationRequests"];
                    SPListItemCollection items = list.Items;

                    foreach (SPListItem item in items)
                    {
                        title = item["Title"].ToString();
                        description = item["SiteDescription"].ToString();
                        url = item["SiteURL"].ToString();
                        template = item["SiteTemplate"].ToString();

                        try
                        {
                            using (SPSite mysite = new SPSite("http://mypc:29024/sites/HydTraining/"))
                            {
                                using (SPWeb myWeb = site.OpenWeb())
                                {
                                    myWeb.Webs.Add(url, title, description, 1033, template, false, false);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }

            }





        }
    }
}

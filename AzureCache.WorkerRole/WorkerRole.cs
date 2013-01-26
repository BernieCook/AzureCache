using Microsoft.ApplicationServer.Caching;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace AzureCache.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            DataCache dataCache = null;
            var roleInstanceId = RoleEnvironment.CurrentRoleInstance.Id;

            while (true)
            {
                if (dataCache == null)
                {
                    try
                    {
                        dataCache = new DataCache("default");
                    }
                    catch (DataCacheException)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                }

                var value = dataCache.Get("Tim");
                if (value != null)
                {
                    EventLog.WriteEntry(
                        string.Concat("AzureCache ", roleInstanceId), 
                        string.Concat("Tim = ", (string)value));

                    // Uncomment for Azure testing. 
                    // dataCache.Put("Tim", string.Concat((string)value, RoleEnvironment.CurrentRoleInstance.Id));
                }

                Thread.Sleep(2000);
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            return base.OnStart();
        }
    }
}

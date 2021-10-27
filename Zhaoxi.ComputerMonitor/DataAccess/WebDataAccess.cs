using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.ComputerMonitor.DataAccess.Entity;

namespace Zhaoxi.ComputerMonitor.DataAccess
{
    public class WebDataAccess
    {
        WebClient webClient;
        HttpWebRequest request;

        public List<CoursesEntity> GetCourses()
        {
            try
            {
                if (webClient == null)
                    webClient = new WebClient();

                string josnStr = webClient.DownloadString("http://www.zhaoxiedu.net/static/json/course.json");

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                return serializer.Deserialize<List<CoursesEntity>>(josnStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            webClient?.Dispose();
        }
    }
}

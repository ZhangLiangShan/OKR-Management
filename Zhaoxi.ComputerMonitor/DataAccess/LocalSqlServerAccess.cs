using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.ComputerMonitor.Common;
using Zhaoxi.ComputerMonitor.DataAccess.Entity;
using Zhaoxi.ComputerMonitor.Model;

namespace Zhaoxi.ComputerMonitor.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;
        private LocalDataAccess() { }
        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        public SqlConnection Conn { get; set; }
        public SqlCommand Comm { get; set; }
        public SqlDataAdapter Adap { get; set; }

        private void Dispose()
        {
            if (Adap != null)
            {
                Adap.Dispose();
                Adap = null;
            }
            if (Comm != null)
            {
                Comm.Dispose();
                Comm = null;
            }
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

        private bool DBConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["demoDB"].ConnectionString;
            if (Conn == null)
                Conn = new SqlConnection(connStr);
            try
            {
                Conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserModel CheckUserInfo(string userName, string pwd)
        {
            try
            {
                if (DBConnection())
                {
                    string userSql = "select * from users where user_name=@user_name and password=@password and is_validation=1";
                    Adap = new SqlDataAdapter(userSql, Conn);
                    Adap.SelectCommand.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar) { Value = userName });
                    Adap.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName.ToLower()) });
                    DataTable dataTable = new DataTable();
                    int count = Adap.Fill(dataTable);
                    //Comm = new SqlCommand(userSql, Conn);
                    //Comm.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar) { Value = userName });
                    //Comm.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName.ToLower()) });
                    //var result = Comm.ExecuteScalar();

                    DataRow row = dataTable.Rows[0];

                    if (count <= 0)
                        throw new Exception("用户名或密码不正确！");

                    if (row.Field<Int32>("is_can_login") == 0)
                        throw new Exception("当前用户无权限使用此平台！");

                    UserModel model = new UserModel();
                    model.UserName = row.Field<string>("user_name");
                    model.RealName = row.Field<string>("real_name");
                    model.Password = row.Field<string>("password");
                    model.IsCanLogin = row.Field<Int32>("is_can_login") == 1;
                    model.Avatar = row.Field<string>("avatar");
                    model.Gender = row.Field<Int32>("gender");

                    return model;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }

        public List<CourseSeriesModel> GetCoursePlayRecord()
        {
            try
            {
                if (DBConnection())
                {
                    string courseSql = @"select
                                            a.course_id,a.course_name,b.play_count,b.is_growing,c.platform_name,b.growing_rate
                                        from courses a
                                        left join play_record b
                                            on a.course_id = b.course_id
                                        left join platforms c
                                            on b.platform_id = c.platform_id
                                        order by a.course_id,c.platform_id";
                    Adap = new SqlDataAdapter(courseSql, Conn);
                    DataTable dataTable = new DataTable();
                    Adap.Fill(dataTable);

                    string courseId = "";
                    CourseSeriesModel cmodel = null;
                    List<CourseSeriesModel> cmodelList = new List<CourseSeriesModel>();
                    foreach (DataRow dr in dataTable.AsEnumerable())
                    {
                        string tempId = dr.Field<string>("course_id");
                        if (courseId != tempId)
                        {
                            cmodel = new CourseSeriesModel();
                            courseId = tempId;
                            cmodel.CourseName = dr.Field<string>("course_name");
                            cmodel.SeriesCollection = new LiveCharts.SeriesCollection();
                            cmodel.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();
                            cmodelList.Add(cmodel);
                        }

                        if (cmodel != null)
                        {
                            cmodel.SeriesList.Add(new SeriesModel
                            {
                                SeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<decimal>("play_count"),
                                IsGrowing = dr.Field<Int32>("is_growing") == 1,
                                GrowingRate = (int)dr.Field<decimal>("growing_rate")
                            });

                            cmodel.SeriesCollection.Add(new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
                                DataLabels = false,
                            });
                        }
                    }

                    return cmodelList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }

        public List<string> GetTeachers()
        {
            try
            {
                if (DBConnection())
                {
                    string courseSql = @"select * from users where is_teacher=1 and is_validation=1";
                    Adap = new SqlDataAdapter(courseSql, Conn);
                    DataTable dataTable = new DataTable();
                    Adap.Fill(dataTable);

                    List<string> trachers = new List<string>();
                    foreach (DataRow dr in dataTable.AsEnumerable())
                    {
                        trachers.Add(dr.Field<string>("real_name"));
                    }

                    return trachers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }
        public List<CourseModel> GetCourses()
        {
            try
            {
                if (DBConnection())
                {
                    string courseSql = @"select a.course_id,a.course_name,a.course_cover,a.course_url,a.description,c.real_name
                                        from courses a
                                        full join course_teacher_relation b
                                            on a.course_id=b.course_id
                                        left join users c
                                            on b.teacher_id=c.user_id
                                        order by a.course_id";
                    Adap = new SqlDataAdapter(courseSql, Conn);
                    DataTable dataTable = new DataTable();
                    Adap.Fill(dataTable);

                    string courseId = "";
                    CourseModel cmodel = null;
                    List<CourseModel> cmodelList = new List<CourseModel>();
                    foreach (DataRow dr in dataTable.AsEnumerable())
                    {
                        string tempId = dr.Field<string>("course_id");
                        if (courseId != tempId)
                        {
                            cmodel = new CourseModel();
                            courseId = tempId;

                            cmodel.CourseName = dr.Field<string>("course_name");
                            cmodel.CoverImg = dr.Field<string>("course_cover");
                            cmodel.Url = dr.Field<string>("course_url");
                            cmodel.Description = dr.Field<string>("description");
                            cmodel.Teachers = new List<string>();
                            cmodelList.Add(cmodel);
                        }

                        if (cmodel != null)
                        {
                            cmodel.Teachers.Add(dr.Field<string>("real_name"));
                        }
                    }

                    return cmodelList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }
    }
}

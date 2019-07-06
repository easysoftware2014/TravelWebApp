using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using FluentNHibernate.Testing.Values;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Service.Services;

namespace TravelWebApp
{
    public class NotificationComponent
    {
        private readonly UserService _userService;
        public NotificationComponent()
        {
            _userService = new UserService();
        }
        public void RegisterNotification(DateTime currentTime)
        {
            var connString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            var sqlCommand = @"SELECT [],[],[],[],[] FROM [dbo].User where [created_at] > @created_at";

            using (var con = new SqlConnection(connString))
            {
                    var cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@created_at", currentTime);

                    if(con.State == ConnectionState.Open)
                        con.Open();

                    cmd.Notification = null;

                    var sqlDependency = new SqlDependency(cmd);
                    sqlDependency.OnChange += SqlDependencyOnOnChange;

                    using (var reader = cmd.ExecuteReader())
                    {
                            
                    }

            }
        }

        private void SqlDependencyOnOnChange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                var sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= SqlDependencyOnOnChange;

                var travelHub = GlobalHost.ConnectionManager.GetHubContext<TravelHub>();
                travelHub.Clients.All.announce("Added");

                RegisterNotification(DateTime.Now);
            }
        }

        public List<User> GetUserList(DateTime afterDate)
        {
            return _userService.GetAll().Where(x => x.CreatedAt > afterDate).OrderByDescending(x => x.CreatedAt)
                                        .ToList();
        }
    }
}
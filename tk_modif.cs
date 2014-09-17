using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

using tk4000.log;

namespace tk4000.modif
{
    public class TModifConnection
    {
        public TCustomLog Log;

        public string sServer { get; set; }
        public string sDatabase { get; set; }
        public string sLogin { get; set; }
        public string sPassword { get; set; }

        public Server msSRV;
        public Database msDB;
        public ServerConnection msConn;

        public void Connect()
        {
            if (msSRV!=null) return;
            Log.StartLine("Подключение к MSSQL SMO ...");
            try
            {
                // For remote connection, remote server name / ServerInstance needs to be specified
                // http://msdn.microsoft.com/ru-ru/library/ms162132.aspx
                msConn=new ServerConnection(sServer);
                msConn.LoginSecure = false;
                msConn.Login = sLogin;
                msConn.Password = sPassword;

                msSRV = new Server(msConn);
      
                Log.AddOK("OK Сервер:"+msSRV.Information.Version);   // connection is established                
                
            }
            catch (Exception ex)
            {
                Log.AddError("Ошибка: " + ex.Message);
            }

        }
    }
}

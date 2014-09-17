using System;
using System.Windows.Forms;
using System.Xml;
using tk4000.log;
using tk4000.modif;

namespace tk4000.ncc
{
    public class TModifyManager
    {
        protected TCustomLog Log;
        protected TModifConnection MDB;

        public TModifyManager()
        {
        }
        public void ProceedXMLJob(string XMLFileName)
        {
            XmlNode RootNode=null;
            Log = new TMSGLog();
            XmlDocument FXML = new XmlDocument();
            FXML.Load(XMLFileName);
            foreach (XmlNode Node in FXML.ChildNodes)
            {
                if (Node.Name.ToUpper() == "ROOT") { RootNode = Node; break; }
            }
            if (RootNode == null) return;
            foreach (XmlNode Node in RootNode.ChildNodes)
            {
                if (Node.Name.ToUpper() == "DBCONNECT")
                {
                    MDB = new TModifConnection();
                    MDB.Log = Log;

                    MDB.sServer = Node.Attributes["SERVER"].Value;
                    MDB.sDatabase = Node.Attributes["DATABASE"].Value;
                    MDB.sLogin = Node.Attributes["LOGIN"].Value;
                    MDB.sPassword = Node.Attributes["PASSWORD"].Value;

                    MDB.Connect();
                }
             //   Log.Add(Node.Name);

            }
            
        }
    }
}

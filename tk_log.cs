using System;
using System.Windows.Forms;

namespace tk4000.log
{
    abstract public class TCustomLog
    {
        protected abstract void AddInternal( string s, bool flag_newline);
        public virtual void Add(string S)
        {
            AddInternal(S,true);
            fStartLine="";
        }
        public virtual void AddError( string S)
        {
            Add(S);
        }
        public virtual void AddOK( string S="OK")
        {
            Add(S);
        }
        public virtual void StartLine(string s)
        {
            AddInternal(s, false);
            fStartLine += s;
        }
        public bool RaiseOnError { get;set;}
      
        public string fStartLine;

    

    }
    public class TNullLog : TCustomLog
    {
        protected override void AddInternal(string s, bool flag_newline)
        {
            
        }
    }
    public class TConsoleLog : TCustomLog
    {
        protected override void AddInternal(string s, bool flag_newline)
        {
            if (flag_newline) { Console.WriteLine(s); } else { Console.Write(s); };
        }
        public override void AddError(string S)
        {
            Add("***"+S);
            if (RaiseOnError) throw new Exception(S);
        }
    }

    public class TMSGLog : TCustomLog
    {
        protected override void AddInternal(string s, bool flag_newline)
        {
          MessageBox.Show(s);
        }
        public override void AddError(string S)
        {
            Add("***" + S);
            if (RaiseOnError) throw new Exception(S);
        }
    }
}

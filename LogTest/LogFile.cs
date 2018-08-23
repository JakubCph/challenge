using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogTest
{
    public class LogFile
    {
        public StreamWriter _writer;
        private StringBuilder stringBuilder;
        //TODO:
        public DateTime _curDate = DateTime.Now;
        public String filename;
        public Boolean isTest = false;
        public LogFile()
        {
            createLogFile();
        }

        public void writeToFile(string allText)
        {
            this.newDayFile();
            this._writer.Write(allText);
            
        }
        private void createLogFile()
        {
            if (!Directory.Exists(@"C:\LogTest"))
                Directory.CreateDirectory(@"C:\LogTest");

            this.filename = @"C:\LogTest\Log" + _curDate.ToString("yyyyMMdd HHmmss fff") + ".log";
            
            this._writer = File.AppendText(this.filename);
            this._writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
            this._writer.AutoFlush = true;
        }
        private void newDayFile()
        {
            if (DateTime.Now.Date != _curDate.Date)
            {
                if (!this.isTest) { this._curDate = DateTime.Now; }
                createLogFile();
            }
        }
    }
}

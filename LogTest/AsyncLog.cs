namespace LogTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;


    public class AsyncLog : ILog
    {
        private Thread _runThread;
        private List<LogLine> _lines = new List<LogLine>();
        private bool _QuitWithFlush = false;
        private bool _exit = false;

        //TODO:
        public LogFile logFile;
        public AsyncLog()
        {
            this.logFile = new LogFile();
            this._runThread = new Thread(this.MainLoop);
            this._runThread.Start();
        }

        public LogFile getLogFile()
        {
            return this.logFile;
        }

        private void MainLoop()
        {
            try
            {
            while (!this._exit)
            {
                if (this._lines.Count > 0)
                {
                        List<LogLine> _handled;
                        lock (this._lines)
                        {
                            _handled = new List<LogLine>(this._lines);
                            this._lines.Clear();
                        }
                        string allText = _handled.Select(a => a.LineText()).Aggregate((x, y) => x + y);

                        if (!this._exit)
                        {
                            this.logFile.writeToFile(allText);
                        }
                    if (this._QuitWithFlush == true && this._lines.Count == 0) 
                        this._exit = true;
                    Thread.Sleep(500);
                    }
            }
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }

        public void StopWithoutFlush()
        {
            this._exit = true;
        }

        public void StopWithFlush()
        {
            this._QuitWithFlush = true;
        }

        public void Write(string text)
        {
                lock (this._lines)
                {
                    this._lines.Add(new LogLine() { Text = text, Timestamp = DateTime.Now });
                }
            //var t = Task.Factory.StartNew(() => { });
        }
    }
}
using LoggerSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Services
{
    public class LogFile : ILogFile
    {
        private const string FilePath = "../../../log.txt";

        public int Size
        {
            get
            {
                if (!File.Exists(FilePath))
                {
                    return 0;
                }

                FileInfo fileInfo = new FileInfo(FilePath);
                return (int)fileInfo.Length;
            }
        }

        public void Write(string content)
        {
            File.AppendAllText(FilePath, content + Environment.NewLine, Encoding.UTF8);
        }
    }
}

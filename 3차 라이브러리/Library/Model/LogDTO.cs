using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    //DateTime value = reader.GetDateTime("timestamp");
    //string timeStamp = value.ToString("yyyy-MM-dd HH:mm:ss");
    //logDTO.SetTime(timeStamp);
    class LogDTO
    {
        private string id; // autoincrement by DB

        private string logTime;
        private string user;
        private string action;
        private string note;

        public LogDTO(string id, string logTime, string user, string action, string note)
        {
            this.id = id;
            this.logTime = logTime;
            this.user = user;
            this.action = action;
            this.note = note;
        }

        public string GetID() { return id; }
        public string GetTime() { return logTime; }
        public string GetUser() { return user; }
        public string GetAction() { return action; }
        public string GetNote() { return note; }
    }
}

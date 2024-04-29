using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogDTO
    {
        private int id;

        private DateTime timeStamp;
        private bool mode;
        private string action;

        public LogDTO() { }

        public LogDTO(DateTime timeStamp, bool mode, string action)
        {
            this.timeStamp = timeStamp;
            this.mode = mode;
            this.action = action;
        }

        public int GetID() { return id; }
        public DateTime GetTime() { return timeStamp; }
        public bool GetMode() { return mode; }
        public string GetAction() { return action; }

        public void SetID(int id) { this.id = id; }
        public void SetTime(DateTime timeStamp) { this.timeStamp = timeStamp; }
        public void SetMode(bool mode) { this.mode = mode; }
        public void SetAction(string action) { this.action = action; }
    }
}

using System;
using System.Collections.Generic;

namespace LTT
{
    class LectureDTO
    {
        public const string[] lectureTimes = {
                            "09:00", "09:30", "10:00", "10:30", "11:00", "11:30",
                            "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                            "15:00", "15:30", "16:00", "16:30", "17:00", "17:30",
                            "18:00", "18:30", "19:00", "19:30", "20:00", "20:30",
                            "21:00"
            };

        private string LectureID; // NO
        private string department; // 개설학과전공
        private string number; // 학수번호
        private string section; // 분반
        private string name; // 교과목명
        private string courseType; // 이수구분
        private string year; // 학년
        private string credit; // 학점
        private string time; // 요일 및 강의시간
        private string classroom; // 강의실
        private string professor; // 교수명
        private string language; // 강의언어
        private List<int> lectureTimeNumbers;

        public string GetLectureID() { return LectureID; }
        public void SetLectureID(string lectureID) { LectureID = lectureID; }

        public string GetDepartment() { return department; }
        public void SetDepartment(string department) { this.department = department; }

        public string GetNumber() { return number; }
        public void SetNumber(string number) { this.number = number; }

        public string GetSection() { return section; }
        public void SetSection(string section) { this.section = section; }

        public string GetName() { return name; }
        public void SetName(string name) { this.name = name; }

        public string GetCourseType() { return courseType; }
        public void SetCourseType(string courseType) { this.courseType = courseType; }

        public string GetYear() { return year; }
        public void SetYear(string year) { this.year = year; }

        public string GetCredit() { return credit; }
        public void SetCredit(string credit) { this.credit = credit; }

        public string GetTime() { return time; }
        public void SetTime(string time) { this.time = time; }

        public string GetClassroom() { return classroom; }
        public void SetClassroom(string classroom) { this.classroom = classroom; }

        public string GetProfessor() { return professor; }
        public void SetProfessor(string professor) { this.professor = professor; }

        public string GetLanguage() { return language; }
        public void SetLanguage(string language) { this.language = language; }

        public void SetLectureTimeNumbers()
        {
            lectureTimeNumbers = new List<int>();

            // 수 16:30~18:30 -> 수 16:30~18:30
            // 수 16:30~18:30, 금 09:00~12:00 -> 수 16:30~18:30 and 금 09:00~12:00
            // 월 수 16:00~18:30 -> 월 수 16:00~18:30
            string time = "월 수 10:30~12:30";
            string[] wordsArray = time.Split(',');
            List<string> times = new List<string>(wordsArray);

            List<string> realTimes = new List<string>();

            for (int i = 0; i < times.Count; i++)
            {
                times[i] = times[i].Trim();
                if (times[i].Split(' ').Length >= 3)
                {
                    realTimes.Add(times[i].Remove(0, 2));
                    realTimes.Add(times[i].Remove(2, 2));
                }
                else
                {
                    realTimes.Add(times[i]);
                }
            }

            // 이제 모두 "월 16:30~18:30" 꼴임
            for (int i = 0; i < realTimes.Count; i++)
            {
                int baseNum = 0;
                string date = realTimes[i].Split(' ')[0];

                switch (date)
                {
                    case "월":
                        baseNum = 0;
                        break;
                    case "화":
                        baseNum = 1;
                        break;
                    case "수":
                        baseNum = 2;
                        break;
                    case "목":
                        baseNum = 3;
                        break;
                    case "금":
                        baseNum = 4;
                        break;
                }

                string start = realTimes[i].Split(' ')[1].Split('~')[0];
                string end = realTimes[i].Split(' ')[1].Split('~')[1];

                int startNum = 0;
                int endNum = 0;
                for (int x = 0; x < lectureTimes.Length; x++)
                {
                    if (start == lectureTimes[x]) startNum = x;
                    if (end == lectureTimes[x]) endNum = x;
                }

                for (int k = startNum; k < endNum; k++)
                {
                    lectureTimeNumbers.Add(baseNum + 5 * k);
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;

namespace LTT
{
    class LectureDTO
    {
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
    }
}
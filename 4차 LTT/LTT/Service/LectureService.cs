using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // service는 repository 함수들을 이용해서 controller한테 LectureDTO만 전달해주면 됨
    // buisness logic? service logic은 모두 여기서?
    class LectureService
    {
        private static LectureService instance;

        // 어차피 얘는 repository만 참조하면 됨
        private LectureRepository lectureRepository;

        private LectureService()
        {
            this.lectureRepository = LectureRepository.GetInstance();
        }

        public static LectureService GetInstance()
        {
            if (instance == null)
            {
                instance = new LectureService();
            }
            return instance;
        }

        //========================= SINGLETON =========================//

        public LectureDTO GetLectureByID(string id)
        {
            return lectureRepository.GetLectureByID(id);
        }

        // controller한테서 filter받으면 repository이용해서 LectureDTO 다 꺼내서 다시 controller한테 전달
        public List<LectureDTO> GetFilteredLectureResults(List<string> filters)
        {
            string department = filters[0];
            string courseType = filters[1];
            string name = filters[2];
            string professor = filters[3];
            string year = filters[4];

            List<LectureDTO> filteredLectures = new List<LectureDTO>();

            // 항상 lectureDB는 1번부터 시작
            // 0은 dummyDTO임
            for (int i = 1; i < lectureRepository.GetLectureCnt(); i++)
            {

                LectureDTO curLecture = lectureRepository.GetLectureByID(i.ToString());

                // 빈칸 아니고 맞지않으면 패스. 한개라도 틀리면 볼필요가 없음
                if (department != "" && !curLecture.GetDepartment().Contains(department)) continue;
                if (courseType != "" && !curLecture.GetCourseType().Contains(courseType)) continue;
                if (name != "" && !curLecture.GetName().Contains(name)) continue;
                if (professor != "" && !curLecture.GetProfessor().Contains(professor)) continue;
                if (year != "" && !curLecture.GetYear().Contains(year)) continue;

                // 여기까지 왔으면 필터링 통과한거
                filteredLectures.Add(curLecture);
            }

            return filteredLectures;
        }
    }
}

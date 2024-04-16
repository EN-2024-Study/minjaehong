using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace LTT
{
    // DB를 그냥 여기에 넣어놔라
    class LectureModel
    {
        private static LectureModel instance;

        private List<LectureDTO> lectureDB;

        private Excel.Application application;
        private Excel.Workbook workbook;

        private Array data;

        //============== SINGLETON ==============//

        private LectureModel()
        {
            // 초기 DB 세팅
            workbook = GetConnection();
            data = GetExcelSheetData(workbook);
            lectureDB = ConvertDataToDTO(data);

            // 데이터 받아오면 자원 닫고 모두 반환
            //workbook.Close();
            //application.Quit();
            //ReleaseExcelObject(workbook);
            //ReleaseExcelObject(application);
        }

        public static LectureModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LectureModel();
            }
            return instance;
        }

        //============== INITIAL DB SETTING ==============//

        private Excel.Workbook GetConnection()
        {
            try
            {
                application = new Excel.Application();
                workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\2023년도 1학기 강의시간표.xlsx");
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            return workbook;
        }

        private Array GetExcelSheetData(Excel.Workbook workbook)
        {
            Excel.Sheets sheets = workbook.Sheets;
            Excel.Worksheet worksheet = sheets["Sheet1"] as Excel.Worksheet;
            Excel.Range cellRange = worksheet.get_Range("A2", "L185") as Excel.Range;
            Array data = cellRange.Cells.Value2;
            return data;
        }
        
        private List<LectureDTO> ConvertDataToDTO(Array data)
        {
            List<LectureDTO> retList = new List<LectureDTO>();
            
            // List에서도 1번부터 시작하게 dummyDTO 넣고 시작
            // 나중에 강의번호로 바로 참조할 수 있게
            retList.Add(new LectureDTO());
            
            for (int i = 1; i < 185; i++)
            {
                LectureDTO dummyDTO = new LectureDTO();

                dummyDTO.SetLectureID(data.GetValue(i, 1).ToString()); // ID
                dummyDTO.SetDepartment(data.GetValue(i, 2).ToString()); // 학과전공
                dummyDTO.SetNumber(data.GetValue(i, 3).ToString()); // 학수번호
                dummyDTO.SetSection(data.GetValue(i, 4).ToString()); // 분반
                dummyDTO.SetName(data.GetValue(i, 5).ToString()); // 과목명
                dummyDTO.SetCourseType(data.GetValue(i, 6).ToString()); // 이수구분
                dummyDTO.SetYear(data.GetValue(i, 7).ToString()); // 학년
                dummyDTO.SetCredit(data.GetValue(i, 8).ToString()); // 학점
                if (data.GetValue(i, 9) == null) { dummyDTO.SetTime(""); }
                else dummyDTO.SetTime(data.GetValue(i, 9).ToString()); // 요일
                if(data.GetValue(i,10)==null) { dummyDTO.SetClassroom(""); }
                else dummyDTO.SetClassroom(data.GetValue(i, 10).ToString()); // 강의실
                dummyDTO.SetProfessor(data.GetValue(i, 11).ToString()); // 교수명
                dummyDTO.SetLanguage(data.GetValue(i, 12).ToString()); // 언어

                retList.Add(dummyDTO);
            }

            return retList;
        }

        //============= RELEASE DB CONNECTION ===========//
        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        //============== DB FUNCTIONS ==============//

        public List<LectureDTO> GetLectureDB()
        {
            return lectureDB;
        }

        // LectureDTO들의 참조값 List 반환
        // 이게 작동하려면 기존 LectureDTO가 계속 살아있어야함
        // 기존 LectureDTO는 lectureDB에 있는 LectureDTO들
        // 지금 계속 각 객체의 참조값을 넘기면서 작업하는거임
        // 그럼 lectureDB에 있는 객체들이 계속 살아있다는 보장 ㄱㄴ??
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
            for(int i = 1; i < lectureDB.Count; i++)
            {
                LectureDTO curLecture = lectureDB[i];

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

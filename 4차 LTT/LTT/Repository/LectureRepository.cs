using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace LTT
{
    // Lecture를 추가하는 경우는 없으므로
    // 그냥 Lectuer를 꺼내다 주는 것만 하면 됨
    class LectureRepository
    {
        private static LectureRepository instance;

        private List<LectureDTO> lectureDB; 
        private int lectureCnt;

        private Excel.Application application;
        private Excel.Workbook workbook;
        private Array data;

        //=============== SINGLETON ===============//

        private LectureRepository()
        {
            lectureCnt = 0;

            // 초기 DB 세팅
            workbook = GetConnection();
            data = GetExcelSheetData(workbook);
            lectureDB = ConvertDataToDTO(data);

            // 데이터 받아오면 자원 닫고 모두 반환
            workbook.Close();
            application.Quit();
            ReleaseExcelObject(workbook);
            ReleaseExcelObject(application);
        }

        public static LectureRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new LectureRepository();
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

        // 이걸 DTO에 넣어서 DTO에서 setter로 처리하지 않게 하기
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
                dummyDTO.SetCourseNumber(data.GetValue(i, 3).ToString()); // 학수번호
                dummyDTO.SetSection(data.GetValue(i, 4).ToString()); // 분반
                dummyDTO.SetName(data.GetValue(i, 5).ToString()); // 과목명
                dummyDTO.SetCourseType(data.GetValue(i, 6).ToString()); // 이수구분
                dummyDTO.SetYear(data.GetValue(i, 7).ToString()); // 학년
                dummyDTO.SetCredit(data.GetValue(i, 8).ToString()); // 학점
                if (data.GetValue(i, 9) == null) { dummyDTO.SetTime(""); }
                else dummyDTO.SetTime(data.GetValue(i, 9).ToString()); // 요일
                if (data.GetValue(i, 10) == null) { dummyDTO.SetClassroom(""); }
                else dummyDTO.SetClassroom(data.GetValue(i, 10).ToString()); // 강의실
                dummyDTO.SetProfessor(data.GetValue(i, 11).ToString()); // 교수명
                dummyDTO.SetLanguage(data.GetValue(i, 12).ToString()); // 언어

                retList.Add(dummyDTO);
                lectureCnt++;
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

        //============== REPOSITORY FUNCTIONS ==============//

        // Service한테 특정 Lecture return
        public LectureDTO GetLectureByID(string lectureID)
        {
            int ID = int.Parse(lectureID);
            return lectureDB[ID];
        }

        // Service한테 DB에 저장된 Lecture 개수 return
        public int GetLectureCnt()
        {
            return lectureCnt;
        }
    }
}

// LectureDTO들의 참조값 List 반환
// 이게 작동하려면 기존 LectureDTO가 계속 살아있어야함
// 기존 LectureDTO는 lectureDB에 있는 LectureDTO들
// 지금 계속 각 객체의 참조값을 넘기면서 작업하는거임
// 지금 lectureDB에 있는 객체들이 계속 살아있다는 보장이 되어있음
// 그래서 참조값으로 계속 넘겨도 가능함
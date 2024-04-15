using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT
{
    // DB를 그냥 여기에 넣어놔라
    class LectureModel
    {
        private static LectureModel instance;

        private List<LectureDTO> lectureDB;
        private Excel.Worksheet worksheet;
        private Array data;

        //============== SINGLETON ==============//

        private LectureModel()
        {
            // 초기 DB 세팅
            worksheet = GetConnection();
            data = GetExcelSheetData(worksheet);
            lectureDB = ConvertDataToDTO(data);
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

        private Excel.Worksheet GetConnection()
        {
            Excel.Worksheet worksheet = null;

            try
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\2023년도 1학기 강의시간표.xlsx");
                Excel.Sheets sheets = workbook.Sheets;
                worksheet = sheets["Sheet1"] as Excel.Worksheet;
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            return worksheet;
        }

        private Array GetExcelSheetData(Excel.Worksheet worksheet)
        {
            Excel.Range cellRange = worksheet.get_Range("A2", "L185") as Excel.Range;
            Array data = cellRange.Cells.Value2;
            return data;
        }
        
        private List<LectureDTO> ConvertDataToDTO(Array data)
        {
            List<LectureDTO> retList = new List<LectureDTO>();
            
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

        //============== DB FUNCTIONS ==============//

        public List<LectureDTO> GetLectureDB()
        {
            return lectureDB;
        }

    }
}

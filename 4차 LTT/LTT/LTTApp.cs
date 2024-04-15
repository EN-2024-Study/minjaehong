using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT
{
    class LTTApp
    {
        public static void Main()
        {
            LectureModel model = LectureModel.GetInstance();

            List<LectureDTO> db = model.GetLectureDB();

            for(int i = 0; i < db.Count; i++)
            {
                LectureDTO dummy = db[i];
                Console.WriteLine(dummy.GetName());
            }

            Console.ReadLine();

            /*
            Console.CursorVisible = false;

            MainController mainController = new MainController();
            mainController.Run();
            */
        }
    }
}

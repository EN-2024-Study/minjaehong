using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    // Singleton
    // 오로지 한 객체만 생성됨
    public class Common
    {
        // static 참조변수
        private static Common instance;

        // 외부에서 객체 생성 못하게 막기
        private Common()
        {

        }

        public static Common GetInstance()
        {
            if(instance==null)
            {
                instance = new Common();
            }
            // 이미 있으면 기존 객체 반환
            return instance;
        }

        // 보여줄 페이지
        public int pageidx = 0;
        // 현재 진행중인 모드
        public int mode = 0;
        // 현재 USER 이름
        public string username;

        public int comWin = 0;
        public int usrWin = 0;
        
        //==================================//

        // 콘솔 창 너비 높이
        public int WIDTH = 80;
        public int HEIGHT = 40;

        // 로고 출력 시작 좌표
        public int LOGO_X = 5;
        public int LOGO_Y = 5;

        // FRONT 화면 초기 좌표
        public int FRONT_X = 35;
        public int FRONT_Y = 15;

        // MENU 화면 초기 좌표
        public int MENU_X = 35;
        public int MENU_Y = 15;

        // GAME 화면 초기 좌표
        public int GAME_X = 35;
        public int GAME_Y = 15;

        // HISTORY 화면 초기 좌표
        public int HISTORY_X = 35;
        public int HISTORY_Y = 15;
    }
}

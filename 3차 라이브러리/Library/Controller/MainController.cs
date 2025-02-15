﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MainController
    { 
        // MainController와 연결되어야하는 애들
        private UserFrontController userFrontController;
        private ManagerController managerController;

        private BookService bookService;
        private MemberService memberService;

        private MainView mainView;

        public MainController()
        {
            bookService = new BookService();
            memberService = new MemberService();

            userFrontController = new UserFrontController(bookService, memberService);
            managerController = new ManagerController(bookService, memberService);

            mainView = new MainView();
        }

        private void RunManagerMode()
        {
            // MANAGERMODE이면 별로 할게 없기 때문에
            // ID PW 확인만 하고 managercontroller를 여기서 바로 Run해줘도 됨
            LoginDTO loginDTO = mainView.ManagerLoginForm();

            string inputID = loginDTO.GetID();
            string inputPW = loginDTO.GetPW();

            // ID PW 이 둘 다 맞으면
            if (LibraryApp.managerID == inputID)
            {
                if (LibraryApp.managerPW == inputPW)
                {
                    Logger.recordLog(DateTime.Now, "manager", "LOGIN SUCCESS");

                    managerController.RunManagerMode();
                }
                else
                {
                    Logger.recordLog(DateTime.Now, "manager", "LOGIN FAIL", "INCORRECT PW");

                    CommonView.RuntimeMessageForm("MANAGER PASSWORD IS INCORRECT!");
                }
            }
            else
            {
                Logger.recordLog(DateTime.Now, "manager", "LOGIN FAIL", "INCORRECT ID");

                CommonView.RuntimeMessageForm("MANAGER ID IS INCORRECT!");
            }
        }

        public void StartApp()
        {
            LibraryMode mode;

            bool isProgramRunning = true;

            // Library 시작점
            while (isProgramRunning)
            {
                mode = mainView.MainModeSelectForm();

                switch (mode)
                {
                    case LibraryMode.USER_MODE:
                        userFrontController.RunUserFrontMode();
                        break;

                    case LibraryMode.MANAGER_MODE:
                        RunManagerMode();
                        break;

                    case LibraryMode.PROGRAM_EXIT:
                        isProgramRunning = false;
                        break;
                }
            }
        }
    }
}
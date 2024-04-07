using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    interface ControllerInterface
    {

    }

    class Controller : ControllerInterface
    {
        // Controller는 Data 전달을 위해 두 개를 참조해야함
        ModelInterface model;
        View view;

        // 생성자로 MVC 연결 
        public Controller(ModelInterface model, View view)
        {
            this.model = model;
            this.view = view;
        }

        public void run()
        {
            view.indexForm();
            Console.WriteLine("갈 곳 입력:");
            int idx;
            while (true)
            {
                idx = int.Parse(Console.ReadLine());
                if (idx == 1)
                {
                    MemberVO m = view.loginForm();
                    Console.Clear();
                    Console.WriteLine("hi" + m.id);
                }
            }
        }
    }
}
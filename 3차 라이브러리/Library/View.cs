using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class View
    {
        public void indexForm()
        {
            Console.Clear();

            Console.WriteLine("라이브러리");
            Console.WriteLine("1. LoginForm");
            Console.WriteLine("2. DeleteForm");
            Console.WriteLine("1. UpdateForm");
            Console.WriteLine("1. FindForm");

        }

        public MemberVO loginForm()
        {
            Console.Clear();

            Console.WriteLine("[LoginForm]");
            Console.Write("enter id : ");
            string id = Console.ReadLine();

            MemberVO memberVO = new MemberVO(id);

            return memberVO;
        }


    }
}

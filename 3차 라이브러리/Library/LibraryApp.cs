using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryApp
    {
        public static void Main()
        {
            ModelInterface model = new Model();
            View view = new View();
            Controller controller = new Controller(model, view);
            controller.run();
        }
    }
}

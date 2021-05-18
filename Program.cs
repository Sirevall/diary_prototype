using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Diary_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            IFile diaryFile = new DiaryFile();

            Application app = new Application(diaryFile);

            app.RunApp();
        }
    }
}

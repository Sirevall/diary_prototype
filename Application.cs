using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_prototype
{
    class Application
    {

        IFile file;

        public Application(IFile file)
        {
            this.file = file;
        }

        public void RunApp()
        {
            Diary diary = new Diary();

            List<Note> Notes = new List<Note>();

            Notes = file.ReadFile(Notes);            

            while (true)
            {
                Console.WriteLine($"Чего надобно? " +
                    $"\n1 - Показать    2 - Добавить" +
                    $"\n3 - Удалить     4 - Редактировать" +
                    $"\n5 - Сортировать 6 - Показать записи определенного диапазона дат" +
                    $"\n7 - Выйти");
                Console.Write("\nОтвет - ");

                string answer = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                switch (answer)
                {
                    case "1": diary.Print(Notes); break;
                    case "2": diary.Add(Notes); break;
                    case "3": diary.RemoveNote(Notes); break;
                    case "4": diary.Edit(Notes); break;
                    case "5": diary.Sort(Notes); break;
                    case "6": diary.PrintToSpecialDate(Notes); break;
                    case "7":
                        Notes.Sort((a, b) => a.NoteNumber.CompareTo(b.NoteNumber));
                        file.WriteFile(Notes);
                        Environment.Exit(-1);
                        break;
                    default: Console.WriteLine("\nЯ таки-та не разобралась...\n"); break;
                }
            }
        }
    }
}

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

            List<Note> notes = new List<Note>();

            notes = file.ReadFile(notes);

            while (true)
            {
                Console.WriteLine($"Чего надобно? " +
                    $"\n1 - Показать    2 - Добавить" +
                    $"\n3 - Удалить     4 - Редактировать" +
                    $"\n5 - Сортировать 6 - Показать записи определенного диапазона дат" +
                    $"\n7 - Выйти");
                Console.Write("\nОтвет - ");

                string answerUser = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                switch (answerUser)
                {
                    case "1":
                        diary.Print(notes);
                        break;
                    case "2":
                        diary.Add(notes);
                        break;
                    case "3":
                        diary.RemoveNote(notes);
                        break;
                    case "4":
                        diary.Edit(notes);
                        break;
                    case "5":
                        diary.Sort(notes);
                        break;
                    case "6":
                        diary.PrintToSpecialDate(notes);
                        break;
                    case "7":
                        notes.Sort((a, b) => a.NoteNumber.CompareTo(b.NoteNumber));
                        file.WriteFile(notes);
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("\nЯ таки-та не разобралась...\n");
                        break;
                }
            }
        }
    }
}

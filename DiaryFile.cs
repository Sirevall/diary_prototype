using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Diary_prototype
{
    class DiaryFile : IFile
    {
        string json;
        string pathToFile = "DiaryData.json";

        public List<Note> ReadFile(List<Note> notes)
        {
            if (!File.Exists(pathToFile))
                File.Create(pathToFile).Close();

            json = File.ReadAllText(pathToFile);

            if (json != "")
                notes = JsonConvert.DeserializeObject<List<Note>>(json);

            return notes;
        }
        public void WriteFile(List<Note> notes)
        {
            json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(pathToFile, json);
        }
    }
}

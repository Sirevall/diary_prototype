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

        string PathToFile = "DiaryData.json";

        public List<Note> ReadFile(List<Note> Notes)
        {
            if (!File.Exists(PathToFile)) File.Create(PathToFile).Close();

            json = File.ReadAllText(PathToFile);

            if (json != "")
            {
                Notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            return Notes;
        }

        public void WriteFile(List<Note> Notes)
        {
            json = JsonConvert.SerializeObject(Notes);
            File.WriteAllText(PathToFile, json);
        }
    }
}

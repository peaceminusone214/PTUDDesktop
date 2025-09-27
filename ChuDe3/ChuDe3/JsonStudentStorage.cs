using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChuDe3
{
    public class JsonStudentStorage : IStudentStorage
    {
        private string filePath;
        public JsonStudentStorage(string path) => filePath = path;

        public List<Student> Load()
        {
            if (!File.Exists(filePath))
            {
                // Tạo file rỗng ngay lần đầu
                File.WriteAllText(filePath, "[]");
                return new List<Student>();
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
        }

        public void Save(List<Student> students)
        {
            // Nếu file chưa có, tạo trước
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }

}

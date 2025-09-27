using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ChuDe3
{
    public class XmlStudentStorage : IStudentStorage
    {
        private readonly string filePath;
        public XmlStudentStorage(string path) => filePath = path;

        public List<Student> Load()
        {
            if (!File.Exists(filePath)) return new List<Student>();

            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<Student>)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[XML LOAD ERROR] {ex.Message}");
                return new List<Student>();
            }
        }

        public void Save(List<Student> students)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, students);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[XML SAVE ERROR] {ex.Message}");
            }
        }
    }
}

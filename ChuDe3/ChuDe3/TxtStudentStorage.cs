using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChuDe3
{
    public class TxtStudentStorage : IStudentStorage
    {
        private readonly string filePath;
        public TxtStudentStorage(string path) => filePath = path;

        public List<Student> Load()
        {
            var result = new List<Student>();
            if (!File.Exists(filePath)) return result;

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length >= 10)
                    {
                        DateTime ngaySinh;
                        DateTime.TryParse(parts[3], out ngaySinh);

                        var st = new Student
                        {
                            MSSV = parts[0],
                            HoTenLot = parts[1],
                            Ten = parts[2],
                            NgaySinh = ngaySinh,
                            CMND = parts[4],
                            DiaChi = parts[5],
                            GioiTinhNam = parts[6].Equals("Nam", StringComparison.OrdinalIgnoreCase),
                            Lop = parts[7],
                            SoDT = parts[8],
                            MonHoc = parts[9]
                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(m => m.Trim())
                                .ToList()
                        };
                        result.Add(st);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TXT LOAD ERROR] {ex.Message}");
            }

            return result;
        }

        public void Save(List<Student> students)
        {
            try
            {
                var lines = students.Select(st =>
                    $"{st.MSSV}|{st.HoTenLot}|{st.Ten}|{st.NgaySinh:dd/MM/yyyy}|{st.CMND}|{st.DiaChi}|{(st.GioiTinhNam ? "Nam" : "Nu")}|{st.Lop}|{st.SoDT}|{string.Join(",", st.MonHoc)}"
                );
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TXT SAVE ERROR] {ex.Message}");
            }
        }
    }
}

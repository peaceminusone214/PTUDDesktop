using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuDe3
{
    public class StudentManager
    {
        private List<Student> students = new List<Student>();
        private IStudentStorage storage;

        public StudentManager(IStudentStorage storage)
        {
            this.storage = storage;
            students = storage.Load();
        }

        public List<Student> GetAll() => students;

        public void AddOrUpdate(Student st)
        {
            if (st == null) return; // tránh null
            var existing = students.FirstOrDefault(s => s?.MSSV == st.MSSV);
            if (existing != null)
            {
                students.Remove(existing);
            }
            students.Add(st);
            storage.Save(students.Where(s => s != null).ToList());
        }
        public void Delete(List<string> mssvList)
        {
            students.RemoveAll(s => s != null && mssvList.Contains(s.MSSV));
            storage.Save(students.Where(s => s != null).ToList());
        }

        public List<Student> Search(string mssv = null, string ten = null, string lop = null)
        {
            return students.Where(s =>
                (string.IsNullOrEmpty(mssv) || s.MSSV.Contains(mssv)) &&
                (string.IsNullOrEmpty(ten) || s.Ten.ToLower().Contains(ten.ToLower())) &&
                (string.IsNullOrEmpty(lop) || s.Lop.ToLower().Contains(lop.ToLower()))
            ).ToList();
        }
    }
}

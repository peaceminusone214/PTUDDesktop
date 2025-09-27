using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuDe3
{
    public interface IStudentStorage
    {
        List<Student> Load();
        void Save(List<Student> students);
    }

}

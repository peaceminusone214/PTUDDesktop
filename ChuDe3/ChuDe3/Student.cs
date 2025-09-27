using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuDe3
{
    [Serializable] // để hỗ trợ XML/JSON serialization
    public class Student
    {
        public string MSSV { get; set; }   // 7 chữ số AABBCCC
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CMND { get; set; }   // 9 chữ số
        public string DiaChi { get; set; }
        public bool GioiTinhNam { get; set; }
        public string Lop { get; set; }
        public string SoDT { get; set; }   // 10 chữ số
        public List<string> MonHoc { get; set; } = new List<string>();
    }
}

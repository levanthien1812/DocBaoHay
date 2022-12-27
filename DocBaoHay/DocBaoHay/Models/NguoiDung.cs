using System;
using System.Collections.Generic;
using System.Text;

namespace DocBaoHay.Models
{
    public class NguoiDung
    {
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public static NguoiDung nguoiDung;
    }
}

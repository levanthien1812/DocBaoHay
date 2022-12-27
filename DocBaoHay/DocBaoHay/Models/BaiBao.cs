using System;
using System.Collections.Generic;
using System.Text;

namespace DocBaoHay.Models
{
    public class BaiBao
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public DateTime ThoiGianDang { get; set; }
        public int ChuDe { get; set; }
        public int TacGia { get; set; }
    }
}

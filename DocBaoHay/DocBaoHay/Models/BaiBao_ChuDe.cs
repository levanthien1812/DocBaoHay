using System;
using System.Collections.Generic;
using System.Text;

namespace DocBaoHay.Models
{
    public class BaiBao_ChuDe
    {
        public string Id { get; set; }
        public string TieuDe { get; set; }
        // Hinh tac gia
        public string TacGiaHinh { get; set; }
        public string TacGiaId { get; set; }
        public string Thumbnail { get; set; }
        public string KhoangTG { get; set; }
    }
}

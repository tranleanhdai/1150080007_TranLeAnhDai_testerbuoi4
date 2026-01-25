namespace LabUnit
{
    public class HocVien
    {
        public string MaSo { get; }
        public string HoTen { get; }
        public string QueQuan { get; }
        public double M1 { get; }
        public double M2 { get; }
        public double M3 { get; }

        public HocVien(string maSo, string hoTen, string queQuan, double m1, double m2, double m3)
        {
            MaSo = maSo;
            HoTen = hoTen;
            QueQuan = queQuan;
            M1 = m1; M2 = m2; M3 = m3;
        }

        public double DiemTrungBinh() => (M1 + M2 + M3) / 3.0;

        public bool DuDieuKienHocBong()
            => DiemTrungBinh() >= 8.0 && M1 >= 5.0 && M2 >= 5.0 && M3 >= 5.0;
    }
}
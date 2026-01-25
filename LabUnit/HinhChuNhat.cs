using System;

namespace LabUnit
{
    public class HinhChuNhat
    {
        public Diem TopLeft { get; }
        public Diem BottomRight { get; }

        public HinhChuNhat(Diem topLeft, Diem bottomRight)
        {
            if (topLeft == null || bottomRight == null)
                throw new ArgumentException("Invalid Data");

            if (bottomRight.X <= topLeft.X || bottomRight.Y >= topLeft.Y)
                throw new ArgumentException("Invalid Data");

            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public double DienTich()
        {
            return (BottomRight.X - TopLeft.X) * (TopLeft.Y - BottomRight.Y);
        }

        public bool GiaoNhau(HinhChuNhat other)
        {
            if (other.BottomRight.X < this.TopLeft.X) return false;
            if (other.TopLeft.X > this.BottomRight.X) return false;
            if (other.TopLeft.Y < this.BottomRight.Y) return false;
            if (other.BottomRight.Y > this.TopLeft.Y) return false;
            return true;
        }
    }
}
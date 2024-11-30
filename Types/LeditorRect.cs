using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser {
    public class LeditorRect {
        public float left;
        public float top;
        public float right;
        public float bottom;

        public float width => right - left;

        public float height => bottom - top;

        public LeditorRect() {
            this.left = 0;
            this.top = 0;
            this.right = 0;
            this.bottom = 0;
        }

        public LeditorRect(float a) {
            this.left = a;
            this.top = a;
            this.right = a;
            this.bottom = a;
        }

        public LeditorRect(float left, float top, float right, float bottom) {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public float this[int index] {
            get {
                if (index == 0) return this.left;
                if (index == 1) return this.top;
                if (index == 2) return this.right;
                if (index == 3) return this.bottom;
                throw new IndexOutOfRangeException();
            }
            set {
                if (index == 0) this.left = value;
                if (index == 1) this.top = value;
                if (index == 2) this.right = value;
                if (index == 3) this.bottom = value;
                if (index != 0 && index != 1 && index != 2 && index != 3) throw new IndexOutOfRangeException();
            }
        }

        public override string ToString() => $"rect({left}, {top}, {right}, {bottom})";

        public static LeditorRect operator +(LeditorRect a, LeditorRect b) => new(a.left + b.left, a.top + b.top, a.right + b.right, a.bottom + b.bottom);

        public static LeditorRect operator +(LeditorRect a, float b) => new(a.left + b, a.top + b, a.right + b, a.bottom + b);

        public static LeditorRect operator +(float a, LeditorRect b) => new(a + b.left, a + b.top, a + b.right, a + b.bottom);

        public static LeditorRect operator -(LeditorRect a, LeditorRect b) => new(a.left - b.left, a.top - b.top, a.right - b.right, a.bottom - b.bottom);

        public static LeditorRect operator -(LeditorRect a, float b) => new(a.left - b, a.top - b, a.right - b, a.bottom - b);

        public static LeditorRect operator -(float a, LeditorRect b) => new(a - b.left, a - b.top, a - b.right, a - b.bottom);

        public static LeditorRect operator *(LeditorRect a, LeditorRect b) => new(a.left * b.left, a.top * b.top, a.right * b.right, a.bottom * b.bottom);

        public static LeditorRect operator *(LeditorRect a, float b) => new(a.left * b, a.top * b, a.right * b, a.bottom * b);

        public static LeditorRect operator *(float a, LeditorRect b) => new(a * b.left, a * b.top, a * b.right, a * b.bottom);

        public static LeditorRect operator /(LeditorRect a, LeditorRect b) => new(a.left / b.left, a.top / b.top, a.right / b.right, a.bottom / b.bottom);

        public static LeditorRect operator /(LeditorRect a, float b) => new(a.left / b, a.top / b, a.right / b, a.bottom / b);

        public static LeditorRect operator /(float a, LeditorRect b) => new(a / b.left, a / b.top, a / b.right, a / b.bottom);

        public static LeditorRect operator %(LeditorRect a, LeditorRect b) => new(a.left % b.left, a.top % b.top, a.right % b.right, a.bottom % b.bottom);

        public static LeditorRect operator %(LeditorRect a, float b) => new(a.left % b, a.top % b, a.right % b, a.bottom % b);

        public static LeditorRect operator %(float a, LeditorRect b) => new(a % b.left, a % b.top, a % b.right, a % b.bottom);

        public static LeditorRect operator -(LeditorRect a) => new(-a.left, -a.top, -a.right, -a.bottom);

        public static LeditorRect operator &(LeditorRect a, LeditorRect b) => new(a.left < b.left ? b.left : a.left, a.right > b.right ? b.right : a.right, a.top < b.top ? b.top : a.top, a.bottom > b.bottom ? b.bottom : a.bottom);

        public static bool operator ==(LeditorRect a, LeditorRect b) => a.Equals(b);

        public static bool operator !=(LeditorRect a, LeditorRect b) => !a.Equals(b);

        public override bool Equals(object? obj) {
            return obj is LeditorRect b && Equals(b);
        }

        public bool Equals(LeditorRect b) {
            return left.Equals(b.left) && top.Equals(b.top) && right.Equals(b.right) && bottom.Equals(b.bottom);
        }

        public override int GetHashCode() {
            return HashCode.Combine(left, top, right, bottom);
        }

    }
}

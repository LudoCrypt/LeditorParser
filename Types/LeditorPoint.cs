using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser.Types {
    public class LeditorPoint {
        public float loch;
        public float locv;

        public LeditorPoint() {
            loch = 0;
            locv = 0;
        }
        public LeditorPoint(float loch, float locv) {
            this.loch = loch;
            this.locv = locv;
        }

        public float this[int index] {
            get {
                if (index == 0) return loch;
                if (index == 1) return locv;
                throw new IndexOutOfRangeException();
            }
            set {
                if (index == 0) loch = value;
                if (index == 1) locv = value;
                if (index != 0 && index != 1) throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => $"point({loch}, {locv})";

        public static LeditorPoint operator +(LeditorPoint a, LeditorPoint b) => new(a.loch + b.loch, a.locv + b.locv);

        public static LeditorPoint operator +(LeditorPoint a, float b) => new(a.loch + b, a.locv + b);

        public static LeditorPoint operator +(float a, LeditorPoint b) => new(a + b.loch, a + b.locv);

        public static LeditorPoint operator -(LeditorPoint a, LeditorPoint b) => new(a.loch - b.loch, a.locv - b.locv);

        public static LeditorPoint operator -(LeditorPoint a, float b) => new(a.loch - b, a.locv - b);

        public static LeditorPoint operator -(float a, LeditorPoint b) => new(a - b.loch, a - b.locv);

        public static LeditorPoint operator *(LeditorPoint a, LeditorPoint b) => new(a.loch * b.loch, a.locv * b.locv);

        public static LeditorPoint operator *(LeditorPoint a, float b) => new(a.loch * b, a.locv * b);

        public static LeditorPoint operator *(float a, LeditorPoint b) => new(a * b.loch, a * b.locv);

        public static LeditorPoint operator /(LeditorPoint a, LeditorPoint b) => new(a.loch / b.loch, a.locv / b.locv);

        public static LeditorPoint operator /(LeditorPoint a, float b) => new(a.loch / b, a.locv / b);

        public static LeditorPoint operator /(float a, LeditorPoint b) => new(a / b.loch, a / b.locv);

        public static LeditorPoint operator %(LeditorPoint a, LeditorPoint b) => new(a.loch % b.loch, a.locv % b.locv);

        public static LeditorPoint operator %(LeditorPoint a, float b) => new(a.loch % b, a.locv % b);

        public static LeditorPoint operator %(float a, LeditorPoint b) => new(a % b.loch, a % b.locv);

        public static LeditorPoint operator -(LeditorPoint a) => new(-a.loch, -a.locv);

        public static bool operator ==(LeditorPoint a, LeditorPoint b) => a.Equals(b);

        public static bool operator !=(LeditorPoint a, LeditorPoint b) => !a.Equals(b);

        public override bool Equals(object? obj) {
            return obj is LeditorPoint b && Equals(b);
        }

        public bool Equals(LeditorPoint b) {
            return loch.Equals(b.loch) && locv.Equals(b.locv);
        }

        public override int GetHashCode() {
            return HashCode.Combine(loch, locv);
        }

    }
}

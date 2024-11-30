using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser {
    public class LeditorColor {
        public byte red;
        public byte green;
        public byte blue;

        public LeditorColor() {
            this.red = 0;
            this.green = 0;
            this.blue = 0;
        }

        public LeditorColor(byte a) {
            this.red = a;
            this.green = a;
            this.blue = a;
        }

        public LeditorColor(byte red, byte green, byte blue) {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public byte this[int index] {
            get {
                if (index == 0) return this.red;
                if (index == 1) return this.green;
                if (index == 2) return this.blue;
                throw new IndexOutOfRangeException();
            }
            set {
                if (index == 0) this.red = value;
                if (index == 1) this.green = value;
                if (index == 2) this.blue = value;
                if (index != 0 && index != 1 && index != 2) throw new IndexOutOfRangeException();
            }
        }

        public override string ToString() => $"color({red}, {green}, {blue})";

        public static bool operator ==(LeditorColor a, LeditorColor b) => a.Equals(b);

        public static bool operator !=(LeditorColor a, LeditorColor b) => !a.Equals(b);

        public override bool Equals(object? obj) {
            return obj is LeditorColor b && Equals(b);
        }

        public bool Equals(LeditorColor b) {
            return red.Equals(b.red) && green.Equals(b.green) && blue.Equals(b.blue);
        }

        public override int GetHashCode() {
            return HashCode.Combine(red, green, blue);
        }

    }
}

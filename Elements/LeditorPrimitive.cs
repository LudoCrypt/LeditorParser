using LeditorParser.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser.Elements {
    public class LeditorPrimitive : LeditorElement {
        private object value;

        public LeditorPrimitive(object value) {
            this.value = value;
        }

        public override LeditorElement DeepCopy() {
            throw new NotImplementedException();
        }
        public override bool GetBool() {
            if (IsBool()) {
                return (bool)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type bool");
        }

        public override float GetFloat() {
            if (IsFloat()) {
                return (float)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type float");
        }

        public override string GetString() {
            if (IsString()) {
                return (string)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type string");
        }

        public override LeditorColor GetColor() {
            if (IsColor()) {
                return (LeditorColor)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type color");
        }

        public override LeditorPoint GetPoint() {
            if (IsPoint()) {
                return (LeditorPoint)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type point");
        }

        public override LeditorRect GetRect() {
            if (IsRect()) {
                return (LeditorRect)value;
            }
            throw new NotSupportedException($"Type '{this.GetType}' is not of type rect");
        }

        public override bool IsBool() {
            return value is bool;
        }

        public override bool IsFloat() {
            return value is float;
        }

        public override bool IsString() {
            return value is string;
        }

        public override bool IsColor() {
            return value is LeditorColor;
        }

        public override bool IsPoint() {
            return value is LeditorPoint;
        }

        public override bool IsRect() {
            return value is LeditorRect;
        }

        public override string AsString() {
            if (IsString()) {
                return "\"" + this.value.ToString() + "\"";
            }

            return this.value.ToString()!;
        }

    }
}

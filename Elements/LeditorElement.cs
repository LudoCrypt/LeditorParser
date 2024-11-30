using LeditorParser.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser.Elements {
    public abstract class LeditorElement {
        protected string? name;

        protected LeditorElement(string? name) {
            this.name = name;
        }

        protected LeditorElement() { }

        public abstract LeditorElement DeepCopy();

        public string? Name() {
            return this.name;
        }

        public LeditorElement Name(string newName) {
            this.name = newName;
            return this;
        }

        public bool Named() {
            return this.name != null;
        }

        public override string? ToString() {
            if (Named()) {
                return "#" + Name() + ": " + AsString();
            }
            return AsString();
        }

        public abstract string AsString();

        public virtual bool GetBool() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual float GetFloat() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual string GetString() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual LeditorColor GetColor() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual LeditorPoint GetPoint() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual LeditorRect GetRect() {
            throw new NotSupportedException($"Type '{this.GetType}' is not primitive");
        }

        public virtual bool IsBool() {
            return false;
        }

        public virtual bool IsFloat() {
            return false;
        }

        public virtual bool IsString() {
            return false;
        }

        public virtual bool IsColor() {
            return false;
        }

        public virtual bool IsPoint() {
            return false;
        }

        public virtual bool IsRect() {
            return false;
        }

        public static implicit operator LeditorElement(bool value) => new LeditorPrimitive(value);
        public static implicit operator bool(LeditorElement element) => element.GetBool();

        public static implicit operator LeditorElement(float value) => new LeditorPrimitive(value);
        public static implicit operator float(LeditorElement element) => element.GetFloat();

        public static implicit operator LeditorElement(string value) => new LeditorPrimitive(value);
        public static implicit operator string(LeditorElement element) => element.GetString();

        public static implicit operator LeditorElement(LeditorColor value) => new LeditorPrimitive(value);
        public static implicit operator LeditorColor(LeditorElement element) => element.GetColor();

        public static implicit operator LeditorElement(LeditorPoint value) => new LeditorPrimitive(value);
        public static implicit operator LeditorPoint(LeditorElement element) => element.GetPoint();

        public static implicit operator LeditorElement(LeditorRect value) => new LeditorPrimitive(value);
        public static implicit operator LeditorRect(LeditorElement element) => element.GetRect();

    }
}

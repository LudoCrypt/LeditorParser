using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LeditorParser.Elements {
    public class LeditorArray : LeditorElement {
        private Dictionary<string, LeditorElement> namedMembers = [];
        private List<LeditorElement> members = [];

        public LeditorArray(string? name) : base(name) { }

        public LeditorArray() : base() { }

        public override LeditorElement DeepCopy() {
            throw new NotImplementedException();
        }

        public override string AsString() {
            return "[" + String.Join(", ", members) + "]";
        }

        public LeditorArray Add(LeditorElement element) {
            members.Add(element);
            return this;
        }

        public LeditorArray Add(string name, LeditorElement element) {
            members.Add(element);

            if (!name.Equals("")) {
                namedMembers.Add(name, element.Name(name));
            }

            return this;
        }

        public LeditorElement this[int index] {
            get {
                return members[index];
            }
            set {
                members[index] = value;
            }
        }

        public LeditorElement this[string name] {
            get {
                return namedMembers[name];
            }
            set {
                namedMembers[name] = value;
            }
        }
    }
}

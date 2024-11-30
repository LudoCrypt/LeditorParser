using LeditorParser.Elements;
using LeditorParser.Types;

namespace LeditorParser {

    public class LeditorParser {

        public int line = 1;
        public int index = 1;

        private readonly CLeditorArrayParser builder;

        public LeditorParser() {
            builder = new CLeditorArrayParser(this);
        }

        public LeditorArray Parse(FileInfo file) {
            using (var reader = file.OpenText()) {
                int ch;
                while ((ch = reader.Read()) != -1) {
                    Step((char)ch);
                }
            }
            return builder.pool;
        }

        public LeditorArray Parse(string s) {
            foreach (char c in s) {
                Step(c);
            }

            return builder.pool;
        }

        public void Step(char c) {
            if (c == '\n') {
                line++;
                index = 1;
            }

            try {
                builder.Step(c);
            }
            catch {
                Console.Write($"Found unexpected identifier '{c}' at line {line} index {index}. ");
                throw;
            }

            index++;
        }
    }

    public class CLeditorArrayParser {

        private LeditorParser parser;

        public CLeditorArrayParser(LeditorParser parser) {
            this.parser = parser;
        }

        private bool insideString;
        private string builtString = "";

        private bool insideName;
        private string builtName = "";

        private bool insideFloat;
        private string builtFloat = "";

        private string builtVectorString = "";

        private bool insideVector;
        private int vectorIndex = 0;
        private float[] builtVector = new float[4];

        private CLeditorArrayParser? parent = null;
        private CLeditorArrayParser? child = null;

        public LeditorArray pool = new();

        static void Unexpected(string message = "") {
            throw new NotSupportedException(message);
        }

        public void Step(char c) {

            if (child != null) {
                child.Step(c);
                return;
            }

            if (c == '\"') {
                if (insideName || insideFloat) {
                    Unexpected("Cannot find '\"' while parsing name or number");
                }

                insideString = !insideString;

                // Beginning a new string
                if (insideString) {
                    builtString = "";
                }
                // Ending a string
                else {
                    pool.Add(builtName, builtString);
                }
                return;
            }

            if (insideString) {
                builtString += c;
                return;
            }

            // Ignore whitespace
            if (c == ' ') {
                return;
            }

            if (c == '#') {
                if (insideName || insideFloat) {
                    Unexpected("Cannot find '#' while parsing name or number");
                }

                insideName = true;
                builtName = "";
                return;
            }

            if (c == ':') {
                if (!insideName) {
                    Unexpected();
                }

                insideName = false;
                return;
            }

            if (insideName) {
                builtName += c;
                return;
            }

            if (c == ',' || c == ']' || c == ')') {
                if (insideFloat) {
                    insideFloat = false;

                    // If building a vector, add to that instead of the pool
                    if (insideVector) {
                        builtVector[vectorIndex] = float.Parse(builtFloat);
                        vectorIndex++;
                    }
                    else {
                        pool.Add(builtName, float.Parse(builtFloat));
                    }

                    builtFloat = "";

                    // Commas serve no other purpose
                    if (c == ',') {
                        return;
                    }
                }
            }

            if (c == '-') {
                if (insideName || insideFloat) {
                    Unexpected("Cannot find '-' while parsing name or number");
                }
            }

            if (c == '-' || c == '.' || Char.IsDigit(c)) {
                insideFloat = true;
                builtFloat += c;
                return;
            }

            // There's probably a more elegant way of doing this

            // If p (point), r (rect), or c (color) are found
            if (c == 'p' || c == 'r' || c == 'c') {
                if (builtVectorString.Equals("")) {
                    builtVectorString += c;
                    return;
                }

                // In the middle of parsing vector
                switch (c) {
                    case 'p':
                        // Cannot find 'p' in the middle of a word
                        Unexpected();
                        break;
                    case 'r':
                        // Cannot find 'r' outside 'colo'r
                        if (!builtVectorString.Equals("colo")) {
                            Unexpected();
                        }
                        break;
                    case 'c':
                        // Cannot find 'c' outisde 're'ct
                        if (!builtVectorString.Equals("re")) {
                            Unexpected();
                        }
                        break;
                }
            }

            if (!insideVector) {
                if (!builtVectorString.Equals("")) {
                    builtVectorString += c;
                }
            }

            if (builtVectorString.Equals("point") || builtVectorString.Equals("rect") || builtVectorString.Equals("color")) {
                insideVector = true;

                if (c == '(') {
                    builtVector = new float[4];
                    vectorIndex = 0;
                }
                else if (c == ')') {
                    switch (builtVectorString) {
                        case "point":
                            pool.Add(builtName, new LeditorPoint(builtVector[0], builtVector[1]));
                            break;
                        case "rect":
                            pool.Add(builtName, new LeditorRect(builtVector[0], builtVector[1], builtVector[2], builtVector[3]));
                            break;
                        case "color":
                            pool.Add(builtName, new LeditorColor((byte)builtVector[0], (byte)builtVector[1], (byte)builtVector[2]));
                            break;
                    }

                    insideVector = false;
                    builtVectorString = "";
                }
            }

            if (c == '[') {
                // Cannot already have child
                if (child != null) {
                    Unexpected();
                }

                child = new CLeditorArrayParser(parser) {
                    parent = this
                };

                return;
            }
            else if (c == ']') {
                // Needs parent
                if (parent == null) {
                    Unexpected();
                }

                // Make the parent forget about child
                this.parent!.child = null;

                // Add child pool to parent pool under parent name
                this.parent!.pool.Add(this.parent!.builtName, this.pool);
                return;
            }

            // Ignore separator characters???? idk whether or not to throw
            if (Char.IsSeparator(c)) {
                return;
            }
        }
    }
}

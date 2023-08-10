using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParserLib {
    public class Command {
        public static Command Parse(string line) {
            Command cmd = new Command();
            line = line.Trim();
            int sindex = line.IndexOf(' ');
            cmd.command = line.Substring(0, sindex);

            if (line.Length > sindex + 1) {
                string rline = line.Substring(sindex + 1, line.Length - (sindex + 1));
                string[] segments = rline.Split(' ');
                List<Argument> args = new List<Argument>();

                for (int i = 0; i < segments.Length; i++) {
                    if (segments[i].StartsWith("-")) {
                        Argument arg = new Argument();
                        arg.header = segments[i].Substring(1, segments[i].Length - 1);
                        if (i < segments.Length - 1) {
                            if (!segments[i + 1].StartsWith("-")) {
                                arg.value = segments[i + 1];
                                arg.raw = arg.header + " " + arg.value;
                                i++;
                            }
                        }
                        args.Add(arg);
                    }
                    else {
                        Argument arg = new Argument();

                        arg.header = null;
                        arg.value = segments[i];
                        arg.raw = segments[i];

                        args.Add(arg);
                    }
                }

                cmd.args = args.ToArray();
            }

            return cmd;
        }

        public string command;
        public Argument[] args;
    }
    public class Argument {
        public string raw;

        public string header;
        public string value;
    }
}

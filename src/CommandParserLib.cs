using System.Collections.Generic;

namespace CommandParserLib {
    public class Command {
        public static Command Parse(string line) {
            Command cmd = new Command();
            line = line.Trim();
            int sindex = line.IndexOf(' ');
            if (sindex != -1) cmd.command = line.Substring(0, sindex);
            else {
                cmd.command = line;
                cmd.args = new Argument[0];
                return cmd;
            }

            if (line.Length > sindex + 1) {
                string rline = line.Substring(sindex + 1, line.Length - (sindex + 1));
                string[] segments = rline.Split(' ');
                List<Argument> args = new List<Argument>();

                for (int i = 0; i < segments.Length; i++) {
                    if (segments[i][0] == '-') {
                        Argument arg = new Argument();
                        arg.header = segments[i].TrimStart('-');
                        if (string.IsNullOrWhiteSpace(arg.header) || string.IsNullOrEmpty(arg.header)) throw new System.Exception("Arguments error");
                        if (i < segments.Length - 1 && segments[i + 1][0] != '-') {
                            arg.value = segments[i + 1];
                            arg.raw = segments[i] + ' ' + arg.value;
                            i++;
                        } else {
                            arg.raw = segments[i];
                        }
                        args.Add(arg);
                    } else {
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

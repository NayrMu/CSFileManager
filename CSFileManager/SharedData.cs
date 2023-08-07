using System;

namespace SharedDataSpace {
    public class SharedData {

        private string? workingDir { get; set; }
        public readonly string? homeDir;
        private string? commandInput;
        private DirectoryInfo? workingDirInfo { get; set; }

        public DirectoryInfo? WorkingDirInfo {
            get { return workingDirInfo; }
            set { workingDirInfo = value; }
        }


        public string WorkingDir {
            get { return workingDir; }

            set {
                if ( Directory.Exists(value) ) {
                    workingDir = value;
                }
            }
        }

        public string CommandInput {
            get { return commandInput; }
            set {
                if (value == "") {
                    throw new ArgumentException("Command not recognized.");
                }
                else {
                    commandInput = value;
                }
            }
        }
        public SharedData() {

            homeDir = @"Z:\TestDirectory";
            WorkingDir = homeDir;
            WorkingDirInfo = new DirectoryInfo(workingDir);
        }
    }
}
using CmpsBot.Utils;
using System;
using System.Collections.Generic;

namespace CmpsBot.Services {
    public class FileSystemService {
        private Dictionary<File, SyncSet> data;

        public FileSystemService() {
            this.data = new Dictionary<File, SyncSet>();
        }

        public SyncSet GetFile(File file) {
            return this.data[file];
        }

        public void RegisterFile(File file, string path) {
            this.registerFile(file, path, new TimeSpan(0, 5, 0));
        }

        public void RegisterFile(File file, string path, TimeSpan interval) {
            this.registerFile(file, path, interval);
        }

        private void registerFile(File file, string path, TimeSpan interval) {
            this.data[file] = new SyncSet(path, interval);
        }
    }
    public enum File {
        AnonymouseRoles
    }
}
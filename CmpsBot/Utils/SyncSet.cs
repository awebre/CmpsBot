using System.Collections.Generic;
using System;
using System.IO;

namespace CmpsBot.Utils {
    public class SyncSet : IDisposable {
        public SyncSet(string path, TimeSpan interval) {
            this.fullPath = path;
            this.data = new HashSet<string>();
            this.LoadInitialData();

            this.timer = new System.Threading.Timer(this.Save , null, interval, interval);
        }
        private string fullPath;
        private System.Threading.Timer timer;
        private HashSet<string> data;

        public void Add(string item) {
            this.data.Add(item);
        }

        public void Remove(string item) {
            this.data.Remove(item);
        }

        public bool Contains(string item) {
            return this.data.Contains(item);
        }

        public IEnumerable<string> Get() {
            return this.data;
        }

        public void Save(object info)
        {
            this.Save();
        }
        public void Save() {
            Console.WriteLine("I'm saving!");
            using (StreamWriter sw = new StreamWriter(this.fullPath))
            {
                foreach (var role in this.data)
                {
                    sw.WriteLine(role);
                }
            }
        }

        private void LoadInitialData() {
            if(System.IO.File.Exists(this.fullPath)) {
                foreach(var role in System.IO.File.ReadAllLines(this.fullPath)) {
                        this.Add(role);
                }
            } else {
                System.IO.File.Create(this.fullPath);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.timer.Dispose();
                    this.Save();
                    this.data = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SyncList()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;

using NWS.Requests;

namespace NWS.Factory {
    public class NwsManager {
        private DispatcherTimer _t = new DispatcherTimer();

        public event EventHandler<ItemEventArgs<List<CapMessage>>> UpdatedNwsEvent;
        public bool IsRunning { get; private set; }
        public NwsCapRequest.LocationType LocationType { get; set; }
        public string LocationCode { get; set; }
        public TimeSpan RequestInterval { get; set; }

        public NwsManager() {
            IsRunning = false;
            _t.Tick += (o, e) => Go();
        }

        public void Start() {
            IsRunning = true;
            _t.Interval = RequestInterval;
            Go();
            _t.Start();
        }

        public void Stop() {
            _t.Stop();
            IsRunning = false;
        }

        private void Go() {
            ThreadPool.SetMaxThreads(1, 1);
            ThreadPool.QueueUserWorkItem(RequestFeed, "");
        }

        private void RequestFeed(object obj) {
            try {
                if (!IsRunning) {
                    return;
                }

                Debug.Write("Fetching results");
                List<CapMessage> results = NwsCapRequest.Request(LocationType, LocationCode);

                if (results == null) {
                    Debug.WriteLine("Request cannot be completed to to null results");
                    return;
                } else {
                    Debug.WriteLine("Fetched results");
                }

                if (UpdatedNwsEvent != null) {
                    UpdatedNwsEvent(this, new ItemEventArgs<List<CapMessage>>(results));
                }
            } catch {
                //not implemented
            }
        }
    }
}
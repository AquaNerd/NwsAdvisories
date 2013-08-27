using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using MapControl;

using NWS;
using NWS.Requests;
using NWS.Factory;
using NWS.Util;

namespace AdvisoryViewer.Shell {
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Screen {
        private NwsManager _manager;

        private ObservableCollection<CapMessage> _nwsCapList = new ObservableCollection<CapMessage>();
        public ObservableCollection<CapMessage> NwsCapList {
            get { return _nwsCapList; }
            set {
                _nwsCapList = value;
                NotifyOfPropertyChange(() => NwsCapList);
            }
        }

        private ObservableCollection<CountyZone> _shapeList = new ObservableCollection<CountyZone>();
        public ObservableCollection<CountyZone> ShapeList {
            get { return _shapeList; }
            set {
                _shapeList = value;
                NotifyOfPropertyChange(() => ShapeList);
            }
        }

        private List<CountyZone> _countyShapeList = new List<CountyZone>();
        public List<CountyZone> CountyShapeList {
            get { return _countyShapeList; }
            set {
                _countyShapeList = value;
                NotifyOfPropertyChange(() => CountyShapeList);
            }
        }

        private bool _isSettingsFlyoutOpen = false;
        public bool IsSettingsFlyoutOpen {
            get { return _isSettingsFlyoutOpen; }
            set {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }

        [ImportingConstructor]
        public ShellViewModel() {
            DisplayName = "NWS Advisories";

            _manager = new NwsManager() {
                RequestInterval = TimeSpan.FromMinutes(1),
                LocationType = NwsCapRequest.LocationType.Country,
                LocationCode = "US"
            };

            _manager.UpdatedNwsEvent += _manager_UpdatedNwsEvent;
        }

        private void _manager_UpdatedNwsEvent(object sender, ItemEventArgs<List<CapMessage>> e) {
            ObservableCollection<CapMessage> temp = new ObservableCollection<CapMessage>();
            foreach (CapMessage msg in e.Item) {
                temp.Add(msg);
                
            }
            NwsCapList = temp;
        }

        protected override void OnViewLoaded(object view) {
            _manager.Start();

            //CountyShapeList = ZoneCoordinates.GetCountyZoneInformation("./Shapes/County/CountyShape.shp", "CntyPubForcastZones.dbx");
        }

        protected override void OnDeactivate(bool close) {
            _manager.Stop();
        }

        public void OpenSettings() {
            IsSettingsFlyoutOpen = true;
        }
    }
}
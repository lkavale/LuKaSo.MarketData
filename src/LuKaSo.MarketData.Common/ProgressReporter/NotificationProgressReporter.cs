using LuKaSo.MarketData.Infrastructure.Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LuKaSo.MarketData.Common.ProgressReporter
{
    public class NotificationProgressReporter : CommonProgressReporter, INotifyPropertyChanged
    {
        /// <summary>
        /// Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region Methods

        /// <summary>
        /// This method is called by the Set accessor of each property.
        /// 
        /// The CallerMemberName attribute that is applied to the optional propertyName
        /// parameter causes the property name of the caller to be substituted as an argument.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Progress status value (eg 0-100%)
        /// </summary>
        public override double Progress
        {
            get
            {
                return 100 * (double)Items / (double)TotalItems;
            }
        }

        /// <summary>
        /// Items
        /// </summary>
        private long _items;

        public override long Items
        {
            get
            {
                return _items;
            }
            protected set
            {
                _items = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged("Progress");
            }
        }

        /// <summary>
        /// Status
        /// </summary>
        private ProgressReporterStatus _status;

        public override ProgressReporterStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                HandleStatusChange(_status, value);

                _status = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Duration
        /// </summary>
        private TimeSpan _duration;

        public override TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Elapsed
        /// </summary>
        private TimeSpan _elapsed;

        public override TimeSpan Elapsed
        {
            get
            {
                return _elapsed;
            }
            set
            {
                _elapsed = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}

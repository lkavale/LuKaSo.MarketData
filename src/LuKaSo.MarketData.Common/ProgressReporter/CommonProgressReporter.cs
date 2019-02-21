using LuKaSo.MarketData.Infrastructure.Common;
using System;

namespace LuKaSo.MarketData.Common.ProgressReporter
{
    public abstract class CommonProgressReporter : IProgressReporter
    {
        /// <summary>
        /// Progress change event
        /// </summary>
        public event ProgressChangeEventHandler ProgressChanged;

        #region Methods

        /// <summary>
        /// Report progress
        /// </summary>
        /// <param name="value"></param>
        public virtual void Report(long value)
        {
            Items = value;

            CalculateLinearPrediction();

            ProgressChanged?.Invoke(this, new ProgressReporterEventArgs(Progress, Items));
        }

        /// <summary>
        /// Predict task duration and update elapsed time
        /// </summary>
        protected void CalculateLinearPrediction()
        {
            Elapsed = DateTime.Now - StartTime;
            long durationTicks = (long)(((double)Elapsed.Ticks / (double)Items) * (double)TotalItems);
            Duration = new TimeSpan(durationTicks);
        }

        /// <summary>
        /// Manage time values depends on status changes
        /// </summary>
        /// <param name="statusFrom">Initial state</param>
        /// <param name="statusTo">Target state</param>
        protected void HandleStatusChange(ProgressReporterStatus statusFrom, ProgressReporterStatus statusTo)
        {
            if (statusFrom != ProgressReporterStatus.Running && statusTo == ProgressReporterStatus.Running)
            {
                StartTime = DateTime.Now;
                EndTime = null;

                Duration = new TimeSpan(0);
                Elapsed = new TimeSpan(0);

                return;
            }

            if (statusFrom == ProgressReporterStatus.Running && statusTo != ProgressReporterStatus.Running)
            {
                EndTime = DateTime.Now;

                Duration = new TimeSpan(0);
                Elapsed = (EndTime ?? DateTime.Now) - StartTime;
            }
        }

        /// <summary>
        /// Starts progress
        /// </summary>
        /// <param name="totalItems"></param>
        public virtual void Start(long totalItems)
        {
            TotalItems = totalItems;
            Status = ProgressReporterStatus.Running;
        }

        /// <summary>
        /// Finish
        /// </summary>
        public virtual void Finish()
        {
            Status = ProgressReporterStatus.Finished;
        }

        /// <summary>
        /// Failed
        /// </summary>
        public void Failed()
        {
            Status = ProgressReporterStatus.Failed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Progress status value (eg 0-100%)
        /// </summary>
        public virtual double Progress
        {
            get
            {
                return 100 * (double)Items / (double)TotalItems;
            }
        }

        /// <summary>
        /// Items
        /// </summary>
        public virtual long Items { get; protected set; }

        /// <summary>
        /// Total items
        /// </summary>
        public virtual long TotalItems { get; protected set; }

        /// <summary>
        /// Status
        /// </summary>
        private ProgressReporterStatus _status;

        public virtual ProgressReporterStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                HandleStatusChange(_status, value);

                _status = value;
            }
        }

        /// <summary>
        /// Start time
        /// </summary>
        public virtual DateTime StartTime { get; protected set; }

        /// <summary>
        /// End time
        /// </summary>
        public virtual DateTime? EndTime { get; protected set; }

        /// <summary>
        /// Duration
        /// </summary>
        public virtual TimeSpan Duration { get; set; }

        /// <summary>
        /// Elapsed
        /// </summary>
        public virtual TimeSpan Elapsed { get; set; }

        #endregion
    }
}

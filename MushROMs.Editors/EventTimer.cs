using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    /// <summary>
    /// A timer which can attach to multiple <see cref="EventWatch"/>
    /// classes for raising events.
    /// </summary>
    public class EventTimer : Timer
    {
        #region Fields
        /// <summary>
        /// The start time when the <see cref="EventTimer"/> was last
        /// enabled.
        /// </summary>
        private long startTime;
        /// <summary>
        /// The time elapsed, in milliseconds, since the
        /// <see cref="EventTimer"/> was last enabled.
        /// </summary>
        private long elapsedTime;

        /// <summary>
        /// A collection of <see cref="EventWatch"/> watches to call
        /// events for.
        /// </summary>
        private List<EventWatch> watches;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the start time when the <see cref="EventTimer"/> was
        /// last enabled.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime StartTime
        {
            get { return new DateTime(startTime * TimeSpan.TicksPerMillisecond); }
        }
        /// <summary>
        /// Gets the time elapsed, in milliseconds, since the
        /// <see cref="EventTimer"/> was last enabled.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long ElapsedTime
        {
            get { return this.elapsedTime; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref="EventTimer"/> should raise the
        /// <see cref="Timer.Elapsed"/> event.
        /// </summary>
        [TimersDescription("TimerEnabled")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                if (base.Enabled = value)
                {
                    this.startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    this.elapsedTime = 0;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTimer"/>
        /// class, and sets all the properties to their initial values.
        /// </summary>
        public EventTimer()
        {
            this.Elapsed += new ElapsedEventHandler(EventTimer_Elapsed);

            this.startTime =
            this.elapsedTime = 0;

            this.watches = new List<EventWatch>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTimer"/>
        /// class, and sets the <see cref="Timer.Interval"/> property to
        /// the specified time period.
        /// </summary>
        /// <param name="interval">
        /// The time, in milliseconds, between events.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The value of the interval parameter is less than or equal to
        /// zero, or greater than <see cref="Int32.MaxValue"/>.
        /// </exception>
        public EventTimer(double interval)
        {
            this.Interval = interval;
            this.Elapsed += new ElapsedEventHandler(EventTimer_Elapsed);

            this.startTime =
            this.elapsedTime = 0;

            this.watches = new List<EventWatch>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts raising the <see cref="Timer.Elapsed"/> event by
        /// setting <see cref="EventTimer.Enabled"/> to true.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <see cref="Timer.Elapsed"/> is created with an interval
        /// equal to or greater than <see cref="Int32.MaxValue"/> + 1, or
        /// set to an interval less than zero.
        /// </exception>
        public new void Start()
        {
            this.startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            this.elapsedTime = 0;

            base.Start();
        }

        /// <summary>
        /// Adds a <see cref="EventWatch"/> to attach to the
        /// <see cref="EventTimer"/> class.
        /// </summary>
        /// <param name="watch">
        /// A reference to the <see cref="EventWatch"/> class to attach.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="watch"/> is null.
        /// </exception>
        public void AddWatch(EventWatch watch)
        {
            if (watch == null)
                throw new ArgumentNullException(Resources.ErrorWatchNull);

            for (int i = this.watches.Count; --i >= 0; )
                if (watches[i] == watch)
                    return;

            watches.Add(watch);
        }

        /// <summary>
        /// Removes the specified <see cref="EventWatch"/> from the
        /// <see cref="EventTimer"/> class. If the watch is not attached,
        /// then no action is done.
        /// </summary>
        /// <param name="watch">
        /// A reference to the <see cref="EventWatch"/> to detach.
        /// </param>
        /// <remarks>
        /// If <paramref name="watch"/> is null, then no action is done
        /// and no exception is thrown.
        /// </remarks>
        public void RemoveWatch(EventWatch watch)
        {
            if (watch == null)
                return;

            for (int i = this.watches.Count; --i >= 0; )
                if (watches[i] == watch)
                    watches.RemoveAt(i);
        }

        private void EventTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Not sure if better to use the current time over the signal time. Check for inconsistencies one day...
            this.elapsedTime = (e.SignalTime.Ticks / TimeSpan.TicksPerMillisecond) - this.startTime;

            for (int i = this.watches.Count; --i >= 0; )
            {
                EventWatch watch = this.watches[i];

                if (this.elapsedTime - watch.total >= watch.interval + watch.offset && (watch.maxCalls == 0 || watch.calls < watch.maxCalls))
                {
                    // This prevents multiple calls during delays
                    while (this.elapsedTime - watch.total >= watch.interval + watch.offset)
                        watch.total += watch.interval;

                    watch.calls++;
                    if (watch.maxCalls != 0)
                        watch.maxCalls++;
                    watch.InvokeElapsed(e);
                }
            }
        }
        #endregion
    }
}
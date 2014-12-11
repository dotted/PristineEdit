namespace BadMechanics.PristineEdit.Common.Data.Loggers
{
    using System;
    using System.Text;

    using BadMechanics.PristineEdit.Common.Events;

    using global::Common.Logging;
    using global::Common.Logging.Simple;

    using Microsoft.Practices.Prism.PubSubEvents;

    #region License

    /*
 * Copyright © 2002-2009 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

    #endregion


    /// <summary>
    /// Sends log messages to <see cref="System.Diagnostics.Debug" />.
    /// </summary>
    /// <author>Gilles Bayon</author>
#if !PORTABLE
        [Serializable]
#endif

    public class EventAggregatorLogger : AbstractSimpleLogger
    {
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Creates and initializes a logger that writes messages to an event aggregator file.
        /// </summary>
        /// <param name="logName">The key, usually type key of the calling class, of the logger.</param>
        /// <param name="logLevel">The current logging threshold. Messages recieved that are beneath this threshold will not be logged.</param>
        /// <param name="showLevel">Include the current log level in the log message.</param>
        /// <param name="showDateTime">Include the current time in the log message.</param>
        /// <param name="showLogName">Include the instance key in the log message.</param>
        /// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
        /// <param name="eventAggregator">The event aggregator</param>
        public EventAggregatorLogger(
            string logName,
            LogLevel logLevel,
            bool showLevel,
            bool showDateTime,
            bool showLogName,
            string dateTimeFormat,
            IEventAggregator eventAggregator)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            this.eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
        /// sending the output to the event aggregator.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel" /> of the message.</param>
        /// <param name="message">The log message.</param>
        /// <param name="e">An optional <see cref="Exception" /> associated with the message.</param>
        protected override void WriteInternal(LogLevel level, object message, Exception e)
        {
            // Use a StringBuilder for better performance
            var sb = new StringBuilder();
            FormatOutput(sb, level, message, e);

            // Print to the appropriate destination
            this.eventAggregator.GetEvent<LogEvent>().Publish(sb.ToString());
        }
    }
}
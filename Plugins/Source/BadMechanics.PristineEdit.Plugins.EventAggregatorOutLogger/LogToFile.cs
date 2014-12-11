namespace BadMechanics.PristineEdit.Plugins.EventAggregatorOutLogger
{
    using System.Composition;
    using System.IO;
    using System.Text;

    using BadMechanics.PristineEdit.Common.Events;
    using BadMechanics.PristineEdit.Common.Interfaces;

    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    [Export(typeof(IPlugin))]
    public class LogToFile : IPlugin
    {

        private readonly IEventAggregator eventAggregator;

        private IFile file;

        private Stream stream;

        private SubscriptionToken subscriptionToken;

        private SubscriptionToken fileSubscriptionToken;

        [ImportingConstructor]
        public LogToFile(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            var fileOpenedEvent = this.eventAggregator.GetEvent<FileOpenedEvent>();
            this.fileSubscriptionToken = fileOpenedEvent.Subscribe(FileOpenedEventHandler);

            var logEvent = this.eventAggregator.GetEvent<LogEvent>();
            this.subscriptionToken = logEvent.Subscribe(LogActionEventHandler);
        }

        private async void FileOpenedEventHandler(IFile logFile)
        {
            this.file = logFile;
            this.stream = await file.OpenAsync(FileAccess.ReadAndWrite);
        }

        public string Name
        {
            get
            {
                return "LogToFile";
            }
        }

        private async void LogActionEventHandler(string logMessage)
        {
            if (this.file == null)
            {
                return;
            }
            var bytes = Encoding.UTF8.GetBytes(logMessage + "\r\n");
            await this.stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
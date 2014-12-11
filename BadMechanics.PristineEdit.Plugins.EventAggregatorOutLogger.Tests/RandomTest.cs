using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMechanics.PristineEdit.Plugins.EventAggregatorOutLogger.Tests
{
    using System.Collections;

    using BadMechanics.PristineEdit.Common.Events;

    using FluentAssertions;

    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    using Rhino.Mocks;

    using Xunit;

    public class RandomTest
    {
        [Fact]
        public void NameIsLogToFile()
        {
            // oh god why
            var fileOpenedEvent = MockRepository.GenerateStub<FileOpenedEvent>();
            var logEvent = MockRepository.GenerateStub<LogEvent>();

            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            eventAggregator.Stub(aggregator => aggregator.GetEvent<FileOpenedEvent>()).Return(fileOpenedEvent);
            eventAggregator.Stub(aggregator => aggregator.GetEvent<LogEvent>()).Return(logEvent);

            var fileOpenedAction = MockRepository.GenerateStub<Action<IFile>>();
            fileOpenedEvent.Stub(@event => @event.Subscribe(fileOpenedAction));

            var logAction = MockRepository.GenerateStub<Action<string>>();
            logEvent.Stub(@event => @event.Subscribe(logAction));
            var logToFile = new LogToFile(eventAggregator);

            logToFile.Name.Should().BeEquivalentTo("LogToFile");
        }
    }
}
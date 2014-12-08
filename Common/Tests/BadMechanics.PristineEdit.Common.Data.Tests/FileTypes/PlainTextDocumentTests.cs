namespace BadMechanics.PristineEdit.Common.Data.Tests.FileTypes
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using BadMechanics.PristineEdit.Common.Data.FileTypes;

    using FluentAssertions;

    using Xunit;

    public class PlainTextDocumentTests
    {
        [Fact]
        public void CanInstantiate()
        {
            var task = new Task<Stream>(() => new MemoryStream());
            Action a = () => new PlainTextDocument(task);
            a.ShouldNotThrow();
        }
    }
}

using Xunit;
using BadMechanics.PristineEdit.Common.Data;

namespace BadMechanics.PristineEdit.Common.Data.Tests
{
    using System.IO;
    using System.Text;

    using FluentAssertions;

    using File = BadMechanics.PristineEdit.Common.Data.File;

    public class FileTests
    {
        [Fact]
        public void OpenTest()
        {
            //Assert.True(false, "not implemented yet");
        }

        [Fact]
        public void DetectTextEncodingTest()
        {
            var testString = "testTest123";
            var bytes = Encoding.ASCII.GetBytes(testString);
            var stream = new MemoryStream(bytes);
            var encoding = File.DetectTextEncoding(stream);
            encoding.EncodingName.Should().BeEquivalentTo("us-ascii");
        }
    }
}

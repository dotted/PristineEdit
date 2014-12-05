// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Document type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The document.
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// The backing stream.
        /// </summary>
        private readonly Task<Stream> backingStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="backingStream">
        /// The backing stream.
        /// </param>
        protected Document(Task<Stream> backingStream)
        {
            this.backingStream = backingStream;

            using (var sr = new StreamReader(backingStream.Result, detectEncodingFromByteOrderMarks: true))
            {
                this.Encoding = sr.CurrentEncoding;
            }
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Task<Stream> Stream
        {
            get
            {
                return this.backingStream;
            }
        }

        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public async void Write(string text)
        {
            var bytes = this.Encoding.GetBytes(text);
            await this.Stream.Result.WriteAsync(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var sr = new StreamReader(this.backingStream.Result, this.Encoding);
            return sr.ReadToEnd();
        }
    }
}

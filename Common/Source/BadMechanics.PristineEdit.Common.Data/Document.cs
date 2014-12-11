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

    using global::Common.Logging;

    /// <summary>
    /// The document.
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILog Log;

        /// <summary>
        /// The backing stream.
        /// </summary>
        private readonly Task<Stream> backingStream;

        /// <summary>
        /// the current encoding of the document
        /// </summary>
        private Encoding encoding;

        static Document()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="backingStream">
        /// The backing stream.
        /// </param>
        protected Document(Task<Stream> backingStream)
        {
            this.backingStream = backingStream;
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Task<Stream> Stream
        {
            get
            {
                Log.Trace(m => m("Get stream"));
                return this.backingStream;
            }
        }

        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                Log.Trace(m => m("Get encoding"));
                if (this.encoding == null)
                {
                    this.encoding = File.DetectTextEncoding(this.backingStream.Result);
                }
                return this.encoding;
            }
            set
            {
                // TODO: actually convert the text into the new encoding instead of just changing the property
                Log.Debug(m => m("Set encoding to {0}", value.WebName));
                this.encoding = value;
            }
        }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public async void Write(string text)
        {
            Log.Debug(m => m("Writing {0} to stream", text));
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

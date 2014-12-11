namespace BadMechanics.PristineEdit.Common.Events
{
    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    public class FileOpenedEvent : PubSubEvent<IFile>
    {
    }
}

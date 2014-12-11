namespace BadMechanics.PristineEdit.Common.Data
{
    using System.Composition;

    using Microsoft.Practices.Prism.PubSubEvents;

    [Export]
    public class ExportableEventAggregator : EventAggregator
    {
    }
}

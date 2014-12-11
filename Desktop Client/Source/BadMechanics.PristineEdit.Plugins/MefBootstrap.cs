namespace BadMechanics.PristineEdit.Plugins
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Composition.Convention;
    using System.Composition.Hosting;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BadMechanics.PristineEdit.Common.Interfaces;

    using global::Common.Logging;

    using Microsoft.Practices.Prism.MefExtensions.Events;
    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    public class MefBootstrap : IBootstrap
    {
        private static readonly ILog Log;

        private readonly IFolder assembliesFolder;

        private readonly string fileExtension;

        private readonly ICollection<Assembly> assemblies;

        private readonly IEventAggregator eventAggregator;

        private CompositionHost compositionHost;

        private readonly ObservableCollection<IPlugin> observableCollection;

        static MefBootstrap()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="assembliesFolder"></param>
        public MefBootstrap(string fileExtension, IFolder assembliesFolder):this(fileExtension, assembliesFolder, new EventAggregator(), new HashSet<Assembly>())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="assembliesFoler"></param>
        /// <param name="eventAggregator"></param>
        public MefBootstrap(string fileExtension, IFolder assembliesFoler, IEventAggregator eventAggregator)
            : this(fileExtension, assembliesFoler, eventAggregator, new HashSet<Assembly>())
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="assembliesFolder"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="assemblies"></param>
        public MefBootstrap(string fileExtension, IFolder assembliesFolder, IEventAggregator eventAggregator, ICollection<Assembly> assemblies)
        {
            this.assembliesFolder = assembliesFolder;
            this.assemblies = assemblies;
            this.eventAggregator = eventAggregator;
            this.observableCollection = new ObservableCollection<IPlugin>();

            // Make sure we are finding files ending in .dll
            if (fileExtension.StartsWith("."))
            {
                this.fileExtension = fileExtension;
            }
            else
            {
                this.fileExtension = "." + fileExtension;
            }
            this.InitializePlugins();
        }

        /// <summary>
        /// Helper method to load assemblies and set them up
        /// </summary>
        public async void InitializePlugins()
        {
            Log.Debug(m => m("Initialize plugins"));
            var files = await this.ScanDirectory();
            this.LoadAssemblies(files);
            this.CreateMefContainer();
            this.GetExports();

        }

        /// <summary>
        /// Scan plugin directory for dlls
        /// </summary>
        /// <returns></returns>
        private async Task<HashSet<IFile>> ScanDirectory()
        {
            Log.Debug(m => m("Scan directory"));
            var files = await assembliesFolder.GetFilesAsync();
            var assemblies = new HashSet<IFile>();
            foreach (var file in files.Where(file => file.Name.EndsWith(this.fileExtension)))
            {
                // Fix warning about using foreach variable inside closure
                var temp = file;
                Log.Trace(m => m("Adding assmbly: {0}", temp.Name));
                assemblies.Add(temp);
            }
            return assemblies;
        }

        /// <summary>
        /// Load dll files
        /// </summary>
        /// <param name="files"></param>
        private void LoadAssemblies(IEnumerable<IFile> files)
        {
            Log.Debug(m => m("Load assemblies"));
            foreach (var file in files)
            {

                var assemblyName = AssemblyName.GetAssemblyName(file.Path);
                var assembly = Assembly.Load(assemblyName);
                this.assemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Create container for plugin assemblies and export our event aggregator to them
        /// </summary>
        private void CreateMefContainer()
        {
            Log.Debug(m => m("Create mef container"));
            var conventions = new ConventionBuilder();

            conventions.ForTypesDerivedFrom<IFile>().Export().Shared();
            conventions.ForTypesDerivedFrom<IEventAggregator>().Export<MefEventAggregator>().Shared();

            var configuration = new ContainerConfiguration().WithAssemblies(this.assemblies, conventions);

            this.compositionHost = configuration.CreateContainer();
        }

        /// <summary>
        /// Get the Exports from all loaded plugins
        /// </summary>
        private void GetExports()
        {
            Log.Debug(m => m("Get exports"));
            var exports = this.compositionHost.GetExports<IPlugin>();
            foreach (var export in exports)
            {
                //export.Initialize(this.eventAggregator);
                this.observableCollection.Add(export);
            }
        }
    }
}

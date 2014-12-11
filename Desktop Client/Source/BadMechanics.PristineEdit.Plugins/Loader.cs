namespace BadMechanics.PristineEdit.Plugins
{
    using System.Collections.Generic;
    using System.Composition.Convention;
    using System.Composition.Hosting;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BadMechanics.PristineEdit.Common.Interfaces;

    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    public class Loader
    {
        private readonly string pluginDirectoryPath;

        private readonly string fileExtension;

        private readonly ICollection<Assembly> assemblies;

        private readonly IEventAggregator eventAggregator;

        private CompositionHost compositionHost;

        public Loader(string pluginDirectoryPath, string fileExtension):this(pluginDirectoryPath, fileExtension, new HashSet<Assembly>(), new EventAggregator())
        {
        }

        public Loader(string pluginDirectoryPath, string fileExtension, ICollection<Assembly> assemblies, EventAggregator eventAggregator)
        {
            this.assemblies = assemblies;
            this.eventAggregator = eventAggregator;

            this.pluginDirectoryPath = pluginDirectoryPath;

            // Make sure we are finding files ending in .dll
            if (fileExtension.StartsWith("."))
            {
                this.fileExtension = fileExtension;
            }
            else
            {
                this.fileExtension = "." + fileExtension;
            }
        }

        public async void InitializePlugins()
        {
            var files = await this.ScanDirectory();
            this.LoadAssemblies(files);
            this.CreateMefContainer();
            this.GetExports();

        }

        private async Task<HashSet<IFile>> ScanDirectory()
        {
            var folder = await FileSystem.Current.GetFolderFromPathAsync(this.pluginDirectoryPath);
            var files = await folder.GetFilesAsync();
            var assembles = new HashSet<IFile>();
            foreach (var file in files.Where(file => file.Name.EndsWith(this.fileExtension)))
            {
                assembles.Add(file);
            }
            return assembles;
        }

        private void LoadAssemblies(IEnumerable<IFile> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                var an = AssemblyName.GetAssemblyName(filePath.Path);
                var assembly = Assembly.Load(an);
                this.assemblies.Add(assembly);
            }
        }

        private void CreateMefContainer()
        {
            var conventions = new ConventionBuilder();

            //conventions.ForTypesDerivedFrom<IPlugin>().Export().Shared();

            var configuration = new ContainerConfiguration().WithAssemblies(assemblies, conventions);
            this.compositionHost = configuration.CreateContainer();
        }

        private void GetExports()
        {
            var exports = this.compositionHost.GetExports<IPlugin>();
            foreach (var export in exports)
            {
                export.Initialize(this.eventAggregator);
            }
        }
    }
}

namespace BadMechanics.PristineEdit.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BadMechanics.PristineEdit.Common.Events;
    using BadMechanics.PristineEdit.Common.Interfaces;

    using global::Common.Logging;
    using global::Common.Logging.Configuration;

    using Microsoft.Practices.Prism.PubSubEvents;

    using PCLStorage;

    public class SimpleBootstrap : IBootstrap
    {
        private static readonly ILog Log;

        private IFolder assembliesFolder;

        private readonly ICollection<Assembly> assemblies;

        private readonly IEventAggregator eventAggregator;

        private readonly ObservableCollection<IPlugin> observableCollection;

        private readonly string fileExtension;

        private readonly ICollection<IPlugin> plugins;

        private IFile logFile;

        static SimpleBootstrap()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        public SimpleBootstrap(string fileExtension, IEventAggregator eventAggregator)
            : this(
                fileExtension,
                eventAggregator,
                new List<Assembly>(),
                new List<IPlugin>())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="assembliesFolder"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="assemblies"></param>
        public SimpleBootstrap(string fileExtension, IEventAggregator eventAggregator, ICollection<Assembly> assemblies, ICollection<IPlugin> plugins)
        {
            this.assemblies = assemblies;
            this.plugins = plugins;
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

            eventAggregator.GetEvent<FolderOpenedEvent>().Subscribe(FolderOpenedEventHandler);
        }

        private async void FolderOpenedEventHandler(IFolder folder)
        {
            if (folder.Name == @"Logs")
            {
                this.logFile = await folder.CreateFileAsync("logs.txt", CreationCollisionOption.OpenIfExists);
            }
            else if (folder.Name == @"Plugins")
            {
                this.assembliesFolder = folder;
                var files = await this.ScanDirectory();
                this.LoadAssemblies(files);
                this.LoadPlugins();

                this.eventAggregator.GetEvent<FileOpenedEvent>().Publish(this.logFile);
            }
        }

        private void LoadAssemblies(ICollection<IFile> files)
        {
            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file.Path);
                var assembly = Assembly.Load(assemblyName);
                this.assemblies.Add(assembly);
            }

        }

        /// <summary>
        /// Scan plugin directory for dlls
        /// </summary>
        /// <returns></returns>
        private async Task<HashSet<IFile>> ScanDirectory()
        {
            Log.Debug(m => m("Scan directory"));
            var files = await assembliesFolder.GetFilesAsync();
            var hashSet = new HashSet<IFile>();
            foreach (var file in files.Where(file => file.Name.EndsWith(this.fileExtension)))
            {
                // Fix warning about using foreach variable inside closure
                var temp = file;
                Log.Trace(m => m("Adding assmbly: {0}", temp.Name));
                hashSet.Add(temp);
            }
            return hashSet;
        }

        private void LoadPlugins()
        {
            var pluginType = typeof(IPlugin);
            var pluginTypes = new List<Type>();
            foreach (var assembly in this.assemblies)
            {
                if (assembly != null)
                {
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }
            }

            foreach (var type in pluginTypes)
            {
                var plugin = (IPlugin)Activator.CreateInstance(type, this.eventAggregator);
                this.plugins.Add(plugin);
            }
        }

        void IBootstrap.InitializePlugins()
        {
            return;
        }
    }
}
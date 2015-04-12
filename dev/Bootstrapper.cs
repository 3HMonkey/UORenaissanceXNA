﻿#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UltimaXNA.Configuration;
using UltimaXNA.Core;
using UltimaXNA.Core.Diagnostics;
using UltimaXNA.Core.Diagnostics.Tracing;
using UltimaXNA.Core.Diagnostics.Tracing.Listeners;
using UltimaXNA.Core.Patterns;
#endregion

namespace UltimaXNA
{
    internal sealed class Bootstrapper
    {
        [STAThread]
        private static void Main(string[] args)
        {
            new Bootstrapper(args).Initialize();
        }

        private bool m_IsInitialized;

        private List<IModule> m_Modules = new List<IModule>();

        public Bootstrapper(string[] args)
        {
            GeneralExceptionHandler.Instance = new GeneralExceptionHandler();
        }

        private void LoadModule(IModule module)
        {
            m_Modules.Add(module);
            module.Load();
        }
        
        public string BaseApplicationPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        private void ConfigureTraceListeners()
        {
            if (Debugger.IsAttached)
            {
                Tracer.RegisterListener(new DebugOutputEventListener());
            }

            Tracer.RegisterListener(new FileLogEventListener("debug.log"));

            if (Settings.Debug.IsConsoleEnabled)
            {
                Tracer.RegisterListener(new ConsoleOutputEventListener());
            }
        }

        private void Configure()
        {
            LoadModule(new CoreModule());
            ConfigurePlugins();
        }

        private void ConfigurePlugins()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(BaseApplicationPath, "plugins"));

            if (!directory.Exists)
            {
                return;
            }

            FileInfo[] assemblies = directory.GetFiles("*.dll");

            foreach (FileInfo file in assemblies)
            {
                try
                {
                    Tracer.Info("Loading plugin {0}.", file.Name);

                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    IEnumerable<Type> modules = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof (IModule)));

                    foreach (Type module in modules)
                    {
                        Tracer.Info("Activating module {0}.", module.FullName);

                        IModule instance = (IModule)Activator.CreateInstance(module);

                        LoadModule(instance);
                    }
                }
                catch (Exception e)
                {
                    Tracer.Warn("An error occurred while trying to load plugin. [{0}]", file.FullName);
                    Tracer.Warn(e);
                }
            }
        }

        public void Initialize()
        {
            if (m_IsInitialized)
            {
                return;
            }

            m_IsInitialized = true;

            ConfigureTraceListeners();

            if (Settings.Debug.IsConsoleEnabled && !ConsoleManager.HasConsole)
            {
                ConsoleManager.Show();
            }

            try
            {
                StartEngine();
            }
            finally
            {
                if (ConsoleManager.HasConsole)
                {
                    ConsoleManager.Hide();
                }
            }
        }

        private void Prepare()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void StartEngine()
        {
            Prepare();
            Configure();

            using (UltimaEngine engine = UltimaServices.GetService<UltimaEngine>())
            {
                engine.Run();
            }
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GeneralExceptionHandler.Instance.OnError((Exception) e.ExceptionObject);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            GeneralExceptionHandler.Instance.OnError(e.Exception);
        }
    }
}
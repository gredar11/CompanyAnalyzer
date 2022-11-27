using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistance;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyAnalyzerWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // add dependencies
            var builder = new DbContextOptionsBuilder<CompanyEmployeesDbContext>();
            builder.UseSqlite("Filename=CompanyAnalitics.db", b => b.MigrationsAssembly("CompanyAnalyzerWpf"));
            containerRegistry.RegisterInstance(builder.Options);
            containerRegistry.Register<CompanyEmployeesDbContext>();
        }
    }
}

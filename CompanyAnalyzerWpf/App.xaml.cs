using CompanyAnalyzerWpf.ViewModels;
using Contracts.cs.RepositoryContracts;
using Persistance;
using Persistance.Repositories;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
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
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // add dependencies
            //var b = containerRegistry.Register<CompanyEmployeesDbContext>();
            var dbContextFactory = new CompanyEmployeesDbContextFactory();
            containerRegistry.RegisterInstance(typeof(CompanyEmployeesDbContext), dbContextFactory.CreateDbContext(new string[] {}));
            
            containerRegistry.Register<ICompanyRepository, CompanyRepository>();
        }
    }
}

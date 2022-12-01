using AutoMapper;
using CompanyAnalyzerWpf.ViewModels;
using CompanyAnalyzerWpf.Views;
using Contracts.cs.RepositoryContracts;
using Domain.Models;
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
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Employee, Employee>();
            //    cfg.CreateMap<Department, Department>();
            //    cfg.CreateMap<Company,Company>();

            //});
            //containerRegistry.RegisterInstance(typeof(IMapper), new Mapper(configuration));
            var dbContextFactory = new CompanyEmployeesDbContextFactory();
            var dbContext = dbContextFactory.CreateDbContext(new string[] { });
            containerRegistry.RegisterInstance(typeof(RepositoryManager), new RepositoryManager(dbContext));
            containerRegistry.RegisterDialog<EditCompanyDialog, EditCompanyDialogViewModel>();
            containerRegistry.RegisterDialog<SalaryReportView, SalaryReportViewModel>();
            containerRegistry.RegisterDialog<EditDepartmentDialog, EditDepartmentDialogViewModel>();
            containerRegistry.RegisterDialog<EditEmployeeDialog, EditEmployeeDialogViewModel>();
            containerRegistry.RegisterDialog<DeleteEntityDialog, DeleteEntityDialogViewModel>();
        }
    }
}

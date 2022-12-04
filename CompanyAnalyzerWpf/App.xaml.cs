using AutoMapper;
using CompanyAnalyzerWpf.ViewModels;
using CompanyAnalyzerWpf.ViewModels.EditDialogs;
using CompanyAnalyzerWpf.ViewModels.ReportDialogs;
using CompanyAnalyzerWpf.Views;
using Domain.Models;
using Persistance;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Service.Dtos;
using System.Configuration;
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
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            string connString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            var dbContextFactory = new CompanyEmployeesDbContextFactory();
            containerRegistry.Register<CompanyEmployeesDbContext>(() => dbContextFactory.CreateDbContext(new string[] { connString }));
            containerRegistry.Register<RepositoryManager>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();

                cfg.CreateMap<Company, CompanyDto>().ReverseMap();

                cfg.CreateMap<Department, DepartmentDto>().ReverseMap();

            });
            containerRegistry.RegisterInstance(typeof(IMapper), new Mapper(configuration));
            containerRegistry.RegisterDialog<EditCompanyDialog, EditCompanyDialogViewModel>();
            containerRegistry.RegisterDialog<SalaryReportView, SalaryReportViewModel>();
            containerRegistry.RegisterDialog<EditDepartmentDialog, EditDepartmentDialogViewModel>();
            containerRegistry.RegisterDialog<EditEmployeeDialog, EditEmployeeDialogViewModel>();
            containerRegistry.RegisterDialog<DeleteEntityDialog, DeleteEntityDialogViewModel>();
            containerRegistry.RegisterDialog<ExperienceReportView, ExperienceReportViewModel>();

            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}

using CompanyAnalyzerWpf.Tools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Service;
using Service.Dtos;
using System;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EditCompanyDialogViewModel : BindableBase, IDialogAware
    {
        private readonly PersistanceServiceManager _repositoryManager;
        private bool newEntity = false;
        public EditCompanyDialogViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public CompanyDto CompanyModel { get; set; }
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                SetProperty(ref _companyName, value);
            }
        }
        private DateOnly _establishmentDate;
        public DateOnly EstablishmentDate
        {
            get { return _establishmentDate; }
            set
            {
                SetProperty(ref _establishmentDate, value);
            }
        }
        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set
            {
                SetProperty(ref _adress, value);
            }
        }
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));
        public string Title => "Edit company";

        public event Action<IDialogResult> RequestClose;
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                CompanyModel.EstablishmentDate = EstablishmentDate;
                CompanyModel.CompanyName = CompanyName;
                CompanyModel.Adress = Adress;
               
                result = ButtonResult.OK;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var repuestObject = (parameters as DialogParametersWithObj).RequestParameter;
            CompanyModel = (CompanyDto)repuestObject;
            CompanyName = CompanyModel.CompanyName;
            Adress = CompanyModel.Adress;
            EstablishmentDate = CompanyModel.EstablishmentDate;
        }
    }
}

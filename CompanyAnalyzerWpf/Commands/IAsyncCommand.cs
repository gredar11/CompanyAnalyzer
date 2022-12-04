using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyAnalyzerWpf.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}

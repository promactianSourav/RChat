using System.Threading.Tasks;
using RChat.Configuration.Dto;

namespace RChat.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

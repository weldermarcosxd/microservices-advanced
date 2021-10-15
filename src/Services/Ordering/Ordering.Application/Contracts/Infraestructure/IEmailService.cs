using Ordering.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infraestructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email, CancellationToken cancellationToken);
    }
}

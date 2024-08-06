using Microsoft.AspNetCore.Http;
using WebClientProcessing.Models;

namespace WebClientRepository.Service.Interfaces
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetAsync();
        public Task<Client> GetByIdAsync(int id);
        public Task AddAsync(Client client);
        public Task SaveChangesAsync();
    }
}

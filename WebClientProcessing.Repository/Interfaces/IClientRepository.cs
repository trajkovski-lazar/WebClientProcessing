using WebClientProcessing.Models;

namespace WebClientProcessing.Repository.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetAsync();
        public Task<Client> GetByIdAsync(int id);
        public Task AddAsync(Client client);
        public Task SaveChangesAsync();
        Task AddRangeAsync(IEnumerable<Client> clients);
    }
}

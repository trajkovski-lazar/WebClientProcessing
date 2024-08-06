using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebClientProcessing.Models;
using WebClientProcessing.Repository.Interfaces;
using WebClientProcessing.Database.DataContext;
using System.Globalization;
using System.Xml.Linq;

namespace WebClientProcessing.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbSet<Client> _dbSet;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(ApplicationDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _dbSet = _context.Set<Client>();
            _logger = logger;
        }

        public async Task AddAsync(Client client)
        {
            try
            {
                await _dbSet.AddAsync(client);
                await SaveChangesAsync();

                _logger.LogInformation("Client added successfully with ID: {ClientId}", client.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the client with ID: {ClientId}", client.Id);
                throw; 
            }
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            try
            {
                var clients = await _context.Clients
                    .Include(c => c.Addresses)
                    .ToListAsync();

                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching clients.");
                throw;
            }
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching client with ID: {ClientId}", id);

                var client = await _context.Clients
                    .Include(c => c.Addresses)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (client == null)
                {
                    _logger.LogWarning("Client with ID: {ClientId} not found.", id);
                    throw new KeyNotFoundException($"Client with ID: {id} was not found.");
                }
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the client with ID: {ClientId}", id);
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<Client> clients)
        {
            try
            {
                await _dbSet.AddRangeAsync(clients);
                await SaveChangesAsync();

                _logger.LogInformation("Clients added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple clients.");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {    
                await _context.SaveChangesAsync();
                _logger.LogInformation("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
                throw;
            }
        }
    }
}

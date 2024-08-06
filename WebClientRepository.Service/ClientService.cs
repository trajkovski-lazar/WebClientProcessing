using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml.Linq;
using WebClientProcessing.Models;
using WebClientProcessing.Repository.Interfaces;
using WebClientRepository.Service.Interfaces;

namespace WebClientRepository.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task AddAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            try
            {
                return await _clientRepository.GetAsync();
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
                var client = await _clientRepository.GetByIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning("Client with ID: {ClientId} not found.", id);
                }
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while fetching the client with ID: {ClientId}", id);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _clientRepository.SaveChangesAsync();
                _logger.LogInformation("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while saving changes.");
                throw;
            }
        }
    }
}

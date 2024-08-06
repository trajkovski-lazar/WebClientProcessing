using Microsoft.AspNetCore.Mvc;
using WebClientProcessing.Models;
using WebClientRepository.Service;
using WebClientRepository.Service.Interfaces;
using Microsoft.Extensions.Logging; // Ensure to include this

namespace WebClientProcessing.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientService.GetAsync();
            if (clients == null || !clients.Any())
            {
                _logger.LogWarning("No clients found.");
                return View();
            }

            return View(clients);
        }
    }
}

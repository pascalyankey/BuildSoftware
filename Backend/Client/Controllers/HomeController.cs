using Client.Interfaces;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;

        public HomeController(ILogger<HomeController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var werven = await _clientService.GetWervenAsync();

            return View(werven);
        }

        public async Task<IActionResult> Werknemers(string werfNaam, int werfId)
        {
            var werknemers = await _clientService.GetWerknemersByWerfNaam(werfNaam);

            return View(werknemers);
        }

        public IActionResult WerknemerForm()
        {
            return View(new WerknemerViewModel());
        }

        public IActionResult WerfForm()
        {
            return View(new WerfViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> WerknemerToevoegen(WerknemerViewModel werknemerViewModel)
        {
            var response = await _clientService.WerknemerToevoegen(werknemerViewModel);

            return RedirectToAction("Werknemers", new { response.FirstOrDefault().Value.WerfNaam, werknemerViewModel.WerfId });
        }

        [HttpPost]
        public async Task<IActionResult> WerknemerVerwijderen(int id, int werfId, string werfNaam)
        {
            var response = await _clientService.DeleteWerknemer(id);

            return RedirectToAction("Werknemers", new { werfNaam, werfId });
        }

        [HttpPost]
        public async Task<IActionResult> WerfToevoegen(WerfViewModel werfViewModel)
        {
            var response = await _clientService.WerfToevoegen(werfViewModel);

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

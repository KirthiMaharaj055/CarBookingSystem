using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarBookingSystem.Views.Cars
{
    public class CarListView : PageModel
    {
        private readonly ILogger<CarListView> _logger;

        public CarListView(ILogger<CarListView> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
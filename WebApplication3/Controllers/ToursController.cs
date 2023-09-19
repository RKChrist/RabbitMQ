using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rab;
using WebApplication3.Data;

namespace WebApplication3.Controllers
{
    public class ToursController : Controller
    {
        private RabbitMQProducer _service;

        public ToursController(RabbitMQProducer service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View(new List<Tour>());
        }


        // GET: Tours/Create
        public IActionResult Create()
        {
            var tour = new Tour();
            return View(tour);
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Tours,Book,Cancel")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                if (tour.Cancel)
                {
                    await _service.Send("Tour.Cancel", tour);
                }
                else
                {
                    await _service.Send("Tour.Booked", tour);
                }
            }
            return View(tour);
        }

    }
}

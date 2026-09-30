using Microsoft.AspNetCore.Mvc;
using CarInsurance.Models;
using System;
using System.Linq;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private readonly CarInsuranceContext _context;

        public InsureeController(CarInsuranceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Insuree insuree)
        {
            double quote = 50;
            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (age <= 18) quote += 100;
            else if (age >= 19 && age <= 25) quote += 50;
            else if (age >= 26) quote += 25;

            if (insuree.CarYear < 2000) quote += 25;
            if (insuree.CarYear > 2015) quote += 25;

            if (insuree.CarMake == "Porsche") quote += 25;
            if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera") quote += 25;

            quote += insuree.SpeedingTickets * 10;

            if (insuree.DUI) quote *= 1.25;
            if (insuree.CoverageType == "Full") quote *= 1.5;

            insuree.Quote = quote;

            _context.Insurees.Add(insuree);
            _context.SaveChanges();

            return View("Result", insuree);
        }

        public IActionResult Admin()
        {
            var insurees = _context.Insurees.ToList();
            return View(insurees);
        }
    }
}

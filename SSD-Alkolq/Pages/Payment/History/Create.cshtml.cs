﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.Payment.History
{
    public class CreateModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public CreateModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public PaymentRecord PaymentRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PaymentRecords.Add(PaymentRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

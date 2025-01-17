﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.Feedback
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DeleteModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FeedbackRecord FeedbackRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeedbackRecord = await _context.FeedbackRecords.FirstOrDefaultAsync(m => m.ID == id);

            if (FeedbackRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeedbackRecord = await _context.FeedbackRecords.FindAsync(id);

            if (FeedbackRecord != null)
            {
                _context.FeedbackRecords.Remove(FeedbackRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.Rating
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public IndexModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<ProductRating> ProductRating { get;set; }

        public async Task OnGetAsync()
        {
            ProductRating = await _context.ProductRatings.ToListAsync();
        }
    }
}

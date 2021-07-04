using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Alkolq.Pages.AlcoholProducts
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
        public Models.AlcoholProduct AlcoholProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlcoholProduct = await _context.AlcoholProduct.FirstOrDefaultAsync(m => m.ID == id);

            if (AlcoholProduct == null)
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

            AlcoholProduct = await _context.AlcoholProduct.FindAsync(id);

            if (AlcoholProduct != null)
            {
                _context.AlcoholProduct.Remove(AlcoholProduct);
                //  await _context.SaveChangesAsync();

                // Once a record is deleted, create an audit record
                if (await _context.SaveChangesAsync() > 0)
                {
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Delete Movie Record";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.KeyAlcoholFieldID = AlcoholProduct.ID;
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToPage("./Index");
        }
    }
}

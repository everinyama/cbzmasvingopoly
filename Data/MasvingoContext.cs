using BillPayments_LookUp_Validation.Data;
using BillPayments_LookUp_Validation.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BillPayments_LookUp_Validation.Data
{
    public class MasvingoContext : DbContext
    {
        public MasvingoContext(DbContextOptions<MasvingoContext> options) : base(options)
        {

        }
        public DbSet<tblPending> tblPendings { get; set; }

    }
}
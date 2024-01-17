using BillPayments_LookUp_Validation.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BillPayments_LookUp_Validation.Data
{
    public class MasvingoContext : DbContext
    {
        public MasvingoContext(DbContextOptions<MasvingoContext> options) : base(options)
        {

        }

    }
}
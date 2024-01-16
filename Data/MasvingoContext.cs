using BillPayments_LookUp_Validation.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BillPayments_LookUp_Validation.Data
{
    public class MasvingoContext : DBContext
    {
        public MasvingoContext(DbContextOptions<MasvingoContext> options) : base(options);

    }
}
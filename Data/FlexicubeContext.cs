using BillPayments_LookUp_Validation.Models;
using Microsoft.EntityFrameworkCore;

namespace BillPayments_LookUp_Validation.Data
{
    public class FlexicubeContext : DbContext
    {
        public FlexicubeContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UDTM_LOV> uDTM_LOVs { get; set; }
    }
}

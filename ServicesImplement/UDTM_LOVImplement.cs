using BillPayments_LookUp_Validation.Data;
using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class UDTM_LOVImplement: IUDTM_LOV
    {
        private readonly FlexicubeContext flexicubeContext;
        public UDTM_LOVImplement(FlexicubeContext dbContext)
        {
            flexicubeContext = dbContext;
        }
        public async Task<List<UDTM_LOV>> GetAllAsync()
        {
            return await flexicubeContext.uDTM_LOVs.ToListAsync();
        }

    }
}

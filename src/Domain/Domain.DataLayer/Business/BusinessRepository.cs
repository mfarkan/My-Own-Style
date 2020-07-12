using Domain.DataLayer.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataLayer.Business
{
    public class BusinessRepository : GenericRepository<BusinessContext>, IBusinessRepository
    {
        public BusinessRepository(BusinessContext businessContext):base(businessContext)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPenaltyManagement.Domain.Enums
{
    public enum PenaltyStatus
    {
        New,
        ManagerApproval,
        FinanceApproval,
        Completed,
        Rejected

    }
}

namespace TrafficPenaltyManagement.Application.Dtos.DashboardDtos
{
    public class DashboardDto
    {
        public int TotalEmployees { get; set; }
        public int TotalVehicles { get; set; }
        public int TotalPenalties { get; set; }
        public int CompletedPenalties { get; set; }

        public int NewPenalties { get; set; }
        public int ManagerApprovalPenalties { get; set; }
        public int FinanceApprovalPenalties { get; set; }
        public int RejectedPenalties { get; set; }
    }
}

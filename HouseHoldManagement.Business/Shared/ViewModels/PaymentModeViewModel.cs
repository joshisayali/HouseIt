namespace HouseHoldManagement.Business.Shared
{
    public class PaymentModeViewModel
    {
        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
        public string PaymentModeDescription { get; set; }
        public bool IsInUse { get; set; }
    }
}


namespace HouseHoldManagement.Business.Shared
{
    public class ExpenseTypeViewModel
    {
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public string ExpenseTypeDescription { get; set; }
        public bool IsInUse { get; set; }
    }
}
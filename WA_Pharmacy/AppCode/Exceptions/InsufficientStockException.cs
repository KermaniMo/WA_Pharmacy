namespace WA_Pharmacy.AppCode.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public string MedicineName { get; }
        public int RequestedQuantity { get; }
        public int AvailableStock { get; }

        public InsufficientStockException(string medicineName, int requestedQuantity, int availableStock)
            : base($"موجودی کافی برای داروی '{medicineName}' وجود ندارد. درخواست: {requestedQuantity}, موجودی: {availableStock}")
        {
            MedicineName = medicineName;
            RequestedQuantity = requestedQuantity;
            AvailableStock = availableStock;
        }
    }
}

namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionItemDto
    {
        public int MedicineId { get; set; }
        public byte Quantity { get; set; }
        
        /// <summary>
        /// قیمت بیمه شده - توسط کاربر وارد می‌شود
        /// </summary>
        public decimal InsurancePrice { get; set; }
    }
}

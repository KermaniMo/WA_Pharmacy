using System.ComponentModel.DataAnnotations;
using WA_Pharmacy.DTOs.Customer;
using WA_Pharmacy.DTOs.Insurance;

namespace WA_Pharmacy.DTOs.Insured
{
    public class InsuredListDto
    {
        public long Id { get; set; }

        [Display(Name = "شماره بیمه")]
        public string InsuranceNumber { get; set; }

        [Display(Name = "تاریخ شروع اعتبار")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان اعتبار")]
        public string ExpireDate { get; set; }

        [Display(Name = "درصد تخفیف")]
        public decimal Discount { get; set; }

        [Display(Name = "مشتری")]
        public string CustomerFullName { get; set; } // Flattened from Customer

        [Display(Name = "شرکت بیمه")]
        public string InsuranceCompanyName { get; set; } // Flattened from Insurance
    }
}

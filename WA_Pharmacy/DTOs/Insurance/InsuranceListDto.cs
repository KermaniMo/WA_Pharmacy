using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Insurance
{
    public class InsuranceListDto
    {
        public long Id { get; set; }

        [Display(Name = "نام شرکت بیمه")]
        public string CompanyName { get; set; }
    }
}

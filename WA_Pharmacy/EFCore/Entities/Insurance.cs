using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Insurance : BaseEntity<long>
    {

        public string CompanyName { get; set; }

        public ICollection<Insured> InsuredList { get; set; }

    }
}

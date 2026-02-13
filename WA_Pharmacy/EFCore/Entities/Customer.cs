using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Customer : BaseEntity<long>
    {

        public string NationalCode { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public bool Sex { get; set; }
        public DateOnly? Birthday { get; set; }
        public DateTime RegisterDate { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Insured> InsuredRecords { get; set; }

    }
}

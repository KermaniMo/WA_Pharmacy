using Microsoft.AspNetCore.Identity;
using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Doctor : BaseEntity<int>
    {
        public string DoctorNumber { get; set; } // Char(8)
        public string NationalCode { get; set; } // Char(10)
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string MobileNumber { get; set; }
        public bool Sex { get; set; }
        public DateOnly Birthday { get; set; }
        public DateTime RegisterDate { get; set; }

        // Navigation Property (One Doctor has many Prescriptions)
        public ICollection<Prescription> Prescriptions { get; set; }

        // Foreign Keys
        // 1. کلید خارجی (Foreign Key)
        // نکته: حتماً باید String باشه چون Id در Identity استرینگ هست
        public string UserId { get; set; }

        // 2. پراپرتی نویگیشن (Navigation Property)
        // این بهت اجازه میده بنویسی: doctor.User.Email
        public IdentityUser User { get; set; }
    }
}

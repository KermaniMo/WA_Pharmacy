using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Doctor
{
    public class DoctorListDto
    {
        public int Id { get; set; }

        [Display(Name = "کد پزشک")]
        public string DoctorNumber { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "تخصص")]
        public string Specialization { get; set; }

        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "جنسیت")]
        public string Sex { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string Birthday { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string RegisterDate { get; set; }

        [Display(Name = "کاربر مرتبط")]
        public string UserId { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}

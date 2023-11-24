using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POE_TEST_ONE.Models
{
    public class Module
    {
        [Key] //key tells the migration that this is the primary key for the database
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Code")]
        public string Module_code { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Name")]
        public string Module_Name { get; set; }

        [Range(1,30,ErrorMessage ="Credits must be between 1-30")]
        [DisplayName("Credits")]
        public int Num_credits { get; set; }

        [DisplayName("Class Hours Per Week")]
        public int Class_hours_per_week { get; set; }

        [Range(1,18,ErrorMessage ="Semester weeks must be between 1-18")]
        [DisplayName("Weeks in Semester")]
        public int Num_weeks_in_semester { get; set; }

        [DisplayName("Semester Start")]
        public DateTime Semester_start_date { get; set; }

        [DisplayName("Date Studied On")]
        public DateTime Studied_on_certin_date { get; set; }

        [Range(1,24,ErrorMessage = "Study hours must be between 1-24hrs")]
        [DisplayName("Hours Studied on day")]
        public int Hours_Studied_On_CertinDate { get; set; }

        [DisplayName("Self Study Hours")]
        public int Self_Study_hours { get; set; }

        [DisplayName("Remaining Study Hours")]
        public int Remaining_study_hours { get; set; }

        public string? UserId { get; set; }

        

    }
}

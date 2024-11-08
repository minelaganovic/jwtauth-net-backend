using System.ComponentModel.DataAnnotations;

namespace serverstudent.Model
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string password { get; set; }


        [StringLength(50)]
        public string firstname { get; set; }


        [StringLength(50)]
        public string lastname { get; set; }
    }
}

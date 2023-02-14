using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Models
{
    public abstract class User
    {
        public User() 
        {
            RND = 0;
        }

        [Key]
        public int ID { get; set; }
        [Display(Name = "שם פרטי")]
        public string? FirstName { get; set; }
        [Display(Name = "שם משפחה")]
        public string? LastName { get; set; }
        [NotMapped, Display(Name ="שם מלא")]
        public string FullName { get { return FirstName + " " + LastName; } }
        [Display(Name = "מספר טלפון")] 
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "כתובת מייל לא נכונה"),Display(Name = "אימייל")]
        public string? Email { get; set; }
        [DataType(DataType.Password), Display(Name = "סיסמא")]
        public string? Password { get; set; } = "1234";
        //מספר נוסף המשתנה רנדומלית עם כל התחברות של למערכת
        public int IDRandom { get; set; }
        [NotMapped]
        public int RND { get 
            {
                return (ID * 100000) + IDRandom;

			} set 
            {
				Random random = new Random(DateTime.Now.Millisecond);
				IDRandom=random.Next(10000, 99999);
			} }
        [Display(Name = "פעיל")]
        public bool Active { get; set; } = true;
        [Display(Name = "פעיל"),NotMapped]
        public string isActive { get { if (Active) return "פעיל";
                        return "לא פעיל"; } }
    }
}

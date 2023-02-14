using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BarberShop.Models
{
    public class SetImg
    {
        public SetImg(IFormFile file) 
        {
            //בדיקה אם הקובץ לא הגיע ריק
            if (file == null) return;
            //יצירת מקום בזכרון המכיל קובץ
            MemoryStream stream = new MemoryStream();
            //העתקת הקובץ מהמשתמש למקום שנוצר זכרון
            file.CopyTo(stream);
            //הפיכת הזכרון למערך כדי שיוכל להיכנס למסד נתונים
            MyImage = stream.ToArray();
        }
        public byte[] MyImage { get; set; }
        
    }
}

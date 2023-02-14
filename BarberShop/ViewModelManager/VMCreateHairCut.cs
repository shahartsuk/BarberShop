using BarberShop.Models;

namespace BarberShop.ViewModelManager
{
    public class VMCreateHairCut
    {
        public VMCreateHairCut()
        {
            Action = new HairCutActions();
            ActionList = new List<HairCutActions>();
        }
        public List<HairCutActions> ActionList { get; set; }
        public HairCutActions Action { get; set; }
        public IFormFile File { get; set; }
    }
}

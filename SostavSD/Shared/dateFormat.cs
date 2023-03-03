using Microsoft.VisualBasic;

namespace SostavSD.Shared
{
    public class dateFormat
    {
        private DateTime currentDate;
        
        public dateFormat()
        {
            currentDate = new DateTime();
        }
        public dateFormat(DateTime currentDate) 
        {
            this.currentDate = currentDate;
        }


         public DateTime CurrentDate
        {
            set { this.currentDate = value; }
            get { return currentDate; }
        }
       

                
    }
}

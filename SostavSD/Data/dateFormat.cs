namespace SostavSD.Data
{
    public class dateFormat 
    {
        public DateTime dateTime { get; set; }

        public dateFormat()
        {
            dateTime = new DateTime();
        }

        //public dateFormat(int day, int month, int year)
        //{
        //    dateTime = new DateTime(day, month, year);
        //}

        public dateFormat(string dateString)
        {
            dateTime = DateTime.Parse(dateString);
        }
        public dateFormat(DateTime currentDate)
        {
            dateTime = currentDate;
        }
       public override string ToString()
        {
            return  this.dateTime.ToString("dd/MM/yyyy");
        } 

    }
}

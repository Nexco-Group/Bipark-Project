using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class PassToMyProfile
    {
        public iUser iUser { get; set; }
        public List<packtime> packtime { get; set; }
        public List<Product> iProduct { get; set; }
        public List<News> iNews { get; set; }
        public int balance { get; set; }
    }

    public partial class packtime
    {
        public int Id { get; set; }
        public int type { get; set; }
        public int date { get; set; }
        public string name { get; set; }
    }

    public partial class getbalss
    {
        public int a1 { get; set; }
        public int a2 { get; set; }
        public int a3 { get; set; }
        public int a4 { get; set; }
        public int a5 { get; set; }
        public int a6 { get; set; }
        public int a7 { get; set; }
        public float a8 { get; set; }
        public float a9 { get; set; }
        public float a10 { get; set; }
    }

    public partial class PassToMyRequesta
    {
        public iUser iUser { get; set; }
        public List<News> iNews { get; set; }
    }
    public partial class PassToMyTransaction
    {
        public List<Balance> iBalance { get; set; }


        public iUser iUser { get; set; }
        public List<News> iNews { get; set; }
    }

    public partial class PassToOrderEdit
    {
        //public Schools iUser { get; set; }
        //public List<Message> iMessage { get; set; }
        //public List<string> iNotification { get; set; }
        //public List<Store_Item> iStoreItems { get; set; }
        public string city { get; set; }
        public string province { get; set; }
    }
    public partial class PassToOrders
    {
        //public Schools iUser { get; set; }
        //public List<Message> iMessage { get; set; }
        //public List<string> iNotification { get; set; }
        public List<packtime> packtime { get; set; }
        public List<Product> iProduct { get; set; }
        public List<News> iNews { get; set; }
    }



    public partial class PassToStore
    {
        public Admins iUser { get; set; }
        //public List<Message> iMessage { get; set; }
        public List<string> iNotification { get; set; }
    }
    public partial class PassToRequests
    {
        public Admins iUser { get; set; }
        //public List<Message> iMessage { get; set; }
        public List<string> iNotification { get; set; }
    }
}

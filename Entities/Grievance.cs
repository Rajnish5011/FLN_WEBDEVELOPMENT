using System;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class GrievanceCategories
    {
        public int Grievance_Category_Id { get; set; }

        public string Category_Name { get; }

    }
    public class GrievanceTicketList
    {
        public int Ticket_Id { get;  }
        public string Ticket_Number { get; }
        public int Grievance_Category_Id { get; }
        public int User_Id { get;  }
        public string Name { get; set; }
        public byte Role_Id { get;  }
        public string Role_Name { get; }
        public string GrievanceForRole { get; }        
        public string Category_Name { get; }
        public string Grievance_Query { get; }
        public DateTime Date_Of_Issue { get; }
        public DateTime Date_Of_Solved { get; }
        public string Contact_Number { get; }
        public string Ticket_Status { get; }
        public string District_Name { get; }
        public string Block_Name { get; }

    }

    public class GrievanceTicketListByUser
    {
        public int Ticket_Id { get; set; }
        public string Ticket_Number { get; set;}
        public int Grievance_Category_Id { get; }        
        public string GrievanceForRole { get; }
        public string Category_Name { get; }
        public string Grievance_Query { get; }
        public string Grievance_Reply { get; }
        public string Date_Of_Issue { get; }
        public string Date_Of_Solved { get; }               
        public string Ticket_Status { get; }
        
    }
    public class GrievanceTicketDetails
    {
        public int Ticket_Id { get; }
        public string Ticket_Number { get; }
        public int Grievance_Category_Id { get; }
        public int User_Id { get; }
        public string Name { get; set; }
        public byte Role_Id { get; }
        public string Role_Name { get; }
        public string GrievanceForRole { get; }
        public string Category_Name { get; }
        public string Grievance_Query { get; }
        public string Grievance_Reply { get; }
        public DateTime Date_Of_Issue { get; }
        public DateTime Date_Of_Solved { get; }
        public string Contact_Number { get; }
        public string Ticket_Status { get; }
        public string District_Name { get; }
        public string Block_Name { get; }

    }
}

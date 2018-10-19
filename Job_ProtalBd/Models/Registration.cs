namespace Job_ProtalBd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Registration
    {
        public int UserId { get; set; }
        public string E_Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public Nullable<System.DateTime> Date_Of_Entry { get; set; }
        public int Contact_Number { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerried { get; set; }
        public System.Guid ActivationCode { get; set; }
    }
}

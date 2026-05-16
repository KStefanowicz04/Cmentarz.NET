using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Model służący do edytowania własnych danych użytkownika przez użytkownika
    public class UserProfileIndex
    {
        // Użytkownik
        public User User { get; set; }

        // ContactData należące do użytkownika
        public ContactData UserCD { get; set; }

        // Działki należące do danego użytkownika
        public List<Plot> UserPlots { get; set; }
    }
}

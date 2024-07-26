using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVCIdentity.Models;

namespace MVCIdentity.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MVCIdentityUser class
public class MVCIdentityUser : IdentityUser
{
    // Ekstra istediğimiz özellikleri bu sınıfa ekliyoruz.
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<OgrenciDers> OgrenciDersler { get; set; }

}


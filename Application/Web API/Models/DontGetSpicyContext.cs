using Microsoft.EntityFrameworkCore;

namespace Web_API.Models
{
   
    public class DontGetSpicyContext : DbContext
    {
        //db set

    
        public DontGetSpicyContext(DbContextOptions o):base(o)
        {

        }
    }
}
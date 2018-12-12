using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autobentley1.Entities
{
    [Table("IletisimFormlari")]
    public class Form
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }


    }
}

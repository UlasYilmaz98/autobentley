using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autobentley1.Entities
{
    [Table("SoruCevaplar")]
    public class SoruCevap
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public string Baginti { get; set; }

        public DateTime CreatedOn { get; set; }


    }
}

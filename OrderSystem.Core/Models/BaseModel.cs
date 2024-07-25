using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents the base class for all entities in the order management system.
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Entities
{
    public class Task : Entity
    {
        #region
        public required string Name { get; set; }

        public required DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        public byte[]? Image {  get; set; }
        #endregion
    }
}

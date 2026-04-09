using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Data_Access_Layer.Entities
{
    public class Task : Entity
    {
        #region Properties
        public required string Name { get; set; }

        public required DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        public byte[]? Image {  get; set; }
        #endregion

        #region Relationships
        public Guid UserId { get; set; }

        public User User { get; set; }
        #endregion

        #region Constructors
        [SetsRequiredMembers]
        public Task(Guid id, string name, DateTime dateCreated, string? description, byte[]? image)
        {
            this.Id = id;
            this.Name = name;
            this.DateCreated = dateCreated;
            this.Description = description;
            this.Image = image;
        }
        [SetsRequiredMembers]
        public Task(Guid id, string name, DateTime dateCreated) : this(id, name, dateCreated, null, null)
        {
        }
        #endregion
    }
}

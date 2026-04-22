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

        public DateTime? FinishDate { get; set; }

        public string? Status { get; set; }

        public string? Priority { get; set; }

        public string? Description { get; set; }

        public byte[]? Image {  get; set; }
        #endregion

        #region Relationships
        public Guid UserId { get; set; }

        public User User { get; set; }
        #endregion

        #region Constructors
        [SetsRequiredMembers]
        public Task(Guid id, string name, DateTime dateCreated, DateTime? finishDate, string? description, byte[]? image, string status, string priority)
        {
            this.Id = id;
            this.Name = name;
            this.DateCreated = dateCreated;
            this.FinishDate = finishDate;
            this.Description = description;
            this.Image = image;
            this.Status = status;
            this.Priority = priority;
        }
        [SetsRequiredMembers]
        public Task(Guid id, string name, DateTime dateCreated) : this(id, name, dateCreated,null, null, null, null, null)
        {
        }

        [SetsRequiredMembers]
        public Task(Guid id, string name, DateTime dateCreated, DateTime finishDate,string priority, string status) : this(id, name, dateCreated, finishDate, null, null, status, priority)
        {
        }
        #endregion
    }
}

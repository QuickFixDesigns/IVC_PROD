﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class Status : EntityBase
    {
        public int StatusID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]

        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }

        //navigation
        public ICollection<Item> Items { get; set; }
        public ICollection<Event> Events { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Status))
                return false;

            return ((Status)obj).StatusID == this.StatusID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

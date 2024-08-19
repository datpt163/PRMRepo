﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public bool IsCheckIn { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }

        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = new Staff();
    }
}

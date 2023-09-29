using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderHistoryDto
{
    public class PostOrderStatusDto
    {
        public Guid Id { get; set; }
        public string EngName { get; set; }
        public string? ArbName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

﻿namespace OrderSystem.DTO
{
    public class InvoiceDto
    {
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

using System;

namespace NhSubJoinTest.Data.Entities
{
	public class Invoice : Document
	{
		/// <summary>
		/// Constructor for creating an instance by HNibernate
		/// </summary>
		protected Invoice() : base()
		{
		}

		/// <summary>
		/// Constructor for public use by developers
		/// </summary>
		public Invoice(Company company, string refNumber, string title, decimal invoiceTotal) : base(company, refNumber, title)
		{
			if (invoiceTotal <= 0)
				throw new ArgumentException("Value must be greater than zero.", nameof(invoiceTotal));

			InvoiceTotal = invoiceTotal;
		}

        public override DocumentTypeEnum DocumentType => DocumentTypeEnum.Invoice;

        public virtual decimal? InvoiceTotal { get; protected set; }
	}
}

using System;

namespace NhSubJoinTest.Data.Entities
{
	public class Order : Document
	{
		/// <summary>
		/// Constructor used for creating an instance by HNibernate
		/// </summary>
		protected Order() : base()
		{
		}

		/// <summary>
		/// Constructor for public use by developers
		/// </summary>
		public Order(Company company, string refNumber, string title, decimal orderTotal) : base(company, refNumber, title)
		{
			if (orderTotal <= 0)
				throw new ArgumentException("Value must be greater than zero.", nameof(orderTotal));

			OrderTotal = orderTotal;
		}

		public override DocumentTypeEnum DocumentType => DocumentTypeEnum.Order;

		public virtual decimal? OrderTotal { get; protected set; }
	}
}

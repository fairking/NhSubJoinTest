using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Linq;
using NhSubJoinTest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NhSubJoinTest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DocumentController : ControllerBase
	{
		private readonly ILogger<DocumentController> _logger;
		private readonly ISession _session;

		public DocumentController(ILogger<DocumentController> logger, ISession session)
		{
			_logger = logger;
			_session = session;
		}

		[HttpGet]
		[Route("one")]
		public Document GetOne(string id)
		{
			var result = _session.Get<Document>(Guid.TryParse(id, out Guid guid) ? guid : Guid.Empty);
			return result;
		}

		[HttpGet]
		[Route("company_invoices")]
		public IEnumerable<Invoice> GetInvoicesFromCompany()
		{
			var company = _session.Query<Company>().ToList().First();

			var allDocs = company.Documents.ToList();

			var invoices = company.Invoices.ToList();

			return invoices;
		}

		[HttpGet]
		[Route("all")]
		public IEnumerable<Document> GetAll()
		{
			var result = _session.Query<Document>().ToList();
			return result;
		}

		[HttpGet]
		[Route("orders")]
		public async Task<IEnumerable<Order>> GetOrders()
		{
			var result = await _session.Query<Order>().ToListAsync();
			return result;
		}

		[HttpGet]
		[Route("invoices")]
		public async Task<IEnumerable<Invoice>> GetInvoices()
		{
			var result = await _session.Query<Invoice>().ToListAsync();
			return result;
		}
	}
}

using NHibernate.Mapping.ByCode;
using NHibernate.SimpleMapping.Attributes;
using NHibernate.SimpleMapping.Generators;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NhSubJoinTest.Data.Entities
{
	[Table("companies")]
    public class Company
    {
		protected Company() { }

		public Company(string name)
        {
			Id = IdentityGenerator.WebHash();

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			Name = name;
		}

		[Key, Length(32), Ansi]
		public virtual string Id { get; protected set; }

		[Length(150)]
		public virtual string Name { get; protected set; }

		[JsonIgnore]
		[Cascade(Cascade.None), Inverse]
		public virtual ISet<Document> Documents { get; protected set; }

		[JsonIgnore]
		[Cascade(Cascade.None), Inverse]
		public virtual ISet<Invoice> Invoices { get; protected set; }

		[JsonIgnore]
		[Cascade(Cascade.None), Inverse]
		public virtual ISet<Order> Orders { get; protected set; }
	}
}

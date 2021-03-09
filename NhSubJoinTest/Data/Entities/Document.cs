using NHibernate.SimpleMapping.Attributes;
using System;

namespace NhSubJoinTest.Data.Entities
{
	[Table("documents")]
	[Discriminator]
	public abstract class Document
	{
		protected Document()
		{
		}

		protected Document(Company company, string refNumber, string title)
		{
			Id = Guid.NewGuid();

			if (string.IsNullOrWhiteSpace(refNumber))
				throw new ArgumentNullException(nameof(refNumber));

			RefNumber = refNumber;

			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentNullException(nameof(title));

			Title = title;

			Company = company ?? throw new ArgumentNullException(nameof(company));
		}

		[Key]
		public virtual Guid Id { get; protected set; }

		[Length(50)]
		public virtual string RefNumber { get; protected set; }

		[Length(150)]
		public virtual string Title { get; protected set; }

		[Discriminator]
		public abstract DocumentTypeEnum DocumentType { get; }

		[NotNull]
		public virtual Company Company { get; protected set; }
	}

	public enum DocumentTypeEnum
    {
		Invoice,
		Order,
    }
}

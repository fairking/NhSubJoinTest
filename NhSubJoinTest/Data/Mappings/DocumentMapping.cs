using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NhSubJoinTest.Data.Entities;

namespace NhSubJoinTest.Data.Mappings
{
    public class DocumentMapping : ClassMapping<Document>
    {
        public DocumentMapping()
        {
            Table("documents");

            Discriminator(x =>
            {
                x.Type(TypeFactory.GetAnsiStringType(50));
                x.Length(50);
                x.Column("discriminator");
            });

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Assigned);
                x.Type(NHibernateUtil.AnsiString);
                x.Length(32);
            });

            Property(x => x.RefNumber, x =>
            {
                x.Type(NHibernateUtil.AnsiString);
                x.Length(50);
            });

            Property(x => x.Title, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Length(150);
            });

            ManyToOne(x => x.Company, x =>
            {
                x.Column(nameof(Document.Company));
                x.NotNullable(true);
            });
        }
    }
}

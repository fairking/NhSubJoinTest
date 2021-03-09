using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NhSubJoinTest.Data.Entities;

namespace NhSubJoinTest.Data.Mappings
{
    public class CompanyMapping : ClassMapping<Company>
    {
        public CompanyMapping()
        {
            Table("companies");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Assigned);
                x.Type(NHibernateUtil.AnsiString);
                x.Length(32);
            });

            Property(x => x.Name, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Length(150);
            });

            Set(x => x.Documents, x =>
            {
                x.Cascade(Cascade.None);
                x.Inverse(true);
                x.Key(x =>
                {
                    x.Column(nameof(Document.Company));
                });
            }, m =>
            {
                m.OneToMany();
            });

            Set(x => x.Invoices, x =>
            {
                x.Cascade(Cascade.None);
                x.Inverse(true);
                x.Key(x =>
                {
                    x.Column(nameof(Document.Company));
                });
            }, m =>
            {
                m.OneToMany();
            });

            Set(x => x.Orders, x =>
            {
                x.Cascade(Cascade.None);
                x.Inverse(true);
                x.Key(x =>
                {
                    x.Column(nameof(Document.Company));
                });
            }, m =>
            {
                m.OneToMany();
            });
        }
    }
}

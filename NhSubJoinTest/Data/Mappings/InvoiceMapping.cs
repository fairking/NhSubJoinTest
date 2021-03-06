using NHibernate.Mapping.ByCode.Conformist;
using NhSubJoinTest.Data.Entities;

namespace NhSubJoinTest.Data.Mappings
{
    public class InvoiceMapping : SubclassMapping<Invoice>
    {
        public InvoiceMapping()
        {
            DiscriminatorValue(DocumentTypeEnum.Invoice.ToString());

            Property(p => p.InvoiceTotal);
        }
    }
}

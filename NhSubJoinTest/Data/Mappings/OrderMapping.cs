using NHibernate.Mapping.ByCode.Conformist;
using NhSubJoinTest.Data.Entities;

namespace NhSubJoinTest.Data.Mappings
{
    public class OrderMapping : SubclassMapping<Order>
    {
        public OrderMapping()
        {
            Lazy(true);
            DiscriminatorValue(DocumentTypeEnum.Order.ToString());
            Property(p => p.OrderTotal);
        }
    }
}

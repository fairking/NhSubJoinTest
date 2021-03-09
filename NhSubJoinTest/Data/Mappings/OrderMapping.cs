using NHibernate.Mapping.ByCode.Conformist;
using NhSubJoinTest.Data.Entities;

namespace NhSubJoinTest.Data.Mappings
{
    public class OrderMapping : SubclassMapping<Order>
    {
        public OrderMapping()
        {
            DiscriminatorValue(DocumentTypeEnum.Order.ToString());

            Property(p => p.OrderTotal);
        }
    }
}

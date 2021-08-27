using CRM_Shop.Context;

namespace CRM_Shop.DatabaseTransaction
{
    interface IUpdatable
    {
        bool UpdateRecord(int recordId, CRMContext context);
    }
}

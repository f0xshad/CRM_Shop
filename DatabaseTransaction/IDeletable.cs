using CRM_Shop.Context;

namespace CRM_Shop.DatabaseTransaction
{
    interface IDeletable
    {
        bool DeleteRecord(int recordId, CRMContext context);
    }
}

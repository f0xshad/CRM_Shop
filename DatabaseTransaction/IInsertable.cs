using CRM_Shop.Context;

namespace CRM_Shop.DatabaseTransaction
{
    interface IInsertable
    {
        bool InsertRecord(CRMContext context);
    }
}

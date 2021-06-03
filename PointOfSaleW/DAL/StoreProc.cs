using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class StoreProc
    {

       public enum Name
       {
           spRecentSaleGetById,
           spSaleReturn,
           spTokenIdSelect,
           spSaleCheckEdit,
           spRecipeGetSaleList,
           spRecipeDelById,
           spRecipeGet,
           spRecipeInsert,
           spProductGetList,
           spGetDataByCurrentDate,
           spGetDataByDate,
           spProductDeleteCheckById,
           spBranchGet,
           spProductGet,
           spProductPriceGetById,
           spSaleGet,
           spSaleDetailGet,
           spBranchGetById,
           spProductGetById,
           spSaleGetById,
           spSaleDetailGetById,
           spSaleInsert,
           spSaleInsertEdit,
           spSaleUpdate,
           spSaleDetailUpdate,
           spSaleDetailInsert,
           spSaleDeleteById,
           spSaleDetailDeleteById,
           spProductDeleteById,
           spProductInsert,
           spProductUpdate,
           spGetRecietDataById,
           spSaleDetailGetByIdEdit,
           spSaleDetailDelByIdEdit,

           spOrderQueueGet,
           spOrderQueueInsert,
           spOrderQueueDelete,

           spCheckTokenId,
           spSaleDetailGetByTokenIdEdit,
           spSaleInsertEditByTokenId,
           spCheckTokenIdOrderQueue,


           spProductTypeGet,
           spGetTypeIdFromProduct
       }
    }
}

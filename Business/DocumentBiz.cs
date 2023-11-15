using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Document;

namespace NTKServer.Business
{
    public class DocumentBiz
    {
        #region CRUD
		public static List<DocumentList> GetDocumentList(DocumentFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsDocumentService.FindDocumentList(whereSql);
        }

        public static CmsDocumentDto Get(int pk)
        {
            return CmsDocumentService.Find(pk);
        }

        public static void PostCreate(CmsDocumentDto req)
        {
            if (CmsDocumentService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsDocumentDto req)
        {
            if( CmsDocumentService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void PostCopy(CmsDocumentDto req)
        {
            if (CmsDocumentService.FindByCIDLANG(req.cid, req.lang) != null)
            {
                throw new AppException(3011, "record_copy_false_already_existed");
            }

            if (CmsDocumentService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsDocumentService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using Dapper;
using United_IMS.Web.Models;
using System.Transactions;
using United_IMS.Web.Repository;

namespace United_IMS.Web.Repository
{
	public class ItemRepo
	{
		public List<ItemVM> GetItemList(int organizationId=0,string ItemName="",string ItemCode="",int Category = 0)
		{

            string sql = " select b.ItemId, b.ItemName, b.ItemCode,b.CategoryId,c.CategoryName,b.OrganizationId,o.OrganizationName,b.ItemDescription " +
						 " from MS_Item b " +
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId"+
						 " left join MS_Category c on c.CategoryId = b.CategoryId ";
			sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;

			}
			if (!string.IsNullOrEmpty(ItemName))
			{
				sql += " and b.ItemName like '%" + ItemName+"%'";
			}
			if (!string.IsNullOrEmpty(ItemCode))
			{
				sql += " and b.ItemCode like '%" + ItemCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ItemVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_Item GetItemById(int Id)
		{
			string sql = " Select *  from MS_Item where ItemId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_Item>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public int InsertItem(MS_Item Item)
		{
			try
			{
				string sql = " Insert into  MS_Item (ItemName, ItemCode,ItemDescription,CategoryId,OrganizationId, EnteredBy, EnteredDate," +
				" LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
				" values " +
                "(@ItemName, @ItemCode,@ItemDescription,@CategoryId,@OrganizationId, @EnteredBy, @EnteredDate," +
				" 0, null, 0, 0, null) SELECT CAST(SCOPE_IDENTITY() as int)";
				using (var db = DbHelper.GetDBConnection())
				{
					using (var trsn = new TransactionScope())
					{
						//db.Execute(sql);
						var lst = db.Query<int>(sql, Item).SingleOrDefault();
						trsn.Complete();
						db.Close();

						return lst;


					}
				}
			}
			catch
			{
				return 0;
			}
		}
		public bool UpdateItem(MS_Item Item)
		{
			string sql = " Update MS_Item set ItemName=@ItemName, ItemCode=@ItemCode,ItemDescription=@ItemDescription, " +
            " OrganizationId=@OrganizationId,CategoryId=@CategoryId, " +
			" EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdatedBy=@LastUpdatedBy, " +
			" LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where ItemId=@ItemId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, Item);
					trsn.Complete();
					db.Close();
					if (lst > 0)
					{
						return true;
					}
					else
					{
						return false;
					}

				}
			}
		}
		public bool DeleteItem(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update MS_Item set IsDeleted=1, DeletedBy=" + deletedBy+", DeletedDate='"+deletedDate+ "' where ItemId= " + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql);
					trsn.Complete();
					db.Close();
					if (lst > 0)
					{
						return true; 
					}
					else 
					{ 
						return false; 
					}
					
				}
			}
		}
		public ItemVM GetItemDetail(int id)
		{
		
			string sql = " select b.ItemId, b.ItemName, b.ItemCode,b.CategoryId,c.CategoryName,b.OrganizationId,o.OrganizationName,b.ItemDescription " +
						 " from MS_Item b " +
                         " left join MS_Category c on c.CategoryId = b.CategoryId"+

                         " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and b.IsDeleted=0 and b.ItemId="+id;
			
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ItemVM>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		
		}
		public ItemDetailVM GetItemDetails(int id)
		{
			ItemUnitRepo iu = new ItemUnitRepo();
			ItemDetailVM item = new ItemDetailVM();
			item.Item = GetItemDetail(id);
			item.Units = iu.GetItemUnitListByItemId(id);
			return item;
		}
	}
}
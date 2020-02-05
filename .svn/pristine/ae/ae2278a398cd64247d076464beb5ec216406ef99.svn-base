using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using Dapper;
using United_IMS.Web.Models;
using System.Transactions;

namespace United_IMS.Web.Repository
{
	public class ItemUnitRepo
	{
		public List<ItemUnitVM> GetItemUnitListByItemId(int ItemId=0)
		{
			string sql = " select iu.ItemUnitId,iu.OrganizationId, o.OrganizationName, iu.ItemId ,i.ItemName,iu.UnitId ,u.UnitName, iu.UnitSellingPrice, iu.IsDefault, iu.QuantityInPiece " +
						 " from MS_ItemUnit iu " +
						 " left join MS_Item i on i.ItemId = iu.ItemId" +
						 " left join MS_Unit u on u.UnitId = iu.UnitId" +
						" left join MS_Organization o on o.OrganizationId = iu.OrganizationId";
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and iu.IsDeleted=0 and iu.ItemId="+ItemId;	
			
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ItemUnitVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_ItemUnit GetItemUnitById(int Id)
		{
			string sql = " Select *  from MS_ItemUnit where ItemUnitId="+Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_ItemUnit>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertItemUnit(MS_ItemUnit itemunit)
		{
			string sql = " Insert into  MS_ItemUnit ( OrganizationId, ItemId, UnitId, UnitSellingPrice, IsDefault, QuantityInPiece, "+
			" EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate,IsDeleted, DeletedBy,DeletedDate) " +
			" values " +
			"( @OrganizationId, @ItemId, @UnitId, @UnitSellingPrice, @IsDefault, @QuantityInPiece, "+
			" @EnteredBy, @EnteredDate, 0, null,0, 0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, itemunit);
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
		public bool UpdateItemUnit(MS_ItemUnit itemunit)
		{
			string sql = " Update MS_ItemUnit set  OrganizationId=@OrganizationId, ItemId=@ItemId, UnitId=@UnitId, UnitSellingPrice=@UnitSellingPrice, IsDefault=@IsDefault, QuantityInPiece=@QuantityInPiece, "+
			" LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate,IsDeleted=0" +
			" where ItemUnitId=@ItemUnitId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, itemunit);
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
		public bool DeleteItemUnit(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update MS_ItemUnit set IsDeleted=1, DeletedBy="+deletedBy+", DeletedDate='"+deletedDate+"' where ItemUnitId= "+Id;
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
		public bool DeleteAllItemUnitByItem(int Id,int deletedby,DateTime deleteddate)
		{
			string sql = " Update MS_ItemUnit set IsDeleted=1 where  ItemId= " + Id;
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
	}
}
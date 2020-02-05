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
	public class InventoryVendorRepo
	{
		public List<InventoryVendorVM> GetInventoryVendorList(int organizationId=0,string vendorName="")
		{
			string sql = " select b.VendorId, b.VendorName, b.Address,b.Phone,b.Mobile,b.OrganizationId,o.OrganizationName,b.PanNo " +
						 " from INV_Vendor b " +
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
            //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
            sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;

			}
			if (!string.IsNullOrEmpty(vendorName))
			{
				sql += " and b.VendorName like '%" + vendorName+"%'";
			}
			
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<InventoryVendorVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public INV_Vendor GetInventoryVendorById(int Id)
		{
			string sql = " Select *  from INV_Vendor where VendorId="+Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<INV_Vendor>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertInventoryVendor(INV_Vendor a)
		{
			string sql = " Insert into  INV_Vendor (VendorName, Address,Phone,Mobile,PanNo,OrganizationId, EnteredBy, EnteredDate," +
			" LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
            "(@VendorName, @Address,@Phone,@Mobile,@PanNo,@OrganizationId, @EnteredBy, @EnteredDate," +
			" 0, null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, a);
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
		public bool UpdateInventoryVendor(INV_Vendor a)
		{
			string sql = " Update INV_Vendor set VendorName=@VendorName, Address=@Address,Phone=@Phone,Mobile=@Mobile,PanNo=@PanNo, " +
			" OrganizationId=@OrganizationId, " +
			" LastUpdatedBy=@LastUpdatedBy, " +
			" LastUpdatedDate=@LastUpdatedDate"+// IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where VendorId=@VendorId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, a);
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
		public bool DeleteInventoryVendor(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update INV_Vendor set IsDeleted=1, DeletedBy="+deletedBy+", DeletedDate='"+deletedDate+"' where vendorId= "+Id;
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
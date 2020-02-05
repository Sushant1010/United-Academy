﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using Dapper;
namespace United_IMS.Web.Repository
{
	public class GetDropdown
	{
        //DbHelper db=n
        public List<Basic> GetYearList()
        {
            string sql = " Select AcademicYearId Id,AcademicYearName Name from MS_AcademicYear";
            sql += " Order By AcademicYearId";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetCategoryList(int orgid)
        {
            string sql = " Select CategoryId Id,CategoryName Name from MS_Category";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += " Order By CategoryName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetItemList(int orgid,int cateid)
        {
            string sql = " Select ItemId Id,ItemName Name from MS_Item ";
            sql += " where 1=1 and IsDeleted="+0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (cateid > 0)
            {
                sql += " and CategoryId=" + cateid;
            }
            sql += "Order By ItemName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetItemList1(int orgid, int cateid)
        {
            string sql = " Select AssetItemId Id,AssetName Name from FA_AssetItem ";
            sql += " where 1=1 and IsDeleted="+0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (cateid > 0)
            {
                sql += " and AssetCategoryId=" + cateid;
            }
            sql += "Order By AssetName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetItemListTransfer(int orgid, int cateid)
        {
            string sql = "select ai.AssetItemId AId, a.AssetId Id, ai.AssetName Name from FA_Asset a"+
                        " left join FA_AssetItem ai on ai.AssetItemId = a.AssetItemId";
            //string sql = " Select AssetItemId Id,AssetName Name from FA_AssetItem ";
            sql += " where 1=1 and ai.IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and a.OrganizationId=" + orgid;
            }
            if (cateid > 0)
            {
                sql += " and ai.AssetCategoryId=" + cateid;
            }
            sql += "Order By AssetName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetOBL(int asseid)
        {
            string sql = "select ai.OrganizationId Id, o.OrganizationName Name, b.BranchName Bname, l.LocationName Lname," +
                            " ai.IsTransfered IsTransfer  from FA_Asset ai "
                            + "left join MS_Organization o on o.OrganizationId = ai.OrganizationId "
                            + "left join FA_Branch b on b.BranchId = ai.BranchId "
                            + "left join FA_Location l on l.LocationId = ai.LocationId "
                            + "where ai.AssetId ="+asseid ;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetOBLaTrans(int asseid)
        {
            string sql = "select o.organizationName Name, b.BranchName Bname, l.LocationName Lname" +
                " from fa_assettransfer atr " +
                "left join MS_Organization o on o.OrganizationId = atr.OrganizationId " +
                "left join FA_Branch b on b.BranchId = atr.BranchId " +
                "left join FA_Location l on l.LocationId = atr.LocationId"
                +" where atr.AssetId="+asseid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetBranchByOrgId(int orgid)
        {
            string sql = "select BranchId Id, BranchName Bname from FA_Branch" +
                " where organizationId=" + orgid + " and IsDeleted=0 Order By BranchName";
            

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetUserByOrgId(int orgid)
        {
            string sql = "select UserId Id, FullName Name from SC_User" +
                " where organizationId=" + orgid + " and IsDeleted=0 Order By FullName";


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetLocationByOrgId(int orgid)
        {
            string sql = "select LocationId Id, LocationName Lname from FA_Location" +
                " where organizationId=" + orgid + " and IsDeleted=0 Order By LocationName";


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetProgramList( int orgid=0)
		{
			string sql = " Select ProgramId Id,ProgramName Name from ACD_Program";
			sql += " where 1=1 and IsDeleted=0";
			if(orgid>0)
			{
				sql += " and OrganizationId="+orgid;
			}
            sql += "Order By ProgramName";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<Basic>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public List<Basic> GetOrganizationList()
		{
			string sql = " Select OrganizationId Id,OrganizationName Name from MS_Organization Order By OrganizationName";
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<Basic>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public List<Basic> GetClassList(int orgid)
		{
            string sql = " Select ClassId Id,ClassName Name from ACD_Class";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid != 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            //sql += "Order By ClassName";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<Basic>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public List<Basic> GetSectionList(int orgid)
		{
			string sql = " Select SectionId Id,SectionName Name from ACD_Section";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid != 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By SectionName";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<Basic>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public List<Basic> GetBatchList(int orgid)
		{
			string sql = " Select BatchId Id,BatchName Name from ACD_Batch";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By BatchName";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<Basic>(sql).ToList();
				db.Close();
				return lst;
			}
		}
        public List<Basic> GetUnitList(int orgid)
        {
            string sql = " Select UnitId Id,UnitName Name from MS_Unit";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        //public List<Basic> GetUnitList1(int orgid, int itemid)
        //{
        //    string sql = " Select UnitId Id,UnitName Name from MS_Unit";
        //    sql += " where 1=1";
        //    if (orgid > 0)
        //    {
        //        sql += " and OrganizationId=" + orgid;
        //    }
        //    sql += "Order By UnitName";
        //    using (var db = DbHelper.GetDBConnection())
        //    {
        //        var lst = db.Query<Basic>(sql).ToList();
        //        db.Close();
        //        return lst;
        //    }
        //}
        public List<Basic> GetInvVenodrList(int orgid)
        {
            string sql = " Select VendorId Id,VendorName Name from INV_Vendor";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By VendorName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetStaffList(int orgid)
        {
            string sql = " Select StaffId Id,'['+StaffCode+'] - '+staffName Name from ACD_Staff";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By StaffName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetStudentList(int orgid,int batch,int classid,int section)
        {
            string sql = " Select StudentId Id,'['+StudentRegNo+'] - '+StudentName Name from ACD_Student";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid != 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (batch != 0)
            {
                sql += " and BatchId=" + batch;
            }
            if (classid != 0)
            {
                sql += " and CurrentClassId=" + classid;
            }
            if (section != 0)
            {
                sql += " and CurrentSectionId=" + section;
            }
            sql += "Order By StudentName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetUnitByItemList(int itemid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from MS_ItemUnit iu "+
                " left join MS_Unit u on u.UnitId = iu.UnitId ";
            sql += " where 1=1 and iu.IsDeleted=0 ";
            if (itemid > 0)
            {
                sql += " and ItemId=" + itemid;
            }
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetUnitBySold(int soldid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from MS_ItemUnit iu " +
                " left join MS_Unit u on u.UnitId = iu.UnitId" +
                " where 1 = 1 and iu.IsDeleted = 0  and iu.ItemId in (select distinct(ItemId) from INV_SoldItem where SoldBillId = " + soldid + ")";
            //sql += "where ItemId=" + itemid;

            //if (itemid > 0)
            //{
            //    sql += " and ItemId=" + itemid;
            //}
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetUnitByPurchase(int purchaseid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from MS_ItemUnit iu "+
                " left join MS_Unit u on u.UnitId = iu.UnitId"+
                " where 1 = 1 and iu.IsDeleted = 0  and iu.ItemId in (select distinct(ItemId) from INV_PurchaseItem where purchaseid = "+purchaseid+")";
            //sql += "where ItemId=" + itemid;

            //if (itemid > 0)
            //{
            //    sql += " and ItemId=" + itemid;
            //}
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public int GetRateByItemUnit(int itemid,int unitid)
        {
            string sql = "select UnitSellingPrice Name from MS_ItemUnit iu "+
                " left join MS_Unit u on u.UnitId = iu.UnitId where iu.ItemId = "+itemid+" and iu.UnitId = "+unitid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetDepreciationList(int orgid)
        {
            string sql = " select MethodId Id,MethodName Name from FA_DepreciationMethod ";

            //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
            sql += " where 1=1 and IsDeleted=0 ";
            if (orgid != 0)
                sql += " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetAssetCategoryList(int orgid)
        {
            string sql = " Select AssetCategoryId Id,CategoryName Name from FA_AssetCategory";
            sql += " where 1=1 and IsDeleted = 0 ";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By CategoryName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetBranchList(int orgid)
        {
            string sql = " Select BranchId Id,BranchName Name from FA_Branch";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid!= 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By BranchName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetLocationList(int orgid)
        {
            string sql = " Select LocationId Id, LocationName Name from FA_Location";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid != 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By LocationName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetAssetItemList(int orgid)
        {
            string sql = " Select AssetItemId Id, AssetName Name from FA_AssetItem";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By AssetName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetAssetList1(int orgid)
        {
            string sql = "select ai.assetitemid, ai.assetname from FA_Asset a " +
                "left join FA_AssetItem ai on ai.AssetItemId = a.AssetItemId" +
                " where 1=1 and a.IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and a.OrganizationId=" + orgid;
            }
            sql += "Order By ai.AssetName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetAssetItemList1(int catid)
        {
            string sql = " Select AssetItemId Id, AssetName Name from FA_AssetItem";
            sql += " where 1=1 and IsDeleted=0 ";
            if (catid > 0)
            {
                sql += " and AssetCategoryID=" + catid;
            }
            sql += "Order By AssetName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetAssetList(int orgid)
        {
            string sql = " Select AssetId Id, AssetUniqueCode Name from FA_Asset";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetUserList(int orgid)
        {
            string sql = " Select UserId Id, FullName Name from SC_User";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetLocationName(int orgid)
        {
            string sql = "Select LocationCode Name from fa_location where locationId=" + orgid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public string GetAssetCode(int assid)
        {
            string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int GetAssetUniqueID(int assid, int locid, int orgid)
        {
            string sql = "select count(*)+1 from fa_asset where organizationid="+orgid+
                " and locationid="+locid+
                " and assetitemid="+assid;
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public string GetAssetName(int orgid, string AName)
        {
            string sql = " Select AssetName Name from Fa_assetItem ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (AName != null)
            {
                sql += " and AssetName='" + AName+"'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public string GetAssetCode(int orgid, string ACode)
        {
            string sql = " Select AssetCode Name from Fa_assetItem ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ACode != null)
            {
                sql += " and AssetCode='" + ACode + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetStudentRegNo(int orgid, string ReName)
        {
            string sql = " Select StudentRegNo Name from ACD_Student ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and StudentRegNo='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetUnitName(int orgid, string ReName)
        {
            string sql = " Select UnitName Name from MS_Unit ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and UnitName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetCategoryName(int orgid, string ReName)
        {
            string sql = " Select CategoryName Name from MS_Category ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and CategoryName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetCategoryCode(int orgid, string ReName)
        {
            string sql = " Select CategoryCode Name from MS_Category ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and CategoryCode='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetVendorNames(int orgid, string ReName)
        {
            string sql = " Select VendorName Name from INV_Vendor ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and VendorName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetItemName(int orgid, string ReName)
        {
            string sql = " Select ItemName Name from MS_Item ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
          
            
            if (ReName != null)
            {
                sql += " and ItemName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetBranchName(int orgid, string ReName)
        {
            string sql = " Select BranchName Name from FA_Branch ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and BranchName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public string GetBranchCode(int orgid, string ReName)
        {
            string sql = " Select BranchCode Name from FA_Branch ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and BranchCode='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }

        }
        public string GetLocationNames(int orgid, string ReName)
        {
            string sql = " Select LocationName Name from FA_Location ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and LocationName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }

        }
        public string GetLocationCode(int orgid, string ReName)
        {
            string sql = " Select LocationCode Name from FA_Location ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and LocationCode='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }

        }
        public string GetCategoryNames(int orgid, string ReName)
        {
            string sql = " Select CategoryName Name from FA_AssetCategory ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and CategoryName='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }

        }
        public string GetCategoryCodes(int orgid, string ReName)
        {
            string sql = " Select CategoryCode Name from FA_AssetCategory ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (ReName != null)
            {
                sql += " and CategoryCode='" + ReName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }

        }

        public int GetStudentTotal(int orgid, int classId)
        {
            string sql = "select count(*) from ACD_Student where IsDeleted=0 and organizationid =" + orgid + " and CurrentClassId = " + classId +
                " and IsDeleted=0";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int GetStudentTotal(int orgid, int classId, int sectionid)
        {
            string sql = "select count(*) from ACD_Student where IsDeleted=0 and organizationid =" + orgid + " and CurrentClassId = " + classId + " and CurrentSectionId=" + sectionid +
                " and IsDeleted=0";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public int GetStudentTotal1(int orgid, int classId, int studentid)
        {
            string sql = "select count(*) from ACD_Student where IsDeleted=0 and organizationid =" + orgid + " and CurrentClassId = " + classId+
                " and StudentId=" + studentid+" and IsDeleted=0";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int GetStudentTotal1(int orgid, int classId, int sectionid, int studentid)
        {
            string sql = "select count(*) from ACD_Student where IsDeleted=0 and organizationid =" + orgid + " and CurrentClassId = " + classId+ " and CurrentSectionId=" + sectionid+
                " and StudentId="+studentid + " and IsDeleted=0";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int GetLastNo()
        {
            string sql = "select top 1 SoldBillId from inv_soldbill order by SoldBillId desc";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public int GetLastItemNo()
        {
            string sql = "select top 1 SoldItemId from inv_soldItem order by SoldItemId desc";
            //string sql = "Select AssetCode Name from fa_assetitem where assetitemid=" + assid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetSoldBulkBillById(int orgid, int soldbillid, int firstid, int lastid)
        {
            string sql = "select * from INV_SoldBill " +
                " where 1=1 and IsDeleted=0 and SoldBillId>" + firstid + " and SoldBillId<=" +lastid+
                " and OrganizationId=" + orgid;


            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetClassStudentList(int orgid, int classid)
        {
            string sql = " Select StudentId Id,'['+StudentRegNo+'] - '+StudentName Name from ACD_Student";
            sql += " where 1=1 and IsDeleted=0";
                sql += " and OrganizationId=" + orgid;
                sql += " and CurrentClassId=" + classid;

            sql += "Order By StudentName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetClassStudentList(int orgid, int classid, int section)
        {
            string sql = " Select StudentId Id,'['+StudentRegNo+'] - '+StudentName Name from ACD_Student";
            sql += " where 1=1 and IsDeleted=0";
                sql += " and OrganizationId=" + orgid;
                sql += " and ClassId=" + classid;
                sql += " and SectionId=" + section;
            sql += "Order By StudentName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public string GetDepreciationid(int orgid, int dep)
        {
            string sql = " Select DepreciationRate Name from FA_DepreciationMethod";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid != 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (dep != 0)
            {
                sql += " and MethodId=" + dep;
            }
           
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKCategoryList(int orgid)
        {
            string sql = " Select CategoryId Id,CategoryName Name from KI_Category";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += " Order By CategoryName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKUnitByPurchase(int purchaseid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from KI_ItemUnit iu " +
                " left join KI_Unit u on u.UnitId = iu.UnitId" +
                " where 1 = 1 and iu.IsDeleted = 0  and iu.ItemId in (select distinct(ItemId) from KI_PurchaseItem where purchaseid = " + purchaseid + ")";
            //sql += "where ItemId=" + itemid;

            //if (itemid > 0)
            //{
            //    sql += " and ItemId=" + itemid;
            //}
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        //Kitchen Inventory Purchase Section
        public List<Basic> GetKitchenCategoryList(int orgid)
        {
            string sql = " Select CategoryId Id,CategoryName Name from KI_Category";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += " Order By CategoryName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKitchenItemList(int orgid, int cateid)
        {
            string sql = " Select ItemId Id,ItemName Name from KI_Item ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (cateid > 0)
            {
                sql += " and CategoryId=" + cateid;
            }
            sql += "Order By ItemName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKitchenUnitByItemList(int orgid,int itemid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from KI_ItemUnit iu " +
                " left join KI_Unit u on u.UnitId = iu.UnitId ";
            sql += " where 1=1 and iu.IsDeleted=0 ";
            if (orgid > 0)
            {
                sql += " and iu.OrganizationId=" + orgid;
            }
            if (itemid > 0)
            {
                sql += " and iu.ItemId=" + itemid;
            }
            sql += " Order By u.UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKitchenVenodrList(int orgid)
        {
            string sql = " Select VendorId Id,VendorName Name from KI_Vendor";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By VendorName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKitchenUnitByItem(int itemid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from KI_ItemUnit iu " +
                " left join KI_Unit u on u.UnitId = iu.UnitId ";
            sql += " where 1=1 and iu.IsDeleted=0 ";
            if (itemid > 0)
            {
                sql += " and ItemId=" + itemid;
            }
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetKitchenUnitByItem1(int orgid)
        {
            string sql = " select iu.UnitId Id, u.UnitName Name from KI_ItemUnit iu " +
                " left join KI_Unit u on u.UnitId = iu.UnitId ";
            sql += " where 1=1 and iu.IsDeleted=0 ";
            if (orgid > 0)
            {
                sql += " and iu.OrganizationId=" + orgid;
            }
            sql += "Order By UnitName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public int GetKitchenRateByItemUnit(int itemid, int unitid)
        {
            string sql = "select UnitSellingPrice Name from KI_ItemUnit iu " +
                " left join KI_Unit u on u.UnitId = iu.UnitId where iu.ItemId = " + itemid + " and iu.UnitId = " + unitid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public List<Basic> GetKitchenItemByCategory(int orgid, int cateid)
        {
            string sql = " Select ItemId Id,ItemName Name from KI_Item ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (cateid > 0)
            {
                sql += " and CategoryId=" + cateid;
            }
            sql += "Order By ItemName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<Basic> GetKitchenInvVenodrList(int orgid)
        {
            string sql = " Select VendorId Id,VendorName Name from KI_Vendor";
            sql += " where 1=1 and IsDeleted=0";
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            sql += "Order By VendorName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Basic>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public string GetKitchenUnitName(int orgid, string AName)
        {
            string sql = " Select UnitName Name from KI_Unit ";
            sql += " where 1=1 and IsDeleted=" + 0;
            if (orgid > 0)
            {
                sql += " and OrganizationId=" + orgid;
            }
            if (AName != null)
            {
                sql += " and UnitName='" + AName + "'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }


    }

}
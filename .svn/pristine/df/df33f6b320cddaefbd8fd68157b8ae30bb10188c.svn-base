using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using Dapper;
namespace United_IMS.Web.Repository
{
    public class ReportRepo
    {
        public List<DetailedSalesVM> GetSalesDetail(int organizationId = 0, string FromBillDate="",string ToBillDate="",
            string GroupNo="",string BillNo ="",string IsStaff= "", int ClassId =0,int SectionId = 0,int StudentId =0,int StaffId=0)
        {
            string sql = " exec DetailedSalesReport "+organizationId;
            if (!string.IsNullOrEmpty(FromBillDate))
            {
                sql += " ,'" + FromBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(ToBillDate))
            {
                sql += " ,'" + ToBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(GroupNo))
            {
                sql += " ,'" + GroupNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(BillNo))
            {
                sql += " ,'" + BillNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(IsStaff))
            {
                if (IsStaff == "Yes")
                {
                    sql += " ,1,0,0,0";
                    if(StaffId>0)
                    {
                        sql += ","+StaffId;
                    }
                }
                else if (IsStaff == "No")
                {
                    sql += " ,0";
                    if(ClassId>0)
                    {
                        sql += ","+ClassId;
                    }
                    else
                    {
                        sql += " ,0";
                    }

                    if (SectionId > 0)
                    {
                        sql += "," + SectionId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    if (StudentId > 0)
                    {
                        sql += "," + StudentId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    sql += ",0";
                }
                else
                {
                    sql += " ,null,0,0,0,0";
                }
            }
            else
            {
                sql += " ,null,0,0,0,0";
            }

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<DetailedSalesVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<DetailedSalesVM> GetSalesDetailedSummary(int organizationId = 0, string FromBillDate = "", string ToBillDate = "",
            string GroupNo = "", string BillNo = "", string IsStaff = "", int ClassId = 0, int SectionId = 0, int StudentId = 0, int StaffId = 0)
        {
            string sql = " exec DetailedSummarySalesReport " + organizationId;
            if (!string.IsNullOrEmpty(FromBillDate))
            {
                sql += " ,'" + FromBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(ToBillDate))
            {
                sql += " ,'" + ToBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(GroupNo))
            {
                sql += " ,'" + GroupNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(BillNo))
            {
                sql += " ,'" + BillNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(IsStaff))
            {
                if (IsStaff == "Yes")
                {
                    sql += " ,1,0,0,0";
                    if (StaffId > 0)
                    {
                        sql += "," + StaffId;
                    }
                }
                else if (IsStaff == "No")
                {
                    sql += " ,0";
                    if (ClassId > 0)
                    {
                        sql += "," + ClassId;
                    }
                    else
                    {
                        sql += " ,0";
                    }

                    if (SectionId > 0)
                    {
                        sql += "," + SectionId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    if (StudentId > 0)
                    {
                        sql += "," + StudentId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    sql += ",0";
                }
                else
                {
                    sql += " ,null,0,0,0,0";
                }
            }
            else
            {
                sql += " ,null,0,0,0,0";
            }

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<DetailedSalesVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<SummarySalesVM> GetSalesSummary(int organizationId = 0, string FromBillDate = "", string ToBillDate = "",
            string GroupNo = "", string BillNo = "", string IsStaff = "", int ClassId = 0, int SectionId = 0, int StudentId = 0, int StaffId = 0)
        {
            string sql = " exec SummarySalesReport " + organizationId;
            if (!string.IsNullOrEmpty(FromBillDate))
            {
                sql += " ,'" + FromBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(ToBillDate))
            {
                sql += " ,'" + ToBillDate + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(GroupNo))
            {
                sql += " ,'" + GroupNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(BillNo))
            {
                sql += " ,'" + BillNo + "'";
            }
            else
            {
                sql += " ,null";
            }
            if (!string.IsNullOrEmpty(IsStaff))
            {
                if (IsStaff == "Yes")
                {
                    sql += " ,1,0,0,0";
                    if (StaffId > 0)
                    {
                        sql += "," + StaffId;
                    }
                }
                else if (IsStaff == "No")
                {
                    sql += " ,0";
                    if (ClassId > 0)
                    {
                        sql += "," + ClassId;
                    }
                    else
                    {
                        sql += " ,0";
                    }

                    if (SectionId > 0)
                    {
                        sql += "," + SectionId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    if (StudentId > 0)
                    {
                        sql += "," + StudentId;
                    }
                    else
                    {
                        sql += " ,0";
                    }
                    sql += ",0";
                }
                else
                {
                    sql += " ,null,0,0,0,0";
                }
            }
            else
            {
                sql += " ,null,0,0,0,0";
            }

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SummarySalesVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
    }
}